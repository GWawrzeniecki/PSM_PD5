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
        //private const string FirstRule = "F[-X]+X";

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
                //test(ref _pattern);
                //_pattern = _pattern.Replace("A", FirstRule);
                //_pattern = _pattern.Replace("B", SecondRule);


            }
            Console.WriteLine(_pattern);
            _pattern = _pattern.Replace(AuxiliarySymbol.ToString(), "");
        }

        private void FirstReplace(string firstWord, out string pattern)
        {
            pattern = firstWord.Replace(FirstRuleReplaceChar, FirstRule);

            //pattern = pattern.Replace(SecondRuleChar, SecondRule);
        }

        private void test(ref string pattern)
        {
            pattern = pattern.Replace(AuxiliarySymbol.ToString(), "A");
            pattern = pattern.Replace(GoForwardOperation.ToString(), "B");
        }

        public void GenerateCordinates()
        {
            _ = _pattern ?? throw new Exception("Generate patttern at first");
           
            var operations = _pattern.ToCharArray();

            foreach (var operation in operations)
            {
                var cord = _operations[operation](_cordinates[_cordinates.Count - 1]);

                //if(!operation.Equals(PushStackOperation) && !operation.Equals(AuxiliarySymbol))
                
                _cordinates.Add(cord);
            }
        }

        public void ShowCordinatesX()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cordinate in _cordinates)
            {
                //Console.WriteLine($"X: {cordinate.x} Y: {cordinate.y} A: {cordinate.a}");
                //Console.WriteLine(cordinate.x.ToString().Replace('.',','));
                //Console.WriteLine($"Y: {cordinate.y}");
                sb.AppendLine(cordinate.x.ToString().Replace('.', ','));
            }
            File.WriteAllText(@"/Users/grzegorzwawrzeniecki/Public/untitled folder/x.txt", sb.ToString());
        }

        public void ShowCordinatesY()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cordinate in _cordinates)
            {
                //Console.WriteLine($"X: {cordinate.x} Y: {cordinate.y} A: {cordinate.a}");
                //Console.WriteLine(cordinate.y.ToString().Replace('.', ','));
                //Console.WriteLine($"Y: {cordinate.y}");
                sb.AppendLine(cordinate.y.ToString().Replace('.', ','));
            }
            File.WriteAllText(@"/Users/grzegorzwawrzeniecki/Public/untitled folder/y.txt", sb.ToString());

        }
    }
}
