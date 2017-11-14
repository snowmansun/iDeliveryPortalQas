using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
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
    public partial class DSD_M_TruckCheckListService : BaseService<DSD_M_TruckCheckList>, IDSD_M_TruckCheckListService
    {
        public List<ViewTruckCheckListData> GetTruckCheckList(string Content, string TruckType, ParameterQuery param)
        {
            string sql = @"SELECT tcl.Id,tcl.Content,tcl.Description,tcl.LastUpdateTime,
                                    CASE tcl.Valid WHEN 1 THEN CASE WHEN tcl.StartDate>=GETDATE() OR tcl.EndDate<=GETDATE() THEN 0 ELSE 1 END
		                             ELSE 0 END AS Valid,
                                    tca.TruckType,d.Description TruckTypeDesc,tcl.StartDate,tcl.EndDate
	                            INTO #ViewTmp
                            FROM dbo.DSD_M_TruckCheckList tcl
	                            LEFT JOIN dbo.DSD_M_TruckCheckAssign tca ON tcl.Id=tca.CheckListId
	                            LEFT JOIN dbo.MD_Dictionary d ON d.Category='TruckType' AND d.Value=tca.TruckType
                            WHERE tcl.Level=1 ";

            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            if (!string.IsNullOrEmpty(Content))
            {
                sql = sql + " AND tcl.Content Like '%'+@Content+'%'";
                paramQ = new SqlParameter()
                {
                    ParameterName = "Content",
                    Value = Content
                };
                listParams.Add(paramQ);
            }
            sql +=" SELECT * INTO #Tmp FROM #ViewTmp a";
            if (!string.IsNullOrEmpty(TruckType))
            {
                sql += " WHERE a.Id IN (SELECT Id FROM #ViewTmp WHERE TruckType=@TruckType)";
                paramQ = new SqlParameter()
                {
                    ParameterName = "TruckType",
                    Value = TruckType
                };
                listParams.Add(paramQ);
            }
            sql += @"
                    SELECT a.Id,a.Content,a.Description,CAST(a.Valid AS BIT) Valid,a.LastUpdateTime,a.StartDate,a.EndDate,
	                    TruckType=STUFF((SELECT ','+TruckType FROM #Tmp WHERE Id=a.Id FOR XML PATH('')),1,1,''),
	                    TruckTypeDesc=STUFF((SELECT ','+TruckTypeDesc FROM #Tmp WHERE Id=a.Id FOR XML PATH('')),1,1,'')
                    FROM #Tmp a WHERE 1=1 ";

            sql += @"
                    GROUP BY a.Id,a.Content,a.Description,a.Valid,a.LastUpdateTime,a.StartDate,a.EndDate
                    DROP TABLE #ViewTmp,#Tmp";
            IList<ViewTruckCheckListData> LstData = _dbSession.ExecuteSqlNonQuery<ViewTruckCheckListData>(sql, listParams.ToArray());
            var data = from d in LstData
                       select new ViewTruckCheckListData
                       {
                           Id = d.Id,
                           Content = d.Content,
                           Description = d.Description,
                           TruckType = d.TruckType,
                           TruckTypeDesc = d.TruckTypeDesc,
                           Valid = d.Valid,
                           StartDate = d.StartDate,
                           EndDate = d.EndDate,
                           LastUpdateTime = d.LastUpdateTime
                       };
            param.total = data.Count();
            if (param.pageIndex > 0)
            {
                data = data.OrderByDescending(d => d.LastUpdateTime)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }

            return data.AsQueryable().ToList();
        }

        public List<ViewTruckTypeData> GetTruckTypeList(int ListId,ParameterQuery param)
        {
            var dataDic = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Valid == true && p.Category == "TruckType");
            var dataAssign = _dbSession.DSD_M_TruckCheckAssignRepository.Entities.Where(p => p.CheckListId == ListId);
            var data = from d in dataDic
                       join da in dataAssign on d.Value equals da.TruckType into tmp
                       from tt in tmp.DefaultIfEmpty()
                       select new ViewTruckTypeData
                       {
                           TruckType = d.Value,
                           Description = d.Description,
                           IsAssigned = tt.CheckListId == null ? false : true,
                           Sequence=d.Sequence
                       };
            param.total = data.Count();
            if (param.pageIndex > 0)
            {
                data = data.OrderByDescending(p => p.IsAssigned)
                    .ThenBy(p => p.Sequence)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.ToList();
        }

        public void SaveTruckAssign(List<DSD_M_TruckCheckAssign> items,int ListId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var data = _dbSession.DSD_M_TruckCheckAssignRepository.Entities.Where(p => p.CheckListId == ListId);
                foreach (DSD_M_TruckCheckAssign item in data)
                {
                    _dbSession.DSD_M_TruckCheckAssignRepository.DeleteEntities(item);
                    _dbSession.DSD_M_TruckCheckAssignRepository.Commit();
                }

                foreach (DSD_M_TruckCheckAssign item in items)
                {
                    _dbSession.DSD_M_TruckCheckAssignRepository.AddEntities(item);
                    _dbSession.DSD_M_TruckCheckAssignRepository.Commit();
                }

                scope.Complete();
            }
        }

        public void SaveTruckCheckList(TruckCheckListData item,string LoginName,string mode)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DSD_M_TruckCheckList dataTitle = new DSD_M_TruckCheckList();
                DSD_M_TruckCheckList dataQuestion = new DSD_M_TruckCheckList();
                DSD_M_TruckCheckList dataAnswer = new DSD_M_TruckCheckList();
                if (mode == "Edit")
                {
                    var dataList = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.CheckListId == item.CheckListId);

                    foreach (DSD_M_TruckCheckList d in dataList.ToList())
                    {
                        if (d.Level == 1)
                        {
                            d.Content = item.Title;
                            d.Description = item.Description;
                            d.StartDate = item.StartDate;
                            d.EndDate = item.EndDate;
                            d.LastUpdateTime = DateTime.Now;
                            d.LastUpdateUser = LoginName;
                            _dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(d);

                            dataTitle = d;
                        }
                        else
                        {
                            d.Valid = false;
                            d.CreateTime = DateTime.Now;
                            d.CreateUser = LoginName;
                            _dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(d);
                        }
                        _dbSession.DSD_M_TruckCheckListRepository.Commit();
                    }
                    //dataTitle = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.Id == item.CheckListId).FirstOrDefault();
                    //dataTitle.Content = item.Title;
                    //dataTitle.Description = item.Description;
                    //dataTitle.StartDate = item.StartDate;
                    //dataTitle.EndDate = item.EndDate;
                    //dataTitle.LastUpdateTime = DateTime.Now;
                    //dataTitle.LastUpdateUser = LoginName;
                    //_dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(dataTitle);
                }
                else
                {
                    dataTitle = new DSD_M_TruckCheckList();
                    dataTitle.Content = item.Title;
                    dataTitle.Description = item.Description;
                    dataTitle.StartDate = item.StartDate;
                    dataTitle.EndDate = item.EndDate;
                    dataTitle.InputType = "0";
                    dataTitle.Level = 1;
                    dataTitle.Valid = true;
                    dataTitle.CreateTime = DateTime.Now;
                    dataTitle.CreateUser = LoginName;
                    dataTitle.LastUpdateTime = DateTime.Now;
                    dataTitle.LastUpdateUser = LoginName;
                    _dbSession.DSD_M_TruckCheckListRepository.AddEntities(dataTitle);
                    _dbSession.DSD_M_TruckCheckListRepository.Commit();

                    dataTitle.CheckListId = dataTitle.Id;
                    _dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(dataTitle);
                    _dbSession.DSD_M_TruckCheckListRepository.Commit();
                }

                int sequence = 1;
                foreach (Questions q in item.LstQuestion)
                {
                    if (q.Id == 0)
                    {
                        dataQuestion = new DSD_M_TruckCheckList();
                        dataQuestion.CheckListId = dataTitle.Id;
                        dataQuestion.ParentId = dataTitle.Id;
                        dataQuestion.Content = q.Question;
                        dataQuestion.InputType = q.InputeType;
                        dataQuestion.MustToDo = q.MustToDo;
                        dataQuestion.Level = 2;
                        dataQuestion.Sequence = sequence;
                        dataQuestion.Valid = true;
                        dataQuestion.CreateTime = DateTime.Now;
                        dataQuestion.CreateUser = LoginName;
                        dataQuestion.LastUpdateTime = DateTime.Now;
                        dataQuestion.LastUpdateUser = LoginName;
                        _dbSession.DSD_M_TruckCheckListRepository.AddEntities(dataQuestion);
                    }
                    else
                    {
                        dataQuestion = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.Id == q.Id).FirstOrDefault();

                        dataQuestion.Content = q.Question;
                        dataQuestion.InputType = q.InputeType;
                        dataQuestion.MustToDo = q.MustToDo;
                        dataQuestion.Sequence = sequence;
                        dataQuestion.Valid = true;
                        dataQuestion.LastUpdateTime = DateTime.Now;
                        dataQuestion.LastUpdateUser = LoginName;
                        _dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(dataQuestion);
                    }
                    _dbSession.DSD_M_TruckCheckListRepository.Commit();

                    int parentId = dataQuestion.Id;
                    int aSequence = 1;
                    foreach (Answers a in q.LstAnswer)
                    {
                        if (a.Id == 0)
                        {
                            dataAnswer = new DSD_M_TruckCheckList();
                            dataAnswer.CheckListId = dataTitle.Id;
                            dataAnswer.ParentId = parentId;
                            dataAnswer.Content = a.Answer;
                            dataAnswer.InputType = q.InputeType;
                            dataAnswer.Level = 3;
                            dataAnswer.Sequence = aSequence;
                            dataAnswer.Valid = true;
                            dataAnswer.CreateTime = DateTime.Now;
                            dataAnswer.CreateUser = LoginName;
                            dataAnswer.LastUpdateTime = DateTime.Now;
                            dataAnswer.LastUpdateUser = LoginName;
                            _dbSession.DSD_M_TruckCheckListRepository.AddEntities(dataAnswer);

                        }
                        else
                        {
                            dataAnswer = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.Id == a.Id).FirstOrDefault();
                            
                            dataAnswer.Content = a.Answer;
                            dataAnswer.InputType = q.InputeType;
                            dataAnswer.Sequence = aSequence;
                            dataAnswer.Valid = true;
                            dataAnswer.LastUpdateTime = DateTime.Now;
                            dataAnswer.LastUpdateUser = LoginName;
                            _dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(dataAnswer);
                        }
                        _dbSession.DSD_M_TruckCheckListRepository.Commit();

                        aSequence++;
                    }
                    sequence++;
                }

                scope.Complete();
            }
        }

        public TruckCheckListData GetTruckCheckDataById(int id)
        {
            TruckCheckListData rData = new TruckCheckListData();
            var data = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.Id == id).FirstOrDefault();
            rData.Title = data.Content;
            rData.Description = data.Description;
            rData.StartDate = data.StartDate;
            rData.EndDate = data.EndDate;

            var qData = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.ParentId == id && p.Valid == true);
            var dicData = _dbSession.MD_DictionaryRepository.Entities.Where(p=>p.Category=="CheckType");
            List<Questions> LstQuestion = (from a in qData
                                           join d in dicData on a.InputType equals d.Value
                                           select new Questions
                                           {
                                               Id = a.Id,
                                               Question = a.Content,
                                               MustToDo = a.MustToDo,
                                               InputeType = a.InputType,
                                               InputeTypeDesc = d.Description
                                           }).ToList();
            rData.LstQuestion = LstQuestion;
            foreach (Questions item in rData.LstQuestion)
            {
                var aData = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.ParentId == item.Id && p.Valid == true);
                List<Answers> LstAnswer = (from a in aData
                                           join d in dicData on a.InputType equals d.Value
                                           select new Answers
                                           {
                                               Id = a.Id,
                                               Answer = a.Content
                                           }).ToList();
                item.LstAnswer = LstAnswer;
                item.AnswerCount = LstAnswer.Count();
            }
            return rData;
        }

        public DSD_M_TruckCheckList GetDataById(int Id)
        {
            var data = _dbSession.DSD_M_TruckCheckListRepository.Entities.Where(p => p.Id == Id).FirstOrDefault();
            return data;
        }

        public void UpdateTruckList(DSD_M_TruckCheckList item)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.DSD_M_TruckCheckListRepository.UpdateEntities(item);
                _dbSession.DSD_M_TruckCheckListRepository.Commit();

                scope.Complete();
            }
        }
    }
}
