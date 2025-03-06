using EventRegistration.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(p => p.EventId);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Person)
                .WithMany(p => p.Participants)
                .HasForeignKey(p => p.PersonId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Participants)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Participant>()
                .Property(p => p.PaymentMethod)
                .HasConversion<int>();
        }
    }
}
