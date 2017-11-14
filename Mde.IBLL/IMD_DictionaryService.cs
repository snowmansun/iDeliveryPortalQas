using Mde.Common;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface IMD_DictionaryService : IBaseService<MD_Dictionary>
    {
        List<ViewComboboxStringData> GetDictionaryData(string Category, string Code);   
        
        List<MD_Dictionary> GetDictionaryList(string Category, string Description, string Status, ParameterQuery param);

        List<ViewComboboxStringData> GetDictionaryCodeList(string Category);

        List<ViewComboboxStringData> GetDictionaryCategoryList();

        MD_Dictionary CheckData(string Category, string Value);

        MD_Dictionary GetDataById(int id);

        int GetMaxSequence(string Category);

        void EditData(List<MD_Dictionary> LstData, string UserName);

        void ChangeSequence(List<MD_Dictionary> LstData, string LoginName);
    }
}
