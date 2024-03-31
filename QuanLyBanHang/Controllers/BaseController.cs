using DataBase.EF;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public enum NotificationState
    {
        error,
        success,
        warning
    }

    public enum typeNotify
    {
        alert,
        toast
    }

    public class BaseController : Controller
    {
        public static MainDbContext _context = new MainDbContext();


        public void Notify(string message, typeNotify type, NotificationState notificationType = NotificationState.success, string title = "Thông báo")
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = type,
                provider = GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        private string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProvider"];

            return value;
        }
    }
}
