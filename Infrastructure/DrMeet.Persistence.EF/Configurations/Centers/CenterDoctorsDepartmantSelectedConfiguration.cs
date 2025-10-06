

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterDoctorsDepartmantSelected"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به ارتباط پزشک منتخب با بخش درمانی مرکز را مشخص می‌کند.
/// </summary>
public class CenterDoctorsDepartmantSelectedConfiguration : IEntityTypeConfiguration<CenterDoctorsDepartmantSelected>
{
    public void Configure(EntityTypeBuilder<CenterDoctorsDepartmantSelected> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterDoctorsDepartmantSelected");

        // کلید اصلی
        builder.HasKey(cd => cd.Id);

        // شناسه پزشک منتخب در مرکز: اختیاری
        builder.Property(cd => cd.CenterDoctorId)
            .IsRequired(false);

        // شناسه بخش درمانی مرکز: اختیاری
        builder.Property(cd => cd.CenterDepartmentId)
            .IsRequired(false);

        // رابطه با پزشک منتخب مرکز
        builder.HasOne(cd => cd.Center)
            .WithMany(c => c.CenterDoctorsDepartmant)
            .HasForeignKey(cd => cd.CenterDoctorId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف پزشک، ارتباط قطع می‌شود

        // رابطه با بخش درمانی مرکز
        builder.HasOne(cd => cd.CenterDepartment)
            .WithMany(d => d.CenterDoctorsDepartmantSelected)
            .HasForeignKey(cd => cd.CenterDepartmentId)
            .OnDelete(DeleteBehavior.SetNull); // در صورت حذف بخش، ارتباط قطع می‌شود

        // ایندکس ترکیبی برای جلوگیری از ثبت نگاشت تکراری پزشک-بخش
        builder.HasIndex(cd => new { cd.CenterDoctorId, cd.CenterDepartmentId })
            .IsUnique();
    }
}
