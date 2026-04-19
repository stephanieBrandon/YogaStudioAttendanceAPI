using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YogaStudioAttendanceAPI.Models
{
    // Minimal read-only reference - full model lives in main app
    [Table("YS_LEAVE_REQUESTS")]
    public class LeaveRequest
    {
        [Key]
        [Column("REQUEST_ID")]
        public int RequestId { get; set; }

        [Column("EMPLOYEE_ID")]
        public int EmployeeId { get; set; }

        [Column("START_DATE")]
        public DateTime StartDate { get; set; }

        [Column("END_DATE")]
        public DateTime EndDate { get; set; }

        [Column("REQUEST_STATUS")]
        public string Status { get; set; } = string.Empty;
    }
}
