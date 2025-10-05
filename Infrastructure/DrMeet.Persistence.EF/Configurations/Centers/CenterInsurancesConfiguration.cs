

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterInsurances"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به بیمه‌های طرف قرارداد مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterInsurancesConfiguration : IEntityTypeConfiguration<CenterInsurances>
{
    public void Configure(EntityTypeBuilder<CenterInsurances> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterInsurances");

        // کلید اصلی
        builder.HasKey(i => i.Id);

        // نام بیمه: الزامی با حداکثر طول 100 کاراکتر
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        // شناسه مرکز: اختیاری
        builder.Property(i => i.CenterId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(i => i.Center)
            .WithMany(c => c.CenterInsurances)
            .HasForeignKey(i => i.CenterId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف مرکز، بیمه‌ها باقی می‌مانند اما ارتباط قطع می‌شود

        // ایندکس ترکیبی برای جلوگیری از ثبت بیمه تکراری در یک مرکز
        builder.HasIndex(i => new { i.Name, i.CenterId })
            .IsUnique();
    }
}
