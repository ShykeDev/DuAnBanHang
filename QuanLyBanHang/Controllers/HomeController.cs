using DataBase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;
using System.Diagnostics;
using System.Text.Json;
using DataBase.EF;
using System.Text.RegularExpressions;

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
                HttpContext.Response.Cookies.Append("Role", "1");
                HttpContext.Response.Cookies.Append("Name", "Người dùng");
            }
            if (HttpContext.Request.Cookies["Role"] == "0")
            {
                ViewData["Layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ViewData["Layout"] = "~/Views/Shared/_LayoutUser.cshtml";
            }
            return View();
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

        public IActionResult GioHang()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> OnLogin(string username, string password)
        {
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d$@$!%*?&]{6,}$"))
            {
                return Json("Password phải chứa cả ký tự hoa, ký tự và số");
            }
            if (password.Length < 6)
            {
                return Json("Password phải nhiều hơn 6 ký tự");
            }
            MainDbContext _context = new MainDbContext();
            if (_context.Users.Any(user => user.UserName == username && user.Password == password && user.State == 0))
            {
                var User = _context.Users.First(user => user.UserName == username && user.Password == password);
                HttpContext.Response.Cookies.Delete("ID");
                HttpContext.Response.Cookies.Delete("Name");
                HttpContext.Response.Cookies.Delete("UserName");
                HttpContext.Response.Cookies.Delete("Password");
                HttpContext.Response.Cookies.Delete("Role");
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
            if (!Regex.IsMatch(user.SDT, @"^\d{10}$"))
            {
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
            if (!Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d$@$!%*?&]{6,}$"))
            {
                return Json("Password phải chứa cả ký tự hoa, ký tự và số");
            }
            if (user.Password.Length < 6)
            {
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
            if (!Regex.IsMatch(user.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
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
            MainDbContext _context = new MainDbContext();
            try
            {
                if (_context.Users.Any(user => user.ID == ID))
                {
                    var User = _context.Users.Include(u => u.GioHangChiTiets).First(user => user.ID == ID);
                    return Json(User);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                return Json("Lỗi");
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetSanPhamGioHang(Guid id)
        {
            MainDbContext _context = new MainDbContext();
            try
            {
                if (id != null && id.ToString() != "")
                {
                    var tmp = _context.GioHangChiTiets.Include(sp => sp.sanPham).Where(gh => gh.UserID == id).ToList();
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        tmp[i].sanPham.anhs = _context.ItemImages.FirstOrDefault(sp => sp.ID == tmp[i].IDSanPham);
                    }
                    return Json(tmp);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }

            return Json(false);
        }

        [HttpGet]
        public async Task<JsonResult> RemoveSanPhamGioHang(string id)
        {
            MainDbContext _context = new MainDbContext();
            try
            {
                _context.GioHangChiTiets.Remove(_context.GioHangChiTiets.First(gh => gh.ID.ToString() == id));
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }


        [HttpPost]
        public async Task<JsonResult> AddGioHang(Guid userID, Guid sanPhamID, string thuocTinh, int soLuong)
        {
            MainDbContext _context = new MainDbContext();
            try
            {
                if (!await _context.Users.AnyAsync(u => u.ID == userID))
                {
                    //Chưa đăng nhập
                    return Json(false);
                }
                var giohangTmp = _context.GioHangChiTiets.FirstOrDefault(gh => gh.UserID == userID && gh.IDSanPham == sanPhamID && gh.ThuocTinh == thuocTinh);
                if (giohangTmp == null)
                {
                    Console.WriteLine("Thêm");
                    _context.GioHangChiTiets.Add(new GioHangChiTiet(Guid.NewGuid(), sanPhamID, userID, soLuong, thuocTinh));
                    await _context.SaveChangesAsync();
                    return Json(true);
                }
                else
                {
                    Console.WriteLine("Update");
                    giohangTmp.SoLuong += soLuong;
                    _context.Update(giohangTmp);
                    await _context.SaveChangesAsync();
                    return Json(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Json("Đã xảy ra lỗi");
            }

        }

        [HttpPost]
        public void ThemHoaDon(HoaDon hoaDon, List<HoaDonChiTiet> hoaDonChiTiets)
        {
            MainDbContext _context = new MainDbContext();
            hoaDon.ID = Guid.NewGuid();
            _context.HoaDons.Add(hoaDon);
            foreach (var item in hoaDonChiTiets)
            {
                _context.HoaDonChiTiets.Add(new HoaDonChiTiet(Guid.NewGuid(), hoaDon.ID, item.IDSanPham, item.GiaSanPham, item.SoLuong, item.ThuocTinh));
            }
            _context.SaveChanges();
        }

        private bool UserExists(string userName)
        {
            MainDbContext _context = new MainDbContext();
            return _context.Users.Any(e => e.UserName == userName);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
