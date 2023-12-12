using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArithmeticOperations
{
    public class arithmetic
    {
        public static bool Precedence(char operator1, char operator2)
        {
            if (((operator1 == '/' && operator2 == '*') || (operator1 == '^' && operator2 == '^') || (operator1 == '^' && (operator1 == '*' || operator1 == '/' || operator2 == '+' || operator2 == '-')) || ((operator1 == '*' || operator1 == '/') && (operator2 == '+' || operator2 == '-'))))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static double Operations(double a, double b, char operators)
        {
            switch (operators)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;

                case '/':
                    if (b == 0)
                    {
                        throw new DivideByZeroException("cannot divide by zero.");
                    }
                    return a / b;
                case '^':
                    return Math.Pow(a, b);
            }
            return 0;
        }
        public static double Evaluate(string Expression)
        {
            //    Console.Write("Enter an expression:");
            //    string Expression = Console.ReadLine();
            int expressionIndex = 0;
            Stack<char> operators = new Stack<char>(10);
            Stack<double> operands = new Stack<double>(10);
            bool negativeIntegerAtFirst = false, numberIsNext = true;
            string number = "";

            if (Expression[0] == '-')
            {
                negativeIntegerAtFirst = true;
            }

            while (expressionIndex < Expression.Length)
            {
                if (Expression[expressionIndex] == '-' && Expression[expressionIndex + 1] == '(')
                {
                    negativeIntegerAtFirst = false;
                }

                if ((negativeIntegerAtFirst == true && Expression[expressionIndex] == '-' && Expression[expressionIndex + 1] != '(') || (Expression[expressionIndex] >= '0' && Expression[expressionIndex] <= '9') || Expression[expressionIndex] == '.')
                {
                    //Console.WriteLine("ertyu");
                    if (negativeIntegerAtFirst == true && Expression[expressionIndex] == '-')
                    {                        // Console.WriteLine("Comple number and indexx" + expressionIndex);
                        expressionIndex++;
                    }

                    number += Expression[expressionIndex];
                    //  Console.WriteLine(number + "   " + expressionIndex + " come");

                    if (expressionIndex == Expression.Length - 1 || (expressionIndex < Expression.Length - 1 && !((Expression[expressionIndex + 1] >= '0' && Expression[expressionIndex + 1] <= '9') || Expression[expressionIndex + 1] == '.')))
                    {

                        numberIsNext = false;
                    }
                    if (numberIsNext == false)
                    {
                        if (negativeIntegerAtFirst == true)
                        {
                            //Console.WriteLine(number +"fghj");
                            operands.Push(-Convert.ToDouble(number));
                            number = "";
                            numberIsNext = true;
                        }
                        else
                        {
                            operands.Push(Convert.ToDouble(number));
                            // Console.WriteLine(number);
                            number = "";
                            numberIsNext = true;
                        }
                        negativeIntegerAtFirst = false;
                    }
                }
                else if (Expression[expressionIndex] == '+' || Expression[expressionIndex] == '-' || Expression[expressionIndex] == '/' || Expression[expressionIndex] == '*' || Expression[expressionIndex] == '^')
                {
                    if (operands.Count == 0)
                    {
                        operands.Push(0);
                    }
                    if (operators.Count == 0)
                    {
                        operators.Push(Expression[expressionIndex]);
                    }
                    else
                    {
                        if (Precedence(operators.Peek(), Expression[expressionIndex]) == true)
                        {
                            operators.Push(Expression[expressionIndex]);  //4-2+2^2
                        }
                        else
                        {
                            while (operators.Count != 0 && (Precedence(operators.Peek(), Expression[expressionIndex]) == false))
                            {
                                double b = operands.Pop();
                                double a = operands.Pop();
                                operands.Push(Operations(a, b, operators.Peek()));
                                operators.Pop();
                            }
                            operators.Push(Expression[expressionIndex]);
                        }
                    }
                }
                else if (Expression[expressionIndex] == '(' || Expression[expressionIndex] == ')')
                {
                    if (Expression[expressionIndex] == '(')
                    {
                        //Console.WriteLine("ghjkk");
                        operators.Push(Expression[expressionIndex]);
                        if (Expression[expressionIndex + 1] == '-')
                        {
                            negativeIntegerAtFirst = true;
                        }
                    }
                    else
                    {
                        while (operators.Peek() != '(')
                        {

                            double b = operands.Pop();
                            double a = operands.Pop();
                            operands.Push(Operations(a, b, operators.Peek()));
                            operators.Pop();
                        }
                        operators.Pop();
                    }
                }
                expressionIndex++;

                if (expressionIndex == Expression.Length && operators.Count != 0)
                {
                    while (operators.Count != 0)
                    {
                        // Console.WriteLine("yyu");
                        double b = operands.Pop();
                        double a = operands.Pop();
                        char c = operators.Peek();
                        operands.Push(Operations(a, b, operators.Peek()));
                        //Console.WriteLine("trtfhgv");
                        operators.Pop();
                        if (operators.Count != 0 && operators.Peek() == '-')
                        {
                            // Console.WriteLine("ttty");
                            operands.Pop();
                            operands.Push(-(Operations(-a, b, c)));
                        }
                    }
                }//(-5/5+32*5-6-9)-(-7)
            }

            return (operands.Peek());

        }
    }
}