using System;
namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class MultiplicativeCipherTests
    {
        [TestMethod]
        public void TestEncryption()
        {
            MultiplicativeCipher cipher = new MultiplicativeCipher();

            string plaintext = "HELLO";
            string expectedCiphertext = "VMHHQ";
            string actualCiphertext = cipher.Encrypt(plaintext);

            Assert.AreEqual(expectedCiphertext, actualCiphertext);
        }

        [TestMethod]
        public void TestDecryption()
        {
            MultiplicativeCipher cipher = new MultiplicativeCipher();

            string ciphertext = "VMHHQ";
            string expectedPlaintext = "HELLO";
            string actualPlaintext = cipher.Decrypt(ciphertext);

            Assert.AreEqual(expectedPlaintext, actualPlaintext);
        }
    }
}

