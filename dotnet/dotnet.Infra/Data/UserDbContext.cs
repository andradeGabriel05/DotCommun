using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) 
        { }

        public UserDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DEPLOY\\SQLEXPRESS;Database=DotCommon;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
        
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Configura a relação para Sender com DeleteBehavior.Restrict
                entity.HasOne(m => m.Sender)
                    .WithMany() 
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);
    
                // Configura a relação para Receiver com DeleteBehavior.Restrict
                entity.HasOne(m => m.Receiver)
                    .WithMany()
                    .HasForeignKey(m => m.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}