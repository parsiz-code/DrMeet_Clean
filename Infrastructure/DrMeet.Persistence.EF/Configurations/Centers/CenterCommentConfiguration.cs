

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterComment"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به نظرات ثبت‌شده برای مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterCommentConfiguration : IEntityTypeConfiguration<CenterComment>
{
    public void Configure(EntityTypeBuilder<CenterComment> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterComments");

        // شناسه مرکز: الزامی
        builder.Property(c => c.CenterId)
            .IsRequired();

        // رابطه با مرکز درمانی
        builder.HasOne(c => c.Center)
            .WithMany(center => center.CenterComment)
            .HasForeignKey(c => c.CenterId)
            .OnDelete(DeleteBehavior.Cascade);


        // کلید اصلی
        builder.HasKey(c => c.Id);

        // نام ثبت‌کننده نظر: الزامی با حداکثر طول 100 کاراکتر
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        // موضوع نظر: الزامی با حداکثر طول 200 کاراکتر
        builder.Property(c => c.Subject)
            .IsRequired()
            .HasMaxLength(200);

        // ایمیل: الزامی با حداکثر طول 150 کاراکتر
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(150);

        // متن نظر: الزامی با حداکثر طول 2000 کاراکتر
        builder.Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(2000);

        // امتیاز: الزامی
        builder.Property(c => c.Score)
            .IsRequired();

        // شناسه کاربر: الزامی
        builder.Property(c => c.UserId)
            .IsRequired();

        // رابطه با کاربر
        builder.HasOne(c => c.User)
            .WithMany(u => u.CenterComments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
