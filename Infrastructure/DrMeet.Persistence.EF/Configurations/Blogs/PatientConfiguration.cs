using DrMeet.Domain.Blogs;
using DrMeet.Domain.Patient;
using DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Patients;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="Blog"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول وبلاگ‌ها در پایگاه داده را مشخص می‌کند،
/// شامل نام جدول، کلید اصلی، محدودیت‌های ویژگی‌ها و روابط با موجودیت‌های مرتبط.
/// </summary>
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت Blog.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        // نام جدول در پایگاه داده
        builder.ToTable("Blogs");

        // کلید اصلی جدول
        builder.HasKey(b => b.Id);

        // تنظیم ویژگی Title: الزامی با حداکثر طول ۲۰۰ کاراکتر
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        // تنظیم ویژگی SummaryText: اختیاری با حداکثر طول ۱۰۰۰ کاراکتر
        builder.Property(b => b.SummaryText)
            .HasMaxLength(1000);

        // تنظیم ویژگی ImagePath: الزامی با حداکثر طول ۵۰۰ کاراکتر
        builder.Property(b => b.ImagePath)
            .IsRequired()
            .HasMaxLength(500);

        // تنظیم ویژگی Text: نوع داده nvarchar(max) برای متن کامل مقاله
        builder.Property(b => b.Text)
            .HasColumnType("nvarchar(max)");

        // تنظیم ویژگی Tags: اختیاری با حداکثر طول ۳۰۰ کاراکتر
        builder.Property(b => b.Tags)
            .HasMaxLength(300);

        // تنظیم ویژگی UserId: الزامی
        builder.Property(b => b.UserId)
            .IsRequired();

        // رابطه یک‌به‌چند با موجودیت User
        builder.HasOne(b => b.User)
            .WithMany(u => u.Blogs)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف مقاله هنگام حذف کاربر

        // رابطه یک‌به‌چند با BlogComment
        builder.HasMany(b => b.Comments)
            .WithOne(c => c.Blog)
             .HasForeignKey(b => b.BlogId)
            .OnDelete(DeleteBehavior.Cascade); // حذف نظرات هنگام حذف مقاله
    }
}
/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="BlogComment"/>.
/// این کلاس تنظیمات مربوط به نگاشت جدول نظرات وبلاگ در پایگاه داده را مشخص می‌کند،
/// شامل نام جدول، کلید اصلی، محدودیت‌های ویژگی‌ها و رابطه با موجودیت User.
/// </summary>
public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
{
    /// <summary>
    /// اعمال تنظیمات Fluent API برای موجودیت BlogComment.
    /// </summary>
    /// <param name="builder">ابزار پیکربندی موجودیت.</param>
    public void Configure(EntityTypeBuilder<BlogComment> builder)
    {
        // نام جدول در پایگاه داده
        builder.ToTable("BlogComments");

        // کلید اصلی جدول
        builder.HasKey(c => c.Id);

        // تنظیم ویژگی Name: الزامی با حداکثر طول ۱۰۰ کاراکتر
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        // تنظیم ویژگی Subject: الزامی با حداکثر طول ۲۰۰ کاراکتر
        builder.Property(c => c.Subject)
            .IsRequired()
            .HasMaxLength(200);

        // تنظیم ویژگی Email: اختیاری با حداکثر طول ۱۵۰ کاراکتر
        builder.Property(c => c.Email)
            .HasMaxLength(150);

        // تنظیم ویژگی Text: الزامی با نوع داده nvarchar(max)
        builder.Property(c => c.Text)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        // تنظیم ویژگی Score: امتیاز عددی الزامی
        builder.Property(c => c.Score)
            .IsRequired();

        // تنظیم ویژگی UserId: الزامی
        builder.Property(c => c.UserId)
            .IsRequired();

        // رابطه یک‌به‌چند با موجودیت User
        builder.HasOne(c => c.User)
            .WithMany(u => u.BlogComments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict); // جلوگیری از حذف نظر هنگام حذف کاربر
    }
}
