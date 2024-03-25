using HRISSystemBackend.Data;
using HRISSystemBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRISSystemBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<DataTableResponse>> GetAllPositions()
        {
            var pos = await _context.Positions.ToListAsync();
            return new DataTableResponse
            {
                RecordsTotal = 1,
                RecordsFiltered = 0,
                Data = pos.ToArray()

            };

        }
    }
}
