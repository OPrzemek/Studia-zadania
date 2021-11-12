using System;

namespace zadanie_7
{
    class Program
    {
        static void Main(string[] args)
        {
            //Zamiana argumentow na date
            string[] date1 = args[0].Split("-");
            string[] date2 = args[1].Split(":");

            int year = int.Parse(date1[0]);
            int month = int.Parse(date1[1]);
            int day = int.Parse(date1[2]);
            int hour = int.Parse(date2[0]);
            int minute = int.Parse(date2[1]);
            int second = int.Parse(date2[2]);

            DateTime date = new DateTime(year, month, day, hour, minute, second);

            //Modyfikacje
            for (int i = 2; i < args.Length; i++)
            {
                string mod = args[i];
                int number;

                if (mod.EndsWith("y"))
                {
                    //lata
                    number = int.Parse(mod.Substring(0, mod.Length - 1));
                    date = date.AddYears(number);
                }
                else if (mod.EndsWith("mo"))
                {
                    //miesiące
                    number = int.Parse(mod.Substring(0, mod.Length - 2));
                    date = date.AddMonths(number);
                }
                else if (mod.EndsWith("d"))
                {
                    //dni
                    number = int.Parse(mod.Substring(0, mod.Length - 1));
                    date = date.AddDays(number);
                }
                else if (mod.EndsWith("h"))
                {
                    //godziny
                    number = int.Parse(mod.Substring(0, mod.Length - 1));
                    date = date.AddHours(number);
                }
                else if (mod.EndsWith("m"))
                {
                    //minuty
                    number = int.Parse(mod.Substring(0, mod.Length - 1));
                    date = date.AddMinutes(number);
                }
                else if (mod.EndsWith("s"))
                {
                    //sekundy
                    number = int.Parse(mod.Substring(0, mod.Length - 1));
                    date = date.AddSeconds(number);
                }
            }
            Console.WriteLine(date);
        }
    }
}
