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



        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await DBContext.Users
                .Select(user => new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.PhoneNumber,
                    user.Gender,
                    user.Age,
                    user.CreationTime,
                    user.LastLoginTime,
                    user.ImagePath,
                    user.IsDeleted
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginDTO loginDto)
        {
            var admin = await DBContext.Admins
                .FirstOrDefaultAsync(a => a.Email == loginDto.Email);

            if (admin == null || admin.Password != loginDto.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Update last login time
            admin.LastLoginTime = DateTime.UtcNow;
            await DBContext.SaveChangesAsync();

            return Ok(admin);
        }


        [Route("getBookings")]
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


        [HttpPut("bookings/{id}/assign")]
        public async Task<IActionResult> UpdateBooking(long id, [FromBody] AssignProviderDTO updateDto)
        {
            // Fetch the booking by ID
            var booking = await DBContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Update booking details
            booking.Status = updateDto.Status;
            booking.ProviderId = updateDto.ProviderId;

            // Save changes
            await DBContext.SaveChangesAsync();

            return Ok("Booking updated successfully.");
        }


        //[HttpPut("getOrders/ongoing/{id}/set-complete")]
        //public async Task<IActionResult> MarkBookingAsCompleted(long id)
        //{
        //    // Fetch the booking by ID
        //    var booking = await DBContext.Bookings.FindAsync(id);


        //    if (booking == null)
        //    {
        //        return NotFound(); 
        //    }


        //    if (booking.Status != "ONGOING")
        //    {
        //        return BadRequest("Booking status is not ongoing"); // Return 400 if the status is not "ONGOING"
        //    }


        //    booking.Status = "COMPLETED";


        //    await DBContext.SaveChangesAsync();

        //    return Ok("Booking status updated to COMPLETED."); // Return a 200 response indicating success
        //}

        [HttpPut("getOrders/ongoing/{id}/set-complete")]
        public async Task<IActionResult> MarkBookingAsCompleted(long id)
        {
            // Fetch the booking by ID
            // Fetch the booking by ID
            var booking = await DBContext.Bookings.FindAsync(id);
            var subcategory = await DBContext.Subcategories.FindAsync(booking.SubcategoryId);

            if (booking == null)
            {
                return NotFound();
            }

            if (booking.Status != "ONGOING")
            {
                return BadRequest("Booking status is not ongoing");
            }

            // Create a new order from the booking information
            var order = new Order
            {
                OrderDate = booking.BookingDate, 
                Description = subcategory.Description, 
                OrderRate = (decimal?)subcategory.Price, 
                Status = "COMPLETED",
                OrderTime = booking.BookingTime, 
                ProviderId = booking.ProviderId,
                SubcategoryId = booking.SubcategoryId,
                UserId = booking.UserId
            };

            // Add the order to the Orders table
            DBContext.Orders.Add(order);

            // Remove the booking from the Bookings table
            DBContext.Bookings.Remove(booking);

            // Save changes to the database
            await DBContext.SaveChangesAsync();

            return Ok("Booking status updated to COMPLETED and moved to Orders.");// Return a 200 response indicating success
        }

        [HttpPut("getOrders/{id}/set-cancelled")]
        public async Task<IActionResult> MarkBookingAsCancelled(long id)
        {
            // Fetch the booking by ID
            var booking = await DBContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound("Booking not found.");
            }


            // Create a new order from the booking information
            var order = new Order
            {
                OrderDate = booking.BookingDate, // Using BookingDate as OrderDate
                Description = null, // Assuming there is no direct description in Booking, set as null or map accordingly
                OrderRate = null, // Assuming no direct rate in Booking, set as null or map accordingly
                Status = "CANCELLED",
                OrderTime = booking.BookingTime, // Using BookingTime as OrderTime
                ProviderId = booking.ProviderId,
                SubcategoryId = booking.SubcategoryId,
                UserId = booking.UserId
            };

            // Add the order to the Orders table
            DBContext.Orders.Add(order);
          

            // Remove the booking from the Bookings table
            DBContext.Bookings.Remove(booking);

            // Save changes to the database
            await DBContext.SaveChangesAsync();

            return Ok("Booking status updated to CANCELLED and moved to Orders.");
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            // Fetch all orders from the database
            var orders = await DBContext.Orders
                                         // Include related entities if needed
                                        .Include(b=>b.Subcategory)
                                        
                                        .ToListAsync();

            // Check if there are any orders
            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found.");
            }

            // Return the list of orders
            return Ok(orders);
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
