using EventRegistration.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event?> Events { get; set; }
        public DbSet<Participant?> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(p => p.EventId);

            modelBuilder.Entity<Participant>()
                .Property(p => p.PaymentMethod)
                .HasConversion<int>();
        }
    }
}
