using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBase.EF;
using DataBase.Entities;

namespace QuanLyBanHang.Controllers
{
    public class HoaDonsController : Controller
    {

        // GET: HoaDons
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["Role"] != "0")
            {
                return RedirectToAction("Page404", "Home");
            }
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetAllHoaDon()
        {
            MainDbContext _context = new MainDbContext();
            try
            {
                var listUser = _context.Users.Include(sp => sp.HoaDons);
                var listHoaDons = _context.HoaDons.Include(sp => sp.HoaDonChiTiets);
                var listSanPham = _context.SanPhams.Include(sp => sp.anhs);
                return Json(new { success = true, User = listUser, hoaDon = listHoaDons, sanPham = listSanPham });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateHoaDon(Guid id, int state)
        {
            MainDbContext _context = new MainDbContext();
            try
            {
                var hoaDonItem = _context.HoaDons.Include(hd => hd.HoaDonChiTiets).First(hd => hd.ID == id);
                hoaDonItem.TrangThaiDonHang = state;
                if (state == 3)
                {
                    foreach (var item in hoaDonItem.HoaDonChiTiets)
                    {
                        var sanPham = _context.SanPhams.First(sp => sp.ID == item.IDSanPham);
                        sanPham.SoLuong += item.SoLuong;
                        _context.SanPhams.Update(sanPham);
                    }
                }
                _context.Update(hoaDonItem);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }




        // GET: HoaDons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var _context = new MainDbContext();
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,NgayMua,SDT,Email,DiaChi,ChuThich,TrangThaiDonHang")] HoaDon hoaDon)
        {
            var _context = new MainDbContext();
            if (ModelState.IsValid)
            {
                hoaDon.ID = Guid.NewGuid();
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoaDon);
        }

        // GET: HoaDons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            var _context = new MainDbContext();
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,UserID,NgayMua,SDT,Email,DiaChi,ChuThich,TrangThaiDonHang")] HoaDon hoaDon)
        {
            var _context = new MainDbContext();
            if (id != hoaDon.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.ID))
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
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var _context = new MainDbContext();
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var _context = new MainDbContext();
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(Guid id)
        {
            var _context = new MainDbContext();
            return _context.HoaDons.Any(e => e.ID == id);
        }
    }
}
