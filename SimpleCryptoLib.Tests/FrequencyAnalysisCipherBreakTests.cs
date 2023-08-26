using System;
namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class FrequencyAnalysisCipherBreakTests
    {
        private RandomSubstitutionCipher _cipher;
        private const string Key = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [TestInitialize]
        public void Setup()
        {
            _cipher = new RandomSubstitutionCipher(Key);
        }

        [TestMethod]
        public void TestFrequencyAnalysisCipherBreaker()
        {
            string originalText = "The quick brown Fox, jumping over 123 lazy dogs in 2023, was seen by all! And indeed, " +
                "the sight was surprising to many. For Mr. Fox had never done such a thing before.";

            var encryptedText = _cipher.Encrypt(originalText);
            var breaker = new FrequencyAnalysisCipherBreaker();

            var decryptedText = breaker.AnalyzeFrequency(encryptedText);

            double matchingCharacters = originalText.Where((t, i) => i < decryptedText.Length && t == decryptedText[i]).Count();
            double accuracy = matchingCharacters / originalText.Length;

            Assert.IsTrue(accuracy >= 0.1, $"Expected accuracy >= 10% but got {accuracy:P2}");
        }
    }
}