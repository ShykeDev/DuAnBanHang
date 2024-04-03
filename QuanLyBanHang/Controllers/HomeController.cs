

using DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;
using System.Diagnostics;

namespace QuanLyBanHang.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["Name"] == null || HttpContext.Request.Cookies["Name"] == "")
            {
                HttpContext.Response.Cookies.Append("Name", "Người dùng");
                HttpContext.Response.Cookies.Append("Role", "1");
            }
            if (HttpContext.Request.Cookies["Role"] == "0")
            {
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
                return RedirectToAction("Index", "Users");
            } else {
                ViewData["Layout"] = "~/Views/Shared/_LayoutUser.cshtml";
            }
            return View(await _context.SanPhams.Include(sp => sp.anhs).ToListAsync());
        }

        public IActionResult Page404()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> OnLogin(string username, string password)
        {
            if (_context.Users.Any(user => user.UserName == username && user.Password == password))
            {
                var User = _context.Users.First(user => user.UserName == username && user.Password == password);
                HttpContext.Response.Cookies.Append("ID", User.ID.ToString());
                HttpContext.Response.Cookies.Append("Name", User.Name);
                HttpContext.Response.Cookies.Append("UserName", User.UserName);
                HttpContext.Response.Cookies.Append("Password", User.Password);
                HttpContext.Response.Cookies.Append("Role", User.Role.ToString());
                Notify("Xin chào " + User.Name, typeNotify.alert, NotificationState.success, "Đăng nhập thành công");
                return Json(true);
            }
            else
            {
                return Json("Tài khoản không tồn tại, vui lòng nhập lại");
            }
        }

        [HttpGet]
        public async Task<JsonResult> OnRegister(User user)
        {
            if (user.Name == "" || user.Name == null)
            {
                return Json("Vui lòng nhập Họ và tên");
            }
            if (user.SDT == "" || user.SDT == null)
            {
                return Json("Vui lòng nhập số điện thoại");
            }
            if (user.UserName == "" || user.UserName == null)
            {
                return Json("Vui lòng nhập username");
            }
            if (user.Password == "" || user.Password == null)
            {
                return Json("Vui lòng nhập password");
            }
            if (user.DiaChi == "" || user.DiaChi == null)
            {
                return Json("Vui lòng nhập địa chỉ");
            }
            if (user.Email == "" || user.Email == null)
            {
                return Json("Vui lòng nhập email");
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
                    user.Role = 1;
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

        [HttpGet]
        public async Task<JsonResult> GetUser(Guid ID)
        {
            if (_context.Users.Any(user => user.ID == ID))
            {
                var User = _context.Users.First(user => user.ID == ID);
                return Json(User);
            }
            else
            {
                return Json(false);
            }
        }

        private bool UserExists(string userName)
        {
            return _context.Users.Any(e => e.UserName == userName);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
