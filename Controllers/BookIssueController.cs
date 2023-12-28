using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BookIssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookIssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookIssue
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookIssue.Include(b => b.Book).Include(b => b.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookIssue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookIssueModel = await _context.BookIssue
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookIssueModel == null)
            {
                return NotFound();
            }

            return View(bookIssueModel);
        }

        // GET: BookIssue/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: BookIssue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,MemberId,IssueDate,ExpectedReturnDate,ActualRetuerndate")] BookIssueModel bookIssueModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookIssueModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookIssueModel.BookId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookIssueModel.MemberId);
            return View(bookIssueModel);
        }

        // GET: BookIssue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookIssueModel = await _context.BookIssue.FindAsync(id);
            if (bookIssueModel == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookIssueModel.BookId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookIssueModel.MemberId);
            return View(bookIssueModel);
        }

        // POST: BookIssue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,MemberId,IssueDate,ExpectedReturnDate,ActualRetuerndate")] BookIssueModel bookIssueModel)
        {
            if (id != bookIssueModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookIssueModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookIssueModelExists(bookIssueModel.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookIssueModel.BookId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookIssueModel.MemberId);
            return View(bookIssueModel);
        }

        // GET: BookIssue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookIssueModel = await _context.BookIssue
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookIssueModel == null)
            {
                return NotFound();
            }

            return View(bookIssueModel);
        }

        // POST: BookIssue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookIssueModel = await _context.BookIssue.FindAsync(id);
            _context.BookIssue.Remove(bookIssueModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookIssueModelExists(int id)
        {
            return _context.BookIssue.Any(e => e.Id == id);
        }
    }
}
