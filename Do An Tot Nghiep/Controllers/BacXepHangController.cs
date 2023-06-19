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
    public class BacXepHangController : Controller
    {
        private readonly dbKA _context;

        public BacXepHangController(dbKA context)
        {
            _context = context;
        }

        // GET: BacXepHang
        public async Task<IActionResult> Index()
        {
            return View(await _context.BacXepHangs.ToListAsync());
        }

        // GET: BacXepHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacXepHang = await _context.BacXepHangs
                .FirstOrDefaultAsync(m => m.BacXepHangId == id);
            if (bacXepHang == null)
            {
                return NotFound();
            }

            return View(bacXepHang);
        }

        // GET: BacXepHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BacXepHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BacXepHangId,BacHang,Anh")] BacXepHang bacXepHang, IFormFile anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await anh.CopyToAsync(stream);
                        bacXepHang.Anh = stream.ToArray();
                    }
                }
                _context.Add(bacXepHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bacXepHang);
        }

        // GET: BacXepHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacXepHang = await _context.BacXepHangs.FindAsync(id);
            if (bacXepHang == null)
            {
                return NotFound();
            }
            return View(bacXepHang);
        }

        // POST: BacXepHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BacXepHangId,BacHang,Anh")] BacXepHang bacXepHang, IFormFile anh)
        {
            if (id != bacXepHang.BacXepHangId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var oldAnh = await _context.BacXepHangs.AsNoTracking().Where(c => c.BacXepHangId == id).Select(c => c.Anh).FirstOrDefaultAsync();
                if (anh != null && anh.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await anh.CopyToAsync(stream);
                        bacXepHang.Anh = stream.ToArray();
                    }
                }
                else
                {
                    bacXepHang.Anh = oldAnh;
                }
                try
                {
                    _context.Update(bacXepHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BacXepHangExists(bacXepHang.BacXepHangId))
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
            return View(bacXepHang);
        }

        // GET: BacXepHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacXepHang = await _context.BacXepHangs
                .FirstOrDefaultAsync(m => m.BacXepHangId == id);
            if (bacXepHang == null)
            {
                return NotFound();
            }

            return View(bacXepHang);
        }

        // POST: BacXepHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacXepHang = await _context.BacXepHangs.FindAsync(id);
            _context.BacXepHangs.Remove(bacXepHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BacXepHangExists(int id)
        {
            return _context.BacXepHangs.Any(e => e.BacXepHangId == id);
        }
    }
}
