using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;

namespace SalesManagement.Controllers
{
    public class SaleMastersController : Controller
    {
        private readonly SaleContext _context;

        public SaleMastersController(SaleContext context)
        {
            _context = context;
        }

        // GET: SaleMasters
        public async Task<IActionResult> Index()
        {
            return View( await _context.SaleMaster.ToListAsync());
        }

        // GET: SaleMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMaster = await _context.SaleMaster
                .FirstOrDefaultAsync(m => m.SaleMasterId == id);

            if (saleMaster == null)
            {
                return NotFound();
            }
            else {
                var details = _context.SaleDetail.Include(s => s.SaleMaster).Where(a => a.SaleMaster.SaleMasterId == id).ToList();
                saleMaster.SaleDetails = details;
            }

            return View(saleMaster);
        }

        // GET: SaleMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SaleMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleMasterId,Customer,Date,Tax,Total")] SaleMaster saleMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleMaster);
        }

        // GET: SaleMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMaster = await _context.SaleMaster.FindAsync(id);
            if (saleMaster == null)
            {
                return NotFound();
            }
            return View(saleMaster);
        }

        // POST: SaleMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleMasterId,Customer,Date,Tax,Total")] SaleMaster saleMaster)
        {
            if (id != saleMaster.SaleMasterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleMasterExists(saleMaster.SaleMasterId))
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
            return View(saleMaster);
        }

        // GET: SaleMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMaster = await _context.SaleMaster
                .FirstOrDefaultAsync(m => m.SaleMasterId == id);
            if (saleMaster == null)
            {
                return NotFound();
            }

            return View(saleMaster);
        }

        // POST: SaleMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleMaster = await _context.SaleMaster.FindAsync(id);
            IEnumerable<SaleDetail> details1 = _context.SaleDetail.Where(a => a.SaleMaster.SaleMasterId == id);
            _context.SaleDetail.RemoveRange(details1);
            var details = await _context.SaleDetail.ToListAsync();
            _context.SaleMaster.Remove(saleMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool SaleMasterExists(int id)
        {
            return _context.SaleMaster.Any(e => e.SaleMasterId == id);
        }

        public async Task<IActionResult> CreateSale(int? id)
        {
            if (id.HasValue)
            {
                var saleMaster = await _context.SaleMaster
                    .FirstOrDefaultAsync(m => m.SaleMasterId == id);
                if (saleMaster == null)
                {
                    return NotFound();
                }
                return View("CreateSale", saleMaster);
            }

            else { 
                return View("CreateSale"); 
            
            }



        }


    }
}
