namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class AffineCipherTests
    {
        [TestMethod]
        public void TestEncryption()
        {
            string plaintext = "HELLO";
            int a = 5, b = 8; 
            string encrypted = AffineCipher.Encrypt(plaintext, a, b);
            Assert.AreNotEqual(plaintext, encrypted);
        }

        [TestMethod]
        public void TestDecryption()
        {
            string plaintext = "HELLO";
            int a = 5, b = 8;
            string encrypted = AffineCipher.Encrypt(plaintext, a, b);
            string decrypted = AffineCipher.Decrypt(encrypted, a, b);
            Assert.AreEqual(plaintext, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidKey()
        {
            string plaintext = "HELLO";
            int a = 4, b = 8; 
            AffineCipher.Encrypt(plaintext, a, b);
        }
    }

}