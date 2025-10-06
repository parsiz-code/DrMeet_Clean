

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterPictureSelected"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به تصویر انتخاب‌شده برای نمایش یا استفاده اصلی در مرکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterPictureSelectedConfiguration : IEntityTypeConfiguration<CenterPictureSelected>
{
    public void Configure(EntityTypeBuilder<CenterPictureSelected> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterPictureSelected");

        // کلید اصلی
        builder.HasKey(s => s.Id);

        // شناسه مرکز: اختیاری
        builder.Property(s => s.CenterId)
            .IsRequired(false);

        // شناسه تصویر: اختیاری
        builder.Property(s => s.CenterPictureId)
            .IsRequired(false);

        // رابطه با مرکز درمانی
        builder.HasOne(s => s.Center)
            .WithMany(c => c.CenterPictureSelected)
            .HasForeignKey(s => s.CenterId)
            .OnDelete(DeleteBehavior.SetNull);

        // رابطه با تصویر انتخاب‌شده
        builder.HasOne(s => s.CenterPicture)
            .WithMany(c => c.CenterPictureSelected)
            .HasForeignKey(s => s.CenterPictureId)
            .OnDelete(DeleteBehavior.SetNull);

        // ایندکس یکتا برای جلوگیری از انتخاب چند تصویر همزمان برای یک مرکز
        builder.HasIndex(s => s.CenterId)
            .IsUnique();
    }
}
