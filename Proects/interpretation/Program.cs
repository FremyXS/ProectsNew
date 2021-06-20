using System;
using System.IO;
using System.Collections.Generic;

namespace Interpretation
{
    public enum CommandsType
    {
        Var, Mov, Add, Sub, Mul, Div
    }
    class Program
    {
        public static string[] fileInfo { get; } = File.ReadAllLines(@"../../../Commands.txt");
        public static Dictionary<string, double> Variables { get; private set; } = new Dictionary<string, double>();
        static void Main(string[] args)
        {
            foreach(var command in fileInfo)
            {
                ExecuteCommand(command.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries));
            }

            foreach(var variable in Variables)
            {
                Console.WriteLine("{0} = {1}", variable.Key, variable.Value);
            }
        }
        private static void ExecuteCommand(string[] command)
        {
            if (command[0] == CommandsType.Var.ToString().ToLower())
            {
                Variables.Add(command[1], 0);
            }
            else if(command[0] == CommandsType.Mov.ToString().ToLower())
            {
                Variables[command[1]] = new Mov().Command(Variables[command[1]], GetValue(command[2]));
            }
            else if (command[0] == CommandsType.Add.ToString().ToLower())
            {
                Variables[command[1]] = new Add().Command(Variables[command[1]], GetValue(command[2]));
            }
            else if (command[0] == CommandsType.Sub.ToString().ToLower())
            {
                Variables[command[1]] = new Sub().Command(Variables[command[1]], GetValue(command[2]));
            }
            else if (command[0] == CommandsType.Mul.ToString().ToLower())
            {
                Variables[command[1]] = new Mul().Command(Variables[command[1]], GetValue(command[2]));
            }
            else if (command[0] == CommandsType.Div.ToString().ToLower())
            {
                Variables[command[1]] = new Div().Command(Variables[command[1]], GetValue(command[2]));
            }
        }
        private static double GetValue(string secondValue)
        {
            if (Variables.ContainsKey(secondValue))
                return Variables[secondValue];

            return double.Parse(secondValue);
        }
    }
    public abstract class Operation
    {
        public abstract double Command(double valueFirst, double valueSecond);
    }
    public class Mov : Operation
    {
        public override double Command(double valueFirst, double valueSecond)
        {
            
            return valueSecond;
        }
    }
    public class Add : Operation
    {
        public override double Command(double valueFirst, double valueSecond)
        {
            return valueFirst + valueSecond;
        }
    }
    public class Sub : Operation
    {
        public override double Command(double valueFirst, double valueSecond)
        {
            return valueFirst - valueSecond;
        }
    }
    public class Mul : Operation
    {
        public override double Command(double valueFirst, double valueSecond)
        {
            return valueFirst * valueSecond;
        }
    }
    public class Div : Operation
    {
        public override double Command(double valueFirst, double valueSecond)
        {
            if (valueSecond == 0)
                throw new Exception("Error: Нельзя делить на ноль");

            return valueFirst / valueSecond;
        }
    }
    
}
