using System.Text;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class CTRTests
    {
        [TestMethod]
        public void TestEncryptionDecryption()
        {
            var ctr = new CTR();

            byte[] key = new byte[16];
            byte[] counter = new byte[16];
            byte[] originalCounter = new byte[16]; 
            new Random().NextBytes(key);
            new Random().NextBytes(counter);
            Array.Copy(counter, originalCounter, counter.Length); 

            string originalText = "Hello, CTR!";
            byte[] originalData = Encoding.UTF8.GetBytes(originalText);

            byte[] encryptedData = ctr.Encrypt(originalData, key, counter);

            Array.Copy(originalCounter, counter, counter.Length);

            byte[] decryptedData = ctr.Encrypt(encryptedData, key, counter); 

            string decryptedText = Encoding.UTF8.GetString(decryptedData);

            Assert.AreEqual(originalText, decryptedText);
        }

    }
}
