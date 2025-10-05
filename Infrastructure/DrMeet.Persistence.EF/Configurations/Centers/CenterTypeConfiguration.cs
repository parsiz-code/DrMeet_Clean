

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای مدل CenterType.
/// </summary>
public class CenterTypeConfiguration : IEntityTypeConfiguration<CenterType>
{
    public void Configure(EntityTypeBuilder<CenterType> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterTypes");

        // کلید اصلی از کلاس پایه
        builder.HasKey(ct => ct.Id);

        // تنظیم ویژگی Name
        builder.Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(100);

        // تنظیم ویژگی Order
        builder.Property(ct => ct.Order)
            .IsRequired();

        // رابطه یک‌به‌چند با Center
        builder.HasMany(ct => ct.Center)
            .WithOne(c => c.CenterType)
            .HasForeignKey(c => c.CenterTypeId)
            .OnDelete(DeleteBehavior.Restrict); // یا .Cascade بسته به نیاز
    }
}
