using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mde.Portal.Model
{
    public class ModuleModel
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string parent_code { get; set; }

        public List<object> children { get; set; }

        public int seq { get; set; }

        public Nullable<int> client_scope { get; set; }

        public Nullable<int> action_mode { get; set; }

        public ModuleActionTypeModel action { get; set; }

        public ModuleModel()
        {
            children = new List<object>();
            action = new ModuleActionTypeModel();
        }
    }

    public class ModuleActionTypeModel
    {
        public List<ModuleKeyValueModel> configuration { get; set; }
        public string default_key { get; set; }

        public ModuleActionTypeModel()
        {
            configuration = new List<ModuleKeyValueModel>();
        }
    }

    public class ModuleKeyValueModel
    {
        public string key { get; set; }
        public string value { get; set; }

    }
}