using System.Security.Cryptography;

namespace SimpleCryptoLib
{
    public class CBC
    {
        private readonly Aes aes;

        public CBC()
        {
            aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            aes.Key = key;
            aes.IV = iv;

            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            aes.Key = key;
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }
}

