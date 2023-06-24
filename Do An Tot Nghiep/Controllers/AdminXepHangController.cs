using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Do_An_Tot_Nghiep.Models;

namespace Do_An_Tot_Nghiep.Controllers
{
    public class AdminXepHangController : Controller
    {
        private readonly dbKA _context;

        public AdminXepHangController(dbKA context)
        {
            _context = context;
        }

        // GET: AdminXepHang
        public async Task<IActionResult> Index()
        {
            var dbKA = _context.XepHangs.Include(x => x.BacXepHang).Include(x => x.NguoiDung)
                .OrderByDescending(x => x.DiemNangLuc);
            return View(await dbKA.ToListAsync());
        }

        // GET: AdminXepHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xepHang = await _context.XepHangs
                .Include(x => x.BacXepHang)
                .Include(x => x.NguoiDung)
                .FirstOrDefaultAsync(m => m.XepHangId == id);
            if (xepHang == null)
            {
                return NotFound();
            }

            return View(xepHang);
        }

        // GET: AdminXepHang/Create
        public IActionResult Create()
        {
            ViewData["BacXepHangId"] = new SelectList(_context.BacXepHangs, "BacXepHangId", "BacXepHangId");
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId");
            return View();
        }

        // POST: AdminXepHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("XepHangId,NguoiDungId,BacXepHangId,DiemNangLuc")] XepHang xepHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xepHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BacXepHangId"] = new SelectList(_context.BacXepHangs, "BacXepHangId", "BacXepHangId", xepHang.BacXepHangId);
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", xepHang.NguoiDungId);
            return View(xepHang);
        }

        // GET: AdminXepHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xepHang = await _context.XepHangs.FindAsync(id);
            if (xepHang == null)
            {
                return NotFound();
            }
            ViewData["BacXepHangId"] = new SelectList(_context.BacXepHangs, "BacXepHangId", "BacXepHangId", xepHang.BacXepHangId);
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", xepHang.NguoiDungId);
            return View(xepHang);
        }

        // POST: AdminXepHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("XepHangId,NguoiDungId,BacXepHangId,DiemNangLuc")] XepHang xepHang)
        {
            if (id != xepHang.XepHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xepHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XepHangExists(xepHang.XepHangId))
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
            ViewData["BacXepHangId"] = new SelectList(_context.BacXepHangs, "BacXepHangId", "BacXepHangId", xepHang.BacXepHangId);
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", xepHang.NguoiDungId);
            return View(xepHang);
        }

        // GET: AdminXepHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xepHang = await _context.XepHangs
                .Include(x => x.BacXepHang)
                .Include(x => x.NguoiDung)
                .FirstOrDefaultAsync(m => m.XepHangId == id);
            if (xepHang == null)
            {
                return NotFound();
            }

            return View(xepHang);
        }

        // POST: AdminXepHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xepHang = await _context.XepHangs.FindAsync(id);
            _context.XepHangs.Remove(xepHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XepHangExists(int id)
        {
            return _context.XepHangs.Any(e => e.XepHangId == id);
        }
    }
}
