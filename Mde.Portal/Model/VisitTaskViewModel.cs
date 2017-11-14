using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mde.Portal.Model
{
    public class VisitTaskViewModel
    {
        public string name { get; set; }

        public string dimension { get; set; }

        public int answer_type { get; set; }

        public string answer_type_desc { get; set; }

        public string need_feedback { get; set; }

        public string need_photo { get; set; }

        public int sequence { get; set; }

        public string dropdown { get; set; }

        public List<string> details { get; set; }

        public VisitTaskViewModel()
        {
            details = new List<string>();
        }
    }
}