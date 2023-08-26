using System;
using System.Text;

namespace SimpleCryptoLib
{
    public class RandomSubstitutionCipher
    {
        private readonly Dictionary<char, char> _encryptionMap;
        private readonly Dictionary<char, char> _decryptionMap;

        public RandomSubstitutionCipher(string key)
        {
            if (key.Length != 62)
                throw new ArgumentException("Key must have exactly 62 characters without repetition.");

            _encryptionMap = new Dictionary<char, char>();
            _decryptionMap = new Dictionary<char, char>();

            var allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < allChars.Length; i++)
            {
                _encryptionMap[allChars[i]] = key[i];
                _decryptionMap[key[i]] = allChars[i];
            }
        }

        public string Encrypt(string input)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in input)
            {
                if (_encryptionMap.ContainsKey(c))
                {
                    result.Append(_encryptionMap[c]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public string Decrypt(string input)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in input)
            {
                if (_decryptionMap.ContainsKey(c))
                {
                    result.Append(_decryptionMap[c]);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}

