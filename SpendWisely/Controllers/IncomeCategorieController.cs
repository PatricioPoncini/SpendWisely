using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpendWisely.Models;

namespace SpendWisely.Controllers
{
    public class IncomeCategorieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomeCategorieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncomeCategorie
        public async Task<IActionResult> Index()
        {
              return _context.IncomeCategoryModel != null ? 
                          View(await _context.IncomeCategoryModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.IncomeCategoryModel'  is null.");
        }

        // GET: IncomeCategorie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IncomeCategoryModel == null)
            {
                return NotFound();
            }

            var incomeCategory = await _context.IncomeCategoryModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (incomeCategory == null)
            {
                return NotFound();
            }

            return View(incomeCategory);
        }

        // GET: IncomeCategorie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeCategorie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,icon")] IncomeCategory incomeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomeCategory);
        }

        // GET: IncomeCategorie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IncomeCategoryModel == null)
            {
                return NotFound();
            }

            var incomeCategory = await _context.IncomeCategoryModel.FindAsync(id);
            if (incomeCategory == null)
            {
                return NotFound();
            }
            return View(incomeCategory);
        }

        // POST: IncomeCategorie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,icon")] IncomeCategory incomeCategory)
        {
            if (id != incomeCategory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeCategoryExists(incomeCategory.id))
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
            return View(incomeCategory);
        }

        // GET: IncomeCategorie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IncomeCategoryModel == null)
            {
                return NotFound();
            }

            var incomeCategory = await _context.IncomeCategoryModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (incomeCategory == null)
            {
                return NotFound();
            }

            return View(incomeCategory);
        }

        // POST: IncomeCategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IncomeCategoryModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IncomeCategoryModel'  is null.");
            }
            var incomeCategory = await _context.IncomeCategoryModel.FindAsync(id);
            if (incomeCategory != null)
            {
                _context.IncomeCategoryModel.Remove(incomeCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeCategoryExists(int id)
        {
          return (_context.IncomeCategoryModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
