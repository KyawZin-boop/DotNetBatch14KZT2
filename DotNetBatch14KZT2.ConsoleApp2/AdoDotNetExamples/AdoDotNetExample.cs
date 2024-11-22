﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14KZT2.ConsoleApp2.AdoDotNetExamples;

public class AdoDotNetExample
{
    private readonly SqlConnectionStringBuilder _connBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-C0JBC3O\\MSSQLSERVER2022",
        InitialCatalog = "test_db",
        UserID = "sa",
        Password = "Kyawzin@123",
        TrustServerCertificate = true
    };

    public void Read()
    {
        SqlConnection con = new SqlConnection(_connBuilder.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand("Select * from tbl_blog", con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        con.Close();

        foreach(DataRow dr in dt.Rows)
        {
            Console.WriteLine("id = " + dr["blog_id"]);
            Console.WriteLine("title = " + dr["blog_title"]);
            Console.WriteLine("author = " + dr["blog_author"]);
            Console.WriteLine("content = " + dr["blog_content"]);
            Console.WriteLine("");
        }
    }

    public void Edit(string id)
    {
        SqlConnection con = new SqlConnection(_connBuilder.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand($"Select * from tbl_blog where blog_id='{id}'", con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        if(dt.Rows.Count == 0)
        {
            Console.WriteLine("Data not found!");
            return;
        }

        DataRow row = dt.Rows[0];

        Console.WriteLine(row["blog_id"]);
        Console.WriteLine(row["blog_title"]);
        Console.WriteLine(row["blog_author"]);
        Console.WriteLine(row["blog_content"]);
    }

    public void Create(string title, string author, string content)
    {
        string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                                   ([blog_title]
                                   ,[blog_author]
                                   ,[blog_content])
                             VALUES
                                   ('{title}'
                                   ,'{author}'
                                   ,'{content}')";

        SqlConnection con = new SqlConnection(_connBuilder.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand(query, con);
        int result = cmd.ExecuteNonQuery();

        con.Close();

        string message = result > 0 ? "Create Success." : "Create Fail!";
        Console.WriteLine(message);
    }

    public void Update(string id, string title, string author, string content)
    {
        string query = $@"Update [dbo].[tbl_blog] 
                                SET [blog_title]='{title}',
                                    [blog_author]='{author}',
                                    [blog_content]='{content}'
                                where blog_id='{id}'";

        SqlConnection con = new SqlConnection(_connBuilder.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand(query, con);
        int result = cmd.ExecuteNonQuery();

        con.Close();

        string message = result > 0 ? "Update Success." : "Update Fail!";
        Console.WriteLine(message);
    }

    public void Delete(string id)
    {
        SqlConnection con = new SqlConnection(_connBuilder.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand($"Delete [dbo].[tbl_blog] where blog_id='{id}'", con);
        int result = cmd.ExecuteNonQuery();

        con.Close();

        string message = result > 0 ? "Delete Success." : "Delete Fail!";
        Console.WriteLine(message);
    }
}
