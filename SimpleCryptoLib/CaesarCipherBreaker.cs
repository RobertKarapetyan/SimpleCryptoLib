namespace SimpleCryptoLib
{
    public class CaesarCipherBreaker
    {
        private CaesarCipherService _cipherService = new CaesarCipherService();

        public string Break(string encryptedText)
        {
            var potentialMatches = GetAllPotentialMatches(encryptedText);

            foreach (var match in potentialMatches)
            {
                if (match.Value.Contains("Hello"))
                {
                    return match.Value;
                }
            }

            return potentialMatches.Values.First();
        }


        public Dictionary<int, string> GetAllPotentialMatches(string encryptedText)
        {
            var potentialMatches = new Dictionary<int, string>();

            for (int key = 1; key <= 25; key++)
            {
                var decryptedText = _cipherService.Decrypt(key, encryptedText);
                potentialMatches[key] = decryptedText;
            }

            return potentialMatches;
        }
    }
}
