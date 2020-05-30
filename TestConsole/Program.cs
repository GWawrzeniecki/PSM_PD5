using System;

namespace TestConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
           
            FractalEngine.Fractal fractal = new FractalEngine.Fractal(25,25);
            fractal.GeneratePattern(8,"X");
            fractal.GenerateCordinates();
            fractal.SaveCordinatesToFiles("x.txt", "y.txt");

        }
    }
}
