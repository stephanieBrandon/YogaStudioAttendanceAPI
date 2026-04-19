using Microsoft.EntityFrameworkCore;
using YogaStudioAttendanceAPI.Models;

namespace YogaStudioAttendanceAPI.Data
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employees { get; set; }       // read-only reference
        public DbSet<LeaveRequest> LeaveRequests { get; set; } // read-only reference

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //no migrations, no seed data - main app owns the schema
            
            //Employees and LeaveRequests are read-only in this context
            modelBuilder.Entity<Employee>().ToTable("YS_EMPLOYEES");
            modelBuilder.Entity<LeaveRequest>().ToTable("YS_LEAVE_REQUESTS");
            modelBuilder.Entity<Attendance>().ToTable("YS_ATTENDANCES");
        }
    }
}
