using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetBatch14KZT2.ConsoleApp1.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNetBatch14KZT2.ConsoleApp1.EFCoreExamples;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppSettings.sqlConnectionString.ConnectionString);
        }
    }

    public DbSet<TblBlog> Blogs { get; set; }
}

[Table("tbl_blog")]
public class TblBlog
{
    [Key]
    [Column("blog_id")]
    public string Id { get; set; }

    [Column("blog_title")]
    public string Title { get; set; }

    [Column("blog_author")]
    public string Author { get; set; }

    [Column("blog_content")]
    public string Content { get; set; }
}

