using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace ArisoTool
{
    public class AES
    {




        // Save an object out to the disk
        public   void SerializeObject<T>(  T toSerialize, String filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            TextWriter textWriter = new StreamWriter(filename);

            xmlSerializer.Serialize(textWriter, toSerialize);
            textWriter.Close();
        }



        public   string SerializeToString<T>(  T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }


        public   string SerializeFromString<T>(  T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }



        public string key = "12345678901234567890123456789012";


 
        /// <summary>
        /// Encrypts string using key
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns>The encrypted result as base64 encoded string</returns>
        public string EncryptString(string message)
        {
            var toEncryptBytes = Encoding.UTF8.GetBytes(message);
            using (var provider = new AesCryptoServiceProvider())
            {


                byte[] keybyte = Encoding.Unicode.GetBytes(key);

                byte[] targetkey = new byte[32];
                
                System.Array.Copy(keybyte, targetkey, 32);

                provider.Key = targetkey;
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                provider.GenerateIV();
                using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        // add the generated vector to the encrypted string (will work as salt to garantee different encrypted strings for the same string)
                        ms.Write(provider.IV, 0, 16);
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(toEncryptBytes, 0, toEncryptBytes.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        /// <summary>
        /// Decrypts base64encoded message using key
        /// </summary>
        /// <param name="encryptedBase64"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        public string DecryptString(string encryptedBase64)
        {
            var encrypted = Convert.FromBase64String(encryptedBase64);

            using (var provider = new AesCryptoServiceProvider())
            {

                byte[] keybyte = Encoding.Unicode.GetBytes(key);

                byte[] targetkey = new byte[32];

                System.Array.Copy(keybyte, targetkey, 32);

                provider.Key = targetkey;
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                using (var ms = new MemoryStream(encrypted))
                {
                    provider.IV = GetVector(ms);
                    using (var decryptor = provider.CreateDecryptor(provider.Key, provider.IV))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            var decrypted = new byte[encryptedBase64.Length];
                            var byteCount = cs.Read(decrypted, 0, encryptedBase64.Length);
                            return Encoding.UTF8.GetString(decrypted, 0, byteCount);
                        }
                    }
                }
            }
        }

        private static byte[] GetVector(Stream ms)
        {
            var vector = new byte[16];
            ms.Read(vector, 0, 16);
            return vector;
        }

         static AES aes;

        public static string Decrypt(string base64)
        {
            if (aes == null) aes = new AES();
            return aes.DecryptString(base64);
        }
    }
}