using DAISInterviewTask.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAISInterviewTask.Data.Context
{
    public class DAISInterviewTaskDbContext : IdentityDbContext<User>
    {
        public DAISInterviewTaskDbContext(DbContextOptions<DAISInterviewTaskDbContext> options) : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=DAISInterviewTask;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Payment>().HasOne(x => x.FromBankAccount).WithMany().HasForeignKey(x => x.FromBankAccountId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
