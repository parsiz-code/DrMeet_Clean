using DrMeet.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="DoctorShift"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به شیفت‌های کاری پزشکان را مشخص می‌کند.
/// </summary>
public class DoctorShiftConfiguration : IEntityTypeConfiguration<DoctorShift>
{
    public void Configure(EntityTypeBuilder<DoctorShift> builder)
    {
        // تعیین نام جدول
        builder.ToTable("DoctorShifts");

        // کلید اصلی
        builder.HasKey(s => s.Id);

     
        // شناسه مرکز درمانی: اختیاری
        builder.Property(s => s.CenterDoctorsDepartmantId)
            .IsRequired(false);

        // توضیحات شیفت: اختیاری با محدودیت طول
        builder.Property(s => s.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        // زمان شروع شیفت: الزامی با فرمت رشته‌ای
        builder.Property(s => s.StartTime)
            .HasMaxLength(10)
            .IsRequired();

        // زمان پایان شیفت: الزامی با فرمت رشته‌ای
        builder.Property(s => s.EndTime)
            .HasMaxLength(10)
            .IsRequired();

        // میانگین زمان ویزیت: الزامی
        builder.Property(s => s.MeetTime)
            .IsRequired();

        // وضعیت فعال بودن شیفت: الزامی
        builder.Property(s => s.ActivityStatus)
            .HasConversion<int>() // ذخیره به‌صورت عددی
            .IsRequired();

        // نوع شیفت: الزامی
        builder.Property(s => s.ShiftType)
            .HasConversion<int>()
            .IsRequired();

        // روز هفته: الزامی
        builder.Property(s => s.DayOfWeek)
            .HasConversion<int>()
            .IsRequired();

        // ایندکس ترکیبی برای جلوگیری از ثبت شیفت‌های تکراری در یک روز برای یک پزشک
        builder.HasIndex(s => new {  s.CenterDoctorsDepartmantId,s.DayOfWeek, s.StartTime, s.EndTime })
            .IsUnique();


        builder.HasOne(_=>_.CenterDoctorsDepartmant)
            .WithMany(_=>_.DoctorShift)
            .HasForeignKey(s => s.CenterDoctorsDepartmantId)
            .OnDelete(DeleteBehavior.Restrict);

      
    }
}
