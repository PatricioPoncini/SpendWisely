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
    public class IncomesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Incomes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IncomeModel.Include(i => i.incomeCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Incomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IncomeModel == null)
            {
                return NotFound();
            }

            var income = await _context.IncomeModel
                .Include(i => i.incomeCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // GET: Incomes/Create
        public IActionResult Create()
        {
            ViewData["incomeCategoryId"] = new SelectList(_context.IncomeCategoryModel, "id", "title");
            return View();
        }

        // POST: Incomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,description,registrationDate,amount,incomeCategoryId")] Income income)
        {
            _context.Add(income);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Incomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IncomeModel == null)
            {
                return NotFound();
            }

            var income = await _context.IncomeModel.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }
            ViewData["incomeCategoryId"] = new SelectList(_context.IncomeCategoryModel, "id", "title", income.incomeCategoryId);
            return View(income);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,description,registrationDate,amount,incomeCategoryId")] Income income)
        {
            if (id != income.id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(income);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeExists(income.id))
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

        // GET: Incomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IncomeModel == null)
            {
                return NotFound();
            }

            var income = await _context.IncomeModel
                .Include(i => i.incomeCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (income == null)
            {
                return NotFound();
            }

            return View(income);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IncomeModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IncomeModel'  is null.");
            }
            var income = await _context.IncomeModel.FindAsync(id);
            if (income != null)
            {
                _context.IncomeModel.Remove(income);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeExists(int id)
        {
          return (_context.IncomeModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
