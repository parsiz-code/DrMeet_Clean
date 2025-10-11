using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMeet.Persistence.EF.Configurations.Others;

using global::DrMeet.Domain.Others;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



/// <summary>
/// پیکربندی Fluent API برای مدل ProviderServices.
/// </summary>
public class ProviderServicesConfiguration : IEntityTypeConfiguration<ProviderServices>
{
    public void Configure(EntityTypeBuilder<ProviderServices> builder)
    {
        // تعیین نام جدول
        builder.ToTable("ProviderServices");

        // کلید اصلی از کلاس پایه
        builder.HasKey(ps => ps.Id);

        // تنظیم ویژگی Name
        builder.Property(ps => ps.Name)
            .IsRequired()
            .HasMaxLength(100); // محدودیت طول برای جلوگیری از داده‌های غیرمجاز

        // تنظیم ویژگی Order
        builder.Property(ps => ps.Order)
            .IsRequired(); // ترتیب نمایش خدمت در لیست‌ها
    }
}

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="Holidays"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول روزهای تعطیل در پایگاه داده را مشخص می‌کند.
/// </summary>
public class HolidaysConfiguration : IEntityTypeConfiguration<Holidays>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت Holidays.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<Holidays> builder)
    {
        // تعیین نام جدول
        builder.ToTable("Holidays");

        // تعریف کلید اصلی
        builder.HasKey(h => h.Id);

        // تنظیم ویژگی Description: الزامی با حداکثر طول ۵۰۰ کاراکتر
        builder.Property(h => h.Description)
            .IsRequired()
            .HasMaxLength(500);

        // تنظیم ویژگی Date: الزامی
        builder.Property(h => h.Date)
            .IsRequired();

        // ایندکس یکتا برای جلوگیری از ثبت تعطیلی‌های تکراری در یک تاریخ
        builder.HasIndex(h => h.Date)
            .IsUnique();
    }
}
