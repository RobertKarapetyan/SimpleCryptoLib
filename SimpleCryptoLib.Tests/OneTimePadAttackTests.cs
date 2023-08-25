using System.Text;

namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class OneTimePadAttackTests
    {
        [TestMethod]
        public void TestRevealMessageAttack()
        {
            string predictableMessage = "STATUS";
            string confidentialMessage = "SECRET";

            byte[] key = OneTimePad.GenerateRandomKey(Encoding.UTF8.GetBytes(predictableMessage).Length);

            byte[] predictableCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(predictableMessage), key);
            byte[] targetCiphertext = OneTimePad.Encrypt(Encoding.UTF8.GetBytes(confidentialMessage), key);

            string revealedMessage = OneTimePadAttack.RevealMessageUsingPredictableMessage(predictableCiphertext, targetCiphertext, predictableMessage);

            Assert.AreEqual(confidentialMessage, revealedMessage);
        }
    }
}
