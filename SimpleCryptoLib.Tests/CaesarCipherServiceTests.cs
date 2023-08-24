namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class CaesarCipherServiceTests
    {
        [TestMethod]
        public void TestEncryptionDecryption()
        {
            var cipherService = new CaesarCipherService();

            var key = cipherService.GenerateKey();

            var plaintext = "Hello, World!";
            var encryptedText = cipherService.Encrypt(key, plaintext);
            Assert.AreNotEqual(plaintext, encryptedText, "Encrypted text should be different from plaintext.");

            var decryptedText = cipherService.Decrypt(key, encryptedText);
            Assert.AreEqual(plaintext, decryptedText, "Decrypted text should be the same as the original plaintext.");
        }
    }
}
