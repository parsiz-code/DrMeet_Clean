using DrMeet.Domain.Patients;
using DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Patients;

/// <summary>
/// پیکربندی Fluent API برای مدل Patient.
/// </summary>
public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // 📌 استفاده از روش Table-per-Type (TPT)
        // این خط مشخص می‌کند که کلاس Patient در جدول جداگانه‌ای به نام "Patients" ذخیره شود.
        builder.ToTable("Patients");

        // 🔑 تعریف کلید اصلی
        // کلید اصلی از کلاس پایه BaseEntityPerson به ارث رسیده و به عنوان شناسه اصلی جدول استفاده می‌شود.
        builder.HasKey(p => p.Id);

        // ⚙️ تنظیم ویژگی‌های عددی و متنی

        // شناسه کاربری مرتبط با بیمار، برای اتصال به موجودیت User استفاده می‌شود.
        builder.Property(p => p.UserId)
            .IsRequired();

        // شناسه بیمار در سیستم‌های خارجی (اختیاری)
        builder.Property(p => p.PatientRemoteId)
            .IsRequired(false);

        //// شناسه مرکز درمانی که بیمار در آن ثبت شده است (اختیاری)
        //builder.Property(p => p.CenterId)
        //    .IsRequired(false);

        // مسیر تصویر پروفایل بیمار، با محدودیت طول ۳۰۰ کاراکتر
        builder.Property(p => p.Picture)
            .HasMaxLength(300)
            .IsRequired();

 

        // 🔗 تعریف رابطه یک‌به‌یک با موجودیت User
        // مشخص می‌کند که هر Patient دقیقاً یک User دارد و کلید خارجی در جدول Patient قرار دارد.
        // رفتار حذف را می‌توان با Restrict یا Cascade تنظیم کرد.
        builder.HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict); // یا .Cascade بسته به نیاز


        builder.HasOne(p => p.IranProvince)
            .WithMany(p => p.Patients)
            .HasForeignKey(p => p.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p =>p.IranCity)
            .WithMany(p => p.Patients)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(p => p.Insurance)
         .WithMany(p => p.Patients)
         .HasForeignKey(p => p.InsuranceId)
         .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(p => p.Insurance)
         .WithMany(p => p.Patients)
         .HasForeignKey(p => p.InsuranceId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
