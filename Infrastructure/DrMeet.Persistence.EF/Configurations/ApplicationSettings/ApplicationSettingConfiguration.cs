
namespace DrMeet.Persistence.EF.Configurations.ApplicationSettings;

using global::DrMeet.Domain.ApplicationSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="ApplicationSetting"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به نسخه‌های اپلیکیشن و تنظیمات آپلود فایل را مشخص می‌کند.
/// </summary>
public class ApplicationSettingConfiguration : IEntityTypeConfiguration<ApplicationSetting>
{
    public void Configure(EntityTypeBuilder<ApplicationSetting> builder)
    {
        // تعیین نام جدول
        builder.ToTable("ApplicationSetting");

        // کلید اصلی
        builder.HasKey(a => a.Id);

        // نسخه اپلیکیشن: اختیاری با حداکثر طول 50 کاراکتر
        builder.Property(a => a.AppVersion)
            .HasMaxLength(50);

        // نسخه API: اختیاری با حداکثر طول 50 کاراکتر
        builder.Property(a => a.ApiVersion)
            .HasMaxLength(50);

        // رابطه با تنظیمات آپلود فایل
        builder.HasMany(a => a.FileUploadSetting)
            .WithOne(f => f.ApplicationSetting)
            .HasForeignKey(f => f.ApplicationSettingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

