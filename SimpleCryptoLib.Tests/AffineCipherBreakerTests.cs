namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class AffineCipherBreakerTests
    {
        [TestMethod]
        public void TestAllPossibleDecrypts()
        {
            string originalPlaintext = "HELLOTHISISASECRETMESSAGE";
            int a = 5, b = 8; 
            string encrypted = AffineCipher.Encrypt(originalPlaintext, a, b);

            var results = AffineCipherBreaker.AllPossibleDecrypts(encrypted);
            foreach (var result in results)
            {
                Console.WriteLine($"Using a={result.a} and b={result.b}: {result.plaintext}");
            }

            var successfulDecrypt = results.FirstOrDefault(r => r.plaintext == originalPlaintext);

            Assert.IsNotNull(successfulDecrypt);
        }
    }
}