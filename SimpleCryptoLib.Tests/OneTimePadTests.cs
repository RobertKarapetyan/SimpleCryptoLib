using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class OneTimePadTests
    {
        [TestMethod]
        public void TestEncryptionAndDecryption()
        {
            string plaintext = "Hello, World!";
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            byte[] key = OneTimePad.GenerateRandomKey(plaintextBytes.Length);
            byte[] encrypted = OneTimePad.Encrypt(plaintextBytes, key);
            byte[] decrypted = OneTimePad.Decrypt(encrypted, key);

            string decryptedString = Encoding.UTF8.GetString(decrypted);

            Assert.AreEqual(plaintext, decryptedString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMismatchedKeyLength()
        {
            string plaintext = "Hello, World!";
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

            byte[] shortKey = OneTimePad.GenerateRandomKey(5);
            OneTimePad.Encrypt(plaintextBytes, shortKey);
        }
    }
}
