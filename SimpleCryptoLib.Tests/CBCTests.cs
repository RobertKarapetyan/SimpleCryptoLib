using System.Text;

namespace SimpleCryptoLib.Tests
{	
    [TestClass]
    public class CBCTests
    {
        [TestMethod]
        public void TestEncryptionDecryption()
        {
            var cbc = new CBC();

            byte[] key = new byte[16];
            byte[] iv = new byte[16];
            new Random().NextBytes(key);
            new Random().NextBytes(iv);

            string originalText = "Hello, CBC!";
            byte[] originalData = Encoding.UTF8.GetBytes(originalText);

            byte[] encryptedData = cbc.Encrypt(originalData, key, iv);
            byte[] decryptedData = cbc.Decrypt(encryptedData, key, iv);

            string decryptedText = Encoding.UTF8.GetString(decryptedData);

            Assert.AreEqual(originalText, decryptedText);
        }
    }
}

