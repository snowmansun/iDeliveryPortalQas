using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mde.BLL
{
    public partial class MD_DictionaryService : BaseService<MD_Dictionary>,IMD_DictionaryService
    {
        public List<ViewComboboxStringData> GetDictionaryData(string Category, string Code)
        {
            var data = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Valid == true);
            if (!string.IsNullOrEmpty(Category))
            {
                data = data.Where(d => d.Category == Category);
            }
            if (!string.IsNullOrEmpty(Code))
            {
                data = data.Where(d => d.Description.Contains(Code));
            }
            var comboxData = from d in data
                             orderby d.Sequence
                             select new ViewComboboxStringData
                             {
                                 value = d.Value,
                                 name = d.Description
                             };

            return comboxData.ToList();
        }

        public List<ViewComboboxStringData> GetDictionaryCategoryList()
        {
            var data = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category != "");
            var comboxData = (from d in data
                             orderby d.Category
                             select new ViewComboboxStringData
                             {
                                 value = d.Category,
                                 name = d.Category
                             }).Distinct();

            return comboxData.ToList();
        }
        public List<ViewComboboxStringData> GetDictionaryCodeList(string Category)
        {
            var data = _dbSession.MD_DictionaryRepository.Entities;
            data = data.Where(p => p.Category == Category);
            var comboxData = from d in data
                             orderby d.Description
                             select new ViewComboboxStringData
                             {
                                 value = d.Description,
                                 name = d.Description
                             };

            return comboxData.ToList();
        }

        public List<MD_Dictionary> GetDictionaryList(string Category, string Description, string Status, ParameterQuery param)
        {
            var data = _dbSession.MD_DictionaryRepository.Entities;
            if (!string.IsNullOrEmpty(Category))
            {
                data = data.Where(p => p.Category == Category);
            }
            if (!string.IsNullOrEmpty(Description))
            {
                data = data.Where(p => p.Description.Contains(Description));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                Boolean valid = Status == "1" ? true : false;
                data = data.Where(p => p.Valid == valid);
            }
            ////统计当前满足条件的记录数
            //param.total = data.Count();

            ////分页
            //if (param.pageIndex > 0)
            //    data = data.OrderBy(p => p.Category)
            //        .ThenByDescending(p => p.Valid)
            //        .ThenBy(p => p.Sequence)
            //        .Skip((param.pageIndex - 1) * param.pageSize)
            //        .Take(param.pageSize);
            data = data.OrderBy(p => p.Category)
                    .ThenByDescending(p => p.Valid)
                    .ThenBy(p => p.Sequence);
            return data.AsQueryable().ToList();
        }

        public MD_Dictionary CheckData(string Category, string Value)
        {
            var data = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == Category);
            if (!string.IsNullOrEmpty(Value))
            {
                data = data.Where(p => p.Value == Value);
            }
            return data.FirstOrDefault();
        }

        public MD_Dictionary GetDataById(int id)
        {
            MD_Dictionary data = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.ID == id).FirstOrDefault();
            return data;
        }

        public int GetMaxSequence(string Category)
        {
            var data = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == Category);
            var maxSequence=0;
            if (data.FirstOrDefault() != null)
            {
                maxSequence = (from a in data select a.Sequence).Max();
            }
            maxSequence = maxSequence + 1;
            return maxSequence;
        }

        public void EditData(List<MD_Dictionary> LstData, string UserName)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (MD_Dictionary item in LstData)
                {
                    item.LastUpdateTime = DateTime.Now;
                    item.LastUpdateUser = UserName;

                    _dbSession.MD_DictionaryRepository.UpdateEntities(item);
                    _dbSession.MD_DictionaryRepository.Commit();
                }

                scope.Complete();
            }
        }

        public void ChangeSequence(List<MD_Dictionary> LstData,string LoginName)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                int i=0;
                foreach (MD_Dictionary item in LstData)
                {
                    if (item.Valid)
                    {
                        i++;
                        item.Sequence = i;
                        item.LastUpdateTime = DateTime.Now;
                        item.LastUpdateUser = LoginName;

                        _dbSession.MD_DictionaryRepository.UpdateEntities(item);
                        _dbSession.MD_DictionaryRepository.Commit();
                    }
                }

                scope.Complete();
            }
        }
    }
}
