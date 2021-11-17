using System;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace mysql
{
    class Program
    {
        static void Execute()
        {
            try{
                string connString = @"server=localhost;userid=root";
                MySqlConnection conn = new MySqlConnection(connString);

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("CREATE database Studia", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Baza danych utworzona!");
                conn.Close();
            }
            catch(Exception e) { Console.WriteLine("Wystapil bład", e); }
        }
        static void CreateDatabase()
        {
            try{
                string connString = @"server=localhost;userid=root";
                MySqlConnection conn = new MySqlConnection(connString);

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("CREATE database Studia", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Baza danych utworzona!");
                conn.Close();
            }
            catch(Exception e) { Console.WriteLine("Wystapil bład", e); }
        }

        static void AddUser(string login, string email, string passwd)
        {
            try{
                string connString = @"server=localhost;userid=root;database=Studia";
                MySqlConnection conn = new MySqlConnection(connString);

                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO users (id, login, email, password) values (null, '" + login + "', '" + email + "', '" + passwd + "')", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Uzytkownik dodany!");
                conn.Close();
            }
            catch(Exception e) { Console.WriteLine("Wystapil bład", e); }
        }
        static void Main(string[] args)
        {
            CreateDatabase();
            AddUser("malpiszon", "malpiszon@wp.pl", "123frytki");

        }
    }
}
