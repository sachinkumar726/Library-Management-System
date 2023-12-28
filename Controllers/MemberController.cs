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
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Member
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Member.Include(m => m.MemberType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.Member
                .Include(m => m.MemberType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberModel == null)
            {
                return NotFound();
            }

            return View(memberModel);
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            ViewData["MemberTypeId"] = new SelectList(_context.MemberType, "Id", "Name");
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MemberModel memberModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberTypeId"] = new SelectList(_context.MemberType, "Id", "Name", memberModel.MemberTypeId);
            return View(memberModel);
        }

        // GET: Member/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.Member.FindAsync(id);
            if (memberModel == null)
            {
                return NotFound();
            }
            ViewData["MemberTypeId"] = new SelectList(_context.MemberType, "Id", "Name", memberModel.MemberTypeId);
            return View(memberModel);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Email,Address,MemberTypeId,MemberDate,MemberExpDate")] MemberModel memberModel)
        {
            if (id != memberModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberModelExists(memberModel.Id))
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
            ViewData["MemberTypeId"] = new SelectList(_context.MemberType, "Id", "Name", memberModel.MemberTypeId);
            return View(memberModel);
        }

        // GET: Member/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.Member
                .Include(m => m.MemberType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberModel == null)
            {
                return NotFound();
            }

            return View(memberModel);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberModel = await _context.Member.FindAsync(id);
            _context.Member.Remove(memberModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberModelExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
    }
}
