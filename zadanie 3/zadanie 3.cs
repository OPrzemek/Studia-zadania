using System;

namespace zadanie_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            Console.Write("Wprowadz zakres do ktorego szukasz liczby pierwsze: ");
            int zakres = int.Parse(Console.ReadLine());
            int max = (int) Math.Sqrt(zakres);

            int[] primes = new int[zakres + 1];
            for(i = 1; i < primes.Length; i++) primes[i] = i;

            for(i = 2; i <= max; i++)
            {
                int j = i + i;
                while(j <= zakres)
                {
                    primes[j] = 0;
                    j += i;
                }
            }
            for(i = 2 ; i < primes.Length ; i++)
            {
                if(primes[i] != 0) Console.WriteLine(primes[i]);
            }
        }
    }
}
