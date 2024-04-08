using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBase.EF;
using DataBase.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using QuanLyBanHang.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace QuanLyBanHang.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["Role"] == "0")
            {
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
            } else {
                return RedirectToAction("Page404", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            MainDbContext _context = new MainDbContext();
            return Json(await _context.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<JsonResult> GetUsersByID(Guid id)
        {
            MainDbContext _context = new MainDbContext();
            return Json(await _context.Users.Include(u => u.GioHangChiTiets).FirstOrDefaultAsync(u => u.ID == id));
        }

        [HttpGet]
        public async Task<JsonResult> DeleteUser(Guid id)
        {
            MainDbContext _context = new MainDbContext();
            try
            {
                var user = await _context.Users
                    .Include(u => u.HoaDons)
                    .Include(u => u.GioHangChiTiets)
                    .FirstAsync(u => u.ID == id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch (System.Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddUser(User user)
        {
            MainDbContext _context = new MainDbContext();
            if (user.Name == "" || user.Name == null)
            {
                return Json("Vui lòng nhập Họ và tên");
            }
            if (user.SDT == "" || user.SDT == null)
            {
                return Json("Vui lòng nhập số điện thoại");
            }
            //Regex numberphone
            if (!Regex.IsMatch(user.SDT, @"^\d{10}$")) {
                return Json("Số điện thoại không hợp lệ");
            }
            if (user.UserName == "" || user.UserName == null)
            {
                return Json("Vui lòng nhập username");
            }
            if (user.Password == "" || user.Password == null)
            {
                return Json("Vui lòng nhập password");
            }
            //regex password
            if (!Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d$@$!%*?&]{6,}$")) {
                return Json("Password phải chứa cả ký tự hoa, ký tự và số");
            }
            if (user.Password.Length < 6) {
                return Json("Password phải nhiều hơn 6 ký tự");
            }
            if (user.DiaChi == "" || user.DiaChi == null)
            {
                return Json("Vui lòng nhập địa chỉ");
            }
            if (user.Email == "" || user.Email == null)
            {
                return Json("Vui lòng nhập email");
            }
            if (!Regex.IsMatch(user.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) {
                return Json("Email không hợp lệ");
            }
            if (user.NgaySinh == null)
            {
                return Json("Vui này nhận ngày sinh");
            }
            if (CalculateAge(Convert.ToDateTime(user.NgaySinh)) < 7 || CalculateAge(Convert.ToDateTime(user.NgaySinh)) > 100)
            {
                return Json("Tuổi không hợp lệ");
            }
            if (!UserExists(user.UserName))
            {
                try
                {
                    user.ID = Guid.NewGuid();
                    user.Role = Convert.ToInt32(user.Role);
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return Json(true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Json("Vui lòng thử lại");
                }
            }
            else
            {
                return Json("Username Đã tồn tại");
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveUser(User user)
        {
            MainDbContext _context = new MainDbContext();
            if (user.Name == "" || user.Name == null)
            {
                return Json("Vui lòng nhập Họ và tên");
            }
            if (user.SDT == "" || user.SDT == null)
            {
                return Json("Vui lòng nhập số điện thoại");
            }
            //Regex numberphone
            if (!Regex.IsMatch(user.SDT, @"^\d{10}$")) {
                return Json("Số điện thoại không hợp lệ");
            }
            if (user.UserName == "" || user.UserName == null)
            {
                return Json("Vui lòng nhập username");
            }
            if (user.Password == "" || user.Password == null)
            {
                return Json("Vui lòng nhập password");
            }
            //regex password
            if (!Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d$@$!%*?&]{6,}$")) {
                return Json("Password phải chứa cả ký tự hoa, ký tự và số");
            }
            if (user.Password.Length < 6) {
                return Json("Password phải nhiều hơn 6 ký tự");
            }
            if (user.DiaChi == "" || user.DiaChi == null)
            {
                return Json("Vui lòng nhập địa chỉ");
            }
            if (user.Email == "" || user.Email == null)
            {
                return Json("Vui lòng nhập email");
            }
            if (!Regex.IsMatch(user.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) {
                return Json("Email không hợp lệ");
            }
            if (user.NgaySinh == null)
            {
                return Json("Vui này nhận ngày sinh");
            }
            if (CalculateAge(Convert.ToDateTime(user.NgaySinh)) < 7 || CalculateAge(Convert.ToDateTime(user.NgaySinh)) > 100)
            {
                return Json("Tuổi không hợp lệ");
            }
            var tmp = _context.Users.First(u => u.ID.Equals(user.ID));
            if (tmp != null)
            {
                if (!UserExists(user.UserName, tmp.ID))
                {
                    tmp.Name = user.Name;
                    tmp.UserName = user.UserName;
                    tmp.Password = user.Password;
                    tmp.NgaySinh = user.NgaySinh;
                    tmp.SDT = user.SDT;
                    tmp.State = user.State;
                    tmp.Role = user.Role;
                    tmp.DiaChi = user.DiaChi;
                    try
                    {
                        user.Role = Convert.ToInt32(user.Role);
                        _context.Update(tmp);
                        await _context.SaveChangesAsync();
                        return Json(true);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Json("Vui lòng thử lại");
                    }
                }
                else
                {
                    return Json("Tài khoản đã tồn tại");
                }
            }
            return Json(false);
        }


        private bool UserExists(Guid id)
        {
            MainDbContext _context = new MainDbContext();
            return _context.Users.Any(e => e.ID == id);
        }

        private bool UserExists(string userName)
        {
            MainDbContext _context = new MainDbContext();
            return _context.Users.Any(e => e.UserName == userName);
        }

        private bool UserExists(string userName, Guid id)
        {
            MainDbContext _context = new MainDbContext();
            return _context.Users.Any(e => e.UserName == userName && e.ID != id);
        }

    }
}
