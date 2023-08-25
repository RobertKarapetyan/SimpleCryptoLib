using System;
using System.Linq;

public static class OneTimePad
{
    public static byte[] Encrypt(byte[] plaintext, byte[] key)
    {
        if (plaintext.Length != key.Length)
            throw new ArgumentException("Key length must be equal to plaintext length");

        return plaintext.Zip(key, (p, k) => (byte)(p ^ k)).ToArray();
    }

    public static byte[] Decrypt(byte[] ciphertext, byte[] key)
    {
        return Encrypt(ciphertext, key);
    }

    public static byte[] GenerateRandomKey(int length)
    {
        var random = new Random();
        byte[] key = new byte[length];
        random.NextBytes(key);
        return key;
    }
}
