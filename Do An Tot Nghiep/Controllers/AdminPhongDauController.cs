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
    public class AdminPhongDauController : Controller
    {
        private readonly dbKA _context;

        public AdminPhongDauController(dbKA context)
        {
            _context = context;
        }

        // GET: AdminPhongDau
        public async Task<IActionResult> Index()
        {
            var dbKA = _context.ChiTietPhongDaus.Include(c => c.NguoiDung).Include(c => c.PhongDau);
            return View(await dbKA.ToListAsync());
        }

        // GET: AdminPhongDau/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPhongDau = await _context.ChiTietPhongDaus
                .Include(c => c.NguoiDung)
                .Include(c => c.PhongDau)
                .FirstOrDefaultAsync(m => m.ChiTietPhongDauId == id);
            if (chiTietPhongDau == null)
            {
                return NotFound();
            }

            return View(chiTietPhongDau);
        }

        // GET: AdminPhongDau/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId");
            ViewData["PhongDauId"] = new SelectList(_context.PhongDaus, "PhongDauId", "PhongDauId");
            return View();
        }

        // POST: AdminPhongDau/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietPhongDauId,PhongDauId,NguoiDungId,TongDiem,ThuHang")] ChiTietPhongDau chiTietPhongDau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietPhongDau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", chiTietPhongDau.NguoiDungId);
            ViewData["PhongDauId"] = new SelectList(_context.PhongDaus, "PhongDauId", "PhongDauId", chiTietPhongDau.PhongDauId);
            return View(chiTietPhongDau);
        }

        // GET: AdminPhongDau/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPhongDau = await _context.ChiTietPhongDaus.FindAsync(id);
            if (chiTietPhongDau == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", chiTietPhongDau.NguoiDungId);
            ViewData["PhongDauId"] = new SelectList(_context.PhongDaus, "PhongDauId", "PhongDauId", chiTietPhongDau.PhongDauId);
            return View(chiTietPhongDau);
        }

        // POST: AdminPhongDau/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietPhongDauId,PhongDauId,NguoiDungId,TongDiem,ThuHang")] ChiTietPhongDau chiTietPhongDau)
        {
            if (id != chiTietPhongDau.ChiTietPhongDauId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietPhongDau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietPhongDauExists(chiTietPhongDau.ChiTietPhongDauId))
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
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", chiTietPhongDau.NguoiDungId);
            ViewData["PhongDauId"] = new SelectList(_context.PhongDaus, "PhongDauId", "PhongDauId", chiTietPhongDau.PhongDauId);
            return View(chiTietPhongDau);
        }

        // GET: AdminPhongDau/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietPhongDau = await _context.ChiTietPhongDaus
                .Include(c => c.NguoiDung)
                .Include(c => c.PhongDau)
                .FirstOrDefaultAsync(m => m.ChiTietPhongDauId == id);
            if (chiTietPhongDau == null)
            {
                return NotFound();
            }

            return View(chiTietPhongDau);
        }

        // POST: AdminPhongDau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietPhongDau = await _context.ChiTietPhongDaus.FindAsync(id);
            _context.ChiTietPhongDaus.Remove(chiTietPhongDau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietPhongDauExists(int id)
        {
            return _context.ChiTietPhongDaus.Any(e => e.ChiTietPhongDauId == id);
        }
    }
}
