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
    public class MemberTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberType
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberType.ToListAsync());
        }

        // GET: MemberType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTypeModel = await _context.MemberType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberTypeModel == null)
            {
                return NotFound();
            }

            return View(memberTypeModel);
        }

        // GET: MemberType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MemberTypeModel memberTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberTypeModel);
        }

        // GET: MemberType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTypeModel = await _context.MemberType.FindAsync(id);
            if (memberTypeModel == null)
            {
                return NotFound();
            }
            return View(memberTypeModel);
        }

        // POST: MemberType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MemberTypeModel memberTypeModel)
        {
            if (id != memberTypeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberTypeModelExists(memberTypeModel.Id))
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
            return View(memberTypeModel);
        }

        // GET: MemberType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberTypeModel = await _context.MemberType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberTypeModel == null)
            {
                return NotFound();
            }

            return View(memberTypeModel);
        }

        // POST: MemberType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberTypeModel = await _context.MemberType.FindAsync(id);
            _context.MemberType.Remove(memberTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberTypeModelExists(int id)
        {
            return _context.MemberType.Any(e => e.Id == id);
        }
    }
}
