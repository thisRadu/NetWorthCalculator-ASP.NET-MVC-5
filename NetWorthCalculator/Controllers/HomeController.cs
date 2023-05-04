using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetWorthCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetWorthCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NetWorthContext _context;

     

        public HomeController(ILogger<HomeController> logger, NetWorthContext context)
        {
            _logger = logger;
            _context = context;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Index(int? id)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var netWorth = new NetWorth();
            if(id == null)
            {
                netWorth = _context.NetWorths
                    .Include(n => n.NetWorthItemResults)
                    .ThenInclude(nr => nr.ItemNavigation)
                      .Where(n => n.UserId == userId)
                      .OrderByDescending(n => n.Date)
                      .FirstOrDefault();

            }
            else
            {
               netWorth =  _context.NetWorths
                    .Include(n => n.NetWorthItemResults)
                    .ThenInclude(nr => nr.ItemNavigation)
                        .FirstOrDefault(x => x.Id == id);
            }
           
            if (netWorth == null)
            {
                return View("NoEntriesFound");
            }
            ViewBag.Next = false;
            ViewBag.Previous = false;

            NetWorth temp = _context.NetWorths
                    .OrderByDescending(n => n.Date)
                    .Where(n => n.UserId == userId && n.Date < netWorth.Date)
                    .FirstOrDefault();
            if (temp != null)
            {
                ViewBag.Previous = true;
                ViewBag.PreviousItemId = temp.Id;
            }
            temp = _context.NetWorths
                  .OrderByDescending(n => n.Date)
                  .Where(n => n.UserId == userId && n.Date > netWorth.Date)
                  .FirstOrDefault();
            if (temp != null ) 
            {
                ViewBag.Next = true;
                ViewBag.NextItemId = temp.Id;
            }


            return View(netWorth);
        }


 

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Create()
        {
            return View(new NetWorthViewModel() { NetWorthItemResults = new List<NetWorthItemResult>() }) ;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( NetWorthViewModel netWorthItemResult)
        {
            if (ModelState.IsValid)
            {
                NetWorth netWorth = new NetWorth();
                int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                netWorth.UserId = userId;
                netWorth.Date = DateTime.Now;
                netWorth.Advice = "";
                _context.Add(netWorth);
                await _context.SaveChangesAsync();

                foreach (var item in netWorthItemResult.NetWorthItemResults)
                {
                   var netWorthItem = new NetWorthItem()
                    {
                        Type = item.ItemNavigation.Type,
                        Name = item.ItemNavigation.Name

                    };
                    _context.Add(netWorthItem);
                    await _context.SaveChangesAsync();
                    _context.Add(new NetWorthItemResult()
                    {
                        Item = netWorthItem.Id,
                        Value = item.Value,
                        NetWorthId = netWorth.Id
                    });
                    await _context.SaveChangesAsync();

                }

               
              
                return RedirectToAction(nameof(Index));
            }
       
            return View(netWorthItemResult);
        }

     
*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
