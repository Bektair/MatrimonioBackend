using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MySqlManager.SqlExamples
{
    public class SqlCommands
    {


        public SqlCommands()
        {



        }



        public static void CREATE(string connString)
        {
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                string sqlCreate = "INSERT into wedding.Pro(FirstName, LastName, Subject) values (@FirstName, @LastName, @Subject)";
                string firstName = "Best Pro";
                string lastName = "Lest Pro";
                string subject = "Matte";

                using (var cmd = new SqlCommand(sqlCreate, connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public static void DELETE(string connString)
        {
            using (var connection = new SqlConnection(connString))
            {
                string sqlDelete = "DELETE FROM wedding.pro WHERE ID = @proId";
                int proDeleteId = 6;
                using (var cmd = new SqlCommand(sqlDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@proId", proDeleteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void GET(string connString)
        {
            string sqlRead = "SELECT * FROM wedding.pro";
            using (var connection = new SqlConnection(connString))
            {
                using (var cmd = new SqlCommand(sqlRead, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine($"{reader.GetName(0)} \t {reader.GetName(1)} \t {reader.GetName(2)} \t {reader.GetName(3)}");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetString(0)} \t {reader.GetString(1)} \t {reader.GetString(2)} \t {reader.GetInt32(3)} ");
                        }
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine("EXECUTED Read");
                    }
                }
            }
        }

        public static void UPDATE(string connString)
        {
            using (var connection = new SqlConnection(connString)) { 
                string sqlUpdate = "Update wedding.pro SET LastName = @newLastName WHERE ID = @professorID";
                int proId = 3;
                string newLastName = "Funky";

                using (var cmd = new SqlCommand(sqlUpdate, connection))
                {
                    cmd.Parameters.AddWithValue("@newLastName", newLastName);
                    cmd.Parameters.AddWithValue("@professorID", proId);
                    cmd.ExecuteNonQuery();
                }
            }
        }





    }
}
