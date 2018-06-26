using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodayList.Models;

namespace TodayList.Controllers
{
    public class TodaysController : Controller
    {
        private readonly TodayContext _context;

        public TodaysController(TodayContext context)
        {
            _context = context;
        }

        // GET: Todays
        public async Task<IActionResult> Index()
        {
            return View(await _context.Today.OrderBy(t => t.ListOrder).ToListAsync());
        }

        // GET: Todays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var today = await _context.Today
                .SingleOrDefaultAsync(m => m.TodayId == id);
            if (today == null)
            {
                return NotFound();
            }

            return View(today);
        }

        // GET: Todays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodayId,Done,Description,CreateDate,DoneDate")] Today today)
        {
            if (ModelState.IsValid)
            {
                _context.Add(today);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(today);
        }

        // GET: Todays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var today = await _context.Today.SingleOrDefaultAsync(m => m.TodayId == id);
            if (today == null)
            {
                return NotFound();
            }
            return View(today);
        }

        // POST: Todays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TodayId,Done,Description,CreateDate,DoneDate")] Today today)
        {
            if (id != today.TodayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(today);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodayExists(today.TodayId))
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
            return View(today);
        }

        // GET: Todays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var today = await _context.Today
                .SingleOrDefaultAsync(m => m.TodayId == id);
            if (today == null)
            {
                return NotFound();
            }

            return View(today);
        }

        // POST: Todays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var today = await _context.Today.SingleOrDefaultAsync(m => m.TodayId == id);
            _context.Today.Remove(today);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public  JsonResult UpdateDescription(int id, string description)
        {
            var today = _context.Today.SingleOrDefault(m => m.TodayId == id);
            if (today != null) today.Description = description;
            _context.SaveChanges();
            return Json(null);
        }

        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public JsonResult UpdateDone(int id, bool done)
        {
            var today = _context.Today.SingleOrDefault(m => m.TodayId == id);
            if (today != null)
            {
                today.Done = done;
                today.DoneDate = DateTime.Today;
            }
            _context.SaveChanges();
            return Json(null);
        }

        public JsonResult UpdateSortOrder(int id, string[] todays)
        {
            //go through db records and update ListOrder value
            for (var i =0; i < todays.Length; i++)
            {
                var today = _context.Today.Find(int.Parse(todays[i].Split("_")[1]));
                today.ListOrder = i;
                var success = _context.SaveChanges();
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult DeleteToday(int id)
        {
            var today = _context.Today.SingleOrDefault(t => t.TodayId == id);
            _context.Today.Remove(today);
            var success = _context.SaveChanges();
            return Json(null);
        }

        [HttpPost]
        public ActionResult AddNewToday(string description)
        {
            var today = new Today
            {
                CreateDate = DateTime.Now,
                Done = false,
                Description = description,
                ListOrder = 0
            };
            _context.Today.Add(today);
            var success = _context.SaveChanges();
            return PartialView("TodayItemPartial", today);
        }


        private bool TodayExists(int id)
        {
            return _context.Today.Any(e => e.TodayId == id);
        }
    }
}
