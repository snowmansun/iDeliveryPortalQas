//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mde.Model.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_Module
    {
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string ParentModule { get; set; }
        public string ModulePath { get; set; }
        public int Sequence { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public Nullable<int> ClientScope { get; set; }
        public string Action { get; set; }
        public Nullable<int> ActionMode { get; set; }
    }
}
