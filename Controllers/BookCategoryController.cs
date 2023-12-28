using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookCategory
        //show all data
        public IActionResult Index()
        {
            //select * from bookcategory
            //labmda expression
            return View(_context.BookCategory.ToList());
        }

        // GET: BookCategory/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

//select top 1 * from Bookcategory where id = 
            var bookCategoryModel =  _context.BookCategory.FirstOrDefault(m => m.Id == id);
            if (bookCategoryModel == null)
            {
                return NotFound();
            }

            return View(bookCategoryModel);
        }

        // GET: BookCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCategoryModel bookCategoryModel)
        {
            if (ModelState.IsValid)
            {
                //insert into
                _context.Add(bookCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookCategoryModel);
        }

        // GET: BookCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategoryModel = await _context.BookCategory.FindAsync(id);
            if (bookCategoryModel == null)
            {
                return NotFound();
            }
            return View(bookCategoryModel);
        }

        // POST: BookCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BookCategoryModel bookCategoryModel)
        {
            if (id != bookCategoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryModelExists(bookCategoryModel.Id))
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
            return View(bookCategoryModel);
        }

        // GET: BookCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategoryModel = await _context.BookCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCategoryModel == null)
            {
                return NotFound();
            }

            return View(bookCategoryModel);
        }

        // POST: BookCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCategoryModel = await _context.BookCategory.FindAsync(id);
            _context.BookCategory.Remove(bookCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCategoryModelExists(int id)
        {
            return _context.BookCategory.Any(e => e.Id == id);
        }
    }
}
