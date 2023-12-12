using System;
namespace PowerOperations
{
    public class power
    {
        public static double AngleChanger(double angle, String Status)
        {
            if (Status == "DEG")
            {
                angle = (angle * Math.PI) / 180;
            }
            else if (Status == "GRAD")
            {
                angle = (angle * Math.PI) / 200;

            }
            return angle;
        }

        //Trignometric Function
        public static double Sine(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Sin(angle);

        }

        public static double Cosine(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Cos(angle);
        }

        public static double Tangent(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Tan(angle);
        }

        public static double Cot(double angle, String Status)
        {
            return 1 / Tangent(angle, Status);
        }

        public static double Sec(double angle, String Status)
        {
            return 1 / Cosine(angle, Status);
        }

        public static double Cosec(double angle, String Status)
        {
            return 1 / Sine(angle, Status);
        }

        //Inverse Trignometric Function
        public static double SineInverse(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Asin(angle);
        }

        public static double CosineInverse(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Acos(angle);
        }

        public static double TangentInverse(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Atan(angle);
        }

        public static double CotInverse(double angle, String Status)
        {
            return 1 / TangentInverse(angle, Status);
        }

        public static double SecInverse(double angle, String Status)
        {
            return 1 / CosineInverse(angle, Status);
        }

        public static double CosecInverse(double angle, String Status)
        {
            return 1 / SineInverse(angle, Status);
        }

        //Hyperbolic Function
        public static double SineHyp(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Sinh(angle);
        }

        public static double CosineHyp(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Cosh(angle);
        }

        public static double TangentHyp(double angle, String Status)
        {
            angle = AngleChanger(angle, Status);
            return Math.Tanh(angle);
        }
        public static double CotHyp(double angle, String Status)
        {
            return 1 / TangentHyp(angle, Status);
        }

        public static double SecHyp(double angle, String Status)
        {
            return 1 / CosineHyp(angle, Status);
        }

        public static double CosecHyp(double angle, String Status)
        {
            return 1 / SineHyp(angle, Status);
        }
        //Function
        public static double AbsoluteFunction(double input)
        {
            return Math.Abs(input);
        }

        public static double FloorFunction(double input)
        {
            return Math.Floor(input);
        }
        public static double CeilingFunction(double input)
        {
            return Math.Ceiling(input);
        }

        public static double RandomFunction()
        {
            return new Random().NextDouble();
        }



        public static double ConvertToDMS(double angle1)
        {
            int degrees = (int)Math.Floor(angle1);
            double minutes = (angle1 - degrees) * 60;
            int minutesInt = (int)Math.Floor(minutes);
            double seconds = (minutes - minutesInt) * 60;
            double result = degrees + minutesInt / 100.0 + seconds / 10000.0;
            return result;
        }
        public static double DMSToDegree(double angle1)
        {
            int degrees = (int)Math.Floor(angle1);
            double minutes = (angle1 - degrees);
            double result = degrees + (minutes / 60);
            return result;
        }
        public static string ConvertToScientificNotation(double number)
        {
            string scientificNotation = number.ToString("E");
            return scientificNotation;
        }
        //Power Functions
        public static double Exponentiation(double baseValue, double exponent)
        {
            return Math.Pow(baseValue, exponent);
        }

        public static double Root(double baseValue, double rootValue)
        {
            return Math.Pow(baseValue, 1.0 / rootValue);
        }

        public static double SquareRoot(double number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Invalid input");
            }
            return Math.Sqrt(number);
        }

        public static double CubeRoot(double number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Invalid input");
            }
            return Math.Pow(number, 1.0 / 3.0);
        }

        public static double Log10(double x)
        {
            return Math.Log10(x);
        }

        public static double Ln(double x)
        {
            return Math.Log(x);
        }

        public static double ePowerx(double x)
        {
            return Math.Exp(x);
        }

        public static double Factorial(double n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Invalid input");
            }

            if (n == 0 || n == 1)
            {
                return 1;
            }

            double result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }


    }
}
