using System.Diagnostics;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class RandomSubstitutionCipherBreakTests
    {
        private RandomSubstitutionCipher _cipher;
        private const string Key = "PjHFwuyQGc2Ktd5bzVa6r9vRT3Ii7D0s4WX8J1CkYUZBNmLlofxhAeOMngSEp|";

        [TestInitialize]
        public void Setup()
        {
            _cipher = new RandomSubstitutionCipher(Key);
        }

        [TestMethod]
        public void TestRandomGuess()
        {
            string input = "Hello and welcome to the world!";
            string encrypted = _cipher.Encrypt(input);

            RandomSubstitutionCipherBreaker breaker = new RandomSubstitutionCipherBreaker();
            string guessedKey = breaker.RandomGuess(encrypted, 10000); 

            if (guessedKey != null)
            {
                RandomSubstitutionCipher guessedCipher = new RandomSubstitutionCipher(guessedKey);
                string decrypted = guessedCipher.Decrypt(encrypted);

                if (decrypted.Contains("hello"))
                {
                    Debug.WriteLine("Successfully guessed the key within 10,000 attempts!");
                }
                else
                {
                    Debug.WriteLine("Guessed a key, but it didn't decrypt correctly.");
                }
            }
            else
            {
                Debug.WriteLine("Couldn't guess the key within 10,000 attempts.");
            }
        }

        [TestMethod]
        public void TestGuessUsingCommonWords()
        {
            string input = "Hello and welcome to the world!";
            string encrypted = _cipher.Encrypt(input);

            RandomSubstitutionCipherBreaker breaker = new RandomSubstitutionCipherBreaker();
            var potentialMatches = breaker.GuessUsingCommonWords(encrypted);

            foreach (var pair in potentialMatches)
            {
                Assert.AreEqual(pair.Key.Length, pair.Value.Length);
            }
        }
    }
}