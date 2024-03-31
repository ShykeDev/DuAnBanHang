

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
                return View("Login");
            }
            Console.WriteLine(HttpContext.Request.Cookies["Name"]);
            return View(await _context.SanPhams.Include(sp => sp.anhs).ToListAsync());
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
                Notify("Xin chào "+ User.Name, typeNotify.alert, NotificationState.success, "Đăng nhập thành công");
                return Json(true);
            }
            else
            {
                return Json("Tài khoản không tồn tại, vui lòng nhập lại");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
