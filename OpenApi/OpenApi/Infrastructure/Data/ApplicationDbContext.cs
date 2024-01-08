using Microsoft.EntityFrameworkCore;
using OpenApi.Core.Entities;
using OpenApi.Core.Time;

namespace OpenApi.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    private readonly IClock _clock;
    public DbSet<User> Users { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IClock clock) : base(options)
    {
        _clock = clock;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("User");
            user.HasKey(u => u.Id);
            
            user.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        });
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: User, State: EntityState.Added });

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((User)entityEntry.Entity).CreatedAt = _clock.Current();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}