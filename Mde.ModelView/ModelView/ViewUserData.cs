using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewUserData
    {
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Company { get; set; }
        public string CompanyName { get; set; }
        public string Warehouse { get; set; }
        public string WarehouseName { get; set; }
        public string TypeDesc { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsValid { get; set; }
        public bool IsLocked { get; set; }
        public bool NeedChangePwd { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public int LoginFailed { get; set; }
        public Nullable<int> OrgID { get; set; }
        public Nullable<System.Boolean> HaveRole { get; set; }
    }
}
