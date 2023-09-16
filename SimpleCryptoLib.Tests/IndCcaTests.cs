using System;
using System.Security.Cryptography;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
	public class IndCcaTests
	{
        [TestMethod]
        public void Test_CBC_IND_CCA_Attack()
        {
            int blockSize = 16;
            byte[] key = new byte[blockSize];
            new Random().NextBytes(key);

            byte[] m0 = new byte[2 * blockSize];
            byte[] m1 = new byte[2 * blockSize];
            Array.Fill<byte>(m1, 1);

            Random rnd = new Random();
            int b = rnd.Next(2);

            byte[] ciphertext = Encrypt_CBC(key, b == 0 ? m0 : m1);
            byte[] trimmedCiphertext = new byte[ciphertext.Length - blockSize];
            Array.Copy(ciphertext, trimmedCiphertext, trimmedCiphertext.Length);

            byte[] decrypted = Decrypt_CBC(key, trimmedCiphertext);
            int guessedB = IsAllZeroes(decrypted) ? 0 : 1;

            Assert.AreEqual(b, guessedB);
        }

        private static bool IsAllZeroes(byte[] arr)
        {
            foreach (byte b in arr)
            {
                if (b != 0)
                    return false;
            }
            return true;
        }

        public static byte[] Encrypt_CBC(byte[] key, byte[] plaintext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.None;

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    byte[] encrypted = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
                    byte[] result = new byte[aesAlg.IV.Length + encrypted.Length];
                    Array.Copy(aesAlg.IV, result, aesAlg.IV.Length);
                    Array.Copy(encrypted, 0, result, aesAlg.IV.Length, encrypted.Length);
                    return result;
                }
            }
        }

        public static byte[] Decrypt_CBC(byte[] key, byte[] ciphertext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.None;

                byte[] iv = new byte[16];
                Array.Copy(ciphertext, iv, 16);

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv))
                {
                    return decryptor.TransformFinalBlock(ciphertext, 16, ciphertext.Length - 16);
                }
            }
        }
    }
}

