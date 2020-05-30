using System;
using System.Collections.Generic;
using FractalEngine.Models;

namespace FractalEngine
{
    public class Fractal
    {
        private const string FirstRule = "F+[[X]-X]-F[-FX]+X";
        private const string FirstRuleReplaceChar = "X";
        private const string SecondRule = "FF";
        private const string SecondRuleChar = "F";
        private readonly int _angleOfRotation;
        private double _alfaOnStart;
        private int _xOnStart;
        private int _yOnStart;
        private Stack<int> _stack;
        private List<Cordinate> _cordinates;
        private Dictionary<char, Func<Cordinate, Cordinate>> _operations;
        private const char ChangePlusAngleOperation = '+';
        private const char ChangeMinusAngleOperation = '-';
        private const char PushStackOperation = '[';
        private const char PopStackOperation = ']';
        private const char GoForwardOperation = 'F';


        public Fractal(int angleOfRotation, int alfaOnStart)
        {
            _angleOfRotation = angleOfRotation;
            _alfaOnStart = alfaOnStart;
            _xOnStart = 0;
            _yOnStart = 0;
            _stack = new Stack<int>();
            _cordinates = new List<Cordinate>();
            _operations = new Dictionary<char, Func<Cordinate, Cordinate>>();

        }

        private void LoadOperations()
        {
            _operations.Add(ChangePlusAngleOperation, )
        }

        public void GeneratePattern(int repeatAmount, string firstWord)
        {
            if (repeatAmount < 1)
                throw new ArgumentException(nameof(repeatAmount));

            FirstReplace(firstWord, out string pattern);
            for (int i = 1; i < repeatAmount; i++)
            {
                pattern = pattern.Replace(SecondRuleChar, SecondRule);
                pattern = pattern.Replace(FirstRuleReplaceChar, FirstRule);
            }

            Console.WriteLine(pattern);
        }

        private void FirstReplace(string firstWord, out string pattern)
        {
            pattern = firstWord.Replace(SecondRuleChar, SecondRule);
            pattern = pattern.Replace(FirstRuleReplaceChar, FirstRule);
        }

        public void GenerateCordinates()
        {

        }


    }
}
