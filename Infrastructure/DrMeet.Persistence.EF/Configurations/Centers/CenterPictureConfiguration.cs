

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterPicture"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به تصاویر بارگذاری‌شده مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterPictureConfiguration : IEntityTypeConfiguration<CenterPicture>
{
    public void Configure(EntityTypeBuilder<CenterPicture> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterPictures");

        // کلید اصلی
        builder.HasKey(p => p.Id);

        // مسیر تصویر: الزامی با حداکثر طول 500 کاراکتر
        builder.Property(p => p.Picture)
            .IsRequired()
            .HasMaxLength(500);

        // نوع تصویر: الزامی با حداکثر طول 100 کاراکتر
        builder.Property(p => p.PictureType)
            .IsRequired()
            .HasMaxLength(100);
    }
}
