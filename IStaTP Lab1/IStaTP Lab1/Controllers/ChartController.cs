using IStaTP_Lab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IStaTP_Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DblibraryContext _context;
        
        public ChartController(DblibraryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var categories = _context.Categories.ToList();
            List<object> catBook = new List<object>();
            catBook.Add(new[] {"Категорія", "Кількість книжок"});
            foreach (var category in categories)
            {
                catBook.Add(new object[] {category.Name, _context.Books.Where(b => b.CategoryId == category.Id).Count()});
            }
            return new JsonResult(catBook);
        }
    }
}
