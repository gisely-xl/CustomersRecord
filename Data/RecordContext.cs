using CustomersRec.APIrest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Data
{
    public class RecordContext : DbContext
    {
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<Address> Adresses { get; set; }

        public IConfiguration configuration { get;  }
        public RecordContext(DbContextOptions<RecordContext> options) : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=_RecordDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity => {
                entity.HasKey(e => new { e.Cep });
            });
        }
    }
}
