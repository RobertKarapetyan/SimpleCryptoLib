namespace SimpleCryptoLib
{
    public static class ModularCalculator
    {
        public static long ModExp(long baseValue, long exp, long mod)
        {
            if (mod == 0)
                throw new ArgumentException("The modulus (mod) cannot be zero.");

            long result = 1;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result = ReduceByMod(result * baseValue, mod);

                baseValue = ReduceByMod(baseValue * baseValue, mod);
                exp /= 2;
            }

            return result;
        }

        private static long ReduceByMod(long value, long mod)
        {
            while (value >= mod)
            {
                value -= mod;
            }

            return value;
        }
    }
}

