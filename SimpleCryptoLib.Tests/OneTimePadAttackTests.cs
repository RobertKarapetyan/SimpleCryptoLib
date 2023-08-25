using System.Text;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class OneTimePadAttackTests
    {
        [TestMethod]
        public void TestSamePredictableAndTargetMessage()
        {
            string predictableMessage = "STATUS: OK.";
            string targetMessage = "STATUS: OK.";

            byte[] key = OneTimePad.GenerateRandomKey(Encoding.UTF8.GetBytes(predictableMessage).Length);

            byte[] predictableCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(predictableMessage), key);
            byte[] targetCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(targetMessage), key);

            string revealedMessage = OneTimePadAttack.RevealMessageUsingPredictableMessage(predictableCiphertext, targetCiphertext, predictableMessage);

            Assert.AreEqual(predictableMessage, revealedMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDifferentLengths()
        {
            string predictableMessage = "STATUS: OK.";
            string targetMessage = "STATUS: NOT OK.";

            byte[] key = OneTimePad.GenerateRandomKey(Encoding.UTF8.GetBytes(predictableMessage).Length);

            byte[] predictableCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(predictableMessage), key);
            byte[] targetCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(targetMessage), key);

            OneTimePadAttack.RevealMessageUsingPredictableMessage(predictableCiphertext, targetCiphertext, predictableMessage);
        }

        [TestMethod]
        public void TestDifferentMessages()
        {
            string predictableMessage = "STATUS: OK.";
            string targetMessage = "SECRET DATA.";

            int maxLength = Math.Max(predictableMessage.Length, targetMessage.Length);

            byte[] key = OneTimePad.GenerateRandomKey(maxLength);

            if (predictableMessage.Length < maxLength)
                predictableMessage = predictableMessage.PadRight(maxLength);
            if (targetMessage.Length < maxLength)
                targetMessage = targetMessage.PadRight(maxLength);

            byte[] predictableCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(predictableMessage), key);
            byte[] targetCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(targetMessage), key);

            string revealedMessage = OneTimePadAttack.RevealMessageUsingPredictableMessage(predictableCiphertext, targetCiphertext, predictableMessage);

            Assert.AreEqual(targetMessage, revealedMessage);
        }

    }
}
