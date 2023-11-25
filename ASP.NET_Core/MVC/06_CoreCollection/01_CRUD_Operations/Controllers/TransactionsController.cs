using _01_CRUD_Operations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _01_CRUD_Operations.Controllers;

public class TransactionsController : Controller
{
    private readonly TransactionDbContext _context;

    public TransactionsController(TransactionDbContext context) => _context = context;

    // GET: Transactions
    public async Task<IActionResult> Index()
    {
        return View(await _context.Transactions.ToListAsync());
    }

    // GET: Transactions/Details/5
    public async Task<IActionResult> Details(int? id) {
        if (id == null) {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(m => m.TransactionId == id);
        
        if (transaction == null) {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transactions/AddOrEdit
    public IActionResult AddOrEdit(int id = 0) {
        if (id == 0)
            return View(new Transaction());
        else
            return View(_context.Transactions.Find(id));
    }

    // POST: Transactions/AddOrEdit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
    {
        if (ModelState.IsValid) {
            if (transaction.TransactionId == 0) {
                transaction.Date = DateTime.Now;
                _context.Add(transaction);
            }
            else
                _context.Update(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transaction);
    }

    // POST: Transactions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            _context.Transactions.Remove(transaction);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionExists(int id)
    {
        return _context.Transactions.Any(e => e.TransactionId == id);
    }
}
