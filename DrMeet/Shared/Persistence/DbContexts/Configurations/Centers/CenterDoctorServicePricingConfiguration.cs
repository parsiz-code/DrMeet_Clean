using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterDoctorServicePricing"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به تعرفه‌های پزشک در مراکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterDoctorServicePricingConfiguration : IEntityTypeConfiguration<CenterDoctorServicePricing>
{
    public void Configure(EntityTypeBuilder<CenterDoctorServicePricing> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterDoctorServicePricing");

        // کلید اصلی
        builder.HasKey(p => p.Id);

        // شناسه پزشک: الزامی
        builder.Property(p => p.DoctorId)
            .IsRequired();


        // شناسه خدمت درمانی: الزامی
        builder.Property(p => p.ProviderServicesId)
            .IsRequired();

        // مبلغ تعرفه: الزامی با دقت مالی مناسب
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // درصد پرداختی به پزشک: اختیاری
        builder.Property(p => p.PercentagePayment)
            .HasColumnType("float");

        // رابطه با پزشک
        builder.HasOne(p => p.Doctor)
            .WithMany(d => d.CenterDoctorServicePricing)
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Center)
         .WithMany(d => d.CenterDoctorPricing)
         .HasForeignKey(p => p.CenterId)
         .OnDelete(DeleteBehavior.Cascade);



        // رابطه با خدمت درمانی
        builder.HasOne(p => p.ProviderServices)
            .WithMany(s => s.CenterDoctorPricing)
            .HasForeignKey(p => p.ProviderServicesId)
            .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت تعرفه تکراری برای یک پزشک در یک مرکز برای یک خدمت خاص
        builder.HasIndex(p => new { p.DoctorId,p.CenterId, p.ProviderServicesId })
            .IsUnique();
    }
}


/// <summary>
/// پیکربندی Fluent API برای موجودیت <see cref="CenterDoctorServiceOnlineConsultation"/>.
/// این کلاس تنظیمات نگاشت جدول مربوط به مشاوره آنلاین پزشک برای یک خدمت خاص در یک مرکز درمانی را مشخص می‌کند.
/// </summary>
public class CenterDoctorServiceOnlineConsultationConfiguration : IEntityTypeConfiguration<CenterDoctorServiceOnlineConsultation>
{
    public void Configure(EntityTypeBuilder<CenterDoctorServiceOnlineConsultation> builder)
    {
        // تعیین نام جدول
        builder.ToTable("CenterDoctorServiceOnlineConsultations");

        // کلید اصلی
        builder.HasKey(c => c.Id);

        //// شناسه مرکز درمانی: الزامی
        //builder.Property(c => c.CenterId)
        //    .IsRequired();

        //// شناسه پزشک: الزامی
        //builder.Property(c => c.DoctorId)
        //    .IsRequired();

        // شناسه خدمت درمانی: الزامی
        builder.Property(c => c.ServicesAvailableId)
            .IsRequired();

        // مبلغ مشاوره: الزامی با دقت مالی مناسب
        builder.Property(c => c.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // درصد پرداختی به پزشک: اختیاری
        builder.Property(c => c.PercentagePayment)
            .HasColumnType("float");

        // رابطه با مرکز درمانی
        builder.HasOne(c => c.Center)
            .WithMany(center => center.CenterDoctorServiceOnlineConsultation)
            .HasForeignKey(c => c.CenterId)
            .OnDelete(DeleteBehavior.Cascade);

        // رابطه با مرکز درمانی
        builder.HasOne(c => c.Doctor)
            .WithMany(center => center.CenterDoctorServiceOnlineConsultation)
            .HasForeignKey(c => c.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);


        // رابطه با خدمت درمانی
        builder.HasOne(c => c.ProviderServices)
            .WithMany(service => service.CenterDoctorServiceOnlineConsultations)
            .HasForeignKey(c => c.ServicesAvailableId)
            .OnDelete(DeleteBehavior.Cascade);

        // ایندکس ترکیبی برای جلوگیری از ثبت تکراری مشاوره آنلاین یک خدمت خاص توسط یک پزشک در یک مرکز
        builder.HasIndex(c => new { c.CenterId,c.DoctorId,  c.ServicesAvailableId })
            .IsUnique();
    }
}
