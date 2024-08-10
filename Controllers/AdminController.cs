using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Entities;
using WEBAPI.EntityDTOs;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ProjectContext DBContext;

        public AdminController(ProjectContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [Route("providers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvider()
        {
            return await DBContext.Providers.ToListAsync();
        }

     

        [Route("getOrders")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = DBContext.Bookings.Include(b => b.User).Include(b => b.Provider)
                .Select(b => new AllOrdersDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                    BookingTime = b.BookingTime,
                    ProviderId = b.ProviderId,
                    SubcategoryId = b.SubcategoryId,
                    UserId = b.UserId,
                    User = new UserDTO
                    {
                        UserId = b.User.Id,
                        Name = (b.User.FirstName + b.User.LastName),

                    },
                    Provider = new ProviderDTO
                    {
                        ProviderId = b.Provider.Id,
                        Name = b.Provider.FirstName + b.Provider.LastName

                    }
                }).ToListAsync();
            return Ok(bookings);
        }

        [Route("getOrders/ongoing")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetOngoingBookings()
        {
            var bookings = DBContext.Bookings.Include(b => b.User).Include(b => b.Provider)
                .Where(b => b.Status == "ONGOING")
                .Select(b => new AllOrdersDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                    BookingTime = b.BookingTime,
                    ProviderId = b.ProviderId,
                    SubcategoryId = b.SubcategoryId,
                    UserId = b.UserId,
                    User = new UserDTO
                    {
                        UserId = b.User.Id,
                        Name = (b.User.FirstName + b.User.LastName),

                    },
                    Provider = new ProviderDTO
                    {
                        ProviderId = b.Provider.Id,
                        Name = b.Provider.FirstName + b.Provider.LastName

                    }
                }).ToListAsync();
            return Ok(bookings);
        }

        [Route("getOrders/pending")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetPendingBookings()
        {
            var bookings = DBContext.Bookings.Include(b => b.User).Include(b => b.Provider)
                .Where(b => b.Status == "PENDING")
                .Select(b => new AllOrdersDto
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                    BookingTime = b.BookingTime,
                    ProviderId = b.ProviderId,
                    SubcategoryId = b.SubcategoryId,

                    User = new UserDTO
                    {
                        UserId = b.User.Id,
                        Name = (b.User.FirstName + b.User.LastName),

                    },
                    Provider = new ProviderDTO
                    {
                        ProviderId = b.Provider.Id,
                        Name = b.Provider.FirstName + b.Provider.LastName

                    }
                }).ToListAsync();
            return Ok(bookings);
        }

        [HttpPut("getOrders/pending/{id}/set-ongoing")]
        public async Task<IActionResult> MarkBookingAsOngoing(long id)
        {
            // Fetch the booking by ID
            var booking = await DBContext.Bookings.FindAsync(id);


            if (booking == null)
            {
                return NotFound();
            }


            if (booking.Status != "PENDING")
            {
                return BadRequest("Booking status is not pending"); // Return 400 if the status is not "ONGOING"
            }


            booking.Status = "ONGOING";


            await DBContext.SaveChangesAsync();

            return Ok("Booking status updated to COMPLETED."); // Return a 200 response indicating success
        }


        [HttpPut("getOrders/ongoing/{id}/set-complete")]
        public async Task<IActionResult> MarkBookingAsCompleted(long id)
        {
            // Fetch the booking by ID
            var booking = await DBContext.Bookings.FindAsync(id);

          
            if (booking == null)
            {
                return NotFound(); 
            }

           
            if (booking.Status != "ONGOING")
            {
                return BadRequest("Booking status is not ongoing"); // Return 400 if the status is not "ONGOING"
            }

            
            booking.Status = "COMPLETED";

           
            await DBContext.SaveChangesAsync();

            return Ok("Booking status updated to COMPLETED."); // Return a 200 response indicating success
        }


        // POST: AdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
