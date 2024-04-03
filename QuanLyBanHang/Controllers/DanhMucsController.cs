using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBase.EF;
using DataBase.Entities;
using Newtonsoft.Json.Bson;

namespace QuanLyBanHang.Controllers
{
    public class DanhMucsController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucs.Include(x => x.DanhMucChiTiets).ToListAsync());
        }

        public async Task<IActionResult> GetDanhMuc()
        {
            return Json(await _context.DanhMucs.Include(x => x.DanhMucChiTiets).ToListAsync());
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        [HttpPost]
        public async Task<JsonResult> Create(string name)
        {
            try
            {
                var danhMuc = new DanhMuc();
                danhMuc.ID = Guid.NewGuid();
                danhMuc.Name = name;

                _context.Add(danhMuc);
                await _context.SaveChangesAsync();
                return Json(true);
            } catch
            {
                return Json("Đã xảy ra lỗi!");
            }
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name")] DanhMuc danhMuc)
        {
            if (id != danhMuc.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.ID))
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
            return View(danhMuc);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // POST: DanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc != null)
            {
                _context.DanhMucs.Remove(danhMuc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhMucExists(Guid id)
        {
            return _context.DanhMucs.Any(e => e.ID == id);
        }

        private bool DanhMucExists(Guid id, string name)
        {
            return _context.DanhMucs.Any(e => e.ID != id && e.Name == name);
        }
    }
}
