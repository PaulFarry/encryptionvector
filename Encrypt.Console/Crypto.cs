using System;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt.Application
{
    public class Crypto
    {
        public static SensitiveValue Encrypt(string base64Key, string rawData)
        {
            var result = new SensitiveValue();
            var rawBytes = Encoding.UTF8.GetBytes(rawData);

            using (var encryptor = GetEncrypt())
            {
                encryptor.Key = Convert.FromBase64String(base64Key);
                result.AllocateVector(encryptor.IV);

                using (var enc = encryptor.CreateEncryptor())
                {
                    result.Data = enc.TransformFinalBlock(rawBytes, 0, rawBytes.Length);
                }
            }

            return result;
        }

        public static string GetKey()
        {
            using (var encryptor = GetEncrypt())
            {
                return Convert.ToBase64String(encryptor.Key);
            }
        }


        private static SymmetricAlgorithm GetEncrypt()
        {
            var encryptor = new AesManaged
            {
                BlockSize = 128,
                KeySize = 256,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            return encryptor;
        }

        public static string Decrypt(string base64Key, SensitiveValue encrypted)
        {
            using (var encryptor = GetEncrypt())
            {
                encryptor.Key = Convert.FromBase64String(base64Key);
                encryptor.IV = encrypted.CopyVector();
                using (var enc = encryptor.CreateDecryptor())
                {
                    var data = enc.TransformFinalBlock(encrypted.Data, 0, encrypted.Data.Length);
                    return Encoding.UTF8.GetString(data);
                }
            }
        }
    }
}
