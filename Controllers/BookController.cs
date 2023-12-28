using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(ApplicationDbContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Book
        public IActionResult Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.BookCategory)
                .Include(b => b.Publisher).ToList();
            return View(applicationDbContext);
     
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book
                .Include(b => b.BookCategory)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["BookTypeId"] = new SelectList(_context.BookCategory, "Id", "Name");
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,Price,Available,PublisherId,BookTypeId")] BookModel bookModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if(file != null && file.Length >0)
                {
                    fileName = file.FileName;
                    var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                    file.CopyTo(new FileStream(path, FileMode.Create));
                }
                bookModel.FileName = fileName;
                _context.Add(bookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookTypeId"] = new SelectList(_context.BookCategory, "Id", "Name", bookModel.BookTypeId);
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name", bookModel.PublisherId);
            return View(bookModel);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }
            ViewData["BookTypeId"] = new SelectList(_context.BookCategory, "Id", "Name", bookModel.BookTypeId);
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name", bookModel.PublisherId);
            return View(bookModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,BookModel bookModel, IFormFile file)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (file != null && file.Length > 0)
                    {
                        fileName = file.FileName;
                        var path = Path.Combine(_env.WebRootPath, "uploads", file.FileName);
                        file.CopyTo(new FileStream(path, FileMode.Create));
                    }
                    bookModel.FileName = fileName;
                    _context.Update(bookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookTypeNaId"] = new SelectList(_context.BookCategory, "Id", "Name", bookModel.BookTypeId);
            ViewData["PublisherId"] = new SelectList(_context.Publisher, "Id", "Name", bookModel.PublisherId);
            return View(bookModel);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book
                .Include(b => b.BookCategory)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.Book.FindAsync(id);
            _context.Book.Remove(bookModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
