namespace SimpleCryptoLib
{
    public class RandomSubstitutionCipherBreaker
    {
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly string[] CommonWords = { "the", "and", "for", "you", "with", "have", "this", "from", "they", "that", "hello", "world" };

        public string RandomGuess(string encryptedText, int attempts)
        {
            for (int i = 0; i < attempts; i++)
            {
                string randomKey = new string(Characters.ToCharArray().OrderBy(s => Guid.NewGuid()).ToArray());
                RandomSubstitutionCipher cipher = new RandomSubstitutionCipher(randomKey);

                string decryptedText = cipher.Decrypt(encryptedText);

                if (CommonWords.Any(word => decryptedText.Contains(word)))
                    return randomKey; 
            }

            return null; 
        }

        public Dictionary<string, string> GuessUsingCommonWords(string encryptedText)
        {
            Dictionary<string, string> potentialMatches = new Dictionary<string, string>();

            foreach (var word in CommonWords)
            {
                foreach (var segment in encryptedText.Split(' ')) 
                {
                    if (segment.Length == word.Length)
                    {
                        string potentialKeySegment = "";
                        for (int i = 0; i < word.Length; i++)
                        {
                            int index = Characters.IndexOf(word[i]);
                            if (index != -1)
                            {
                                potentialKeySegment += segment[i];
                            }
                        }

                        potentialMatches[word] = potentialKeySegment;
                    }
                }
            }

            return potentialMatches; 
        }
    }
}

