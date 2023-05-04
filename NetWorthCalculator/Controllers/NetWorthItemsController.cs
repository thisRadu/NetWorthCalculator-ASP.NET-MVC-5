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
    public class NetWorthItemsController : Controller
    {
        private readonly NetWorthContext _context;

        public NetWorthItemsController(NetWorthContext context)
        {
            _context = context;
        }

        // GET: NetWorthItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.NetWorthItems.ToListAsync());
        }

        // GET: NetWorthItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorthItem = await _context.NetWorthItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netWorthItem == null)
            {
                return NotFound();
            }

            return View(netWorthItem);
        }

        // GET: NetWorthItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NetWorthItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type")] NetWorthItem netWorthItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(netWorthItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(netWorthItem);
        }

        // GET: NetWorthItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorthItem = await _context.NetWorthItems.FindAsync(id);
            if (netWorthItem == null)
            {
                return NotFound();
            }
            return View(netWorthItem);
        }

        // POST: NetWorthItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type")] NetWorthItem netWorthItem)
        {
            if (id != netWorthItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(netWorthItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetWorthItemExists(netWorthItem.Id))
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
            return View(netWorthItem);
        }

        // GET: NetWorthItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorthItem = await _context.NetWorthItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netWorthItem == null)
            {
                return NotFound();
            }

            return View(netWorthItem);
        }

        // POST: NetWorthItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var netWorthItem = await _context.NetWorthItems.FindAsync(id);
            _context.NetWorthItems.Remove(netWorthItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetWorthItemExists(int id)
        {
            return _context.NetWorthItems.Any(e => e.Id == id);
        }
    }
}
