namespace DrMeet.Persistence.EF.Configurations.Others;

using global::DrMeet.Domain.Others;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="Insurance"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول شرکت‌های بیمه طرف قرارداد را مشخص می‌کند.
/// شامل نام جدول، کلید اصلی، محدودیت‌های ویژگی‌ها و ترتیب نمایش.
/// </summary>
public class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت Insurance.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
        // تعیین نام جدول در پایگاه داده
        builder.ToTable("Insurances");

        // تعریف کلید اصلی جدول
        builder.HasKey(i => i.Id);

        // تنظیم ویژگی Name: الزامی با حداکثر طول ۲۰۰ کاراکتر
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(200);

        // تنظیم ویژگی Picture: الزامی با حداکثر طول ۳۰۰ کاراکتر
        builder.Property(i => i.Picture)
            .IsRequired()
            .HasMaxLength(300);

        // تنظیم ویژگی Order: عددی الزامی برای مرتب‌سازی
        builder.Property(i => i.Order)
            .IsRequired();

        // تنظیم ویژگی IsBaseInsurance: نوع بیمه (پایه یا تکمیلی)
        builder.Property(i => i.IsBaseInsurance)
            .IsRequired();

        // ایندکس یکتا برای جلوگیری از ثبت بیمه‌های تکراری
        builder.HasIndex(i => i.Name)
            .IsUnique();
    }
}
