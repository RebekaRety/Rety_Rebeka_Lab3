using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rety_Rebeka_Lab2.Data;
using Rety_Rebeka_Lab2.Models;

namespace Rety_Rebeka_Lab2
{
    public class ParfumesController : Controller
    {
        private readonly StoreContext _context;

        public ParfumesController(StoreContext context)
        {
            _context = context;
        }

        // GET: Parfumes
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var parfumes = from b in _context.Parfumes
                           select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                parfumes = parfumes.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    parfumes = parfumes.OrderByDescending(b => b.Name);
                    break;
                case "Price":
                    parfumes = parfumes.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    parfumes = parfumes.OrderByDescending(b => b.Price);
                    break;
                default:
                    parfumes = parfumes.OrderBy(b => b.Name);
                    break;
            }
            int pageSize = 2;

            return View(await PaginatedList<Parfume>.CreateAsync(parfumes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Parfumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parfume = await _context.Parfumes
            .Include(s => s.Orders)
            .ThenInclude(e => e.Customer)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (parfume == null)
            {
                return NotFound();
            }

            return View(parfume);
        }

        // GET: Parfumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parfumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Brand,Price")] Parfume parfume)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(parfume);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(parfume);
        }

        // GET: Parfumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parfume = await _context.Parfumes.FindAsync(id);
            if (parfume == null)
            {
                return NotFound();
            }
            return View(parfume);
        }

        // POST: Parfumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Parfumes.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Parfume>(
            studentToUpdate,
            "",
            s => s.Brand, s => s.Name, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Parfumes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parfume = await _context.Parfumes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parfume == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }
            return View(parfume);
        }

        // POST: Parfumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parfume = await _context.Parfumes.FindAsync(id);
            if (parfume == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Parfumes.Remove(parfume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }


        }
    }
}
