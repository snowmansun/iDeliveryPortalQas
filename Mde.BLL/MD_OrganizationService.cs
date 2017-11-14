using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.BLL
{
    public partial class MD_OrganizationService : BaseService<MD_Organization>, IMDOrganizationService
    {
        #region sql
        private const string SqlComboboxData = @"SELECT Id value,Name name FROM dbo.MD_Organization";
        #endregion
        private List<MD_Organization> dataOrg;
        private List<ViewOrganizationData> dataOrgTree;

        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        /// <returns></returns>
        public List<ViewComboboxIntData> GetComboboxData()
        {
            string sql = string.Format(SqlComboboxData);
            DbParameter[] parameters = new DbParameter[1];
            IList<ViewComboboxIntData> dataResult = _dbSession.ExecuteSqlNonQuery<ViewComboboxIntData>(sql, parameters);
            var data = from x in dataResult
                       select new ViewComboboxIntData
                       {
                           value = x.value,
                           name = x.name
                       };

            return data.ToList();
        }

        /// <summary>
        /// 获取树形Org数据
        /// </summary>
        /// <returns></returns>
        public List<object> GetOrganizationTreeData(int parentId,bool IsAll)
        {
            var data = _dbSession.MD_OrganizationRepository.Entities;
            if (!IsAll)
            {
                data = data.Where(p => p.Valid == true);
            }
            List<MD_Organization> dataOrgList = data.Where(o => o.ParentId == parentId).ToList();
            List<object> jsonData = new List<object>();
            dataOrg = data.ToList();

            dataOrgList.ForEach(item =>
            {
                jsonData.Add(new ViewTreeData
                {
                    id = item.Id.ToString(),
                    text = item.Name,
                    state = "open",
                    children = GetChildren(item.Id)
                });
            });
            return jsonData;
        }

        public List<object> GetChildren(int parentId)
        {
            var data = dataOrg.Where(o => o.ParentId == parentId).ToList();
            List<object> jsonData = new List<object>();

            data.ForEach(item =>
            {
                jsonData.Add(new ViewTreeData
                {
                    id = item.Id.ToString(),
                    text = item.Name,
                    state = "",
                    children = GetChildren(item.Id)
                });
            });
            return jsonData;
        }

        /// <summary>
        /// 获取树形Org数据
        /// </summary>
        /// <returns></returns>
        public List<object> GetOrganizationTreeData(int parentId,string orgIDs)
        {
            var data = _dbSession.MD_OrganizationRepository.Entities.Where(p => p.Valid == true);
            List<MD_Organization> dataOrgList = data.Where(o => o.ParentId == parentId).ToList();
            List<object> jsonData = new List<object>();
            dataOrg = data.ToList();
            string[] orgIDArr = orgIDs == null ? null : orgIDs.Split(',');

            dataOrgList.ForEach(item =>
            {
                bool _isChecked = false;
                if (orgIDs != null && orgIDs != "")
                {
                    for (int i = 0; i < orgIDArr.Length; i++)
                    {
                        if (item.Id == int.Parse(orgIDArr[i]))
                        {
                            _isChecked = true;
                        }
                    }
                }
                jsonData.Add(new ViewTreeData
                {
                    id = item.Id.ToString(),
                    text = item.Name,
                    _ischecked = _isChecked,
                    state = "open",
                    children = GetChildren(item.Id, orgIDs)
                });
            });
            return jsonData;
        }

        public List<object> GetChildren(int parentId, string orgIDs)
        {
            string[] orgIDArr = orgIDs == null ? null : orgIDs.Split(',');
            var data = dataOrg.Where(o => o.ParentId == parentId).ToList();
            List<object> jsonData = new List<object>();

            data.ForEach(item =>
            {
                bool _isChecked = false;
                if (orgIDs != null && orgIDs != "")
                {
                    for (int i = 0; i < orgIDArr.Length; i++)
                    {
                        if (item.Id == int.Parse(orgIDArr[i]))
                        {
                            _isChecked = true;
                        }
                    }
                }
                jsonData.Add(new ViewTreeData
                {
                    id = item.Id.ToString(),
                    text = item.Name,
                    _ischecked = _isChecked,
                    state = "",
                    children = GetChildren(item.Id, orgIDs)
                });
            });
            return jsonData;
        }

        public List<ViewTreeGridData> LoadOrganizationData()
        {

            var data = _dbSession.MD_OrganizationRepository.Entities;
            var dataParent = _dbSession.MD_OrganizationRepository.Entities;
            var orgData = from d in data
                          join dp in dataParent on d.ParentId equals dp.Id into tmp
                          from tt in tmp.DefaultIfEmpty()
                          select new ViewOrganizationData
                          {
                              Id = d.Id,
                              Code = d.Code,
                              Name = d.Name,
                              ParentId = d.ParentId,
                              ParentName = tt.Name,
                              Level = d.Level,
                              Valid = d.Valid
                          };
            List<ViewOrganizationData> dataOrgList = orgData.Where(o => o.ParentId == 0).ToList();
            List<ViewTreeGridData> jsonData = new List<ViewTreeGridData>();
            dataOrgTree = orgData.ToList();

            dataOrgList.ForEach(item =>
            {
                jsonData.Add(new ViewTreeGridData
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    ParentName=item.ParentName,
                    Code = item.Code,
                    Name = item.Name,
                    Level = item.Level,
                    Valid = item.Valid,
                    children = GetTreeGridChild(item.Id)
                });
            });

            return jsonData;
        }

        public List<ViewTreeGridData> GetTreeGridChild(int parentId)
        {
            var data = dataOrgTree.Where(o => o.ParentId == parentId).ToList();
            List<ViewTreeGridData> jsonData = new List<ViewTreeGridData>();

            data.ForEach(item =>
            {
                jsonData.Add(new ViewTreeGridData
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    ParentName=item.ParentName,
                    Code = item.Code,
                    Name = item.Name,
                    Level = item.Level,
                    Valid = item.Valid,
                    children = GetTreeGridChild(item.Id)
                });
            });
            return jsonData;
        }

        public MD_Organization CheckData(string Code)
        {
            return _dbSession.MD_OrganizationRepository.Entities.Where(p => p.Code == Code).FirstOrDefault();
        }

        public MD_Organization GetDataById(int Id)
        {
            return _dbSession.MD_OrganizationRepository.Entities.Where(p => p.Id == Id).FirstOrDefault();
        }

        public int GetLevelById(int Id)
        {
            var data = _dbSession.MD_OrganizationRepository.Entities;
            data = data.Where(p => p.Id == Id);
            return data.FirstOrDefault().Level;
        }
    }
}
