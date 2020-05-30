using System;
namespace FractalEngine.Extensions
{
    public static class DoubleExtension
    {
       
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
