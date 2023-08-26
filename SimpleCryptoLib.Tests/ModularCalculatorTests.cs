namespace SimpleCryptoLib.Tests
{
    [TestClass]
    public class ModularCalculatorTests
    {
        [TestMethod]
        public void TestModExp_3Power8_Mod7()
        {
            long result = ModularCalculator.ModExp(3, 8, 7);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void TestModExp_2Power10_Mod8()
        {
            long result = ModularCalculator.ModExp(2, 10, 8);
            Assert.AreEqual(0, result); // As 2^10 = 1024, and 1024 mod 8 = 0
        }

        [TestMethod]
        public void TestModExp_4Power3_Mod9()
        {
            long result = ModularCalculator.ModExp(4, 3, 9);
            Assert.AreEqual(1, result); // As 4^3 = 64, and 64 mod 9 = 1
        }

        [TestMethod]
        public void TestModExp_10Power0_Mod11()
        {
            long result = ModularCalculator.ModExp(10, 0, 11);
            Assert.AreEqual(1, result); // Anything raised to the power 0 is 1.
        }

        [TestMethod]
        public void TestModExp_5Power3_Mod5()
        {
            long result = ModularCalculator.ModExp(5, 3, 5);
            Assert.AreEqual(0, result); // As 5^3 = 125, and 125 mod 5 = 0
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestModExp_InvalidModulus_ThrowsException()
        {
            ModularCalculator.ModExp(5, 3, 0);
        }
    }
}

