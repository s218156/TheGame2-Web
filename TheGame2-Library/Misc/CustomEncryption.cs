
using System.Security.Cryptography;
using System.Text;
using TheGame2_Library.Models;

namespace TheGame2_Library.Misc
{
    public static class CustomEncryption
    {
        private static string Encrypt(string text, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }

                }
            }
        }

        private static string Decrypt(string cipher, string key)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        public static string GenerateToken(string username, string authToken, string refreshToken)
        {
            string usernameEncrypted = Encrypt(username, authToken);
            return usernameEncrypted + "," + authToken + "," + refreshToken;
        }

        public static UserModel DecryptUser(string token)
        {
            string[] parts = token.Split(',');
            UserModel user = new UserModel();
            user.username = Decrypt(parts[0], parts[1]);
            user.fullname = "";
            user.password = "";
            user.id = 0;
            user.authToken = parts[1];
            user.textureID = 0;
            user.refreshToken = parts[2];
            return user;
        }

        public static string DecryptPassword(string password)
        {
            string key = DateTime.UtcNow.ToString("MM-dd-yyyy:HH_mm");
            return Decrypt(password, key);
        }

        public static string EncryptPassword(string password)
        {
            string key = DateTime.UtcNow.ToString("MM-dd-yyyy:HH_mm");
            return Encrypt(password, key);
        }

        public static string EncryptPasswordForDatabase(UserModel model)
        {
            Console.WriteLine(Encrypt(model.password, model.username));
            return Encrypt(model.password, model.username);
        }
    }
}
