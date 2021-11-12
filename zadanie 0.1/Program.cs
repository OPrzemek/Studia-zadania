using System;

namespace zadanie_0._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wprowadz x");
            double x = double.Parse(Console.ReadLine());
            double y = 0;

            if (x <= -4) //Prosta
            {
                y = -((double)1 / 3) * x + ((double)2 / 3);
            }
            else if (x < 2) //Parabola
            {
                y = ((double)1 / 4) * (x + 3) * (x - 2);
            }
            else //Prosta
            {
                y = -((double)1 / 3) * x + ((double)2 / 3);
            }

            Console.WriteLine("Twoja wartosc dla y to: " + y);
        }
    }
}
