using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AbhiHood2.Data;
using AbhiHood2.Models;
using AbhiHood2.Extensions;
namespace AbhiHood2.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }
        [NonAction]
        private void SetUser()
        {
            var tesmUserId = this.HttpContext.User.GetLoggedInUserId<string>();
            var items = new List<PostedUserData> { new PostedUserData { UserId = tesmUserId } };
            ViewData["UserId"] = new SelectList(items, "UserId", "UserId", tesmUserId);
            
        }
        // GET: Post
        public async Task<IActionResult> Index()
        {
            SetUser();
            var userId = this.HttpContext.User.GetLoggedInUserId<string>();
            var zipCode = _context.UserZipCodeSubscription
                .Where(x => x.UserId == userId)
                .Select(x=>x.ZipCode).ToArray();

            var myPost = _context.PostedUserData.Where(x => x.UserId == userId).ToList();
            var allOtherPost= _context.PostedUserData.Where(x => zipCode.Contains(x.ZipCode) && x.UserId != userId).ToList();
            myPost.AddRange(allOtherPost);
            return View(myPost);
            //var testItems= _context.PostedUserData.Where(x=> x.ZipCode==)
            //return _context.PostedUserData?.Where(x => zipCode.Contains(x.ZipCode)) != null ? 
            //              View(myPost) :
            //              Problem("Entity set 'ApplicationDbContext.PostedUserData'  is null.");
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostedUserData == null)
            {
                return NotFound();
            }
            SetUser();
            var postedUserData = await _context.PostedUserData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postedUserData == null)
            {
                return NotFound();
            }

            return View(postedUserData);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PostedText,PicturePath,Address,City,State,ZipCode,SysAddress,SysCity,SysState,SysZipCode,SysPicCarNumber,SysPicInfo1,SysPicInfo2")] PostedUserData postedUserData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postedUserData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SetUser();
            return View(postedUserData);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostedUserData == null)
            {
                return NotFound();
            }

            var postedUserData = await _context.PostedUserData.FindAsync(id);
            if (postedUserData == null)
            {
                return NotFound();
            }
            SetUser();
            return View(postedUserData);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PostedText,PicturePath,Address,City,State,ZipCode,SysAddress,SysCity,SysState,SysZipCode,SysPicCarNumber,SysPicInfo1,SysPicInfo2")] PostedUserData postedUserData)
        {
            if (id != postedUserData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postedUserData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostedUserDataExists(postedUserData.Id))
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
            return View(postedUserData);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostedUserData == null)
            {
                return NotFound();
            }

            var postedUserData = await _context.PostedUserData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postedUserData == null)
            {
                return NotFound();
            }
            SetUser();
            return View(postedUserData);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostedUserData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PostedUserData'  is null.");
            }
            var postedUserData = await _context.PostedUserData.FindAsync(id);
            if (postedUserData != null)
            {
                _context.PostedUserData.Remove(postedUserData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostedUserDataExists(int id)
        {
          return (_context.PostedUserData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
