

using DrMeet.Domain.Centers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrMeet.Persistence.EF.Configurations.Centers;



/// <summary>
/// پیکربندی Fluent API برای مدل CenterUser.
/// </summary>
public class CenterUserSelectedConfiguration : IEntityTypeConfiguration<CenterUserSelected>
{
    public void Configure(EntityTypeBuilder<CenterUserSelected> builder)
    {
        // کلید اصلی از کلاس پایه
        builder.HasKey(e => e.Id);
        // تعیین نام جدول
        builder.ToTable("CenterUsersSelected");

        // تعیین کلید اصلی ترکیبی از UserId و CenterId
        builder.HasKey(cu => new { cu.UserId, cu.CenterId });

        // تنظیم رابطه با موجودیت User
        builder.HasOne(cu => cu.User)
            .WithMany(u => u.CenterUser) // فرض بر این است که User دارای ICollection<CenterUser> است
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Restrict); // یا .Cascade بسته به نیاز

        // تنظیم رابطه با موجودیت Center
        builder.HasOne(cu => cu.Center)
            .WithMany(c => c.CenterUser) // فرض بر این است که Center دارای ICollection<CenterUser> است
            .HasForeignKey(cu => cu.CenterId)
            .OnDelete(DeleteBehavior.Restrict); // یا .Cascade بسته به نیاز
    }
}
