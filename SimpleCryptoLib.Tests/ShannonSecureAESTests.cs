using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class ShannonSecureAESTests
    {
        private ShannonSecureAES _aesHelper;
        private string _testKey;

        [TestInitialize]
        public void Setup()
        {
            _aesHelper = new ShannonSecureAES();
            _testKey = "SuperSecretTestKey1234SuperSecretTestKey1234"; 
        }

        [TestMethod]
        public void TestDecryptWithWrongKeyFails()
        {
            string plainText = "Hello, Shannon!";

            byte[] cipherText = _aesHelper.Encrypt(plainText, _testKey);
            Assert.IsNotNull(cipherText);

            string wrongKey = "WrongKey1234WrongKey1234WrongKe"; 

            Assert.ThrowsException<CryptographicException>(() => _aesHelper.Decrypt(cipherText, wrongKey));
        }


        [TestMethod]
        public void TestEncryptionDecryptionCycle()
        {
            string plainText = "Hello, Shannon!";

            byte[] cipherText = _aesHelper.Encrypt(plainText, _testKey);
            Assert.IsNotNull(cipherText);

            string decryptedText = _aesHelper.Decrypt(cipherText, _testKey);
            Assert.AreEqual(plainText, decryptedText);
        }
    }

}