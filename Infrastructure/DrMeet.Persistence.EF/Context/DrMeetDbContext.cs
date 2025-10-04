using DrMeet.Domain.Centers;
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
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Center> Centers { get; set; }
    public DbSet<CenterUser> CenterUsers { get; set; }
    #endregion


}
