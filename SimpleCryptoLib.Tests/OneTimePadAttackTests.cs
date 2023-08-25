using System.Text;

[TestClass]
public class OneTimePadAttackTests
{
    [TestMethod]
    public void TestRevealMessageAttack()
    {
        string predictableMessage = "STATUS: ALL GOOD.";
        string confidentialMessage = "SECRET: LAUNCH 12PM";

        // In a real-world scenario, the key should NEVER be reused.
        // But for the sake of this demonstration, we're doing just that.
        byte[] key = OneTimePad.GenerateRandomKey(Encoding.UTF8.GetBytes(predictableMessage).Length);

        byte[] predictableCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(predictableMessage), key);
        byte[] targetCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(confidentialMessage), key);

        string revealedMessage = OneTimePadAttack.RevealMessageUsingPredictableMessage(predictableCiphertext, targetCiphertext, predictableMessage);

        Assert.AreEqual(confidentialMessage, revealedMessage);
    }
}
