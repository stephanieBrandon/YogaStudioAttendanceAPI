using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YogaStudioAttendanceAPI.Constants;
using YogaStudioAttendanceAPI.Data;
using YogaStudioAttendanceAPI.Models;
using YogaStudioAttendanceAPI.Helpers;

namespace YogaStudioAttendanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] //JWT required on all endpoints
    public class AttendanceController : Controller
    {
        private readonly AttendanceDbContext _context;

        public AttendanceController(AttendanceDbContext context)
        {
            _context = context;
        }

        [HttpPost("clockin")]
        public async Task<IActionResult> ClockIn()
        {
            var employeeId = GetEmployeeIdFromToken();

            if (employeeId == null)
            {
                return Unauthorized("Invalid token.");
            }

            var today = DateHelper.Today;

            //check if record already exists for today
            var existing = await _context.Attendances
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date == today);

            if (existing != null)
            {
                //alredy clocked in
                if (existing.ClockIn != null)
                {
                    return BadRequest("You have already clocked in today.");
                }

                //on leave = cannot clock in 
                if (existing.Status == AttendanceStatus.ON_LEAVE)
                {
                    return BadRequest("You ara on approved leave today.");
                }

                //record exists but no clock in yet - update it
                existing.ClockIn = DateHelper.Now;
                existing.Status = AttendanceStatus.PRESENT;
            }
            else
            {
                //no record yet, create one now
                var record = new Attendance
                {
                    EmployeeId = employeeId.Value,
                    Date = today,
                    ClockIn = DateHelper.Now,
                    Status = AttendanceStatus.PRESENT
                };

                _context.Attendances.Add(record);
            }
            await _context.SaveChangesAsync();
            return Ok("Clocked in successfully.");
        }

        [HttpPost("clockout")]
        public async Task<IActionResult> ClockOut()
        {
            var employeeId = GetEmployeeIdFromToken();
            if (employeeId == null)
            {
                return Unauthorized("Invalid token.");
            }

            var today = DateHelper.Today;

            var existing = await _context.Attendances
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date == today);

            //cannot clock out if never clocked in
            if (existing == null || existing.ClockIn == null)
            {
                return BadRequest("You have not clocked in today.");
            }

            //cannot clock out twice
            if (existing.ClockOut != null)
                return BadRequest("You have already clocked out today.");

            existing.ClockOut = DateHelper.Now;

            //calculate total hours between clock in and clock out
            existing.TotalHours = Math.Round(
                (existing.ClockOut.Value - existing.ClockIn.Value).TotalHours, 2);

            await _context.SaveChangesAsync();
            return Ok("Clocked out successfully.");
        }

        //reads the employee id out of the JWT claims
        private int? GetEmployeeIdFromToken()
        {
            var claim = User.FindFirst("EmployeeId")?.Value;
            if (int.TryParse(claim, out var id))
            {
                return id;
            }
            return null;
        }
    }
}
