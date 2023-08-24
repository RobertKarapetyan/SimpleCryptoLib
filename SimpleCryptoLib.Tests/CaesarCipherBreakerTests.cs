using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCryptoLib;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class CaesarCipherBreakerTests
    {
        [TestMethod]
        public void TestCipherBreaker()
        {
            var cipherService = new CaesarCipherService();
            var breaker = new CaesarCipherBreaker();

            string plaintext = "Hello, World!";
            int key = 3; 

            string encryptedText = cipherService.Encrypt(key, plaintext);

            var decryptedText = breaker.Break(encryptedText);

            Assert.AreEqual(plaintext, decryptedText, "The breaker should be able to decrypt the text.");
        }
    }
}
