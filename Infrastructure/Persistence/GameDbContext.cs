using Microsoft.EntityFrameworkCore;
using Domain;
using System;

namespace Infrastructure.Persistence
{
    public class GameDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(game =>
            {
                game.ToTable("Games")
                    .HasKey(g => g.ID);

                game.HasMany(g => g.Matches)
                    .WithOne(m => m.Game)
                    .HasForeignKey("GameID")
                    .OnDelete(DeleteBehavior.Cascade);

                game.Metadata.FindNavigation("Matches")
                    .SetPropertyAccessMode(PropertyAccessMode.Field);


            });


            modelBuilder.Entity<Match>(match =>
            {
                match.ToTable("Matches")
                    .HasKey(g => g.ID);

                match.Property(m => m.ID)
                    .ValueGeneratedNever();

                match.Property(m => m.Option)
                   .HasConversion(m => m.ToString(), g => (GameOption)Enum.Parse(typeof(GameOption), g));

                match.Property(m => m.CpuOption)
                   .HasConversion(m => m.ToString(), g => (GameOption)Enum.Parse(typeof(GameOption), g));
            });

        }


        public DbSet<Game> Games { get; set; }
        public DbSet<Match> Matches { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}



