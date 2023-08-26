namespace SimpleCryptoLib
{
    public class AffineCipher
    {
        private const int m = 26;

        public static string Encrypt(string input, int a, int b)
        {
            if (GCD(a, m) != 1)
            {
                throw new ArgumentException("Key 'a' must be coprime with " + m);
            }

            return new string(input.Select(ch =>
            {
                if (char.IsLetter(ch))
                {
                    char offset = char.IsUpper(ch) ? 'A' : 'a';
                    return (char)(((a * (ch - offset) + b) % m) + offset);
                }
                return ch;
            }).ToArray());
        }

        public static string Decrypt(string input, int a, int b)
        {
            if (GCD(a, m) != 1)
            {
                throw new ArgumentException("Key 'a' must be coprime with " + m);
            }

            int a_inv = ModInverse(a, m);
            return new string(input.Select(ch =>
            {
                if (char.IsLetter(ch))
                {
                    char offset = char.IsUpper(ch) ? 'A' : 'a';
                    return (char)(((a_inv * (ch - offset - b + m)) % m) + offset);
                }
                return ch;
            }).ToArray());
        }

        private static int GCD(int a, int b)
        {
            if (b == 0)
                return a;
            return GCD(b, a % b);
        }

        private static int ModInverse(int a, int m)
        {
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                    return x;
            }
            throw new ArgumentException($"Inverse for {a} under mod {m} does not exist.");
        }
    }

}

