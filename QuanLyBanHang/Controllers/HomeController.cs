

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
            return View(await _context.SanPhams.ToListAsync());
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> OnLogin(string username, string password)
        {
            if (_context.Users.Any(user => user.UserName == username && user.Password == password))
            {
                var User = _context.Users.First(user => user.UserName == username && user.Password == password);
                Notify("Đăng nhập thành công", typeNotify.alert);
                TempData["User"] = username;
                return View("Index");
            }
            else
            {
                Notify("Tài khoản không tồn tại", typeNotify.alert, NotificationState.error);
            }
            return View("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
