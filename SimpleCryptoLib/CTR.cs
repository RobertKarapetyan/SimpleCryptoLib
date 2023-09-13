using System.Security.Cryptography;

namespace SimpleCryptoLib
{
    public class CTR
    {
        private readonly Aes aes;

        public CTR()
        {
            aes = Aes.Create();
            aes.Mode = CipherMode.ECB; 
            aes.Padding = PaddingMode.None;
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] counter)
        {
            if (counter.Length != aes.BlockSize / 8)
                throw new ArgumentException("Counter size must match block size.");

            byte[] output = new byte[data.Length];
            byte[] encryptedCounter = new byte[counter.Length];

            for (int i = 0; i < data.Length; i += counter.Length)
            {
                encryptedCounter = EncryptCounter(counter, key);
                int remaining = Math.Min(counter.Length, data.Length - i);

                for (int j = 0; j < remaining; j++)
                {
                    output[i + j] = (byte)(data[i + j] ^ encryptedCounter[j]);
                }

                IncrementCounter(counter);
            }

            return output;
        }

        private byte[] EncryptCounter(byte[] counter, byte[] key)
        {
            aes.Key = key;
            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(counter, 0, counter.Length);
        }

        private void IncrementCounter(byte[] counter)
        {
            for (int i = counter.Length - 1; i >= 0; i--)
            {
                if (++counter[i] != 0)
                    break;
            }
        }
    }
}

