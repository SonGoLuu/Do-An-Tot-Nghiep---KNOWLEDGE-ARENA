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
    public class AdminTaiKhoanController : Controller
    {
        private readonly dbKA _context;

        public AdminTaiKhoanController(dbKA context)
        {
            _context = context;
        }

        // GET: AdminTaiKhoan
        public async Task<IActionResult> Index()
        {
            var dbKA = _context.TaiKhoans.Include(t => t.NguoiDung).Include(t => t.PhanQuyen);
            return View(await dbKA.ToListAsync());
        }

        // GET: AdminTaiKhoan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.NguoiDung)
                .Include(t => t.PhanQuyen)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: AdminTaiKhoan/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId");
            ViewData["PhanQuyenId"] = new SelectList(_context.PhanQuyens, "PhanQuyenId", "PhanQuyenId");
            return View();
        }

        // POST: AdminTaiKhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaiKhoanId,TenDangNhap,MatKhau,PhanQuyenId,NguoiDungId")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", taiKhoan.NguoiDungId);
            ViewData["PhanQuyenId"] = new SelectList(_context.PhanQuyens, "PhanQuyenId", "PhanQuyenId", taiKhoan.PhanQuyenId);
            return View(taiKhoan);
        }

        // GET: AdminTaiKhoan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", taiKhoan.NguoiDungId);
            ViewData["PhanQuyenId"] = new SelectList(_context.PhanQuyens, "PhanQuyenId", "PhanQuyenId", taiKhoan.PhanQuyenId);
            return View(taiKhoan);
        }

        // POST: AdminTaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaiKhoanId,TenDangNhap,MatKhau,PhanQuyenId,NguoiDungId")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanId))
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
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", taiKhoan.NguoiDungId);
            ViewData["PhanQuyenId"] = new SelectList(_context.PhanQuyens, "PhanQuyenId", "PhanQuyenId", taiKhoan.PhanQuyenId);
            return View(taiKhoan);
        }

        // GET: AdminTaiKhoan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .Include(t => t.NguoiDung)
                .Include(t => t.PhanQuyen)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: AdminTaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoans.Any(e => e.TaiKhoanId == id);
        }
    }
}
