using IStaTP_Lab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IStaTP_Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsChartController : ControllerBase
    {

        private readonly DblibraryContext _context;

        public AuthorsChartController(DblibraryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var authors = _context.Authors.ToList();
            List<object> autBook = new List<object>();
            autBook.Add(new[] { "Автор", "Кількість книжок" });
            foreach (var author in authors)
            {
                autBook.Add(new object[] { author.Name, _context.AuthorBooks.Where(b => b.AuthorId == author.Id).Count() });
            }
            return new JsonResult(autBook);
        }
    }
}
