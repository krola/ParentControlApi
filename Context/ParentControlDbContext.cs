using Microsoft.EntityFrameworkCore;

public class ParentControlContext : DbContext
{
        public ParentControlContext(DbContextOptions<ParentControlContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserSessions> UserSessions { get; set; }

    }