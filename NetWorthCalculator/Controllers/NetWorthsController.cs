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
    public class NetWorthsController : Controller
    {
        private readonly NetWorthContext _context;

        public NetWorthsController(NetWorthContext context)
        {
            _context = context;
        }

        // GET: NetWorths
        public async Task<IActionResult> Index()
        {
            return View(await _context.NetWorths.ToListAsync());
        }

        // GET: NetWorths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorth = await _context.NetWorths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netWorth == null)
            {
                return NotFound();
            }

            return View(netWorth);
        }

        // GET: NetWorths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NetWorths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Advice")] NetWorth netWorth)
        {
            if (ModelState.IsValid)
            {
                netWorth.UserId = 1;
                _context.Add(netWorth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(netWorth);
        }

        // GET: NetWorths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorth = await _context.NetWorths.FindAsync(id);
            if (netWorth == null)
            {
                return NotFound();
            }
            return View(netWorth);
        }

        // POST: NetWorths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Advice")] NetWorth netWorth)
        {
            if (id != netWorth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(netWorth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetWorthExists(netWorth.Id))
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
         
            return View(netWorth);
        }

        // GET: NetWorths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var netWorth = await _context.NetWorths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (netWorth == null)
            {
                return NotFound();
            }

            return View(netWorth);
        }

        // POST: NetWorths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var netWorth = await _context.NetWorths.FindAsync(id);
            _context.NetWorths.Remove(netWorth);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "Home");
        }

        private bool NetWorthExists(int id)
        {
            return _context.NetWorths.Any(e => e.Id == id);
        }
    }
}
