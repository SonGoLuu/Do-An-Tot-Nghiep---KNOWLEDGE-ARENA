using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Do_An_Tot_Nghiep.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Do_An_Tot_Nghiep.Controllers
{
    public class ChuongNgaiVatController : Controller
    {
        private readonly dbKA _context;

        public ChuongNgaiVatController(dbKA context)
        {
            _context = context;
        }

        // GET: ChuongNgaiVat
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChuongNgaiVats.ToListAsync());
        }

        // GET: ChuongNgaiVat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongNgaiVat = await _context.ChuongNgaiVats
                .FirstOrDefaultAsync(m => m.ChuongNgaiVatId == id);
            if (chuongNgaiVat == null)
            {
                return NotFound();
            }

            return View(chuongNgaiVat);
        }

        // GET: ChuongNgaiVat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChuongNgaiVat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChuongNgaiVatId,SoKyTu,DapAn,Anh")] ChuongNgaiVat chuongNgaiVat, IFormFile anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await anh.CopyToAsync(stream);
                        chuongNgaiVat.Anh = stream.ToArray();
                    }
                }
                _context.Add(chuongNgaiVat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuongNgaiVat);
        }

        // GET: ChuongNgaiVat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongNgaiVat = await _context.ChuongNgaiVats.FindAsync(id);
            if (chuongNgaiVat == null)
            {
                return NotFound();
            }
            return View(chuongNgaiVat);
        }

        // POST: ChuongNgaiVat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChuongNgaiVatId,SoKyTu,DapAn,Anh")] ChuongNgaiVat chuongNgaiVat, IFormFile anh)
        {
            if (id != chuongNgaiVat.ChuongNgaiVatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldAnh = await _context.ChuongNgaiVats.AsNoTracking().Where(c => c.ChuongNgaiVatId == id).Select(c => c.Anh).FirstOrDefaultAsync();
                if (anh != null && anh.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await anh.CopyToAsync(stream);
                        chuongNgaiVat.Anh = stream.ToArray();
                    }
                }
                else
                {
                    chuongNgaiVat.Anh = oldAnh;
                }
                try
                {
                    _context.Update(chuongNgaiVat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuongNgaiVatExists(chuongNgaiVat.ChuongNgaiVatId))
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
            return View(chuongNgaiVat);
        }

        // GET: ChuongNgaiVat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuongNgaiVat = await _context.ChuongNgaiVats
                .FirstOrDefaultAsync(m => m.ChuongNgaiVatId == id);
            if (chuongNgaiVat == null)
            {
                return NotFound();
            }

            return View(chuongNgaiVat);
        }

        // POST: ChuongNgaiVat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chuongNgaiVat = await _context.ChuongNgaiVats.FindAsync(id);
            _context.ChuongNgaiVats.Remove(chuongNgaiVat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuongNgaiVatExists(int id)
        {
            return _context.ChuongNgaiVats.Any(e => e.ChuongNgaiVatId == id);
        }
    }
}
