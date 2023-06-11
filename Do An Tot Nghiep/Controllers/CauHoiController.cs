using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Do_An_Tot_Nghiep.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Do_An_Tot_Nghiep.Controllers
{
    public class CauHoiController : Controller
    {
        private readonly dbKA _context;

        public CauHoiController(dbKA context)
        {
            _context = context;
        }

        // GET: CauHoi
        public async Task<IActionResult> Index()
        {
            var dbKA = _context.CauHois.Include(c => c.ChuongNgaiVat).Include(c => c.GoiCauHoi).Include(c => c.LinhVuc);
            return View(await dbKA.ToListAsync());
        }

        // GET: CauHoi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois
                .Include(c => c.ChuongNgaiVat)
                .Include(c => c.GoiCauHoi)
                .Include(c => c.LinhVuc)
                .FirstOrDefaultAsync(m => m.CauHoiId == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // GET: CauHoi/Create
        public IActionResult Create()
        {
            ViewData["ChuongNgaiVatId"] = new SelectList(_context.ChuongNgaiVats, "ChuongNgaiVatId", "ChuongNgaiVatId");
            ViewData["GoiCauHoiId"] = new SelectList(_context.GoiCauHois, "GoiCauHoiId", "GoiCauHoiId");
            ViewData["LinhVucId"] = new SelectList(_context.LinhVucCauHois, "LinhVucId", "LinhVucId");
            return View();
        }

        // POST: CauHoi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CauHoiId,VongCauHoi,NoiDung,LinhVucId,DapAn,SoKyTu," +
            "ChuongNgaiVatId,MucDo,Anh,GoiCauHoiId")] CauHoi cauHoi, IFormFile anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await anh.CopyToAsync(stream);
                        cauHoi.Anh = stream.ToArray();
                    }
                }
                _context.Add(cauHoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChuongNgaiVatId"] = new SelectList(_context.ChuongNgaiVats, "ChuongNgaiVatId", "ChuongNgaiVatId", cauHoi.ChuongNgaiVatId);
            ViewData["GoiCauHoiId"] = new SelectList(_context.GoiCauHois, "GoiCauHoiId", "GoiCauHoiId", cauHoi.GoiCauHoiId);
            ViewData["LinhVucId"] = new SelectList(_context.LinhVucCauHois, "LinhVucId", "LinhVucId", cauHoi.LinhVucId);
            return View(cauHoi);
        }

        // GET: CauHoi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return NotFound();
            }
            ViewData["ChuongNgaiVatId"] = new SelectList(_context.ChuongNgaiVats, "ChuongNgaiVatId", "ChuongNgaiVatId", cauHoi.ChuongNgaiVatId);
            ViewData["GoiCauHoiId"] = new SelectList(_context.GoiCauHois, "GoiCauHoiId", "GoiCauHoiId", cauHoi.GoiCauHoiId);
            ViewData["LinhVucId"] = new SelectList(_context.LinhVucCauHois, "LinhVucId", "LinhVucId", cauHoi.LinhVucId);
            return View(cauHoi);
        }

        // POST: CauHoi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CauHoiId,VongCauHoi,NoiDung,LinhVucId,DapAn,SoKyTu,ChuongNgaiVatId,MucDo,Anh,GoiCauHoiId")] CauHoi cauHoi, IFormFile anh)
        {
            if (id != cauHoi.CauHoiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldAnh = await _context.CauHois.AsNoTracking().Where(c => c.CauHoiId == id).Select(c => c.Anh).FirstOrDefaultAsync();
                if (anh != null && anh.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await anh.CopyToAsync(stream);
                        cauHoi.Anh = stream.ToArray();
                    }
                }
                else
                {
                    cauHoi.Anh = oldAnh;
                }    
                try
                {
                    _context.Update(cauHoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiExists(cauHoi.CauHoiId))
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
            ViewData["ChuongNgaiVatId"] = new SelectList(_context.ChuongNgaiVats, "ChuongNgaiVatId", "ChuongNgaiVatId", cauHoi.ChuongNgaiVatId);
            ViewData["GoiCauHoiId"] = new SelectList(_context.GoiCauHois, "GoiCauHoiId", "GoiCauHoiId", cauHoi.GoiCauHoiId);
            ViewData["LinhVucId"] = new SelectList(_context.LinhVucCauHois, "LinhVucId", "LinhVucId", cauHoi.LinhVucId);
            return View(cauHoi);
        }

        // GET: CauHoi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoi = await _context.CauHois
                .Include(c => c.ChuongNgaiVat)
                .Include(c => c.GoiCauHoi)
                .Include(c => c.LinhVuc)
                .FirstOrDefaultAsync(m => m.CauHoiId == id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            return View(cauHoi);
        }

        // POST: CauHoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            _context.CauHois.Remove(cauHoi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauHoiExists(int id)
        {
            return _context.CauHois.Any(e => e.CauHoiId == id);
        }
    }
}
