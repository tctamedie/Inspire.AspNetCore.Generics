﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Inspire.DataAccess.Security
{
    public interface IEncryptionService
    {
        string Decrypt(string decryptedText, string pwd);
        string Encrypt(string text, string pwd);
    }
    public class EncryptionService : IEncryptionService
    {
        protected byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            try
            {
                byte[] decryptedBytes = null;
                // Set your salt here to meet your flavor:  
                byte[] saltBytes = passwordBytes;
                // Example:  
                //saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };  

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Aes AES = Aes.Create())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        //AES.Mode = CipherMode.CBC;  

                        using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            //If(cs.Length = ""  
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }
                return decryptedBytes;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public string Decrypt(string decryptedText, string pwd)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);

            // Hash the password with SHA256  
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] decryptedBytes = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            if (decryptedBytes != null)
            {
                // Getting the size of salt  
                int saltSize = GetSaltSize(passwordBytes);

                // Removing salt bytes, retrieving original bytes  
                byte[] originalBytes = new byte[decryptedBytes.Length - saltSize];
                for (int i = saltSize; i < decryptedBytes.Length; i++)
                {
                    originalBytes[i - saltSize] = decryptedBytes[i];
                }
                return Encoding.UTF8.GetString(originalBytes);
            }
            else
            {
                return null;
            }
        }
        private int GetSaltSize(byte[] passwordBytes)
        {
            var key = new Rfc2898DeriveBytes(passwordBytes, passwordBytes, 1000);
            byte[] ba = key.GetBytes(2);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ba.Length; i++)
            {
                sb.Append(Convert.ToInt32(ba[i]).ToString());
            }
            int saltSize = 0;
            string s = sb.ToString();
            foreach (char c in s)
            {
                int intc = Convert.ToInt32(c.ToString());
                saltSize = saltSize + intc;
            }

            return saltSize;
        }
        protected byte[] GetRandomBytes(int length)
        {
            byte[] ba = new byte[length];
            RandomNumberGenerator.Create().GetBytes(ba);
            return ba;
        }
        public string Encrypt(string text, string pwd)
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = null;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);

            // Hash the password with SHA256  
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            // Getting the salt size  
            int saltSize = GetSaltSize(passwordBytes);
            // Generating salt bytes  
            byte[] saltBytes = GetRandomBytes(saltSize);

            // Appending salt bytes to original bytes  
            byte[] bytesToBeEncrypted = new byte[saltBytes.Length + originalBytes.Length];
            for (int i = 0; i < saltBytes.Length; i++)
            {
                bytesToBeEncrypted[i] = saltBytes[i];
            }
            for (int i = 0; i < originalBytes.Length; i++)
            {
                bytesToBeEncrypted[i + saltBytes.Length] = originalBytes[i];
            }

            encryptedBytes = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(encryptedBytes);
        }
        protected byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:  
            byte[] saltBytes = passwordBytes;
            // Example:  
            //saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };  

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

    }
}
