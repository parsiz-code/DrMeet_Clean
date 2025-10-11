namespace DrMeet.Persistence.EF.Configurations.Others;

using global::DrMeet.Domain.Others;
using global::DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// پیکربندی Fluent API برای مدل Slider.
/// </summary>
public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        // تعیین نام جدول
        builder.ToTable("Sliders");

        // کلید اصلی از کلاس پایه
        builder.HasKey(s => s.Id);

        // تنظیم ویژگی Title
        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200); // محدودیت طول برای عنوان اسلایدر

        // تنظیم ویژگی ImagePath
        builder.Property(s => s.ImagePath)
            .IsRequired()
            .HasMaxLength(500); // مسیر تصویر با محدودیت طول

        // تنظیم ویژگی UserId
        builder.Property(s => s.CenterId)
            .IsRequired();

        // رابطه با موجودیت User
        builder.HasOne(s=>s.Center)
            .WithMany(u=>u.Sliders)
            .HasForeignKey(s => s.CenterId)
            .OnDelete(DeleteBehavior.Restrict); // یا .Cascade بسته به نیاز
    }
}
