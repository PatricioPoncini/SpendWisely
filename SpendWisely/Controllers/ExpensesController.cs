﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpendWisely.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using EFCore.BulkExtensions;
using EFCore.BulkExtensions;

namespace SpendWisely.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExpenseModel.Include(e => e.expenseCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpenseModel == null)
            {
                return NotFound();
            }

            var expense = await _context.ExpenseModel
                .Include(e => e.expenseCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["expenseCategoryId"] = new SelectList(_context.ExpenseCategoryModel, "id", "title");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,description,amount,expenseCategoryId,registrationDate")] Expense expense)
        {
            _context.Add(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["expenseCategoryId"] = new SelectList(_context.ExpenseCategoryModel, "id", "title", expense.expenseCategoryId);
            // return View(expense);
            //return RedirectToAction(nameof(Index));
        }

        // GET: Expenses/Edit/5
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
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,description,amount,expenseCategoryId,registrationDate")] Expense expense)
        {
            if (id != expense.id)
            {
                return NotFound();
            }

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

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpenseModel == null)
            {
                return NotFound();
            }

            var expense = await _context.ExpenseModel
                .Include(e => e.expenseCategory)
                .FirstOrDefaultAsync(m => m.id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveExcelData([FromForm] IFormFile ExcelFile)
        {
            Stream stream = ExcelFile.OpenReadStream();

            IWorkbook MyExcel = null;

            if (Path.GetExtension(ExcelFile.FileName) == ".xlsx")
            {
                MyExcel = new XSSFWorkbook(stream);
            } else
            {
                MyExcel = new XSSFWorkbook(stream);
            }

            ISheet ExcelSheet = MyExcel.GetSheetAt(0);

            int cantidadFilas = ExcelSheet.LastRowNum;

            List<Expense> lista = new List<Expense>();

            for (int i = 1; i <= cantidadFilas; i++)
            {
                IRow fila = ExcelSheet.GetRow(i);

                lista.Add(new Expense
                {
                    title = fila.GetCell(0).ToString(),
                    description = fila.GetCell(1).ToString(),
                    amount = Convert.ToInt32(fila.GetCell(2)),
                    expenseCategoryId = Convert.ToInt32(fila.GetCell(3)),
                    registrationDate = DateTime.Parse(fila.GetCell(4).ToString())
                });
            }

            _context.BulkInsert(lista);

            return StatusCode(StatusCodes.Status200OK, new {message = "Ok"});
        }
    }
}
