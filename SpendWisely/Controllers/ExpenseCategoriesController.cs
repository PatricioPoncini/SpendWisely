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
    public class ExpenseCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExpenseCategories
        public async Task<IActionResult> Index()
        {
              return _context.ExpenseCategoryModel != null ? 
                          View(await _context.ExpenseCategoryModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ExpenseCategoryModel'  is null.");
        }

        // GET: ExpenseCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpenseCategoryModel == null)
            {
                return NotFound();
            }

            var expenseCategory = await _context.ExpenseCategoryModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (expenseCategory == null)
            {
                return NotFound();
            }

            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,icon")] ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpenseCategoryModel == null)
            {
                return NotFound();
            }

            var expenseCategory = await _context.ExpenseCategoryModel.FindAsync(id);
            if (expenseCategory == null)
            {
                return NotFound();
            }
            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,icon")] ExpenseCategory expenseCategory)
        {
            if (id != expenseCategory.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseCategoryExists(expenseCategory.id))
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
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpenseCategoryModel == null)
            {
                return NotFound();
            }

            var expenseCategory = await _context.ExpenseCategoryModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (expenseCategory == null)
            {
                return NotFound();
            }

            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpenseCategoryModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExpenseCategoryModel'  is null.");
            }
            var expenseCategory = await _context.ExpenseCategoryModel.FindAsync(id);
            if (expenseCategory != null)
            {
                _context.ExpenseCategoryModel.Remove(expenseCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseCategoryExists(int id)
        {
          return (_context.ExpenseCategoryModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
