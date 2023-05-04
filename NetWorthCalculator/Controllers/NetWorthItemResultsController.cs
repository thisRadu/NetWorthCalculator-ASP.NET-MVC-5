using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetWorthCalculator.Models;

namespace NetWorthCalculator.Controllers
{
    public class NetWorthItemResultsController : Controller
    {
        private readonly NetWorthContext _context;

        public NetWorthItemResultsController(NetWorthContext context)
        {
            _context = context;
        }

        // GET: NetWorthItemResults
        public async Task<IActionResult> Index()
        {
            var netWorthContext = _context.NetWorthItemResults.Include(n => n.ItemNavigation).Include(n => n.NetWorth);
            return View(await netWorthContext.ToListAsync());
        }

        // GET: NetWorthItemResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorthItemResult = await _context.NetWorthItemResults
                .Include(n => n.ItemNavigation)
                .Include(n => n.NetWorth)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netWorthItemResult == null)
            {
                return NotFound();
            }

            return View(netWorthItemResult);
        }

        // GET: NetWorthItemResults/Create
        public IActionResult Create()
        {
            ViewData["Item"] = new SelectList(_context.NetWorthItems, "Id", "Name");
            ViewData["NetWorthId"] = new SelectList(_context.NetWorths, "Id", "Id");
            return View();
        }

        // POST: NetWorthItemResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NetWorthId,Item,Value")] NetWorthItemResult netWorthItemResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(netWorthItemResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Item"] = new SelectList(_context.NetWorthItems, "Id", "Name", netWorthItemResult.Item);
            ViewData["NetWorthId"] = new SelectList(_context.NetWorths, "Id", "Id", netWorthItemResult.NetWorthId);
            return View(netWorthItemResult);
        }

        // GET: NetWorthItemResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorthItemResult = await _context.NetWorthItemResults.FindAsync(id);
            if (netWorthItemResult == null)
            {
                return NotFound();
            }
            ViewData["Item"] = new SelectList(_context.NetWorthItems, "Id", "Name", netWorthItemResult.Item);
            ViewData["NetWorthId"] = new SelectList(_context.NetWorths, "Id", "Id", netWorthItemResult.NetWorthId);
            return View(netWorthItemResult);
        }

        // POST: NetWorthItemResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NetWorthId,Item,Value")] NetWorthItemResult netWorthItemResult)
        {
            if (id != netWorthItemResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(netWorthItemResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetWorthItemResultExists(netWorthItemResult.Id))
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
            ViewData["Item"] = new SelectList(_context.NetWorthItems, "Id", "Name", netWorthItemResult.Item);
            ViewData["NetWorthId"] = new SelectList(_context.NetWorths, "Id", "Id", netWorthItemResult.NetWorthId);
            return View(netWorthItemResult);
        }

        // GET: NetWorthItemResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorthItemResult = await _context.NetWorthItemResults
                .Include(n => n.ItemNavigation)
                .Include(n => n.NetWorth)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netWorthItemResult == null)
            {
                return NotFound();
            }

            return View(netWorthItemResult);
        }

        // POST: NetWorthItemResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var netWorthItemResult = await _context.NetWorthItemResults.FindAsync(id);
            _context.NetWorthItemResults.Remove(netWorthItemResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetWorthItemResultExists(int id)
        {
            return _context.NetWorthItemResults.Any(e => e.Id == id);
        }
    }
}
