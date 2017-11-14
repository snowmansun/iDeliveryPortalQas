using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SFA.BLL.Import;
using SFA.DAL.Models;

namespace SFA.Portal.Export
{
    public class ExportConfig
    {
        public string TableName { get; set; }
        public string FileName { get; set; }
        public List<ExportColumn> Columns { get; set; }

        public ExportConfig(string type, int domainId, string xmlPath, bool showErrorColumn = false, bool showValidColumn = true)
        {
            Columns = new List<ExportColumn>();
            LoadXML(type, domainId, xmlPath, showErrorColumn, showValidColumn);
        }

        /// <summary>
        /// 读取XML配置信息
        /// </summary>
        private void LoadXML(string type, int domainId, string xmlPath, bool showErrorColumn = false, bool showValidColumn = true)
        {
            if (!File.Exists(xmlPath))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            XmlNode tableNode = doc.DocumentElement.SelectSingleNode("//Table");
            this.TableName = tableNode.Attributes["Name"].Value;
            this.FileName = tableNode.Attributes["FileName"].Value;

            XmlNodeList columnNodes = tableNode.SelectNodes("Columns/Column");
            foreach (XmlNode columnNode in columnNodes)
            {
                if (!showErrorColumn)
                {
                    if (columnNode.Attributes["FieldName"].Value == "errormsg")
                        continue;
                }
                if(!showValidColumn)
                    if (columnNode.Attributes["FieldName"].Value == "valid")
                        continue;

                if ((type.ToLower() == "product" || type.ToLower() == "product.en-us") && columnNode.Attributes["FieldName"].Value.Contains("field"))
                {
                    ProductService service = new ProductService(domainId);
                    Prod_KeyType keyType = service.GetFieldType(columnNode.Attributes["FieldName"].Value);
                    if (keyType == null)
                    {
                        continue;
                    }
                    else
                    {
                        columnNode.Attributes["DisplayName"].Value = keyType.TypeDesc;
                    }
                }

                ExportColumn exportColumn = new ExportColumn();
                exportColumn.FieldName = columnNode.Attributes["FieldName"].Value;
                exportColumn.DisplayName = columnNode.Attributes["DisplayName"].Value;

                if (columnNode.Attributes["Required"] == null)
                    exportColumn.Required = false;
                else
                    exportColumn.Required = columnNode.Attributes["Required"].Value.ToLower() == "true";

                if (columnNode.Attributes["Width"] != null)
                    exportColumn.Width = int.Parse(columnNode.Attributes["Width"].Value);

                exportColumn.Sample = columnNode.SelectSingleNode("Sample/Description").InnerText;
                exportColumn.SampleComment = columnNode.SelectSingleNode("Sample/Comment").InnerText;

                this.Columns.Add(exportColumn);
            }
        }
    }
}