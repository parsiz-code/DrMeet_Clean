using DrMeet.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="DoctorExpertise"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به تخصص‌های ثبت‌شده برای پزشکان را مشخص می‌کند.
/// </summary>
public class DoctorExpertiseConfiguration : IEntityTypeConfiguration<DoctorExpertise>
{
    public void Configure(EntityTypeBuilder<DoctorExpertise> builder)
    {
        // تعیین نام جدول در دیتابیس
        builder.ToTable("DoctorExpertises");

        // کلید اصلی جدول
        builder.HasKey(e => e.Id);

        // شناسه پزشک: الزامی
        builder.Property(e => e.DoctorId)
            .IsRequired();

        // شناسه تخصص: الزامی
        builder.Property(e => e.ExpertiseId)
            .IsRequired();

        // رابطه با پزشک
        builder.HasOne(e => e.Doctor)
            .WithMany(d => d.DoctorExpertises)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // رابطه با تخصص
        builder.HasOne(e => e.Expertise)
            .WithMany(x => x.DoctorExpertises)
            .HasForeignKey(e => e.ExpertiseId)
            .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت تخصص تکراری برای یک پزشک
        builder.HasIndex(e => new { e.DoctorId, e.ExpertiseId })
            .IsUnique();
    }
}
