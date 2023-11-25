using _01_CRUD_Operations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _01_CRUD_Operations.Controllers;

public class TransactionsController : Controller
{
    private readonly TransactionDbContext _context;

    public TransactionsController(TransactionDbContext context)
    {
        _context = context;
    }

    // GET: Transactions
    public async Task<IActionResult> Index()
    {
        return View(await _context.Transactions.ToListAsync());
    }

    // GET: Transactions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(m => m.TransactionId == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transactions/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Transactions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transaction);
    }

    // GET: Transactions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return View(transaction);
    }

    // POST: Transactions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
    {
        if (id != transaction.TransactionId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.TransactionId))
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
        return View(transaction);
    }

    // GET: Transactions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(m => m.TransactionId == id);
        if (transaction == null)
        {
            return NotFound();
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
