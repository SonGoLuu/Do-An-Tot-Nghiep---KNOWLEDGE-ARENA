﻿using Do_An_Tot_Nghiep.Hubs;
using Do_An_Tot_Nghiep.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbKA _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<GameHub> _signalrHub;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(dbKA context, ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserName(User);
            var user2 = "luu";
            ViewBag.userName = user;
            ViewBag.userName2 = user2;
            return View();
        }

        public IActionResult TaiKhoanCaNhan()
        {
            var user = _userManager.GetUserName(User);
            var nguoidung = _context.TaiKhoans.Where(x => x.TenDangNhap == user).Select(x => x.NguoiDungId).FirstOrDefault();
            if (nguoidung != null) return RedirectToAction("Details", "NguoiDung", new { id = nguoidung });
            else return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
        
}
