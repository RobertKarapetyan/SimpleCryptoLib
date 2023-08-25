using System.Text;

public static class OneTimePadAttack
{
    public static string RevealMessageUsingPredictableMessage(byte[] predictableCiphertext, byte[] targetCiphertext, string knownMessage)
    {
        byte[] knownMessageBytes = Encoding.UTF8.GetBytes(knownMessage);
        byte[] decryptedXOR = predictableCiphertext.Zip(targetCiphertext, (c1, c2) => (byte)(c1 ^ c2)).ToArray();
        return Encoding.UTF8.GetString(decryptedXOR.Zip(knownMessageBytes, (x, k) => (byte)(x ^ k)).ToArray());
    }
}
