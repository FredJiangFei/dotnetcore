using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly TodoContext _context;

        public FileController(TodoContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult BannerImage()
        {
            var dir = Directory.GetCurrentDirectory();
            var file = Path.Combine(dir, "MyStaticFiles", "images", "panda.jpg");
            return PhysicalFile(file, "image/svg+xml");
        }
    }
}