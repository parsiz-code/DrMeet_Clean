
namespace DrMeet.Persistence.EF.Configurations.ApplicationSettings;

using global::DrMeet.Domain.ApplicationSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="ApplicationSettingFileUpload"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به محدودیت‌های آپلود فایل را مشخص می‌کند.
/// </summary>
public class ApplicationSettingFileUploadConfiguration : IEntityTypeConfiguration<ApplicationSettingFileUpload>
{
    public void Configure(EntityTypeBuilder<ApplicationSettingFileUpload> builder)
    {
        // تعیین نام جدول
        builder.ToTable("FileUploadSettings");

        // کلید اصلی
        builder.HasKey(f => f.Id);

        // نوع فایل: الزامی (Enum)
        builder.Property(f => f.Type)
            .IsRequired();

        // حداکثر حجم مجاز: الزامی
        builder.Property(f => f.MaximumSize)
            .IsRequired();

        // نمایش کاربرپسند حجم: اختیاری با حداکثر طول 20 کاراکتر
        builder.Property(f => f.MaximumSizeFriendlyName)
            .HasMaxLength(20);

        // شناسه تنظیمات اپلیکیشن: الزامی
        builder.Property(f => f.ApplicationSettingId)
            .IsRequired();

        // لیست پسوندهای مجاز: اختیاری، به صورت JSON ذخیره شود
        //builder.Property(f => f.ValidExtensions)
        //    .HasConversion(
        //        v => string.Join(",", v ?? []),
        //        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        //    )
        //    .HasColumnType("nvarchar(max)");
    }
}

