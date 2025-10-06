

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterServiceSelected"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به سرویس‌هایی که توسط مراکز درمانی ارائه می‌شوند را مشخص می‌کند.
/// </summary>
public class CenterServiceSelectedConfiguration : IEntityTypeConfiguration<CenterServiceSelected>
{
    public void Configure(EntityTypeBuilder<CenterServiceSelected> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterServicesSelected");

        // کلید اصلی
        builder.HasKey(s => s.Id);

        // شناسه مرکز: اختیاری
        builder.Property(s => s.CenterId)
            .IsRequired(false);

        // شناسه سرویس مرجع: اختیاری
        builder.Property(s => s.ServiceId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(s => s.Center)
            .WithMany(c => c.CenterServices)
            .HasForeignKey(s => s.CenterId)
            .OnDelete(DeleteBehavior.Restrict); // در صورت حذف مرکز، سرویس باقی می‌ماند اما ارتباط قطع می‌شود

        // رابطه با سرویس مرجع (ProviderServices)
        builder.HasOne(s => s.ProviderServices)
            .WithMany()
            .HasForeignKey(s => s.ServiceId)
            .OnDelete(DeleteBehavior.Restrict); // در صورت حذف سرویس مرجع، ارتباط قطع می‌شود

        // ایندکس ترکیبی برای جلوگیری از ثبت نگاشت‌های تکراری
        builder.HasIndex(s => new { s.CenterId, s.ServiceId })
            .IsUnique();
    }
}
