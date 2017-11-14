using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Utility
{
    public class Encrypt
    {
        /// <summary>
        /// MD5算法加密
        /// </summary>
        /// <param name="cryptText"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string cryptText)
        {
            MD5 myMD5 = new MD5CryptoServiceProvider();
            byte[] hashCode;
            hashCode = Encoding.Default.GetBytes(cryptText);
            hashCode = myMD5.ComputeHash(hashCode);
            StringBuilder enText = new StringBuilder();
            foreach (byte byt in hashCode)
            {
                enText.AppendFormat("{0:x2}", byt);
            }
            return enText.ToString().ToUpper();
        }
    }
}
