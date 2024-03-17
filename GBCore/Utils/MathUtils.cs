using System;

namespace GBCore.Utils
{
    public static class MathUtils
    {
        public static double Round(this double nr, int digits)
        {
            return Math.Round(nr, digits);
        }
    }
}
