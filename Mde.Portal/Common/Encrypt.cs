using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Mde.Portal.Common
{
    public class Encrypt
    {
        #region constructor
        public Encrypt()
        {

        }
        #endregion

        #region public method

        public static string SHA1Encrypt(string encryptText)
        {
            byte[] strRes = Encoding.Default.GetBytes(encryptText);
            HashAlgorithm mySHA = new SHA1CryptoServiceProvider();
            strRes = mySHA.ComputeHash(strRes);
            StringBuilder enText = new StringBuilder();
            foreach (byte Byte in strRes)
            {
                enText.AppendFormat("{0:x2}", Byte);
            }
            return enText.ToString();
        }

        public static string HMACSHA1Encrypt(string encryptText, string encryptKey)
        {
            byte[] strRes = Encoding.Default.GetBytes(encryptText);
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.Default.GetBytes(encryptKey));
            CryptoStream cStream = new CryptoStream(Stream.Null, myHMACSHA1, CryptoStreamMode.Write);
            cStream.Write(strRes, 0, strRes.Length);
            StringBuilder enText = new StringBuilder();
            foreach (byte byt in strRes)
            {
                enText.AppendFormat("{0:x2}", byt);
            }
            return enText.ToString();
        }

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
            return enText.ToString();
        }


        public static string AESEncrypt(string cryptText, string cryptKey, string cryptIV)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] textOut = Encoding.Default.GetBytes(cryptText);
            byte[] aesKey = ASCIIEncoding.ASCII.GetBytes(cryptKey);
            byte[] aesIV = ASCIIEncoding.ASCII.GetBytes(cryptKey);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(aesKey, aesIV), CryptoStreamMode.Write);
            cStream.Write(textOut, 0, textOut.Length);
            cStream.FlushFinalBlock();
            StringBuilder strRes = new StringBuilder();
            foreach (byte byt in mStream.ToArray())
            {
                strRes.AppendFormat("{0:x2}", byt);
            }
            return strRes.ToString();
        }

        public static string AESDecrypt(string cryptText, string cryptKey, string cryptIV)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] textOut = new byte[cryptText.Length / 2];
            for (int Count = 0; Count < cryptText.Length; Count += 2)
            {
                textOut[Count / 2] = (byte)(Convert.ToInt32(cryptText.Substring(Count, 2), 16));
            }
            byte[] aesKey = ASCIIEncoding.ASCII.GetBytes(cryptKey);
            byte[] aesIV = ASCIIEncoding.ASCII.GetBytes(cryptIV);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(aesKey, aesIV), CryptoStreamMode.Write);
            cStream.Write(textOut, 0, textOut.Length);
            cStream.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(mStream.ToArray());
        }

        //  Call this function to remove the key from memory after use for security.
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(ref string aestination, int Length);


        // Function to Generate a 64 bits Key.
        public static string GenerateaesKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            AesCryptoServiceProvider aesCrypto = (AesCryptoServiceProvider)AesCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(aesCrypto.Key);
        }

        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            aes.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform aesencrypt = aes.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, aesencrypt, CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        public static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For aes algorithm.
            aes.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            aes.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            //Create a aes decryptor from the aes instance.
            ICryptoTransform aesdecrypt = aes.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //aes decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, aesdecrypt, CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }


        #endregion

    }
}