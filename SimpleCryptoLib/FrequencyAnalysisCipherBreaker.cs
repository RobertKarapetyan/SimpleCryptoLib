using System;
namespace SimpleCryptoLib
{
    public class FrequencyAnalysisCipherBreaker
    {
        private static readonly string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly List<string> CommonWords = new List<string> { "THE", "AND", "FOR", "YOU", "WITH", "HAVE", "THIS", "FROM", "THEY", "THAT" };

        private static readonly Dictionary<char, double> EnglishLetterFrequency = new Dictionary<char, double>
        {
            {'e', 0.101}, {'t', 0.093}, {'a', 0.082}, {'o', 0.075}, {'i', 0.070}, {'n', 0.067},
            {'s', 0.063}, {'h', 0.061}, {'r', 0.060}, {'d', 0.043}, {'l', 0.040}, {'c', 0.028},
            {'u', 0.028}, {'m', 0.025}, {'f', 0.023}, {'p', 0.021}, {'g', 0.020}, {'w', 0.019},
            {'b', 0.015}, {'y', 0.015}, {'v', 0.010}, {'k', 0.008}, {'j', 0.002}, {'x', 0.0015},
            {'q', 0.001}, {'z', 0.0007},

            {'E', 0.0505}, {'T', 0.0465}, {'A', 0.041}, {'O', 0.0375}, {'I', 0.035}, {'N', 0.0335},
            {'S', 0.0315}, {'H', 0.0305}, {'R', 0.030}, {'D', 0.0215}, {'L', 0.020}, {'C', 0.014},
            {'U', 0.014}, {'M', 0.0125}, {'F', 0.0115}, {'P', 0.0105}, {'G', 0.01}, {'W', 0.0095},
            {'B', 0.0075}, {'Y', 0.0075}, {'V', 0.005}, {'K', 0.004}, {'J', 0.001}, {'X', 0.00075},
            {'Q', 0.0005}, {'Z', 0.00035},

            {'0', 0.005}, {'1', 0.005}, {'2', 0.005}, {'3', 0.005}, {'4', 0.005},
            {'5', 0.005}, {'6', 0.005}, {'7', 0.005}, {'8', 0.005}, {'9', 0.005}
        };

        public string AnalyzeFrequency(string encryptedText)
        {
            var singleLetterCipherFrequency = GetFrequency(encryptedText, 1);
            var estimatedMapping = MapByFrequency(singleLetterCipherFrequency.ToDictionary(kvp => kvp.Key[0], kvp => kvp.Value), EnglishLetterFrequency);

            return string.Concat(encryptedText.Select(c => estimatedMapping.ContainsKey(c) ? estimatedMapping[c] : c));
        }

        private Dictionary<string, double> GetFrequency(string text, int length)
        {
            Dictionary<string, double> frequencies = new Dictionary<string, double>();
            double total = 0;

            for (int i = 0; i <= text.Length - length; i++)
            {
                var substring = text.Substring(i, length);
                if (!frequencies.ContainsKey(substring))
                {
                    frequencies[substring] = 0;
                }
                frequencies[substring]++;
                total++;
            }

            // Normalize the frequencies
            var keys = frequencies.Keys.ToList();
            foreach (var key in keys)
            {
                frequencies[key] /= total;
            }

            return frequencies;
        }

        private Dictionary<char, char> MapByFrequency(Dictionary<char, double> cipherFrequencies, Dictionary<char, double> referenceFrequencies)
        {
            var sortedCipherFrequencies = cipherFrequencies.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).ToList();
            var sortedReferenceFrequencies = referenceFrequencies.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).ToList();

            Dictionary<char, char> mapping = new Dictionary<char, char>();
            int limit = Math.Min(sortedCipherFrequencies.Count, sortedReferenceFrequencies.Count);
            for (int i = 0; i < limit; i++)
            {
                mapping[sortedCipherFrequencies[i]] = sortedReferenceFrequencies[i];
            }

            return mapping;
        }

    }
}