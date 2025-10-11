namespace DrMeet.Persistence.EF.Configurations.Others;

using global::DrMeet.Domain.Others;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// پیکربندی Fluent API برای مدل Expertise.
/// </summary>
public class ExpertiseConfiguration : IEntityTypeConfiguration<Expertise>
{
    public void Configure(EntityTypeBuilder<Expertise> builder)
    {
        // تعیین نام جدول
        builder.ToTable("Expertises");

        // کلید اصلی از کلاس پایه
        builder.HasKey(e => e.Id);

        // تنظیم ویژگی Name
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100); // محدودیت طول برای جلوگیری از داده‌های غیرمجاز

        // ایندکس یکتا برای جلوگیری از ثبت تخصص‌های تکراری
        builder.HasIndex(e => e.Name)
            .IsUnique();
    }
}
