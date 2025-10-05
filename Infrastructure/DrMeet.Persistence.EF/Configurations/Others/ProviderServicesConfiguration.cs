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
