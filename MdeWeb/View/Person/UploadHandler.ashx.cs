using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Utility;

namespace DatapoolWeb.View.Person
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadHandler : IHttpHandler
    {
        IPersonService personService = new PersonService();

        public void ProcessRequest(HttpContext context)
        {
            int resultVal = (int)ReturnVal.Failed;
            try
            {
                HttpPostedFile myFile = context.Request.Files["fulFile"];
                //HttpPostedFile myFile = context.Request.Files["Filedata"];
                string uploadPath = context.Server.MapPath("~\\uploads\\");  

                if (myFile != null)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    myFile.SaveAs(uploadPath + myFile.FileName);  

                    if (myFile.InputStream.Length != 0)
                    {
                        string originalFileName = Path.GetFileName(uploadPath + myFile.FileName);     //原文件

                        DataSet ds = CommonHelp.ToDataTable(uploadPath + myFile.FileName);
                        personService.ImportData(ds);

                        resultVal = (int)ReturnVal.Succeed;
                    }
                    else
                    {
                        resultVal = (int)ReturnVal.FileEmpty;
                    }
                }
                else
                {
                    resultVal = (int)ReturnVal.NotSelected;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
            finally
            {
                context.Response.Write(resultVal);
            }

            //context.Response.ContentType = "text/plain";
            //context.Response.Charset = "utf-8";

            //HttpPostedFile file = context.Request.Files["Filedata"];
            //string uploadPath =
            //    HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

            //if (file != null)
            //{
               
            //    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失  
            //    context.Response.Write("1");
            //}
            //else
            //{
            //    context.Response.Write("0");
            //}    
        }

        #region## 返回值
        /// <summary>
        /// 返回值
        /// </summary>
        private enum ReturnVal : int
        {
            /// <summary>
            /// 不能上传 0 K大小的文件
            /// </summary>
            FileEmpty = -2,

            /// <summary>
            /// 未选中文件
            /// </summary>
            NotSelected = -1,

            /// <summary>
            /// 上传失败
            /// </summary>
            Failed = 0,

            /// <summary>
            /// 成功
            /// </summary>
            Succeed = 1

        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}