using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YogaStudioAttendanceAPI.Models
{
    //minimal read-only reference - full model lives in main app
    [Table("YS_EMPLOYEES")]
    public class Employee
    {
        [Key]
        [Column("EMPLOYEE_ID")]
        public int EmployeeId { get; set; }
    }
}
