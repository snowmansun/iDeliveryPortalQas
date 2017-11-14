using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Utility
{
    public class CommonHelp
    {
        /// <summary>        
        /// 导出为Excel        
        /// </summary>        
        /// <param name="control">控件</param>        
        /// <param name="encoding">导出格式</param>        
        /// <param name="filename">文件名</param>         
        public static void ConvertToExcel(System.Web.UI.Control control, string encoding, string filename)
        {

            //设置文件名格式，防止中文文件名乱码  
            string FileName = System.Web.HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(filename));
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.Charset = "" + encoding + "";
            //下面这行很重要， attachment 参数表示作为附件下载，您可以改成 online在线打开            
            //filename=FileFlow.xls 指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc 　　 .xls 　　 .txt 　　.htm            
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("" + encoding + "");
            //Response.ContentType指定文件类型 可以为application/ms-excel、application/ms-word、application/ms-txt、application/ms-html 或其他浏览器可直接支持文档            
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            control.EnableViewState = false;
            //　定义一个输入流            
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            control.RenderControl(oHtmlTextWriter);
            //this 表示输出本页，你也可以绑定datagrid,或其他支持obj.RenderControl()属性的控件            
            System.Web.HttpContext.Current.Response.Write(oStringWriter.ToString());
            System.Web.HttpContext.Current.Response.End();
        }

        /// <summary>  
        /// 读取Excel文件到DataSet中  
        /// </summary>  
        /// <param name="filePath">文件路径</param>  
        /// <returns></returns>  
        public static DataSet ToDataTable(string filePath)
        {
            string connStr = "";
            string fileType = System.IO.Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileType)) return null;

            if (fileType.ToLower() == ".xls")
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            else
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            string sql_F = "Select * FROM [{0}]";

            OleDbConnection conn = null;
            OleDbDataAdapter da = null;
            DataTable dtSheetName = null;

            DataSet ds = new DataSet();
            try
            {
                // 初始化连接，并打开  
                conn = new OleDbConnection(connStr);
                conn.Open();

                // 获取数据源的表定义元数据                         
                string SheetName = "";
                dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                // 初始化适配器  
                da = new OleDbDataAdapter();
                for (int i = 0; i < dtSheetName.Rows.Count; i++)
                {
                    SheetName = (string)dtSheetName.Rows[i]["TABLE_NAME"];

                    if (SheetName.Contains("$") && !SheetName.Replace("'", "").EndsWith("$"))
                    {
                        continue;
                    }

                    da.SelectCommand = new OleDbCommand(String.Format(sql_F, SheetName), conn);
                    DataSet dsItem = new DataSet();
                    da.Fill(dsItem, SheetName);

                    ds.Tables.Add(dsItem.Tables[0].Copy());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // 关闭连接  
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    da.Dispose();
                    conn.Dispose();
                }
            }
            return ds;
        } 


    }
}
