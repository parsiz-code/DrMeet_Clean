using DrMeet.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DrMeet.Persistence.EF.Context;

public abstract class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // پیدا کردن همه کلاس‌هایی که از BaseEntity ارث‌بری می‌کنن
        var baseEntityType = typeof(BaseEntity);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (baseEntityType.IsAssignableFrom(entityType.ClrType) && entityType.ClrType != baseEntityType)
            {
                var entity = modelBuilder.Entity(entityType.ClrType);

                entity.Property(nameof(BaseEntity.Deleted))
                      .IsRequired();

                entity.Property(nameof(BaseEntity.CreateDate))
                      .IsRequired()
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(nameof(BaseEntity.UpdateDate))
                      .IsRequired()
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(nameof(BaseEntity.Status))
                      .IsRequired();
            }
        }

    }
    public void Commit()
    {
        SaveChangesAsync();
    }
}
