using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AbhiHood2.Data;
using AbhiHood2.Models;
using Microsoft.AspNetCore.Identity;
using AbhiHood2.Extensions;

namespace AbhiHood1.Controllers
{
    public class ZipCodeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZipCodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [NonAction]
        private void SetUser()
        {
            var tesmUserId = this.HttpContext.User.GetLoggedInUserId<string>();
            var items = new List<UserZipCodeSubscription> { new UserZipCodeSubscription { UserId = tesmUserId } };
            ViewData["UserId"] = new SelectList(items, "UserId", "UserId", tesmUserId);
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            var sqkDbAbhiHoodContext = _context.UserZipCodeSubscription;
            return View(await sqkDbAbhiHoodContext.ToListAsync());
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserZipCodeSubscription == null)
            {
                return NotFound();
            }

            var userZipCodeSubscription = await _context.UserZipCodeSubscription
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userZipCodeSubscription == null)
            {
                return NotFound();
            }

            return View(userZipCodeSubscription);
        }

        public IActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ZipCode")] UserZipCodeSubscription userZipCodeSubscription)
        {
            var test = this;
            if (ModelState.IsValid)
            {
                userZipCodeSubscription.UserId = this.HttpContext.User.GetLoggedInUserId<string>();
                _context.Add(userZipCodeSubscription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetUser();
            return View(userZipCodeSubscription);
        }

        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserZipCodeSubscription == null)
            {
                return NotFound();
            }

            var userZipCodeSubscription = await _context.UserZipCodeSubscription.FindAsync(id);
            if (userZipCodeSubscription == null)
            {
                return NotFound();
            }
            SetUser();
            return View(userZipCodeSubscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ZipCode")] UserZipCodeSubscription userZipCodeSubscription)
        {
            if (id != userZipCodeSubscription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userZipCodeSubscription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserZipCodeSubscriptionExists(userZipCodeSubscription.Id))
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
            SetUser();
            return View(userZipCodeSubscription);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserZipCodeSubscription == null)
            {
                return NotFound();
            }

            var userZipCodeSubscription = await _context.UserZipCodeSubscription
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userZipCodeSubscription == null)
            {
                return NotFound();
            }

            return View(userZipCodeSubscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserZipCodeSubscription == null)
            {
                return Problem("Entity set 'SqkDbAbhiHoodContext.UserZipCodeSubscriptions'  is null.");
            }
            var userZipCodeSubscription = await _context.UserZipCodeSubscription.FindAsync(id);
            if (userZipCodeSubscription != null)
            {
                _context.UserZipCodeSubscription.Remove(userZipCodeSubscription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserZipCodeSubscriptionExists(int id)
        {
          return (_context.UserZipCodeSubscription?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
