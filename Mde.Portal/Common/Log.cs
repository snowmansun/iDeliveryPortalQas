using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Mde.Portal.Common
{
    public class Log
    {//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Log));

        public enum EnmLogLevel
        {
            INFORMATION = 0,
            WARNING,
            ERROR
        }

        //private static string PhysicalPath = "";

        //static Log()
        //{
        //    if (HttpContext.Current != null)
        //    {
        //        if (!HttpContext.Current.Request.PhysicalApplicationPath.EndsWith("\\"))
        //            PhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath + "\\";
        //        else
        //            PhysicalPath = HttpContext.Current.Request.PhysicalApplicationPath;
        //    }
        //}
        private static string _physicalPath;

        private static string PhysicalPath
        {
            get
            {
                //if (!string.IsNullOrEmpty(_physicalPath))
                //{
                //    return _physicalPath;
                //}
                ////web
                //if (HttpContext.Current != null)
                //{
                //    return HttpContext.Current.Request.PhysicalApplicationPath.TrimEnd('\\') + "\\Log\\";
                //}

                //return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return "E:\\Log\\";
            }
        }

        private static string _errorlogfilename = "_errlog.log";
        private static string _infologfilename = "_info.log";

        public static void SetLogPath(string path)
        {
            _physicalPath = path;
        }

        public static void Error(Exception e)
        {
            WriteLog(e.ToString(), EnmLogLevel.ERROR);
        }

        //public static void Error(System.Data.Entity.Validation.DbEntityValidationException e)
        //{
        //    WriteLog(ExceptionHelper.GetValidationErrorMessage(e), EnmLogLevel.ERROR);
        //    WriteLog(e.ToString(), EnmLogLevel.ERROR);
        //}

        public static void Error(string message)
        {
            //log.Error(message);
            WriteLog(message, EnmLogLevel.ERROR);
        }

        public static void Error(string filename, string message)
        {
            //log.Error(message);
            WriteLog(filename, message, EnmLogLevel.ERROR);
        }

        public static void Warning(string message)
        {
            Warning(_errorlogfilename, message);
        }

        public static void Warning(string filename, string message)
        {
            WriteLog(filename, message, EnmLogLevel.WARNING);
        }

        public static void Info(string message)
        {
            Info(_infologfilename, message);
        }

        public static void Info(string filename, string message)
        {
            WriteLog(filename, message, EnmLogLevel.INFORMATION);
        }

        private static void WriteLog(string message, EnmLogLevel level)
        {
            WriteLog(_errorlogfilename, message, level);
        }

        /// <summary>
        /// 记录出错信息
        /// </summary>
        /// <param name="message">ex中的出错内容</param>
        private static void WriteLog(string filename, string message, EnmLogLevel level)
        {
            try
            {
                if (!Directory.Exists(PhysicalPath))
                {
                    Directory.CreateDirectory(PhysicalPath);
                }

                if (PhysicalPath.EndsWith("\\"))
                {
                    filename = PhysicalPath + filename;
                }
                else
                {
                    filename = PhysicalPath + "\\" + filename;
                }

                if (File.Exists(filename))
                {
                    FileInfo info = new FileInfo(filename);
                    if (info.Length >= 1024 * 1024)
                    {
                        string newfilename = PhysicalPath + info.Name.Replace(info.Extension, "") + DateTime.Now.ToString("yyMMddHHmmss") + info.Extension;
                        System.IO.File.Move(filename, newfilename);
                    }
                }

                string strerror = "[" + level.ToString() + "]\t[" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "]\r\n" + message + "\r\n";
                WriteLine(filename, strerror);
            }
            catch { }
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filepath">文件名</param>
        /// <param name="text">内容</param>
        /// <param name="append">True：添加；False：覆盖</param>
        /// <returns></returns>
        private static void WriteLine(String filepath, String text)
        {
            try
            {
                System.IO.StreamWriter objfile;
                objfile = new System.IO.StreamWriter(filepath, true);
                objfile.WriteLine(text);
                objfile.Close();
            }
            catch { }
        }

        public static void WriteLogFile(string path, string filename, string content)
        {
            try
            {
                path = PhysicalPath + path;// HttpContext.Current.Request.MapPath("/" + path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!path.EndsWith("\\")) path += "\\";
                filename = path + filename;

                //filename = HttpContext.Current.Request.MapPath("/" + filename);

                //if (File.Exists(filename))
                //{
                //    FileInfo info = new FileInfo(filename);
                //    if (info.Length >= 1024 * 1024)
                //    {
                //        string newfilename = HttpContext.Current.Request.MapPath("/" + info.Name.Replace(info.Extension, "") + DateTime.Now.ToString("yyMMddHHmmss") + info.Extension);
                //        System.IO.File.Move(filename, newfilename);
                //    }
                //}

                WriteLine(filename, content);
            }
            catch { }
        }
    }
}