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
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expense
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExpenseModel.Include(e => e.expenseCategory).Include(e => e.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Expense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpenseModel == null)
            {
                return NotFound();
            }

            var expense = await _context.ExpenseModel
                .Include(e => e.expenseCategory)
                .Include(e => e.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            ViewData["expenseCategoryId"] = new SelectList(_context.ExpenseCategoryModel, "id", "icon");
            ViewData["userId"] = new SelectList(_context.UserModel, "id", "email");
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,description,amount,userId,expenseCategoryId,registrationDate")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["expenseCategoryId"] = new SelectList(_context.ExpenseCategoryModel, "id", "icon", expense.expenseCategoryId);
            ViewData["userId"] = new SelectList(_context.UserModel, "id", "email", expense.userId);
            return View(expense);
        }

        // GET: Expense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpenseModel == null)
            {
                return NotFound();
            }

            var expense = await _context.ExpenseModel.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["expenseCategoryId"] = new SelectList(_context.ExpenseCategoryModel, "id", "icon", expense.expenseCategoryId);
            ViewData["userId"] = new SelectList(_context.UserModel, "id", "email", expense.userId);
            return View(expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,description,amount,userId,expenseCategoryId,registrationDate")] Expense expense)
        {
            if (id != expense.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.id))
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
            ViewData["expenseCategoryId"] = new SelectList(_context.ExpenseCategoryModel, "id", "icon", expense.expenseCategoryId);
            ViewData["userId"] = new SelectList(_context.UserModel, "id", "email", expense.userId);
            return View(expense);
        }

        // GET: Expense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpenseModel == null)
            {
                return NotFound();
            }

            var expense = await _context.ExpenseModel
                .Include(e => e.expenseCategory)
                .Include(e => e.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpenseModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExpenseModel'  is null.");
            }
            var expense = await _context.ExpenseModel.FindAsync(id);
            if (expense != null)
            {
                _context.ExpenseModel.Remove(expense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
          return (_context.ExpenseModel?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
