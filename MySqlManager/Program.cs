using Microsoft.Data.SqlClient;
using System.Data;

SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
builder.ConnectionString = "server=(local),1433;user id=sa;" +
            "password= 1001Spill+;initial catalog=test";

builder["Trusted_Connection"] = false; //Means you get the username and password sent in
builder.Encrypt = false;
Console.WriteLine(builder.ConnectionString);

using(var connection = new SqlConnection(builder.ConnectionString))
{
    connection.Open();
    Console.WriteLine("Done \n");









}


