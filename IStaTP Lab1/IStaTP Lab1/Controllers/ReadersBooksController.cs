using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IStaTP_Lab1.Models;
using Microsoft.AspNetCore.Authorization;

namespace IStaTP_Lab1.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReadersBooksController : Controller
    {
        private readonly DblibraryContext _context;

        public ReadersBooksController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: ReadersBooks
        public async Task<IActionResult> Index()
        {
            var dblibraryContext = _context.ReadersBooks.Include(r => r.Book).Include(r => r.Reader).Include(r => r.Status);
            return View(await dblibraryContext.ToListAsync());
        }

        public async Task<IActionResult> IndexByReader(int? readerId, string? name)
        {

            if (readerId == null) return RedirectToAction("Index", "Readers");
            ViewBag.ReaderId = readerId;
            ViewBag.ReaderName = name;

            var dblibraryContext = _context.ReadersBooks.Where(b => b.ReaderId == readerId).Include(a => a.Reader).Include(a => a.Book).Include(a => a.Status) ;
            return View(await dblibraryContext.ToListAsync());
        }

        // GET: ReadersBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReadersBooks == null)
            {
                return NotFound();
            }

            var readersBook = await _context.ReadersBooks
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readersBook == null)
            {
                return NotFound();
            }

            return View(readersBook);
        }

        // GET: ReadersBooks/Create
        public IActionResult Create(int readerId)
        {
            ViewData["BookId"] = new SelectList(_context.Books.OrderBy(l => l.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewData["ReaderId"] = new SelectList(_context.Readers.OrderBy(l => l.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewData["StatusId"] = new SelectList(_context.Statuses.OrderBy(l => l.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.ReaderId = readerId;
            ViewBag.ReaderName = _context.Readers.Where(c => c.Id == readerId).FirstOrDefault()!.Name;
            return View();
        }

        // POST: ReadersBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int readerId, [Bind("Id,BookId,Issue,PlanReturn,StatusId,FactReturn")] ReadersBook readersBook)
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", readersBook.BookId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersBook.StatusId);
            readersBook.Reader = _context.Readers.Where(c => c.Id == readerId).FirstOrDefault()!;
            readersBook.ReaderId = readerId;
            if (ModelState.IsValid)
            {
                _context.Add(readersBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexByReader", "ReadersBooks", new { readerId = readerId, name = readersBook.Reader.Name });
            }
            return RedirectToAction("IndexByReader", "ReadersBooks", new { readerId = readerId, name = readersBook.Reader.Name });
        }

        // GET: ReadersBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReadersBooks == null)
            {
                return NotFound();
            }

            var readersBook = await _context.ReadersBooks.FindAsync(id);
            if (readersBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", readersBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", readersBook.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersBook.StatusId);
            return View(readersBook);
        }

        // POST: ReadersBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReaderId,BookId,Issue,PlanReturn,StatusId,FactReturn")] ReadersBook readersBook)
        {
            if (id != readersBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(readersBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReadersBookExists(readersBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexByReader", "ReadersBooks", new { readerId = readersBook.ReaderId, name = _context.Readers.Where(a => a.Id == readersBook.ReaderId).FirstOrDefault()!.Name });
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", readersBook.BookId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "Name", readersBook.ReaderId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", readersBook.StatusId);
            return RedirectToAction("IndexByReader", "ReadersBooks", new { readerId = readersBook.ReaderId, name = _context.Readers.Where(a => a.Id == readersBook.ReaderId).FirstOrDefault()!.Name });
        }

        // GET: ReadersBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReadersBooks == null)
            {
                return NotFound();
            }

            var readersBook = await _context.ReadersBooks
                .Include(r => r.Book)
                .Include(r => r.Reader)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (readersBook == null)
            {
                return NotFound();
            }

            return View(readersBook);
        }

        // POST: ReadersBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReadersBooks == null)
            {
                return Problem("Entity set 'DblibraryContext.ReadersBooks'  is null.");
            }
            var readersBook = await _context.ReadersBooks.FindAsync(id);
            if (readersBook != null)
            {
                _context.ReadersBooks.Remove(readersBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexByReader", "ReadersBooks", new { readerId = readersBook.ReaderId, name = _context.Readers.Where(a => a.Id == readersBook.ReaderId).FirstOrDefault()!.Name });
        }

        private bool ReadersBookExists(int id)
        {
          return (_context.ReadersBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
