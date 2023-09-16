using System;
using System.Security.Cryptography;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
	public class IndCpaTests
	{
        [TestMethod]
        public void TestEcbIndCpa()
        {
            // Plaintext
            var plaintext = "HelloHello"; 
            var key = new byte[16];
            var ciphertext1 = EncryptEcb(plaintext, key);
            var ciphertext2 = EncryptEcb(plaintext, key);

            Assert.AreEqual(ciphertext1, ciphertext2); 
        }

        [TestMethod]
        public void TestCbcIndCpa()
        {
            // Plaintext
            var plaintext = "Hello World";
            var key = new byte[16];

            var rng = new RNGCryptoServiceProvider();
            var iv1 = new byte[16];
            var iv2 = new byte[16];

            // Generate random IVs
            rng.GetBytes(iv1);
            rng.GetBytes(iv2);

            var ciphertext1 = EncryptCbc(plaintext, key, iv1);
            var ciphertext2 = EncryptCbc(plaintext, key, iv2);

            // Check if CBC is IND-CPA secure (it should be, for distinct IVs)
            Assert.AreNotEqual(ciphertext1, ciphertext2);
        }

        public string EncryptEcb(string plaintext, byte[] key)
        {
            using (var aes = new AesManaged())
            {
                aes.Mode = CipherMode.ECB;
                aes.Key = key;
                var encryptor = aes.CreateEncryptor();

                var plaintextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
                var ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

                return Convert.ToBase64String(ciphertextBytes);
            }
        }

        public string EncryptCbc(string plaintext, byte[] key, byte[] iv)
        {
            using (var aes = new AesManaged())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = key;
                aes.IV = iv;
                var encryptor = aes.CreateEncryptor();

                var plaintextBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
                var ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

                return Convert.ToBase64String(ciphertextBytes);
            }
        }
    }
}

