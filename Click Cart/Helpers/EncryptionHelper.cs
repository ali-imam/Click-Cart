using System.Text;
using System.Security.Cryptography;


namespace Click_Cart.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly string Key = "Encrypting-theId"; // Change this!

        public static string Encrypt(string text)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = new byte[16]; // Initialize vector (IV)
                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] encrypted = encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                    return Convert.ToBase64String(encrypted);
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = new byte[16];
                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] decrypted = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decrypted);
                }
            }
        }
    }
}
