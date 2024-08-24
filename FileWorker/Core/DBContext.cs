using FileWorker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker.Core
{
    internal class DBContext : DbContext
    {
        public DbSet<StringModel> Lines { get; set; } = null!;
        public DbSet<StatisticsResult> StatisticsResults { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FileLines;Trusted_Connection=True; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StringModel>().HasKey(l => l.Id);
        }
    }
}
