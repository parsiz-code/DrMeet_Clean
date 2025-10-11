

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="Center"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول مراکز درمانی در پایگاه داده را مشخص می‌کند،
/// شامل نام جدول، کلید اصلی، ویژگی‌های متنی و عددی، محدودیت‌ها و روابط با موجودیت‌های مرتبط.
/// </summary>
public class CenterConfiguration : IEntityTypeConfiguration<Center>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت Center.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<Center> builder)
    {
        // تعیین نام جدول در پایگاه داده
        builder.ToTable("Centers");

        // تعریف کلید اصلی جدول
        builder.HasKey(c => c.Id);

        // تنظیم ویژگی‌های عددی و متنی با محدودیت‌های لازم
        builder.Property(c => c.CenterRemoteId)
            .IsRequired(); // شناسه خارجی مرکز

     
        builder.Property(c => c.CenterTypeId)
            .IsRequired(); // نوع مرکز درمانی

        builder.Property(c => c.CityId)
            .IsRequired(false); // شناسه شهر (اختیاری)

        builder.Property(c => c.ProvinceId)
            .IsRequired(false); // شناسه استان (اختیاری)

        builder.Property(c => c.Region)
            .HasMaxLength(100); // منطقه جغرافیایی

        builder.Property(c => c.Bio)
            .HasMaxLength(1000); // معرفی کوتاه مرکز

        builder.Property(c => c.DateOfEstablishment)
            .IsRequired(false); // تاریخ تأسیس (اختیاری)

        builder.Property(c => c.Phone)
            .HasMaxLength(20); // شماره تلفن

        builder.Property(c => c.FaxNumber)
            .HasMaxLength(20); // شماره فکس

        builder.Property(c => c.WebSite)
            .HasMaxLength(200); // آدرس وب‌سایت

        builder.Property(c => c.Address)
            .HasMaxLength(500); // آدرس فیزیکی



        builder.Property(c => c.Description)
            .HasMaxLength(2000); // توضیحات تکمیلی

        builder.Property(c => c.ClinicId)
            .IsRequired(false); // شناسه کلینیک مرتبط

        builder.Property(c => c.OfficeId)
            .IsRequired(false); // شناسه مطب مرتبط

        builder.Property(c => c.TariffExpirationDate)
            .IsRequired(); // تاریخ انقضای تعرفه‌ها

        // تعریف رابطه یک‌به‌چند با CenterUser
        builder.HasMany(c => c.CenterUser)
            .WithOne(cu => cu.Center)
            .HasForeignKey(cu => cu.CenterId)
            .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف کاربران هنگام حذف مرکز

        builder.HasOne(c => c.IranProvince)
            .WithMany(p => p.Centers)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.IranCity)
            .WithMany(p => p.Centers)
            .HasForeignKey(c => c.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
       .HasOne(c => c.CenterLocation)
    .WithOne(cl => cl.Center)
    .HasForeignKey<CenterLocation>(cl => cl.CenterId)
    .OnDelete(DeleteBehavior.Cascade); // یا Restrict بسته به نیاز
    }
}
public class CenterLocationConfiguration : IEntityTypeConfiguration<CenterLocation>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت Center.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<CenterLocation> builder)
    {
        builder.Ignore(_ => _.Location);
    }
}
