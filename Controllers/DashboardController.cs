using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Entities;
using WEBAPI.EntityDTOs;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ProjectContext DBContext;

        public DashboardController(ProjectContext DBContext)
        {
            this.DBContext = DBContext;
        }


        [HttpGet("order-status-counts")]
        public async Task<ActionResult<DashboardCountDTO>> GetOrderStatusCounts()
        {
            // Aggregate counts by status
            var counts = await DBContext.Bookings
                .GroupBy(b => b.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // Map to DTO
            var result = new DashboardCountDTO
            {
                Completed = counts.FirstOrDefault(c => c.Status == "COMPLETED")?.Count ?? 0,
                Ongoing = counts.FirstOrDefault(c => c.Status == "ONGOING")?.Count ?? 0,
                Pending = counts.FirstOrDefault(c => c.Status == "PENDING")?.Count ?? 0
            };

            return Ok(result);
        }
    }
}
