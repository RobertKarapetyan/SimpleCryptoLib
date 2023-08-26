namespace SimpleCryptoLib
{
    public class AffineCipherBreaker
    {
        public static (int a, int b, string plaintext)? BruteForceDecrypt(string ciphertext)
        {
            int[] validAValues = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };
            for (int i = 0; i < validAValues.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    string decrypted = AffineCipher.Decrypt(ciphertext, validAValues[i], j);
                    if (IsValidPlaintext(decrypted)) 
                    {
                        return (validAValues[i], j, decrypted);
                    }
                }
            }
            return null;
        }

        public static List<(int a, int b, string plaintext)> AllPossibleDecrypts(string ciphertext)
        {
            List<(int a, int b, string plaintext)> results = new List<(int a, int b, string plaintext)>();

            int[] validAValues = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };
            for (int i = 0; i < validAValues.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    string decrypted = AffineCipher.Decrypt(ciphertext, validAValues[i], j);
                    results.Add((validAValues[i], j, decrypted));
                }
            }
            return results;
        }

        private static bool IsValidPlaintext(string text)
        {
            string[] commonWords = { "THE", "AND", "FOR", "ARE", "BUT", "NOT", "YOU", "ALL" };
            return commonWords.Any(word => text.Contains(word));
        }
    }
}