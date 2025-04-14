using EventRegistration.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistration.Infra;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPH (Table-per-Hierarchy) inheritance for Participant
        modelBuilder.Entity<Participant>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Person>("Person")
            .HasValue<Company>("Company");

        // Composite key: EventParticipant
        modelBuilder.Entity<EventParticipant>()
            .HasKey(ep => new { ep.EventId, ep.ParticipantId });

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(ep => ep.EventId);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Participant)
            .WithMany(p => p.EventParticipants)
            .HasForeignKey(ep => ep.ParticipantId);

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.PaymentMethod)
            .WithMany()
            .HasForeignKey(ep => ep.PaymentMethodId);

        // Seed default payment methods
        modelBuilder.Entity<PaymentMethod>().HasData(
            new PaymentMethod
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Pangaülekanne"
            },
            new PaymentMethod
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Sularaha"
            }
        );
    }
}