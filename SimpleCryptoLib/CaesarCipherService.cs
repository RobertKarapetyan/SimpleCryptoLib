namespace SimpleCryptoLib
{
    public class CaesarCipherService
    {
        public int GenerateKey()
        {
            var random = new Random();
            return random.Next(1, 25); 
        }

        public string Encrypt(int key, string plaintext)
        {
            return new string(plaintext.Select(c => ShiftChar(c, key)).ToArray());
        }

        public string Decrypt(int key, string encryptedText)
        {
            return new string(encryptedText.Select(c => ShiftChar(c, -key)).ToArray());
        }

        private char ShiftChar(char c, int shift)
        {
            if (!char.IsLetter(c)) return c;

            char offset = char.IsUpper(c) ? 'A' : 'a';
            return (char)((((c - offset) + shift + 26) % 26) + offset);
        }
    }
}
