using Microsoft.EntityFrameworkCore;
using OnTime.Domain.User;

namespace OnTime.Infrastructure.Common.Persistence;

public class OnTimeDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(x => x.Appointments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.User)
            .WithMany(x => x.Appointments)
            .HasForeignKey(x => x.UserId);
    }
}
