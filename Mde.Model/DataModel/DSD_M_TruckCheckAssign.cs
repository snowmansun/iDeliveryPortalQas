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
    
    public partial class DSD_M_TruckCheckAssign
    {
        public string TruckType { get; set; }
        public int CheckListId { get; set; }
        public bool Valid { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    
        public virtual DSD_M_TruckCheckList DSD_M_TruckCheckList { get; set; }
    }
}
