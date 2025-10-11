

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterQuestionAnswerCommentPoints"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول امتیازدهی به پرسش و پاسخ را مشخص می‌کند.
/// </summary>
public class CenterQuestionAnswerCommentPointsConfiguration : IEntityTypeConfiguration<CenterQuestionAnswerCommentPoints>
{
    public void Configure(EntityTypeBuilder<CenterQuestionAnswerCommentPoints> builder)
    {
        // نام جدول
        builder.ToTable("CenterQuestionAnswerCommentPoints");

        // کلید اصلی
        builder.HasKey(p => p.Id);

        // شناسه پرسش یا پاسخ: الزامی
        builder.Property(p => p.CenterQuestionAnswerId)
            .IsRequired();

        // متن پیام: الزامی با حداکثر طول 1000 کاراکتر
        builder.Property(p => p.Message)
            .IsRequired()
            .HasMaxLength(1000);

        // نوع امتیاز: الزامی
        builder.Property(p => p.IsNegativePoints)
            .IsRequired();
    }
}
