using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YogaStudioAttendanceAPI.Constants;

namespace YogaStudioAttendanceAPI.Models
{
    [Table("YS_ATTENDANCES")]
    public class Attendance
    {
        [Key]
        [Column("ATTENDANCE_ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceId { get; set; }

        [Required]
        [Column("EMPLOYEE_ID")]
        public int EmployeeId { get; set; }

        [Required]
        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("CLOCK_IN")]
        public DateTime? ClockIn { get; set; } //nullable - not clocked in yet

        [Column("CLOCK_OUT")]
        public DateTime? ClockOut { get; set; } //nullable - not clocked out yet

        [Column("TOTAL_HOURS")]
        public double? TotalHours { get; set; } //nullable - calculated after clock out

        [Required]
        [Column("STATUS")]
        [StringLength(10)]
        public string Status { get; set; } = AttendanceStatus.ABSENT; // default: Absent
    }
}
