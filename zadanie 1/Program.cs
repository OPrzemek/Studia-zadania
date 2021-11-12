using System;

namespace zadanie_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Wprowadzanie zmiennych
            Console.WriteLine("Wprowadz a");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Wprowadz b");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine("Wprowadz c");
            double c = double.Parse(Console.ReadLine());
            if (a == 0)
            {
                Console.WriteLine("Twoja funkcja nie jest parabola (a jest rowne 0) !");
                return;
            }
            //Obliczanie delty
            double delta = Math.Pow(b, 2) - 4 * a * c;

            //Obliczanie miejsc zerowych
            if (delta == 0)
            {
                double x_0 = (-b) / (2 * a);
                Console.WriteLine("Twoje miejsce zerowe to: " + x_0);
            }
            else if (delta > 0)
            {
                double x_1 = (-b - Math.Sqrt(delta)) / (2 * a);
                double x_2 = (-b + Math.Sqrt(delta)) / (2 * a);
                Console.WriteLine("Twoje miejsca zerowe to: " + x_1 + " oraz " + x_2);
            }
            else Console.WriteLine("Brak miejsc zerowych!");
        }
    }
}
