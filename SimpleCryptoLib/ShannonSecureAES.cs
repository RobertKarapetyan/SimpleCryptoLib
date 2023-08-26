using System.Security.Cryptography;
using System.Text;

namespace SimpleCryptoLib
{
    public class ShannonSecureAES
    {
        private static readonly int keySize = 256; 
        private static readonly int blockSize = 128; 

        public byte[] Encrypt(string plainText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Key = GetKeyBytes(key);
                aes.IV = new byte[blockSize / 8]; 

                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        public string Decrypt(byte[] cipherBytes, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Key = GetKeyBytes(key);
                aes.IV = new byte[blockSize / 8];

                using (MemoryStream memoryStream = new MemoryStream(cipherBytes))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private byte[] GetKeyBytes(string key)
        {
            if (key.Length > 32)
                return Encoding.UTF8.GetBytes(key.Substring(0, 32));
            else
                return Encoding.UTF8.GetBytes(key.PadRight(32, '\0'));
        }
    }
}