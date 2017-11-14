using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Reflection;

namespace Mde.Portal.Common
{
    public class Unit
    {
        /// <summary>
        /// 将一串分隔的字符串返回List
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<int> ConvertToList(string str,char split)
        {
            List<int> result = new List<int>();
            if (!string.IsNullOrWhiteSpace(str))
            {
                string[] arrayStr = str.Split(split);
                for (int i = 0; i < arrayStr.Length; i++)
                    if (!string.IsNullOrWhiteSpace(arrayStr[i]))
                        result.Add(int.Parse(arrayStr[i]));
            }
            return result;
        }

        /// <summary>
        /// List转换成DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}