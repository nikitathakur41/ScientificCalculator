using System;
using ArithmeticOperations;
using PowerOperations;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ScientificCalculator
{

    internal class Program
    {
        static bool dividebyzero = false;
        static bool nexttime = false;
        static string number = "";
        static string angleStatus = "DEG";
        static int angleCountTap = 0;
        static string outputStatus = "F-E";
        static bool ScientificNotation = false;
        static int outputCountTap = 0;
        static double memory = 0;
        static bool memoryHelper = false;
        static bool Isfunction = false;
        static StringBuilder output = new StringBuilder();
        static void Main()
        {
            Console.Title = "Scientific Calculator";
            ConsoleKeyInfo input;
            bool sequentialOperatorCame;
            double result = 0;
            Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
            Console.WriteLine(output.ToString());
            while (true)
            {
                do
                {
                    input = Console.ReadKey(true);
                    sequentialOperatorCame = false;
                    Isfunction = false;
                    string currentstring = Value(input);
                    Console.Clear();
                    Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                    Console.WriteLine(output.ToString());

                    if (IsOperator(currentstring) && output.Length != 0 && IsOperator(output[output.Length - 1].ToString()))
                    {
                        sequentialOperatorCame = true;
                        output.Remove(output.Length - 1, 1);
                        output.Append(currentstring);
                        Console.Clear();
                        Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                        Console.WriteLine(output.ToString());
                    }
                    else if (IsOperator(currentstring) && output.Length == 0)
                    {
                        sequentialOperatorCame = true;
                        output.Append("0");
                        output.Append(currentstring);
                        Console.Clear();
                        Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                        Console.WriteLine(output.ToString());
                    }
                    else
                    {
                        if (currentstring == "" && input.Key != ConsoleKey.Backspace)
                        {
                            continue;
                        }
                        string temp = currentstring;


                        output.Append(temp);
                        if (IsOperand(temp) && nexttime == false)
                        {
                            number += temp;

                        }
                        else
                        {
                            number = "";

                        }
                        Console.Clear();
                        Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                        Console.WriteLine(output.ToString());

                    }
                    if (nexttime == true)
                    {
                        if (output[output.Length - 1] >= '0' && output[output.Length - 1] <= '9' && Isfunction==false)
                        {
                            Console.Clear();
                            output.Clear();
                            Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                            Console.WriteLine(output.ToString());
                            output.Append(currentstring);
                            number = currentstring;
                        }
                        nexttime = false;
                    }

                    if (output.Length > 0 && sequentialOperatorCame == false && Isfunction == false)
                    {
                        //Console.Write(currentstring);
                        Console.Write(number);
                    }

                    if (input.Key == ConsoleKey.Backspace)
                    {
                        if (output.Length > 0)
                        {
                            output.Remove(output.Length - 1, 1);
                        }
                        Console.Clear();
                        Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                        Console.Write(output.ToString());
                    }
                } while ((input.Key != ConsoleKey.Enter) && (input.Key != ConsoleKey.OemPlus));

                Console.Clear();
                if (output.Length > 0)
                {
                    try
                    {
                        result = arithmetic.Evaluate(output.ToString());
                        Console.WriteLine(angleStatus + "\t" + outputStatus + "\t" + String.Format(memoryHelper ? "M" : ""));
                        Console.WriteLine(output.ToString() + "=");
                        if (ScientificNotation)
                        {
                            Console.Write(power.ConvertToScientificNotation(Convert.ToDouble(result)));
                        }
                        else
                        {
                            Console.WriteLine(result);
                        }
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    output.Append(0);
                    Console.WriteLine(output.ToString() + "=");
                    Console.Write(0);
                }
                output.Clear();
                output.Append(result);
                number = result.ToString();
                nexttime = true;
            }
        }
        static string Value(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.D:
                    if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Console.Write($"{output}dms({number})");
                        return power.ConvertToDMS(Convert.ToDouble(number)).ToString();
                    }
                    else
                    {
                        Formating();
                        Console.Write($"{output}deg({number})");
                        return power.DMSToDegree(Convert.ToDouble(number)).ToString();
                    }
                case ConsoleKey.M:
                    if (number == "")
                    {
                        number = "0";
                    }
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)   //MS
                    {
                        memory = Convert.ToDouble(number);
                        memoryHelper = true;
                        return "";
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Shift) != 0) //M+
                    {
                        memory += Convert.ToDouble(number);
                        memoryHelper = true;
                        return "";
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)  //M-
                    {
                        memory -= Convert.ToDouble(number);
                        memoryHelper = true;
                        return "";
                    }
                    else      //MR
                    {
                        if (memoryHelper == false)
                        {
                            return "";
                        }
                        else
                        {
                            if (memory >= 0)
                            {
                                return memory.ToString();
                            }
                            else
                                return "(" + memory.ToString() + ")";
                        }
                    }
                case ConsoleKey.B:  //MC
                    memory = 0;
                    memoryHelper = false;
                    return "";
                case ConsoleKey.S:
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}sinh({newNumber})");
                        string tempValue = "(" + power.SineHyp(newNumber, angleStatus).ToString() + ")";
                        return tempValue;
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}sin^(-1)({newNumber})");
                        string tempValue = "(" + power.SineInverse(newNumber, angleStatus).ToString() + ")";
                        return tempValue;
                    }
                    else
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}sin({number})");
                        string tempValue =  power.Sine(newNumber, angleStatus).ToString() ;
                        return "(" + tempValue + ")";
                    }
                case ConsoleKey.Q:
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Csch({newNumber})");
                        string tempValue = "(" + power.CosecHyp(newNumber, angleStatus).ToString() + ")";
                        return tempValue;
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Csc^(-1)({newNumber})");
                        string tempValue = "(" + power.CosecInverse(newNumber, angleStatus).ToString() + ")";
                        return tempValue;
                    }
                    else
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Csc({newNumber})");
                        string tempValue = "(" + power.Cosec(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                case ConsoleKey.C:
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Cosh({newNumber})");
                        string tempValue = "(" + power.CosineHyp(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Cos^(-1)({newNumber})");
                        string tempValue = "(" + power.CosineInverse(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Cos({newNumber})");
                        string tempValue = "(" + power.Cosine(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                case ConsoleKey.T:
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}tanh({newNumber})");
                        string tempValue = "(" + power.TangentHyp(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}tan^(-1)({newNumber})");
                        string tempValue = "(" + power.TangentInverse(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}tan({newNumber})");
                        string tempValue = "(" + power.Tangent(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                case ConsoleKey.O:
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Coth({newNumber})");
                        string tempValue = "(" + power.CotHyp(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }

                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Cot^(-1)({newNumber})");
                        string tempValue = "(" + power.CotInverse(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Cot({newNumber})");
                        string tempValue = "(" + power.Cot(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                case ConsoleKey.N:
                    if ((input.Modifiers & ConsoleModifiers.Control) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Sech({number})");
                        string tempValue = "(" + power.SecHyp(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Sec^(-1)({newNumber})");
                        string tempValue = "(" + power.SecInverse(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                    else
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Sec({newNumber})");
                        string tempValue = "(" + power.Sec(newNumber, angleStatus).ToString() + ")";
                        number = "";
                        return tempValue;
                    }
                case ConsoleKey.D0:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return ")";
                    }
                    else
                    {
                        return "0";
                    }
                case ConsoleKey.D1: return "1";
                case ConsoleKey.D2: return "2";
                case ConsoleKey.D3: return "3";
                case ConsoleKey.D4: return "4";
                case ConsoleKey.D5:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return "%";
                    }
                    else
                    {
                        return "5";
                    }
                case ConsoleKey.D6:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return "^";
                    }
                    else
                    {
                        return "6";
                    }
                case ConsoleKey.D7: return "7";
                case ConsoleKey.D8:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return "*";
                    }
                    else
                    {
                        return "8";
                    }
                case ConsoleKey.D9:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return "(";
                    }
                    else
                    {
                        return "9";
                    }
                case ConsoleKey.OemMinus:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return "_";
                    }
                    else
                    {
                        return "-";
                    }
                case ConsoleKey.F1:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        return "+";
                    }
                    else
                    {
                        return "";
                    }
                case ConsoleKey.L:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        Formating();
                        Console.Write($"{output}log({number})");
                        Double.TryParse(number, out double newNumber);
                        string ans = power.Log10(newNumber).ToString();
                        return ans;
                    }
                    else
                    {
                        Formating();
                        Console.Write($"{output}ln({number})");
                        Double.TryParse(number, out double newNumber);
                        string ans = power.Ln(newNumber).ToString();
                        return ans;
                    }
                case ConsoleKey.P:
                    if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        string tempValue = "(" + Math.PI.ToString() + ")";
                        return tempValue;
                    }
                    else
                    {
                        return "^";
                    }
                case ConsoleKey.V:
                    outputCountTap++;
                    if (outputCountTap == 0)
                    {
                        outputStatus = "F-E";
                        ScientificNotation = false;
                    }
                    else if (outputCountTap == 1)
                    {
                        outputStatus = "E-F";
                        ScientificNotation = true;
                    }
                    else if (outputCountTap == 2)
                    {
                        outputCountTap = 0;
                        ScientificNotation = false;
                        outputStatus = "F-E";
                    }
                    return "";
                case ConsoleKey.J:
                    if ((input.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}√({newNumber})");
                        string ans = power.SquareRoot(newNumber).ToString();
                        return ans;                                                               
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Shift) != 0)
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}√({newNumber})");
                        string ans = power.CubeRoot(newNumber).ToString();
                        return ans;
                    }
                    else
                    {
                        return "";
                    }
              
                case ConsoleKey.R:
                    if ((input.Modifiers & ConsoleModifiers.Alt) != 0)                    //cube
                    {
                        Formating();
                        Console.Write($"{output}cube({number})");
                        Double.TryParse(number, out double newNumber);
                        string ans = power.Exponentiation(newNumber, 3).ToString();
                        return ans;                                                     
                    }
                    else if ((input.Modifiers & ConsoleModifiers.Shift) != 0)              //squre
                    {
                        Formating();
                        Console.Write($"{output}cube({number})");
                        Double.TryParse(number, out double newNumber);
                        string ans = power.Exponentiation(newNumber, 2).ToString();
                        return ans;
                    }
                    else
                    {
                        return "";
                    }
                case ConsoleKey.G:
                    {
                        Formating();
                        double random = power.RandomFunction();
                        Console.Write($"{random}");
                        return random.ToString();
                    }
                case ConsoleKey.Oem4:
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Floring({newNumber})");
                        string ans = power.FloorFunction(newNumber).ToString();
                        return ans;
                    }
                case ConsoleKey.Oem6:
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}Ceiling({newNumber})");
                        string ans = power.CeilingFunction(newNumber).ToString();
                        return ans;
                    }
                case ConsoleKey.F:
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}fact({newNumber})");
                        string ans = power.Factorial(newNumber).ToString();
                        return ans;
                    }

                case ConsoleKey.Backspace: return "";
                case ConsoleKey.Enter: return "";
                case ConsoleKey.Add: return "+";
                case ConsoleKey.Subtract: return "-";
                case ConsoleKey.Multiply: return "*";
                case ConsoleKey.Divide: return "/";
                case ConsoleKey.Oem2: return "/";
                case ConsoleKey.OemPeriod: return ".";
                case ConsoleKey.Z:
                    angleCountTap++;
                    if (angleCountTap == 0)
                    {
                        angleStatus = "DEG";
                    }
                    else if (angleCountTap == 1)
                    {
                        angleStatus = "RAD";
                    }
                    else if (angleCountTap == 2)
                    {
                        angleStatus = "GRAD";
                    }
                    else if (angleCountTap == 3)
                    {
                        angleCountTap = 0;
                        angleStatus = "DEG";
                    }
                    return "";
                case ConsoleKey.A:
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}abs({newNumber})");
                        string ans = power.AbsoluteFunction(newNumber).ToString();
                        return ans;
                    }
                case ConsoleKey.E:
                    if ((input.Modifiers & ConsoleModifiers.Shift) != 0)            //e value
                    {
                        Formating();
                        Console.Write($"{output}e^({number})");
                        return Math.E.ToString();
                    }
                    else                                                            //e^x
                    {
                        Formating();
                        Double.TryParse(number, out double newNumber);
                        Console.Write($"{output}e^({newNumber})");
                        string ans = power.ePowerx(Convert.ToDouble(newNumber)).ToString();
                        return ans;
                    }

                default: return "";
            }
            void Formating()
            {
                Isfunction = true;
                Console.Clear();
                output.Remove(output.Length - number.Length, number.Length);
            }
        }

        static bool IsOperator(string x)
        {
            return x == "+" || x == "-" || x == "*" || x == "/" || x == "^";
        }
        static bool IsOperand(string x)
        {
            return x == "0" || x == "1" || x == "2" || x == "3" || x == "4" || x == "5" || x == "6" || x == "7" || x == "8" || x == "9" || x == ".";
        }
        static string CaptureContentBeforeCursor()
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            StringBuilder capturedContent = new StringBuilder();

            // Capture the content before the cursor position until the start of the line or an operator is found
            for (int left = currentLeft - 1; left >= 0; left--)
            {
                char currentChar = Console.ReadKey().KeyChar;

                // Stop capturing if an operator is found or if we reached the start of the line
                if (!(currentChar >= '0' && currentChar <= '9') || left == 0)
                    break;

                capturedContent.Insert(0, currentChar);

                // Move the cursor one position to the left
                Console.SetCursorPosition(left - 1, currentTop);
            }

            // Move the cursor back to its original position
            Console.SetCursorPosition(currentLeft, currentTop);

            return capturedContent.ToString();
        }


    }
}
