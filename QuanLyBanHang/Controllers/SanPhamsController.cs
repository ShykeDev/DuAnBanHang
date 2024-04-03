using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBase.EF;
using DataBase.Entities;
using QuanLyBanHang.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Drawing;

namespace QuanLyBanHang.Controllers
{
    public class SanPhamsController : BaseController
    {
        public SanPhamModel sanPhamModel = new SanPhamModel();
        public SanPhamsController()
        {
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["Role"] != "0")
            {
                return RedirectToAction("Page404", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateSanPham(SanPhamModelAdd result)
        {
            result.sanPham.ID = Guid.NewGuid();
            if (result.ThuocTinhChungs != null)
            {
                foreach (var item in result.ThuocTinhChungs)
                {
                    item.ID = Guid.NewGuid();
                    item.thuocTinhChung.ID = item.ID;
                    foreach (var giatri in item.giaTriThuocTinhs)
                    {
                        giatri.IDThuocTinh = item.ID;
                        _context.Add(giatri);
                    }
                    _context.Add(item.thuocTinhChung);
                    _context.Add(new ThuocTinh(item.ID, result.sanPham.ID));
                }
            }
            _context.Add(new ItemImage(result.sanPham.ID, result.ImgsPath));
            _context.Add(result.sanPham);
            try
            {
                await _context.SaveChangesAsync();
                return Json("Thêm thành công");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Json("Thêm thất bại");
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditSanPham(SanPhamModelAdd result)
        {
            try
            {
                var sanPham = await _context.SanPhams
                                            .Include(p => p.thuocTinhs)
                                            .Include(p => p.anhs)
                                            .FirstOrDefaultAsync(m => m.ID == result.sanPham.ID);
                sanPham.Name = result.sanPham.Name;
                sanPham.GiaGoc = Convert.ToInt32(result.sanPham.GiaGoc);
                sanPham.GiaGiamGia = Convert.ToInt32(result.sanPham.GiaGiamGia);
                sanPham.SoLuong = Convert.ToInt32(result.sanPham.SoLuong);
                sanPham.MoTa = result.sanPham.MoTa;
                sanPham.anhs.Img = result.ImgsPath;
                if (sanPham != null && sanPham.thuocTinhs != null)
                {
                    foreach (var item in sanPham.thuocTinhs)
                    {
                        var tmp = _context.ThuocTinhChungs.Include(p => p.GiaTriThuocTinhs).Where(e => e.ID == item.ID).ToList();
                        foreach (var item2 in tmp)
                        {
                            _context.GiaTriThuocTinhs.RemoveRange(item2.GiaTriThuocTinhs);
                        }
                        _context.ThuocTinhChungs.RemoveRange(tmp);
                    }
                    _context.ThuocTinhs.RemoveRange(sanPham.thuocTinhs);
                }
                if (result.ThuocTinhChungs != null)
                {
                    foreach (var item in result.ThuocTinhChungs)
                    {
                        item.ID = Guid.NewGuid();
                        item.thuocTinhChung.ID = item.ID;
                        foreach (var giatri in item.giaTriThuocTinhs)
                        {
                            giatri.IDThuocTinh = item.ID;
                            _context.Add(giatri);
                        }
                        _context.Add(item.thuocTinhChung);
                        _context.Add(new ThuocTinh(item.ID, result.sanPham.ID));
                    }
                }
                _context.Update(sanPham);
                await _context.SaveChangesAsync();
                return Json("Sửa thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(e.Message);
            }
        }


        [HttpPost]
        public async Task<JsonResult> DeleteSanPham(Guid? id)
        {
            if (id == null) return Json("thất bại");
            try
            {
                var sanPham = await _context.SanPhams.Include(p => p.thuocTinhs).FirstOrDefaultAsync(m => m.ID == id);
                if (sanPham != null && sanPham.thuocTinhs != null)
                {
                    foreach (var item in sanPham.thuocTinhs)
                    {
                        var tmp = _context.ThuocTinhChungs.Include(p => p.GiaTriThuocTinhs).Where(e => e.ID == item.ID).ToList();
                        foreach (var item2 in tmp)
                        {
                            _context.GiaTriThuocTinhs.RemoveRange(item2.GiaTriThuocTinhs);
                        }
                        _context.ThuocTinhChungs.RemoveRange(tmp);
                    }
                    _context.ThuocTinhs.RemoveRange(sanPham.thuocTinhs);
                    _context.SanPhams.RemoveRange(sanPham);
                }
                await _context.SaveChangesAsync();
                return Json("Xóa thành công");
            }
            catch (Exception)
            {
                return Json("thất bại");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetSanPham(Guid? id)
        {
            if (id == null) return Json(null);
            try
            {
                var sanPham = await _context.SanPhams
                                            .Include(p => p.thuocTinhs)
                                            .Include(p => p.anhs)
                                            .FirstOrDefaultAsync(m => m.ID == id);
                if (sanPham != null && sanPham.thuocTinhs != null)
                {
                    foreach (var item in sanPham.thuocTinhs)
                    {
                        var tmp = _context.ThuocTinhChungs.Include(p => p.GiaTriThuocTinhs).Where(e => e.ID == item.ID).ToList();
                    }
                }
                return Json(sanPham);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        [HttpGet]
        public async Task<JsonResult> ListSanPham()
        {
            try
            {
                return Json(await _context.SanPhams.ToListAsync());
            }
            catch (Exception)
            {
                return Json(null);
            }
        }


    }
}
