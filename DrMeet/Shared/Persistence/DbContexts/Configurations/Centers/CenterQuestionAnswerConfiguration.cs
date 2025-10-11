

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterQuestionAnswer"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول پرسش و پاسخ مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterQuestionAnswerConfiguration : IEntityTypeConfiguration<CenterQuestionAnswer>
{
    public void Configure(EntityTypeBuilder<CenterQuestionAnswer> builder)
    {
        // نام جدول
        builder.ToTable("CenterQuestionAnswers");

        // کلید اصلی
        builder.HasKey(q => q.Id);

        // شناسه مرکز: الزامی
        builder.Property(q => q.CenterId)
            .IsRequired();

        // شناسه والد: اختیاری
        builder.Property(q => q.ParentId)
            .IsRequired(false);

        // متن سؤال یا پاسخ: الزامی با حداکثر طول ۲۰۰۰ کاراکتر
        builder.Property(q => q.Text)
            .IsRequired()
            .HasMaxLength(2000);

        // نوع آیتم: اختیاری با حداکثر طول ۵۰ کاراکتر
        builder.Property(q => q.Type)
            .HasMaxLength(50);

        // وضعیت فعال بودن: الزامی
        builder.Property(q => q.Status)
            .IsRequired();

        // رابطه با Center
        builder.HasOne(q => q.Center)
            .WithMany(c => c.CenterQuestionAnswer)
            .HasForeignKey(q => q.CenterId)
            .OnDelete(DeleteBehavior.Cascade);

        // رابطه با امتیازهای مثبت و منفی
        builder.HasMany(q => q.PositivePoints)
            .WithOne()
            .HasForeignKey(p => p.CenterQuestionAnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.NegativePoints)
            .WithOne()
            .HasForeignKey(p => p.CenterQuestionAnswerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
