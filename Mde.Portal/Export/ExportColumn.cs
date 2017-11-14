using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA.Portal.Export
{
    public class ExportColumn
    {
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string Sample { get; set; }
        public string SampleComment { get; set; }
        public int Width { get; set; }
        public bool Required { get; set; }
    }
}