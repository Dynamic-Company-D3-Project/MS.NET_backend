using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Entities;
using WEBAPI.EntityDTOs;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly ProjectContext DBContext;

        public ProviderController(ProjectContext dbContext)
        {
            this.DBContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable< Provider>>> GetProviders() {
            return await DBContext.Providers.ToListAsync();
        }



        [HttpPost]
        public async Task<ActionResult<Provider>> CreateProvider(NewProviderDTO dto)
        {
            // Create a new provider instance
            var provider = new Provider
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                Gender = dto.Gender,
                LastName = dto.LastName,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                Country = dto.Country,
                IsDeleted = dto.IsDeleted,
                ProviderImagePath = dto.ProviderImagePath,
                ZipCode = dto.ZipCode,
                CreationTime = DateTime.UtcNow // Optionally set creation time
            };

            // Add the provider to the context
            DBContext.Providers.Add(provider);

            // Save changes to the database
            await DBContext.SaveChangesAsync();

            // Return the created provider
            return Ok(provider);
                //CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
        }

        [HttpPut("delete/{id}")]
        public async Task<ActionResult<NewProviderDTO>> DeleteProvider(long id) {
            var provider =await  DBContext.Providers.FindAsync(id);
            if(provider == null) { return NotFound("Provider not found"); }
            provider.IsDeleted = 1;

           await DBContext.SaveChangesAsync();
            return Ok("Provider is deleted");
        }
    }
}
