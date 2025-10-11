

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

 
        builder.HasData(
        new IranCity { Id = 1, Name = "تهران", ProvinceId = 1},
        new IranCity { Id = 2, Name = "شیراز", ProvinceId = 2 },
        new IranCity { Id = 3, Name = "اصفهان", ProvinceId = 3 },
        new IranCity { Id = 4, Name = "مشهد", ProvinceId = 4 },
        new IranCity { Id = 5, Name = "تبریز", ProvinceId = 5 },
        new IranCity { Id = 6, Name = "ورامین", ProvinceId = 1 },
        new IranCity { Id = 7, Name = "مرودشت", ProvinceId = 2 },
        new IranCity { Id = 8, Name = "کاشان", ProvinceId = 3},
        new IranCity { Id = 9, Name = "نیشابور", ProvinceId = 4 },
        new IranCity { Id = 10, Name = "مراغه", ProvinceId = 5 }
    );
    }
}
