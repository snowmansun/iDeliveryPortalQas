using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mde.BLL
{
    public partial class DSD_M_SystemConfigService : BaseService<DSD_M_SystemConfig>, IDSD_M_SystemConfigService
    {
        public List<ViewConfigData> GetConfigData(string category, string keyname, int orgId)
        {
            var dataConfig = _dbSession.DSD_M_SystemConfigRepository.Entities;
            var dataOrg = _dbSession.DSD_M_OrgSystemConfigRepository.Entities.Where(p => p.Valid == true && p.OrgID == orgId);
            if (!string.IsNullOrEmpty(category))
            {
                dataConfig = dataConfig.Where(p => p.Category == category);
            }
            if (!string.IsNullOrEmpty(keyname))
            {
                dataConfig = dataConfig.Where(p => p.KeyName == keyname);
            }
            var data = from c in dataConfig
                       join oc in dataOrg on c.Id equals oc.SystemConfigID
                       select new ViewConfigData
                       {
                           Category = c.Category,
                           KeyName = c.KeyName,
                           Value = oc.Value,
                           OrgID = oc.OrgID
                       };
            return data.ToList();
        }
        public List<ViewComboboxStringData> GetCategoryComboxData()
        {
            var data = _dbSession.DSD_M_SystemConfigRepository.Entities;
            var dataJson = (from d in data
                            select new ViewComboboxStringData
                            {
                                value = d.Category,
                                name = d.Category
                            }).Distinct();
            return dataJson.ToList();
        }

        public List<ViewComboboxStringData> GetKeyNameComboxData(string category)
        {
            var data = _dbSession.DSD_M_SystemConfigRepository.Entities;
            if(!string.IsNullOrEmpty(category))
            {
                data = data.Where(p => p.Category == category);
            }

            var dataJson = (from d in data
                            select new ViewComboboxStringData
                            {
                                value = d.KeyName,
                                name = d.KeyName
                            }).Distinct();

            return dataJson.ToList();
        }


        public List<ViewSystemConfigData> GetSystemConfigData(string category, string keyName)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            var sql = @"SELECT sc.ID SystemConfigID,sc.Category,sc.KeyName,osc.Value,sc.Description,osc.OrgID,org.Name OrgName
	                        INTO #temp
                        FROM dbo.DSD_M_SystemConfig sc
	                        LEFT JOIN dbo.DSD_M_OrgSystemConfig osc ON sc.Id=osc.SystemConfigID
	                        LEFT JOIN dbo.MD_Organization org ON org.Id=osc.OrgID
                        WHERE 1=1 ";
            
            if (!string.IsNullOrEmpty(category))
            {
                sql = sql + " And sc.Category=@Category";
                parameters.Add(new SqlParameter("@Category", category));
            }
            if (!string.IsNullOrEmpty(keyName))
            {
                sql = sql + " And sc.KeyName=@KeyName";
                parameters.Add(new SqlParameter("@KeyName", keyName));
            }
            
            sql = sql +@" 
                        SELECT SystemConfigID,Category,KeyName,Value KeyValue,Description
                            ,ISNULL(STUFF((SELECT ','+CAST(OrgID AS VARCHAR) FROM #temp tmp WHERE a.Category=tmp.Category AND a.KeyName=tmp.KeyName AND a.Value=tmp.Value 
	                        FOR XML PATH('')),1,1,''),'') AS OrgIDs
                            ,ISNULL(STUFF((SELECT ','+OrgName FROM #temp tmp WHERE a.Category=tmp.Category AND a.KeyName=tmp.KeyName AND a.Value=tmp.Value 
	                        FOR XML PATH('')),1,1,''),'') AS OrgNames
                        FROM #temp a
                        GROUP BY SystemConfigID,Category,KeyName,Value,Description
                        DROP TABLE #temp";
            IList<ViewSystemConfigData> data = _dbSession.ExecuteSqlNonQuery<ViewSystemConfigData>(sql, parameters.ToArray());
            var dataJson = from x in data
                           select new ViewSystemConfigData
                           {
                               SystemConfigID = x.SystemConfigID,
                               Category = x.Category,
                               KeyName = x.KeyName,
                               KeyValue = x.KeyValue,
                               Description = x.Description,
                               OrgIDs = x.OrgIDs,
                               OrgNames = x.OrgNames
                           };
            return dataJson.ToList();
        }

        public void SaveConfigData(int SystemConfigID, string Category, string KeyName, string Description, string KeyValue, string OrgIDs, string OldOrgIDs,string UserCode)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //category和keyname是否存在，不存在insert
                DSD_M_SystemConfig data = _dbSession.DSD_M_SystemConfigRepository.Entities.Where(p => p.Category == Category && p.KeyName == KeyName).FirstOrDefault();
                if (data == null)
                {
                    data = new DSD_M_SystemConfig();

                    data.Category = Category;
                    data.KeyName = KeyName;
                    data.Description = Description;
                    data.CreateUser = UserCode;
                    data.CreateTime = DateTime.Now;
                    data.LastUpdateUser = UserCode;
                    data.LastUpdateTime = DateTime.Now;

                    _dbSession.DSD_M_SystemConfigRepository.AddEntities(data);
                }
                else
                {
                    data.Description = Description;
                    data.LastUpdateUser = UserCode;
                    data.LastUpdateTime = DateTime.Now;

                    _dbSession.DSD_M_SystemConfigRepository.UpdateEntities(data);
                }
                _dbSession.DSD_M_SystemConfigRepository.Commit();

                SystemConfigID = data.Id;

                //删除原数据
                DSD_M_OrgSystemConfig orgConfigData = new DSD_M_OrgSystemConfig();
                if (OldOrgIDs != null && OldOrgIDs != "")
                {
                    string[] OldOrgIDArr = OldOrgIDs.Split(',');
                    for (var i = 0; i < OldOrgIDArr.Length; i++)
                    {
                        int OrgID = int.Parse(OldOrgIDArr[i]);
                        orgConfigData = _dbSession.DSD_M_OrgSystemConfigRepository.Entities.Where(p => p.SystemConfigID == SystemConfigID && p.OrgID == OrgID).FirstOrDefault();
                        if (orgConfigData != null)
                        {
                            _dbSession.DSD_M_OrgSystemConfigRepository.DeleteEntities(orgConfigData);
                            _dbSession.DSD_M_OrgSystemConfigRepository.Commit();
                        }
                    }
                }

                //插入或者修改新数据
                if (OrgIDs != null && OrgIDs != "")
                {
                    string[] NewOrgIDArr = OrgIDs.Split(',');
                    for (var i = 0; i < NewOrgIDArr.Length; i++)
                    {
                        int OrgID = int.Parse(NewOrgIDArr[i]);
                        orgConfigData = _dbSession.DSD_M_OrgSystemConfigRepository.Entities.Where(p => p.SystemConfigID == SystemConfigID && p.OrgID == OrgID).FirstOrDefault();
                        if (orgConfigData == null)
                        {
                            orgConfigData = new DSD_M_OrgSystemConfig();
                            orgConfigData.SystemConfigID = SystemConfigID;
                            orgConfigData.OrgID = OrgID;
                            orgConfigData.Value = KeyValue;
                            orgConfigData.Valid = true;
                            orgConfigData.CreateUser = UserCode;
                            orgConfigData.CreateTime = DateTime.Now;
                            orgConfigData.LastUpdateUser = UserCode;
                            orgConfigData.LastUpdateTime = DateTime.Now;

                            _dbSession.DSD_M_OrgSystemConfigRepository.AddEntities(orgConfigData);
                        }
                        else
                        {
                            orgConfigData.Value = KeyValue;
                            orgConfigData.LastUpdateTime = DateTime.Now;
                            orgConfigData.LastUpdateUser = UserCode;

                            _dbSession.DSD_M_OrgSystemConfigRepository.UpdateEntities(orgConfigData);
                        }
                        _dbSession.DSD_M_OrgSystemConfigRepository.Commit();
                    }
                }

                scope.Complete();
            }
        }
    }
}
