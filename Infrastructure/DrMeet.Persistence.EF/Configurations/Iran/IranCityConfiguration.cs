

namespace DrMeet.Persistence.EF.Configurations.Iran;

using global::DrMeet.Domain.Iran;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="IranCity"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول شهرهای ایران را مشخص می‌کند.
/// </summary>
public class IranCityConfiguration : IEntityTypeConfiguration<IranCity>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت IranCity.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<IranCity> builder)
    {
        // نام جدول
        builder.ToTable("IranCities");

        // کلید اصلی
        builder.HasKey(c => c.Id);

        // تنظیم ویژگی Name: الزامی با حداکثر طول ۱۰۰ کاراکتر
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        // تنظیم ویژگی ProvinceId: الزامی
        builder.Property(c => c.ProvinceId)
            .IsRequired()
            .HasMaxLength(36); // اگر از GUID استفاده می‌شود

        // رابطه با IranProvince از طریق IranProvinceConfiguration تعریف می‌شود
    }
}
