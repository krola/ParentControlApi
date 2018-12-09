using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ParentControlContext : DbContext
{
    private IConfiguration _config;
    public ParentControlContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Timesheet> Timesheets { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<UserSessions> UserSessions { get; set; }

}