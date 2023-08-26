using System;
namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class RandomSubstitutionCipherTests
    {
        private RandomSubstitutionCipher _cipher;


        [TestInitialize]
        public void Setup()
        {
            string key = "PjHFwuyQGc2Ktd5bzVa6r9vRT3Ii7D0s4WX8J1CkYUZBNmLlofxhAeOMngSEp|";
            _cipher = new RandomSubstitutionCipher(key);
        }

        [TestMethod]
        public void TestEncryption()
        {
            string input = "Hello123";
            string encrypted = _cipher.Encrypt(input);

            Assert.AreNotEqual(input, encrypted);
        }

        [TestMethod]
        public void TestDecryption()
        {
            string input = "Hello123";
            string encrypted = _cipher.Encrypt(input);
            string decrypted = _cipher.Decrypt(encrypted);

            Assert.AreEqual(input, decrypted);
        }

        [TestMethod]
        public void TestSpecialCharactersUnchanged()
        {
            string input = "Hello, World!";
            string encrypted = _cipher.Encrypt(input);
            string decrypted = _cipher.Decrypt(encrypted);

            Assert.IsTrue(encrypted.Contains(","));
            Assert.IsTrue(encrypted.Contains("!"));
            Assert.AreEqual(input, decrypted);
        }
    }
}