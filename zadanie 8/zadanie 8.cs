using System;
using System.IO;

namespace zadanie_8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("./data.csv");

            double[] I = new double[lines.Length];
            double[] V = new double[lines.Length];
            double[] t = new double[lines.Length];
            double[] P = new double[lines.Length];
            string[] temp = new string[2];
            t[0] = -0.02;
            double[] E = new double[lines.Length];
            
            for(int i = 1; i < lines.Length; i++)
            {
                temp = lines[i].Split(",");
                I[i] = Math.Round(double.Parse(temp[0].Replace(".", ",")) * 10 , 2);
                V[i] = double.Parse(temp[1].Replace(".", ","));
                t[i] = Math.Round(t[i - 1] + 0.02 , 2);
                P[i] = Math.Round(I[i] * V[i] , 2);
                if(i < lines.Length -1) E[i] = Math.Round((P[i] + P[i + 1]) / 2 , 2);

            }
            string output = "t[ms];I[10A];Ue[V];P[W];E[J]\n";

            for(int i = 1; i < lines.Length; i++)
                output += t[i] + ";" + I[i] + ";" + V[i] + ";" + P[i] + ";" + E[i] + "\r\n";

            // output = output.Replace(",", ".");
            // output = output.Replace(";", ",");

            File.WriteAllText("./output.csv", output);
        }
    }
}
