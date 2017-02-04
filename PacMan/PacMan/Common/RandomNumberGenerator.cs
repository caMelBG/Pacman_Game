namespace PacMan.Logic
{
    using System;
    using System.Security.Cryptography;

    public static class RandomNumberGenerator
    {
        public static int Next(int number)
        {
            short value = 0;
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[number];
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                    value = BitConverter.ToInt16(data, 0);
                }
            }

            var result = value % number;
            if (result < 0)
            {
                result *= -1;
            }

            return result;
        }
    }
}