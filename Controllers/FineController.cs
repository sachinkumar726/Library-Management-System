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
    public class FineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Fine.Include(f => f.BookIssue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Fine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fineModel = await _context.Fine
                .Include(f => f.BookIssue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fineModel == null)
            {
                return NotFound();
            }

            return View(fineModel);
        }

        // GET: Fine/Create
        public IActionResult Create()
        {
            ViewData["BookIssueId"] = new SelectList(_context.BookIssue, "Name", "Name");
            return View();
        }

        // POST: Fine/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookIssueId,Amount")] FineModel fineModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fineModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookIssueId"] = new SelectList(_context.BookIssue, "Name", "Name", fineModel.BookIssueId);
            return View(fineModel);
        }

        // GET: Fine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fineModel = await _context.Fine.FindAsync(id);
            if (fineModel == null)
            {
                return NotFound();
            }
            ViewData["BookIssueId"] = new SelectList(_context.BookIssue, "Name", "Name", fineModel.BookIssueId);
            return View(fineModel);
        }

        // POST: Fine/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookIssueId,Amount")] FineModel fineModel)
        {
            if (id != fineModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fineModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FineModelExists(fineModel.Id))
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
            ViewData["BookIssueId"] = new SelectList(_context.BookIssue, "Name", "Name", fineModel.BookIssueId);
            return View(fineModel);
        }

        // GET: Fine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fineModel = await _context.Fine
                .Include(f => f.BookIssue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fineModel == null)
            {
                return NotFound();
            }

            return View(fineModel);
        }

        // POST: Fine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fineModel = await _context.Fine.FindAsync(id);
            _context.Fine.Remove(fineModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FineModelExists(int id)
        {
            return _context.Fine.Any(e => e.Id == id);
        }
    }
}
