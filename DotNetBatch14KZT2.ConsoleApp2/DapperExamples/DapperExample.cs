using Dapper;
using DotNetBatch14KZT2.ConsoleApp2.Dtos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14KZT2.ConsoleApp2.DapperExamples;

public class DapperExample
{
    private readonly string _connString = AppSettings._sqlConnectionStringBuilder.ConnectionString;

    public void Read()
    {
        using IDbConnection con = new SqlConnection(_connString);

        List<BlogDtos> lst = con.Query<BlogDtos>("Select * from tbl_blog").ToList();
        foreach (BlogDtos item in lst)
        {
            Console.WriteLine("id = " + item.blog_id);
            Console.WriteLine("title = " + item.blog_title);
            Console.WriteLine("author = " + item.blog_author);
            Console.WriteLine("content = " + item.blog_content);
            Console.WriteLine("");
        }
    }

    public void Edit(string id)
    {
        using IDbConnection con = new SqlConnection(_connString);

        var item = con.Query<BlogDtos>($"Select * from tbl_blog where blog_id='{id}'").FirstOrDefault();
        if(item is null)
        {
            Console.WriteLine("Data not found!");
            return;
        }

        Console.WriteLine("id = " + item.blog_id);
        Console.WriteLine("title = " + item.blog_title);
        Console.WriteLine("author = " + item.blog_author);
        Console.WriteLine("content = " + item.blog_content);
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

        using IDbConnection con = new SqlConnection(_connString);

        var result = con.Execute(query);

        string message = result > 0 ? "Create Success." : "Create Fail!";
        Console.WriteLine(message);
    }

    public void Update(string id, string title, string author, string content)
    {
        string query = $@"UPDATE [dbo].[tbl_blog]
                            SET [blog_title] = '{title}'
                                ,[blog_author] = '{author}'
                                ,[blog_content] = '{content}'
                            WHERE blog_id = '{id}'";

        using IDbConnection con = new SqlConnection(_connString);

        var result = con.Execute(query);

        string message = result > 0 ? "Update Success." : "Update Fail!";
        Console.WriteLine(message);
    }

    public void Delete(string id)
    {
        using IDbConnection con = new SqlConnection(_connString);

        var result = con.Execute($"Delete from [dbo].[tbl_blog] where blog_id='{id}'");

        string message = result > 0 ? "Delete Success." : "Delete Fail!";
        Console.WriteLine(message);
    }
}
