using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Entities;
using WEBAPI.EntityDTOs;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    [EnableCors]
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
           var totalCount = await DBContext.Bookings.CountAsync();
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
                Pending = counts.FirstOrDefault(c => c.Status == "PENDING")?.Count ?? 0,
                AllOrders = totalCount
            };

            return Ok(result);
        }

        [HttpGet("orders/last-seven-days")]
        public async Task<IActionResult> GetOrdersForLastSevenDays()
        {
            var sevenDaysAgo = DateTime.UtcNow.Date.AddDays(-6);

            var orders = await DBContext.Orders
                .Where(o => o.OrderDate >= sevenDaysAgo)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalOrders = g.Count()
                })
                .ToListAsync();

            // Fill in the missing days
            var result = Enumerable.Range(0, 7)
                .Select(offset => sevenDaysAgo.AddDays(offset))
                .GroupJoin(orders,
                    date => date,
                    order => order.Date,
                    (date, order) => new
                    {
                        Date = date,
                        TotalOrders = order.FirstOrDefault()?.TotalOrders ?? 0
                    })
                .ToList();

            return Ok(result);
        }

    }
}
