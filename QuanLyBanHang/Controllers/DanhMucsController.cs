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

        [HttpGet]
        public async Task<JsonResult> GetDanhMuc()
        {
            return Json(await _context.DanhMucs.Include(x => x.DanhMucChiTiets).ToListAsync());
        }


        [HttpPost]
        public async Task<JsonResult> Create(string name)
        {
            try
            {
                if (DanhMucExists(name)) {
                    return Json("Tên danh mục đã tồn tại");
                }
                var danhMuc = new DanhMuc();
                danhMuc.ID = Guid.NewGuid();
                danhMuc.Name = name;
                _context.DanhMucs.Add(danhMuc);
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch
            {
                return Json("Đã xảy ra lỗi!");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetDanhMucByID(Guid? id)
        {
            var danhMuc = await _context.DanhMucs.FindAsync(id);
            return Json(danhMuc);
        }

        [HttpPost]
        public async Task<JsonResult> SaveDanhMuc(DanhMuc danhMuc)
        {
            try
            {
                var tmpDanhMuc = await _context.DanhMucs.FindAsync(danhMuc.ID);
                if (DanhMucExists(danhMuc.ID, danhMuc.Name))
                {
                    return Json("Tên danh mục đã tồn tại");
                }
                if (tmpDanhMuc == null)
                {
                    return Json("Có lỗi xảy ra");
                }
                tmpDanhMuc.Name = danhMuc.Name;
                _context.Update(tmpDanhMuc);
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Json("Có lỗi xảy ra");
            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteDanhMuc(Guid id)
        {
            try
            {
                var danhMuc = await _context.DanhMucs.FirstOrDefaultAsync(x => x.ID == id);
                var danhMucChiTiets = await _context.DanhMucChiTiets.Where(x => x.idDanhMuc == id).ToListAsync();
                if (danhMuc != null)
                {
                    _context.DanhMucChiTiets.RemoveRange(danhMucChiTiets);
                    _context.DanhMucs.Remove(danhMuc);
                    await _context.SaveChangesAsync();
                    return Json(true);
                }
                return Json("Xóa thất bại");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Json("Xóa thất bại");
            }
        }

        private bool DanhMucExists(string name)
        {
            return _context.DanhMucs.Any(e => e.Name == name);
        }

        private bool DanhMucExists(Guid id, string name)
        {
            return _context.DanhMucs.Any(e => e.ID != id && e.Name == name);
        }
    }
}
