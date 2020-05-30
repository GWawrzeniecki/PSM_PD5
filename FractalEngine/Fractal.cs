using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FractalEngine.Extensions;
using FractalEngine.Models;

namespace FractalEngine
{
    public class Fractal
    {
        private const string FirstRule = "F+[[X]-X]-F[-FX]+X";
        private const string FirstRuleReplaceChar = "X";
        private const string SecondRule = "FF";
        private const string SecondRuleChar = "F";
        private readonly double _angleOfRotation;
        private double _alfaOnStart;
        private double _xOnStart;
        private double _yOnStart;
        private Stack<Cordinate> _stack;
        private List<Cordinate> _cordinates;
        private Dictionary<char, Func<Cordinate, Cordinate>> _operations;
        private const char ChangePlusAngleOperation = '+';
        private const char ChangeMinusAngleOperation = '-';
        private const char PushStackOperation = '[';
        private const char PopStackOperation = ']';
        private const char GoForwardOperation = 'F';
        private const char AuxiliarySymbol = 'X';
        private string _pattern;


        public Fractal(double angleOfRotation, double alfaOnStart)
        {

            _angleOfRotation = angleOfRotation.ToRadians();
            _alfaOnStart = alfaOnStart;
            _xOnStart = 0.0;
            _yOnStart = 0.0;
            _stack = new Stack<Cordinate>();
            _cordinates = new List<Cordinate>();
            _operations = new Dictionary<char, Func<Cordinate, Cordinate>>();
            LoadOperations();
            _cordinates.Add(new Cordinate(_xOnStart, _yOnStart, _alfaOnStart));
        }

        private void LoadOperations()
        {
            _operations.Add(ChangePlusAngleOperation, new Func<Cordinate, Cordinate>((Cordinate arg) =>
            {
                arg.a -= _angleOfRotation;
                return arg;
            }));

            _operations.Add(ChangeMinusAngleOperation, new Func<Cordinate, Cordinate>((Cordinate arg) =>
            {
                arg.a += _angleOfRotation;
                return arg;
            }));

            _operations.Add(PushStackOperation, new Func<Cordinate, Cordinate>((Cordinate arg) =>
            {
                _stack.Push(arg);
                return arg;
            }));

            _operations.Add(PopStackOperation, new Func<Cordinate, Cordinate>((Cordinate arg) =>
            {
                return _stack.Pop();
            }));

            _operations.Add(GoForwardOperation, new Func<Cordinate, Cordinate>((Cordinate arg) =>
            {
                arg.x += Math.Cos(arg.a);
                arg.y += Math.Sin(arg.a);
                return arg;
            }));
        }

        public void GeneratePattern(int repeatAmount, string firstWord)
        {
            if (repeatAmount < 1)
                throw new ArgumentException(nameof(repeatAmount));

            FirstReplace(firstWord, out _pattern);

            for (int i = 1; i < repeatAmount; i++)
            {
                _pattern = _pattern.Replace(SecondRuleChar, SecondRule);
                _pattern = _pattern.Replace(FirstRuleReplaceChar, FirstRule);
            }
            Console.WriteLine(_pattern);
            _pattern = _pattern.Replace(AuxiliarySymbol.ToString(), "");
        }

        private void FirstReplace(string firstWord, out string pattern)
        {
            pattern = firstWord.Replace(FirstRuleReplaceChar, FirstRule);
        }



        public void GenerateCordinates()
        {
            _ = _pattern ?? throw new Exception("Generate patttern at first");

            var operations = _pattern.ToCharArray();

            foreach (var operation in operations)
            {
                var cord = _operations[operation](_cordinates[_cordinates.Count - 1]);

                if (!operation.Equals(PushStackOperation))

                    _cordinates.Add(cord);
            }
        }

        public void SaveCordinatesToFiles(string xPath, string yPath)
        {
            StringBuilder xSB = new StringBuilder();
            StringBuilder ySB = new StringBuilder();
            foreach (var cordinate in _cordinates)
            {
                xSB.AppendLine(cordinate.x.ToString().Replace('.', ','));
                ySB.AppendLine(cordinate.y.ToString().Replace('.', ','));
            }
            try
            {
                File.WriteAllText(xPath, xSB.ToString());
                File.WriteAllText(yPath, ySB.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not write cordinates to files. {Environment.NewLine}Error: {e.Message}");
            }
        }


    }
}
