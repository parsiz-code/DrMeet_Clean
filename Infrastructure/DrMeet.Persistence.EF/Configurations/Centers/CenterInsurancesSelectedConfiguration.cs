

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;



/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterInsurances"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به بیمه‌های طرف قرارداد مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterInsurancesSelectedConfiguration : IEntityTypeConfiguration<CenterInsurances>
{
    public void Configure(EntityTypeBuilder<CenterInsurances> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterInsurancesSelected");

        // کلید اصلی
        builder.HasKey(ci => ci.Id);

        // شناسه مرکز: اختیاری
        builder.Property(ci => ci.CenterId)
            .IsRequired(false);

        // شناسه بیمه: اختیاری
        builder.Property(ci => ci.InsuranceId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(ci => ci.Center)
            .WithMany(c => c.CenterInsurances)
            .HasForeignKey(ci => ci.CenterId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف مرکز، ارتباط قطع می‌شود اما رکورد باقی می‌ماند

        // رابطه با بیمه
        builder.HasOne(ci => ci.Insurance)
            .WithMany()
            .HasForeignKey(ci => ci.InsuranceId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف بیمه، ارتباط قطع می‌شود

        // ایندکس ترکیبی برای جلوگیری از ثبت بیمه تکراری در یک مرکز
        builder.HasIndex(ci => new { ci.CenterId, ci.InsuranceId })
            .IsUnique();
    }
}
