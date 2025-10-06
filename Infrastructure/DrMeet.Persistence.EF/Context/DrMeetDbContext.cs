using DrMeet.Domain.ApplicationSettings;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Iran;
using DrMeet.Domain.Others;
using DrMeet.Domain.Patient;
using DrMeet.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace DrMeet.Persistence.EF.Context;
public class DrMeetDbContext : ApplicationDbContext
{
    public DrMeetDbContext(DbContextOptions<DrMeetDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    #region Entity  
    public DbSet<ApplicationSetting> ApplicationSetting { get; set; }
    public DbSet<ApplicationSettingFileUpload> ApplicationSettingFileUpload { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Center> Centers { get; set; }
    public DbSet<CenterSocialMediaAccount> CenterSocialMediaAccount { get; set; }
    public DbSet<CenterQuestionAnswerCommentPoints> CenterQuestionAnswerCommentPoints { get; set; }
    public DbSet<CenterDepartment> CenterDepartment { get; set; }
    public DbSet<CenterInsurances> CenterInsurances { get; set; }
    public DbSet<CenterQuestionAnswer> CenterQuestionAnswer { get; set; }
    public DbSet<CenterType> CenterType { get; set; }
    public DbSet<CenterUserSelected> CenterUsers { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Blog> Blog { get; set; }
    public DbSet<BlogComment> BlogComment { get; set; }
    public DbSet<IranCity> IranCity { get; set; }
    public DbSet<IranProvince> IranProvince { get; set; }
    public DbSet<Expertise> Expertise { get; set; }
    public DbSet<Insurance> Insurance { get; set; }
    public DbSet<Licenses> Licenses { get; set; }
    public DbSet<ProviderServices> ProviderServices { get; set; }
    public DbSet<Slider> Slider { get; set; }
    public DbSet<Holidays> Holidays { get; set; }
    #endregion


}
