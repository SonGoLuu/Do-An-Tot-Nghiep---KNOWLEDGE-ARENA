using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Do_An_Tot_Nghiep.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Do_An_Tot_Nghiep.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly dbKA _context;

        public IndexModel(

            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, dbKA context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }



        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }



        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        [BindProperty]
        public NguoiDung NguoiDung { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var IdUser = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == userName)
                            .Select(x => x.NguoiDungId)
                            .FirstOrDefaultAsync();

            var player = await _context.NguoiDungs
                    .Where(x => x.NguoiDungId == IdUser)
                    .FirstOrDefaultAsync();

            NguoiDung = new NguoiDung
            {

            HoVaTen = player.HoVaTen,

            NgaySinh = player.NgaySinh,

            GioiTinh = player.GioiTinh,

            QueQuan = player.QueQuan,

            TruongHoc = player.TruongHoc,

            Avatar = player.Avatar

            };


            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //update infor nguoichoi
            var userName = await _userManager.GetUserNameAsync(user);

            var IdUser = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == userName)
                            .Select(x => x.NguoiDungId)
                            .FirstOrDefaultAsync();

            var player = await _context.NguoiDungs
                    .Where(x => x.NguoiDungId == IdUser)
                    .FirstOrDefaultAsync();

            player.HoVaTen = NguoiDung.HoVaTen;
            player.GioiTinh = NguoiDung.GioiTinh;
            player.NgaySinh = NguoiDung.NgaySinh;
            player.QueQuan = NguoiDung.QueQuan;
            player.TruongHoc = NguoiDung.TruongHoc;

            var oldava = player.Avatar;

            var avatarFile = Request.Form.Files.GetFile("NguoiDung.Avatar");
            if (avatarFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await avatarFile.CopyToAsync(memoryStream);
                    player.Avatar = memoryStream.ToArray();
                }
            }

            else
            {
                player.Avatar = oldava;
            }

            _context.Update(player);
            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
