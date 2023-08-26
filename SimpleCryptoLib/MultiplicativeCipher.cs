namespace SimpleCryptoLib
{
    public class MultiplicativeCipher
    {
        private const int MODULUS = 26;
        private const int KEY = 3; 
        private const int INVERSE_KEY = 9;  

        public string Encrypt(string plaintext)
        {
            string result = "";

            foreach (char letter in plaintext.ToUpper())
            {
                if (Char.IsLetter(letter))
                {
                    int x = letter - 'A';
                    int encryptedCharValue = (x * KEY) % MODULUS;
                    result += (char)(encryptedCharValue + 'A');
                }
                else
                {
                    result += letter;
                }
            }

            return result;
        }

        public string Decrypt(string ciphertext)
        {
            string result = "";

            foreach (char letter in ciphertext.ToUpper())
            {
                if (Char.IsLetter(letter))
                {
                    int x = letter - 'A';
                    int decryptedCharValue = (x * INVERSE_KEY) % MODULUS;
                    result += (char)(decryptedCharValue + 'A');
                }
                else
                {
                    result += letter;
                }
            }

            return result;
        }
    }
}

