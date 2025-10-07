using DrMeet.Domain.Base;
using DrMeet.Domain.Patients;
using DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DrMeet.Persistence.EF.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {

        // کلید اصلی از کلاس پایه
        entity.HasKey(e => e.Id);

        // نام جدول
        entity.ToTable("Users");



        // FullName
        entity.Property(e => e.FullName)
              .HasMaxLength(200)
              .IsUnicode()
              .IsRequired(false);

        // Password
        entity.Property(e => e.Password)
              .HasMaxLength(256)
              .IsRequired(false);

        // MobileNumber
        entity.Property(e => e.MobileNumber)
              .HasMaxLength(15)
              .IsRequired(false);

        // Salt
        entity.Property(e => e.Salt)
              .HasMaxLength(128)
              .IsRequired(false);

        // Picture
        entity.Property(e => e.Picture)
              .HasMaxLength(255)
              .IsRequired(false);

        // VerifyCode
        entity.Property(e => e.VerifyCode)
              .HasMaxLength(10)
              .IsRequired(false);

        // VerifyExpire
        entity.Property(e => e.VerifyExpire)
              .IsRequired(false);



        entity
            .HasOne(user => user.Doctor)
            .WithOne(doctor => doctor.User)
            .HasForeignKey<Doctor>(doctor => doctor.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.
            HasOne(p => p.Patient)
           .WithOne(u => u.User)
           .HasForeignKey<Patient>(p => p.UserId)
           .OnDelete(DeleteBehavior.Restrict); 
    }

}


