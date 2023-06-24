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
    public class AdminPhongChoController : Controller
    {
        private readonly dbKA _context;

        public AdminPhongChoController(dbKA context)
        {
            _context = context;
        }

        // GET: AdminPhongCho
        public async Task<IActionResult> Index()
        {
            var dbKA = _context.PhongChos.Include(p => p.NguoiDung);
            return View(await dbKA.ToListAsync());
        }

        // GET: AdminPhongCho/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongCho = await _context.PhongChos
                .Include(p => p.NguoiDung)
                .FirstOrDefaultAsync(m => m.PhongChoId == id);
            if (phongCho == null)
            {
                return NotFound();
            }

            return View(phongCho);
        }

        // GET: AdminPhongCho/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId");
            return View();
        }

        // POST: AdminPhongCho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhongChoId,NguoiDungId,SoLuongNguoi,ThoiGianLap")] PhongCho phongCho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phongCho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", phongCho.NguoiDungId);
            return View(phongCho);
        }

        // GET: AdminPhongCho/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongCho = await _context.PhongChos.FindAsync(id);
            if (phongCho == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", phongCho.NguoiDungId);
            return View(phongCho);
        }

        // POST: AdminPhongCho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhongChoId,NguoiDungId,SoLuongNguoi,ThoiGianLap")] PhongCho phongCho)
        {
            if (id != phongCho.PhongChoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongCho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongChoExists(phongCho.PhongChoId))
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
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", phongCho.NguoiDungId);
            return View(phongCho);
        }

        // GET: AdminPhongCho/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongCho = await _context.PhongChos
                .Include(p => p.NguoiDung)
                .FirstOrDefaultAsync(m => m.PhongChoId == id);
            if (phongCho == null)
            {
                return NotFound();
            }

            return View(phongCho);
        }

        // POST: AdminPhongCho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phongCho = await _context.PhongChos.FindAsync(id);
            _context.PhongChos.Remove(phongCho);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongChoExists(int id)
        {
            return _context.PhongChos.Any(e => e.PhongChoId == id);
        }
    }
}
