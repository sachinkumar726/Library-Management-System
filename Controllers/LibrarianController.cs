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
    public class LibrarianController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrarianController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Librarian
        public async Task<IActionResult> Index()
        {
            return View(await _context.Librarian.ToListAsync());
        }

        // GET: Librarian/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarianModel = await _context.Librarian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (librarianModel == null)
            {
                return NotFound();
            }

            return View(librarianModel);
        }

        // GET: Librarian/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Librarian/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Addresss")] LibrarianModel librarianModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librarianModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(librarianModel);
        }

        // GET: Librarian/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarianModel = await _context.Librarian.FindAsync(id);
            if (librarianModel == null)
            {
                return NotFound();
            }
            return View(librarianModel);
        }

        // POST: Librarian/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Addresss")] LibrarianModel librarianModel)
        {
            if (id != librarianModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librarianModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrarianModelExists(librarianModel.Id))
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
            return View(librarianModel);
        }

        // GET: Librarian/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var librarianModel = await _context.Librarian
                .FirstOrDefaultAsync(m => m.Id == id);
            if (librarianModel == null)
            {
                return NotFound();
            }

            return View(librarianModel);
        }

        // POST: Librarian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var librarianModel = await _context.Librarian.FindAsync(id);
            _context.Librarian.Remove(librarianModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarianModelExists(int id)
        {
            return _context.Librarian.Any(e => e.Id == id);
        }
    }
}
