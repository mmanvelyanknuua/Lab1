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
    public class AuthorBooksController : Controller
    {
        private readonly DblibraryContext _context;

        public AuthorBooksController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: AuthorBooks
        public async Task<IActionResult> Index()
        {
            var dblibraryContext = _context.AuthorBooks.Include(a => a.Author).Include(a => a.Book);
            return View(await dblibraryContext.ToListAsync());
        }
        
        // GET: AuthorBooks
        public async Task<IActionResult> IndexByAuthor(int? authorId, string? name)
        {

            if (authorId == null) return RedirectToAction("Index", "Authors");
            ViewBag.AuthorId = authorId;
            ViewBag.AuthorName = name;

            var dblibraryContext = _context.AuthorBooks.Where(b => b.AuthorId == authorId).Include(a => a.Author).Include(a => a.Book);
            return View(await dblibraryContext.ToListAsync());
        }

        // GET: AuthorBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuthorBooks == null)
            {
                return NotFound();
            }

            var authorBook = await _context.AuthorBooks
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorBook == null)
            {
                return NotFound();
            }

            return View(authorBook);
        }

        // GET: AuthorBooks/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create(int authorId)
        {

			ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", ViewBag.BookId);
			ViewBag.AuthorId = authorId;
            ViewBag.AuthorName = _context.Authors.Where(c => c.Id == authorId).FirstOrDefault()!.Name;
			return View();
        }

        // POST: AuthorBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(int authorId, [Bind("BookId,Id")] AuthorBook authorBook)
        {

			ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", authorBook.BookId);
			authorBook.Author = _context.Authors.Where(c => c.Id == authorId).FirstOrDefault()!;
            authorBook.AuthorId = authorId;
			if (ModelState.IsValid)
            {
                _context.Add(authorBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexByAuthor", "AuthorBooks", new { authorId = authorId, name = authorBook.Author.Name });
            }
            return RedirectToAction("IndexByAuthor", "AuthorBooks", new { authorId = authorId, name = authorBook.Author.Name });
        }

        // GET: AuthorBooks/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuthorBooks == null)
            {
                return NotFound();
            }

            var authorBook = await _context.AuthorBooks.FindAsync(id);
            if (authorBook == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", authorBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", authorBook.BookId);
            return View(authorBook);
        }

        // POST: AuthorBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,AuthorId,Id")] AuthorBook authorBook)
        {
            if (id != authorBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorBookExists(authorBook.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				return RedirectToAction("IndexByAuthor", "AuthorBooks", new { authorId = authorBook.AuthorId, name = _context.Authors.Where(a => a.Id == authorBook.AuthorId).FirstOrDefault()!.Name });
			}
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", authorBook.AuthorId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Name", authorBook.BookId);
			return RedirectToAction("IndexByAuthor", "AuthorBooks", new { authorId = authorBook.AuthorId, name = _context.Authors.Where(a => a.Id == authorBook.AuthorId).FirstOrDefault()!.Name });
		}

        // GET: AuthorBooks/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuthorBooks == null)
            {
                return NotFound();
            }

            var authorBook = await _context.AuthorBooks
                .Include(a => a.Author)
                .Include(a => a.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorBook == null)
            {
                return NotFound();
            }

            return View(authorBook);
        }

        // POST: AuthorBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuthorBooks == null)
            {
                return Problem("Entity set 'DblibraryContext.AuthorBooks'  is null.");
            }
            var authorBook = await _context.AuthorBooks.FindAsync(id);
            if (authorBook != null)
            {
                _context.AuthorBooks.Remove(authorBook);
            }
            
            await _context.SaveChangesAsync();
			return RedirectToAction("IndexByAuthor", "AuthorBooks", new { authorId = authorBook.AuthorId, name = _context.Authors.Where(a => a.Id == authorBook.AuthorId).FirstOrDefault()!.Name });
		}

        private bool AuthorBookExists(int id)
        {
          return (_context.AuthorBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
