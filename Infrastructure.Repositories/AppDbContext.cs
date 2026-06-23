using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<PairWord> PairWords => Set<PairWord>();
        public DbSet<Dictionary> Dictionaries => Set<Dictionary>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PairWord>()
                .HasOne(i => i.Dictionary)
                .WithMany(i => i.PairWords)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
