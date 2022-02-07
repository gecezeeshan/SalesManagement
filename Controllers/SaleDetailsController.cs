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
    public class SaleDetailsController : Controller
    {
        private readonly SaleContext _context;

        public SaleDetailsController(SaleContext context)
        {
            _context = context;
        }

        // GET: SaleDetails
        //public async Task<IActionResult> Index()
        //{
        //    var saleContext = _context.SaleDetail.Include(s => s.SaleMaster);
        //    return View(await saleContext.ToListAsync());
        //}
// GET: SaleDetails
        public async Task<IActionResult> Index(int? id)
        {
            var saleContext = _context.SaleDetail.Include(s => s.SaleMaster).Where(a => a.SaleMaster.SaleMasterId == id);
            
            return View(await saleContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail
                .Include(s => s.SaleMaster)
                .FirstOrDefaultAsync(m => m.SaleDetailId == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public IActionResult Create(int? id)
        {
            ViewBag.SMID = id;

            ViewData["SaleMasterId"] = new SelectList(_context.SaleMaster, "SaleMasterId", "SaleMasterId");
            return View();
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("SaleDetailId,SaleMasterId,ItemNo,ItemName,QTY,Tax,Price")] SaleDetail saleDetail)
        {
            var id1 =  ViewBag.SMID;
            saleDetail.SaleMasterId = id;
            if (ModelState.IsValid)
            {
               
                _context.Add(saleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SaleMasterId"] = new SelectList(_context.SaleMaster, "SaleMasterId", "SaleMasterId", saleDetail.SaleMasterId);
            return View(saleDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSaleDetail([Bind("SaleDetailId,SaleMasterId,ItemNo,ItemName,QTY,Tax,Price")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {

                _context.Add(saleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["SaleMasterId"] = new SelectList(_context.SaleMaster, "SaleMasterId", "SaleMasterId", saleDetail.SaleMasterId);
            return RedirectToAction("Details", "SaleMasters", new { id = saleDetail.SaleMasterId });
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            ViewData["SaleMasterId"] = new SelectList(_context.SaleMaster, "SaleMasterId", "SaleMasterId", saleDetail.SaleMasterId);
            return View(saleDetail);
        }// GET: SaleDetails/Edit/5
        public async Task<IActionResult> EditSaleDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail.FindAsync(id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            //ViewData["SaleMasterId"] = new SelectList(_context.SaleMaster, "SaleMasterId", "SaleMasterId", saleDetail.SaleMasterId);
            return RedirectToAction("Details", "SaleMasters", new { id = saleDetail.SaleMasterId });
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleDetailId,SaleMasterId,ItemNo,ItemName,QTY,Tax,Price")] SaleDetail saleDetail)
        {
            if (id != saleDetail.SaleDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saleDetail.SaleDetailId))
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
            ViewData["SaleMasterId"] = new SelectList(_context.SaleMaster, "SaleMasterId", "SaleMasterId", saleDetail.SaleMasterId);
            return View(saleDetail);
        }

        // GET: SaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetail
                .Include(s => s.SaleMaster)
                .FirstOrDefaultAsync(m => m.SaleDetailId == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleDetail = await _context.SaleDetail.FindAsync(id);
            _context.SaleDetail.Remove(saleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("DeleteSaleDetail")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSaleDetail(int id)
        {
            var saleDetail = await _context.SaleDetail.FindAsync(id);
            var masterId = saleDetail.SaleMasterId;
            _context.SaleDetail.Remove(saleDetail);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Details", "SaleMasters", new { id =masterId });
        }

        private bool SaleDetailExists(int id)
        {
            return _context.SaleDetail.Any(e => e.SaleDetailId == id);
        }
    }
}
