

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterDoctorsServiceSelected"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به خدماتی که توسط پزشک منتخب در مرکز درمانی ارائه می‌شود را مشخص می‌کند.
/// </summary>
public class CenterDoctorsServiceSelectedConfiguration : IEntityTypeConfiguration<CenterDoctorsServiceSelected>
{
    public void Configure(EntityTypeBuilder<CenterDoctorsServiceSelected> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterDoctorsServiceSelected");

        // کلید اصلی
        builder.HasKey(cds => cds.Id);

        // شناسه پزشک منتخب مرکز: اختیاری
        builder.Property(cds => cds.CenterId)
            .IsRequired(false);

        // شناسه خدمت درمانی: اختیاری
        builder.Property(cds => cds.ProviderServiceId)
            .IsRequired(false);

        // رابطه با پزشک منتخب مرکز
        builder.HasOne(cds => cds.Center)
            .WithMany(c => c.CenterDoctorsServiceSelected)
            .HasForeignKey(cds => cds.CenterId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف پزشک، ارتباط قطع می‌شود

        // رابطه با پزشک منتخب مرکز
        builder.HasOne(cds => cds.Doctor)
            .WithMany(c => c.CenterDoctorsServiceSelected)
            .HasForeignKey(cds => cds.DoctorId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف پزشک، ارتباط قطع می‌شود


        // رابطه با خدمت درمانی
        builder.HasOne(cds => cds.ProviderService)
            .WithMany(p => p.CenterDoctorsService)
            .HasForeignKey(cds => cds.ProviderServiceId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف خدمت، ارتباط قطع می‌شود

        // ایندکس ترکیبی برای جلوگیری از ثبت نگاشت تکراری پزشک-خدمت
        builder.HasIndex(cds => new { cds.CenterId,cds.DoctorId, cds.ProviderServiceId })
            .IsUnique();
    }
}
