using DrMeet.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="DoctorShiftTimeItem"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به بازه‌های زمانی داخل شیفت پزشک را مشخص می‌کند.
/// </summary>
public class DoctorShiftTimeItemConfiguration : IEntityTypeConfiguration<DoctorShiftTimeItem>
{
    public void Configure(EntityTypeBuilder<DoctorShiftTimeItem> builder)
    {
        // تعیین نام جدول در دیتابیس
        builder.ToTable("DoctorShiftTimeItems");

        // کلید اصلی جدول
        builder.HasKey(t => t.Id);

        // زمان شروع بازه: الزامی با محدودیت طول
        builder.Property(t => t.StartTime)
            .HasMaxLength(10)
            .IsRequired();

        // زمان پایان بازه: الزامی با محدودیت طول
        builder.Property(t => t.EndTime)
            .HasMaxLength(10)
            .IsRequired();

        // وضعیت در دسترس بودن بازه: الزامی
        builder.Property(t => t.IsShiftAvailable)
            .IsRequired();

        // شناسه شیفت مرتبط: الزامی
        builder.Property(t => t.DoctorShiftId)
            .IsRequired();

        // رابطه با شیفت پزشک
        builder.HasOne(t => t.DoctorShift)
            .WithMany(s => s.DoctorShiftTimeItems)
            .HasForeignKey(t => t.DoctorShiftId)
            .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت بازه‌های تکراری در یک شیفت
        builder.HasIndex(t => new { t.DoctorShiftId, t.StartTime, t.EndTime })
            .IsUnique();
    }
}
