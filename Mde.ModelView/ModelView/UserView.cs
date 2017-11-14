using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Model
{
    public class UserView
    {
        public decimal ID { get; set; }
        public string CODE { get; set; }
        public string PASSWORD { get; set; }
        public string PERSON_ID { get; set; }
        public decimal? GROUP_ID { get; set; }
        public string VALID { get; set; }

        public string GROUP_NM { get; set; }
        public string PERSON_NM { get; set; }
        public string ACCOUNT_CODE { get; set; }


        public int DOMAIN_ID
        {
            get { return 1; }
            set { }
        }

        public string FORCE_EXPORT_ALL
        {
            get { return "N"; }
            set { }
        }

        public int FAILLOGINCOUNT
        {
            get { return 0; }
            set { }
        }
    }
}
