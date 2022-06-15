using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data.Common;


namespace Prn.Se1623;
public class Program
{
    public static void Main()
    {
        // test 
        //Console.WriteLine(GetConnectionString());
        //Console.ReadLine();

        //GetAllCustomer();
        //Console.ReadLine();

        List<Customer> customers =  GetAllCustomer();
        foreach(Customer customer in customers)
            Console.WriteLine(customer);

    }

    private static string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSetting.json")
            .Build();
        return config["ConnectionStrings:FptEduDBConn"];
    }

    public static List<Customer> GetAllCustomer()
    {
        //0. vsql
        string vSQL = "select * from Customers";       
        List<Customer> list = new List<Customer>();
        //1 create a connection to SQL Server
        DbProviderFactory factory = SqlClientFactory.Instance;
        DbConnection? connection = factory.CreateConnection();//  dau hoi cham (?) cho 
        if(connection == null)
        {
            Console.WriteLine($"Init Connection fail .....");
            return null;
        }
        connection.ConnectionString = GetConnectionString();
        connection.Open();
        //2. initial command (sql) to query data
        DbCommand? command = factory.CreateCommand();
        command.CommandText = vSQL;
        command.Connection = connection;

        //3. excute command
        DbDataReader dataReader = command.ExecuteReader();// nổ máy
        //4. read data and show to console
        while (dataReader.Read())
        {
            Customer customer = new Customer();
            customer.Id = (string)dataReader["CustomerId"];
            customer.Name = (string)dataReader["CompanyName"];
            customer.Address = (string)dataReader["Address"];
            customer.Phone = (string)dataReader["Phone"];
            list.Add(customer); 
        }
        return list;
    }
}