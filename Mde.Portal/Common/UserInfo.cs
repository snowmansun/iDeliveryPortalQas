using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mde.Portal.Common
{
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// 用户姓
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string LoginName{ get; set; }
       
        /// <summary>
        /// 系统域ID
        /// </summary>
        public int DomainID{ get; set; }

        /// <summary>
        /// 系统域描述
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID{ get; set; }

        /// <summary>
        /// 角色的描述
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 当前登录用户所属的组织架构
        /// </summary>
        public Nullable<int> OrgId { get; set; }
        //public List<int> LstOrgUnitId { get; set; }

        //public UserInfo()
        //{
        //    LstOrgUnitId = new List<int>();
        //}
    }
}