

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterDepartment"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به بخش‌های داخلی مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterDepartmentConfiguration : IEntityTypeConfiguration<CenterDepartment>
{
    public void Configure(EntityTypeBuilder<CenterDepartment> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterDepartments");

        // کلید اصلی
        builder.HasKey(d => d.Id);

        // نام بخش: الزامی با حداکثر طول 100 کاراکتر
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        // شناسه مرکز: اختیاری
        builder.Property(d => d.CenterId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(d => d.Center)
            .WithMany(c => c.CenterDepartment)
            .HasForeignKey(d => d.CenterId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف مرکز، بخش‌ها باقی می‌مانند اما ارتباط قطع می‌شود
    }
}
