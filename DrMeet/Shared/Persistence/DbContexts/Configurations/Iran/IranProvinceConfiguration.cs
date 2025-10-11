

namespace DrMeet.Persistence.EF.Configurations.Iran;

using global::DrMeet.Domain.Iran;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="IranProvince"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول استان‌های ایران را مشخص می‌کند.
/// </summary>
public class IranProvinceConfiguration : IEntityTypeConfiguration<IranProvince>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت IranProvince.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<IranProvince> builder)
    {
    
        // نام جدول
        builder.ToTable("IranProvinces");

        // کلید اصلی
        builder.HasKey(p => p.Id);

        // تنظیم ویژگی Name: الزامی با حداکثر طول ۱۰۰ کاراکتر
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        // رابطه یک‌به‌چند با IranCity
        builder.HasMany(p => p.Cities)
            .WithOne(c => c.Province)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.Cascade); // حذف شهرها هنگام حذف استان

        builder.HasData(
        new IranProvince { Id = 1, Name = "تهران",  },
        new IranProvince { Id = 2, Name = "فارس" },
        new IranProvince { Id = 3, Name = "اصفهان" },
        new IranProvince { Id = 4, Name = "خراسان رضوی" },
        new IranProvince { Id = 5, Name = "آذربایجان شرقی" }
    );
    }
}
