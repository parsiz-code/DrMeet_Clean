

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای مدل SocialMediaAccount.
/// </summary>
public class SocialMediaAccountConfiguration : IEntityTypeConfiguration<CenterSocialMediaAccount>
{
    public void Configure(EntityTypeBuilder<CenterSocialMediaAccount> builder)
    {
        // نام جدول
        builder.ToTable("SocialMediaAccounts");

        // کلید اصلی
        builder.HasKey(s => s.Id);

        // تنظیم ویژگی CenterId
        builder.Property(s => s.CenterId)
            .IsRequired();

        // تنظیم ویژگی Platform به عنوان enum
        builder.Property(s => s.Platform)
            .IsRequired()
            .HasConversion<int>(); // ذخیره به صورت عددی در دیتابیس

        // تنظیم ویژگی UsernameOrUrl
        builder.Property(s => s.UsernameOrUrl)
            .HasMaxLength(300)
            .IsRequired(false);

        // رابطه با Center
        builder.HasOne(s => s.Center)
            .WithMany(c => c.SocialMediaAccount)
            .HasForeignKey(s => s.CenterId)
            .OnDelete(DeleteBehavior.Restrict); // حذف حساب‌ها هنگام حذف مرکز
    }
}
