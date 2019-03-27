using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROG1442_Exercise4.Models
{
    public class ArtContext : DbContext
    {
        public ArtContext(DbContextOptions<ArtContext> options)
            : base(options)
        {

        }
        public DbSet<ArtType> ArtTypes { get; set; }
        public DbSet<Artwork> Artworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("art");            
            
            modelBuilder.Entity<ArtType>()
                .HasMany(a => a.Artworks)
                .WithOne(w => w.ArtType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Artwork>()
                .HasIndex(a => new { a.Name, a.ArtTypeID })
                .IsUnique();

        }
    }
}
