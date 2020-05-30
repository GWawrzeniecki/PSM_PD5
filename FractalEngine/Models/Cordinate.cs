using System;
namespace FractalEngine.Models
{
    public struct Cordinate
    {
        public double x;
        public double y;
        public double a;

        public Cordinate(double x, double y, double a)
        {
            this.x = x;
            this.y = y;
            this.a = a;
        }
    }
}
