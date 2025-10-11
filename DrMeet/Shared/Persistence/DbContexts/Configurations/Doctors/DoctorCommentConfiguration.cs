using DrMeet.Api.Shared.Helpers;
using DrMeet.Domain.Doctors;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Infrastructure.Persistence.Configurations.Users;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="DoctorComment"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به نظرات ثبت‌شده برای پزشکان را مشخص می‌کند.
/// </summary>
public class DoctorCommentConfiguration : IEntityTypeConfiguration<DoctorComment>
{
    public void Configure(EntityTypeBuilder<DoctorComment> builder)
    {
        // تعیین نام جدول
        builder.ToTable("DoctorComments");

        // کلید اصلی
        builder.HasKey(dc => dc.Id);

        // امتیاز برخورد مناسب: الزامی
        builder.Property(dc => dc.BehaviorScore)
            .IsRequired();

        // امتیاز کیفیت درمان: الزامی
        builder.Property(dc => dc.TreatmentQualityScore)
            .IsRequired();

        // امتیاز صرفه اقتصادی: الزامی
        builder.Property(dc => dc.EconomicEfficiencyScore)
            .IsRequired();

        // امتیاز بهبودی بیمار: الزامی
        builder.Property(dc => dc.RecoveryScore)
            .IsRequired();

        // شناسه پزشک: الزامی
        builder.Property(dc => dc.DoctorId)
            .IsRequired();

        // رابطه با پزشک
        builder.HasOne(dc => dc.Doctor)
            .WithMany(d => d.DoctorComments)
            .HasForeignKey(dc => dc.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت نظر تکراری توسط یک کاربر برای یک پزشک
        builder.HasIndex(dc => new { dc.DoctorId, dc.UserId })
            .IsUnique();
     
    
    }
}
