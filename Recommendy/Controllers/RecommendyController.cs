using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Repository;
namespace Recommendy.Controllers
{

  
    [Route("api/[controller]")]
    [ApiController]
    public class _RecommendyController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public _RecommendyController(RepositoryContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult test(Country con)
        {
            Console.WriteLine(con.Name);
            
            _context.Countries.Add(con);
            _context.SaveChanges();
            return Created();
        }
    }
}
