

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;

/// <summary>
/// پیکربندی Fluent API برای مدل Center.
/// </summary>
public class CenterConfiguration : IEntityTypeConfiguration<Center>
{
    public void Configure(EntityTypeBuilder<Center> builder)
    {
        // تعیین نام جدول
        builder.ToTable("Centers");

        // کلید اصلی
        builder.HasKey(c => c.Id);

        // ویژگی‌های عددی و متنی
        builder.Property(c => c.CenterRemoteId)
            .IsRequired();

        builder.Property(c => c.CenterTypeId)
            .IsRequired();

        builder.Property(c => c.CityId)
            .IsRequired(false);

        builder.Property(c => c.ProvinceId)
            .IsRequired(false);

        builder.Property(c => c.Region)
            .HasMaxLength(100);

        builder.Property(c => c.Bio)
            .HasMaxLength(1000);

        builder.Property(c => c.DateOfEstablishment)
            .IsRequired(false);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.FaxNumber)
            .HasMaxLength(20);

        builder.Property(c => c.WebSite)
            .HasMaxLength(200);

        builder.Property(c => c.Address)
            .HasMaxLength(500);

        builder.Property(c => c.LicensesId)
            .IsRequired(false);

        builder.Property(c => c.Description)
            .HasMaxLength(2000);

        builder.Property(c => c.ClinicId)
            .IsRequired(false);

        builder.Property(c => c.OfficeId)
            .IsRequired(false);

        builder.Property(c => c.TariffExpirationDate)
            .IsRequired();

        // رابطه یک‌به‌چند با CenterUser
        builder.HasMany(c => c.CenterUser)
            .WithOne(cu => cu.Center)
            .HasForeignKey(cu => cu.CenterId)
            .OnDelete(DeleteBehavior.Restrict); // یا Cascade بسته به نیاز
    }
}
