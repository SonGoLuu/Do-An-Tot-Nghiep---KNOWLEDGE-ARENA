using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Do_An_Tot_Nghiep.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Do_An_Tot_Nghiep.Hubs;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Connections.Features;

namespace Do_An_Tot_Nghiep.Controllers
{
    public class PhongChoController : Controller
    {
        private readonly dbKA _context;
        private readonly IHubContext<GameHub> _signalrHub;
        private readonly UserManager<IdentityUser> _userManager;

        public PhongChoController(dbKA context, UserManager<IdentityUser> userManager, IHubContext<GameHub> signalrHub)
        {
            _context = context;
            _userManager = userManager;
            _signalrHub = signalrHub;
        }

        // GET: PhongCho
        public async Task<IActionResult> Index()
        {
            var dbKA = _context.PhongChos.Include(p => p.NguoiDung);
            return View(await dbKA.ToListAsync());
        }

        //Join
        [HttpPost]
        public async Task<IActionResult> Join(int phongChoId)
        {
            var user = _userManager.GetUserName(User);
            //UserAndRoom userroom = new UserAndRoom();
            //userroom.UserName = user;
            //userroom.CurrentRoom = phongChoId.ToString();
            //ConnectedUser.Ids.Add(userroom);
            ConnectedUser.UpdateCurrentRoom(user, phongChoId.ToString());
            var userroom = ConnectedUser.Ids.FirstOrDefault(x => x.UserName == user);
            
            await _signalrHub.Groups.AddToGroupAsync(userroom.ConnectionId, phongChoId.ToString());  

            var userId = await _context.TaiKhoans
                .FirstOrDefaultAsync(pc => pc.TenDangNhap == user);
            var phongCho = await _context.PhongChos.Include(pc => pc.NguoiDung)
                .FirstOrDefaultAsync(pc =>pc.PhongChoId == phongChoId);
            if (phongCho != null && phongCho.SoLuongNguoi<4)
            {
                ChiTietPhongCho ctpc = new ChiTietPhongCho();
                ctpc.PhongChoId = phongChoId;
                ctpc.NguoiDungId = userId.TaiKhoanId;
                ctpc.ThoiGianVao = DateTime.Now;
                _context.Add(ctpc);
                await _context.SaveChangesAsync();

                // Gửi thông điệp đến tất cả các kết nối trong phòng chờ để thông báo rằng có một người dùng mới đã tham gia vào phòng chờ
                //await _signalrHub.Clients.Group(/*phongCho.PhongChoId.ToString()*/"1").SendAsync("NewUserJoined", user);
            }

            

            var username = await _context.TaiKhoans
                .Include(pc => pc.NguoiDung)
                .Where(pc => pc.TenDangNhap == user)
                .Select(pc => pc.NguoiDung.HoVaTen)
                .FirstOrDefaultAsync();

            await _signalrHub.Clients.Group(phongCho.PhongChoId.ToString()).SendAsync("NewUserJoined", username);
            // Chuyển hướng đến trang Room
            return RedirectToAction("Room", new { phongChoId });
        }


        //Room
        public async Task<IActionResult> Room(int phongChoId)
        {
            // Lấy thông tin phong cho
            var phongcho = await _context.PhongChos
                    .FirstOrDefaultAsync(pc => pc.PhongChoId == phongChoId);

            // Lấy danh sách người dùng trong phòng chờ
            var nguoiDung = await _context.ChiTietPhongChos
                    .Include(pc => pc.PhongCho)
                    .Include(pc => pc.NguoiDung)
                    .Where(pc=>pc.PhongChoId == phongChoId)
                    .Select(pc => pc.NguoiDung.HoVaTen)
                    .ToListAsync();

            // Truyền danh sách người dùng và thông tin phòng chờ vào view để hiển thị
            ViewBag.NguoiDung = nguoiDung;
            ViewBag.PhongCho = phongcho;

            
            var user = _userManager.GetUserName(User);
            var username = await _context.TaiKhoans
                .Include(pc => pc.NguoiDung)
                .Where(pc => pc.TenDangNhap == user)
                .Select(pc => pc.NguoiDung.HoVaTen)
                .FirstOrDefaultAsync();       
            return View();
        }

        public async Task<IActionResult> NavigateToPage(int phongchoid)
        {
            var user = _userManager.GetUserName(User);
            var IdUser = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == user)
                            .Select(x=> x.NguoiDungId)
                            .FirstOrDefaultAsync();
            var ctpc = await _context.ChiTietPhongChos
                        .Include(x => x.PhongCho)
                        .Include(x => x.NguoiDung)
                        .Where(x=> x.PhongChoId == phongchoid)
                        .ToListAsync();

            var phongcho = await _context.PhongChos
                        .Include(x => x.NguoiDung)
                        .Where(x => x.PhongChoId == phongchoid)
                        .FirstOrDefaultAsync();

            //thêm phòng đấu vào db
            var idphongdau = await _context.PhongDaus.Where(x => x.PhongDauId == phongcho.PhongChoId).FirstOrDefaultAsync();
            if(idphongdau == null)
            {
                PhongDau pd = new PhongDau();
                pd.PhongDauId = phongcho.PhongChoId;
                pd.NguoiDungId = phongcho.NguoiDungId;
                pd.ThoiGianBatDau = DateTime.Now;
                _context.Add(pd);
                await _context.SaveChangesAsync();
                //thêm chi tiết phòng đấu vào db
                foreach (var pc in ctpc)
                {
                    ChiTietPhongDau ctpd = new ChiTietPhongDau();
                    ctpd.PhongDauId = (int)pc.PhongChoId;
                    ctpd.NguoiDungId = pc.NguoiDungId;
                    ctpd.TongDiem = 0;
                    ctpd.ThuHang = 0;
                    _context.Add(ctpd);
                    await _context.SaveChangesAsync();
                }
            }    
            
            string groupName = phongchoid.ToString();
            string absoluteUrl = Url.Action("PhongDau", "PhongCho", new { phongdauid = phongchoid }, Request.Scheme);
            await _signalrHub.Clients.Group(groupName).SendAsync("navigateToPage", absoluteUrl);
            return Ok();
        }


        public async Task<IActionResult> NavigateToPage2(int phongchoid)
        {
            string groupName = phongchoid.ToString();
            string absoluteUrl = Url.Action("Vong2", "PhongCho", new { phongdauid = phongchoid }, Request.Scheme);
            await _signalrHub.Clients.Group(groupName).SendAsync("navigateToPage1", absoluteUrl);
            return Ok();
        }

        public async Task<IActionResult> PhongDau(int phongdauid)
        {
            var user = _userManager.GetUserName(User);
            var IdUser = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == user)
                            .Select(x => x.NguoiDungId)
                            .FirstOrDefaultAsync();
            ViewBag.playerid = IdUser;
            var HoVaTen = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == user)
                            .Select(x => x.NguoiDung.HoVaTen)
                            .FirstOrDefaultAsync();
            ViewBag.hovaten = HoVaTen;
            var players = await _context.ChiTietPhongDaus
                .Include(p => p.NguoiDung)
                .Where(p => p.PhongDauId == phongdauid)
                .ToListAsync();
            ViewBag.players = players;

            var cauhoi = await _context.CauHois
                .Include(x=>x.LinhVuc)
                .Where(x=>x.VongCauHoi==1).ToListAsync();
            ViewBag.cauhoi = cauhoi;

            ViewBag.phongdauid = phongdauid;
                
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckAnswer(int phongdauid, int playerid, int cauhoiid, string answer)
        {
            var ctpd = await _context.ChiTietPhongDaus
                        .Where(x => x.NguoiDungId == playerid && x.PhongDauId == phongdauid)
                        .FirstOrDefaultAsync();
            var cauhoi = await _context.CauHois
                            .Where(x => x.CauHoiId == cauhoiid)
                            .FirstOrDefaultAsync();
            if (answer == cauhoi.DapAn)
            {
                ctpd.TongDiem += 10;
                await _context.SaveChangesAsync();
            }
            var players = await _context.ChiTietPhongDaus
                                .Include(x => x.NguoiDung)
                                .Where(x => x.PhongDauId == phongdauid)
                                .Select(x => new PlayerScore
                                {
                                    Name = x.NguoiDung.HoVaTen,
                                    Score = (int)x.TongDiem
                                })
                                .ToListAsync();
            var html = "";
            foreach (var player in players)
            {
                //html += "<li>" + player.Name + ", Score: " + player.Score + "</li>";
                html += "<div class=\"box\">";
                html += "<p>" + player.Name.ToUpper() + "</p>";
                html += "<span>" + player.Score + "</span>";
                html += "</div>";
            }

            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateScore1", html);
            return Ok();
        }

        //Vòng 2

        public async Task<IActionResult> Vong2(int phongdauid)
        {
            var user = _userManager.GetUserName(User);
            var IdUser = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == user)
                            .Select(x => x.NguoiDungId)
                            .FirstOrDefaultAsync();
            ViewBag.playerid = IdUser;
            var HoVaTen = await _context.TaiKhoans.Include(x => x.NguoiDung)
                            .Where(x => x.TenDangNhap == user)
                            .Select(x => x.NguoiDung.HoVaTen)
                            .FirstOrDefaultAsync();
            ViewBag.hovaten = HoVaTen;
            var players = await _context.ChiTietPhongDaus
                .Include(p => p.NguoiDung)
                .Where(p => p.PhongDauId == phongdauid)
                .ToListAsync();
            ViewBag.players = players;

            var cauhoi = await _context.CauHois
                .Include(x => x.LinhVuc)
                .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1).ToListAsync();
            ViewBag.cauhoi = cauhoi;

            var chuongngaivat = await _context.ChuongNgaiVats
                .Where(x => x.ChuongNgaiVatId == 1).FirstOrDefaultAsync();
            ViewBag.chuongngaivat = chuongngaivat;

            ViewBag.phongdauid = phongdauid;

            Solangoi.Ids.Clear();

            //await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("NextQuestion", 0);

            return View();
        }

        //Check Answer
        //Dictionary<int, List<int>> usedQuestionsByPlayer = new Dictionary<int, List<int>>();

        private static DateTime lastSignalRSendTime = DateTime.MinValue;
        List<NguoiChoiTraLoiDung> nctld = new List<NguoiChoiTraLoiDung>();
        private static readonly object _lockObject = new object();
        [HttpPost]
        public async Task<IActionResult> CheckAnswerVong2(int chon, int phongdauid, int playerid, int cauhoiid, string answer, int solangoi)
        {
            var cauhoiv2 = await _context.CauHois
                .Include(x => x.LinhVuc)
                .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1).ToListAsync();
            var cauhoi2 = System.Text.Json.JsonSerializer.Serialize(cauhoiv2);
            if (chon == 1)
            {
                int check = 2;

                if (Solangoi.Ids.Any(x => x.idcauhoiid == cauhoiid))
                {
                    check = 1;
                }

                else if (cauhoiid != 0)
                {
                    var listcauhoi = await _context.CauHois
                    .Include(x => x.LinhVuc)
                    .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1 && x.CauHoiId == cauhoiid).FirstOrDefaultAsync();
                    if (listcauhoi != null)
                    {
                        check = 0;
                        //usedQuestions.Add(listcauhoi.CauHoiId);
                        //usedQuestionsByPlayer[playerid] = usedQuestions;
                        solan sl = new solan();
                        sl.idcauhoiid = cauhoiid;
                        Solangoi.Ids.Add(sl);
                    }
                }

                //checkCNV
                string checkcnv = await TraLoiCNV(phongdauid);
                if (checkcnv == "dung")
                {
                    var getAnswerDung = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "dung").FirstOrDefaultAsync();
                    var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
                    var dung = "dung";
                    var sai = "sai";
                    var dapandung = getAnswerDung.Answer;
                    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", getAnswerDung.NguoiDungId, dapandung, dung);
                    if (getAnswerSai.Count > 0)
                    {
                        foreach (TraLoiCNV an in getAnswerSai)
                        {
                            var dapansai = an.Answer;
                            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
                        }
                    }
                    await Task.Delay(5000);
                    var cnvnguoidungid = await _context.TraLoiCNVs
                                 .Where(x => x.PhongDauId == phongdauid && x.TrangThaiNguoiChoi == "dung")
                                 .Select(x => x.NguoiDungId).FirstOrDefaultAsync();

                    if (cnvnguoidungid == playerid)
                    {
                        var ctpdcnv = await _context.ChiTietPhongDaus
                                .Where(x => x.PhongDauId == phongdauid && x.NguoiDungId == cnvnguoidungid)
                                .FirstOrDefaultAsync();
                        ctpdcnv.TongDiem += 50;
                        var diemmoi = ctpdcnv.TongDiem;
                        await _context.SaveChangesAsync();
                    }
                    string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
                    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
                }

                else if (checkcnv == "sai")
                {
                    var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
                    if (getAnswerSai.Count > 0)
                    {
                        foreach (TraLoiCNV an in getAnswerSai)
                        {
                            var dapansai = an.Answer;
                            var sai = "sai";
                            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
                            an.TrangThaiNguoiChoi = "loai";
                            await _context.SaveChangesAsync();
                        }
                    }

                }

                var listbiloai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "loai").ToListAsync();

                var nguoibiloai = listbiloai.Where(x => x.NguoiDungId == playerid).FirstOrDefault();

                var ctpd = await _context.ChiTietPhongDaus
                        .Where(x => x.NguoiDungId == playerid && x.PhongDauId == phongdauid)
                        .FirstOrDefaultAsync();

                var cauhoi = await _context.CauHois
                                .Where(x => x.CauHoiId == cauhoiid)
                                .FirstOrDefaultAsync();

                TraLoiVong2 tlv2 = new TraLoiVong2();
                tlv2.PlayerId = playerid;
                tlv2.PhongDauId = phongdauid;
                if (listbiloai.Count > 0)
                {
                    if (nguoibiloai != null)
                    {
                        answer = "ĐÃ BỊ LOẠI";
                    }
                }
                else
                {

                }
                if(answer != null)
                {
                    if (answer == "")
                    {
                        tlv2.Answer = "chưa trả lời";
                    }
                    else
                    {
                        tlv2.Answer = answer;
                    }
                }
                else
                {
                    tlv2.Answer = "chưa trả lời";
                }

                if (answer == cauhoi.DapAn)
                {
                    //NguoiChoiTraLoiDung nc = new NguoiChoiTraLoiDung();
                    //nc.PlayerId = (int)ctpd.NguoiDungId;
                    //nctld.Add(nc);
                    ctpd.TongDiem += 10;
                    tlv2.Result = 1;
                    //await _context.SaveChangesAsync();

                }
                else
                {
                    tlv2.Result = 0;
                }
                tlv2.Id = 0;
                //var checktrungid = _context.TraLoiVong2s.Where(x => x.PlayerId == playerid).FirstOrDefault();
                //if (checktrungid != null)
                //{

                //}
                //else
                //{
                //    _context.Add(tlv2);
                //    _context.SaveChanges();
                //}

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var checktrungid = _context.TraLoiVong2s.Where(x => x.PlayerId == playerid).FirstOrDefault();
                        if (checktrungid != null)
                        {
                            // Nếu đối tượng đã tồn tại, không làm gì cả
                        }
                        else
                        {
                            _context.Add(tlv2);
                            await _context.SaveChangesAsync();
                            
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // Xử lý lỗi nếu có
                    }
                }

    //            var duplicates = _context.TraLoiVong2s
    //            .GroupBy(x => x.PlayerId)
    //             .Where(g => g.Count() > 1)
    //                .SelectMany(g => g.Skip(1))
    //            .AsEnumerable()
    //.ToList();


                //_context.TraLoiVong2s.RemoveRange(duplicates);
                //await _context.SaveChangesAsync();

                CauDaHoi cdh = new CauDaHoi();
                cdh.Id = 0;
                cdh.PhongDauId = phongdauid;
                cdh.CauHoiId = cauhoi.CauHoiId;
                _context.Add(cdh);
                await _context.SaveChangesAsync();

                var answerv2 =  _context.TraLoiVong2s.Where(x => x.PhongDauId == phongdauid).OrderBy(x => x.PlayerId).ToList();
                //hiển thị answer
                var html2 = "";
                foreach (var a in answerv2)
                {
                    if (a.Result == 1)
                    {
                        html2 += "<div class=\"box\" id=\"player" + a.PlayerId + "\">";
                    }
                    else
                    {
                        html2 += "<div class=\"box\" id=\"player" + a.PlayerId + "\">";
                    }
                    if (a.Answer != "")
                    {
                        html2 += "<p>" + a.Answer.ToUpper() + "</p>";
                    }
                    else
                    {
                        html2 += "<p>" + "CHƯA TRẢ LỜI" + "</p>";
                    }
                    html2 += "</div>";
                }

                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateAnswer", html2);

                await Task.Delay(3000);

                var answerv22 = _context.TraLoiVong2s.Where(x => x.PhongDauId == phongdauid).OrderBy(x => x.PlayerId).ToList();


                //hienthiketqua
                var html1 = "";

                foreach (var a in answerv22)
                {
                    if (a.Result == 1)
                    {
                        html1 += "<div class=\"box\" id=\"player" + a.PlayerId + "\" style=\"background: rgb(35, 147, 66);\">";
                    }
                    else
                    {
                        html1 += "<div class=\"box\" id=\"player" + a.PlayerId + "\" style=\"background: rgb(229, 61, 48);\">";
                    }
                    if (a.Answer != "")
                    {
                        html1 += "<p>" + a.Answer.ToUpper() + "</p>";
                    }
                    else
                    {
                        html1 += "<p>" + "CHƯA TRẢ LỜI" + "</p>";
                    }
                    html1 += "</div>";
                }

                var results = answerv2.Where(x => x.Result == 1).FirstOrDefault();

                int result;

                if (results != null) result = 1;
                else result = 0;

                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateResult", html1, cauhoiid, result);

                await Task.Delay(5000);

                var players = await _context.ChiTietPhongDaus
                                    .Include(x => x.NguoiDung)
                                    .Where(x => x.PhongDauId == phongdauid)
                                    .OrderBy(x => x.NguoiDungId)
                                    .Select(x => new PlayerScore
                                    {
                                        Name = x.NguoiDung.HoVaTen,
                                        Score = (int)x.TongDiem,
                                        Id = (int)x.NguoiDungId
                                    })
                                    .ToListAsync();
                var html = "";
                foreach (var player in players)
                {
                    //html += "<li>" + player.Name + ", Score: " + player.Score + "</li>";
                    html += "<div class=\"box\" id=\"player" + player.Id + "\">";
                    html += "<p>" + player.Name.ToUpper() + "</p>";
                    html += "<span>" + player.Score + "</span>";
                    html += "</div>";
                }

                //var listnc = System.Text.Json.JsonSerializer.Serialize(nctld);

                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateScore2", html);

                if (check == 0)
                {
                    // Kiểm tra thời gian giữa các lần gọi SignalR
                    if (DateTime.Now - lastSignalRSendTime < TimeSpan.FromSeconds(10))
                    {
                        return Ok(); // Nếu thời gian giữa các lần gọi SignalR nhỏ hơn 10 giây, bỏ qua yêu cầu SignalR này
                    }
                    lastSignalRSendTime = DateTime.Now; // Lưu trữ thời gian cuối cùng khi phương thức SignalR được gọi
                    var solangoii = solangoi;
                    // Gọi phương thức SignalR
                    //await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("NextQuestionne", solangoi);
                    var listnguoibiloai = System.Text.Json.JsonSerializer.Serialize(listbiloai);
                    var nguoichoi = await _context.ChiTietPhongDaus
                    .Include(p => p.NguoiDung)
                    .Where(p => p.PhongDauId == phongdauid)
                    .ToListAsync();
                    var listnguoichoi = System.Text.Json.JsonSerializer.Serialize(nguoichoi);

                    List<int> idbiloai = new List<int>();
                    for (int i = 0; i < listbiloai.Count; i++)
                    {
                        for (int j = 0; j < nguoichoi.Count; j++)
                        {
                            if (listbiloai[i].NguoiDungId == nguoichoi[j].NguoiDungId)
                            {
                                idbiloai.Add(j);
                            }
                        }
                    }
                    if (idbiloai.Count > 0)
                    {
                        idbiloai.Sort();
                    }

                    var list = System.Text.Json.JsonSerializer.Serialize(idbiloai);

                    var cdhs = await _context.CauDaHois.Where(x => x.PhongDauId == phongdauid).Select(x => x.CauHoiId).ToListAsync();

                    var cauhoichuahoi = await _context.CauHois
                    .Include(x => x.LinhVuc)
                    .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1 && !cdhs.Contains(x.CauHoiId)).ToListAsync();

                    var indexList = cauhoiv2
                    .Select((value, index) => index)
                    .Where(i => cauhoichuahoi.Any(c => c.Equals(cauhoiv2[i])))
                    .ToList();

                    var listchch = System.Text.Json.JsonSerializer.Serialize(indexList);
                    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("NextQuestionne", solangoi, list, listchch);

                }

            }

            //nếu chọn ==0
            else if (chon == 0)
            {
                //nctld.Clear();
                //// Lấy DbSet của bảng MyTable từ DbContext
                //var myTable = _context.Set<TraLoiVong2>();

                //// Lấy tất cả các bản ghi trong bảng MyTable
                //var records = myTable.Where(x => x.PhongDauId == phongdauid).ToList();

                //// Xóa tất cả các bản ghi trong bảng MyTable
                //myTable.RemoveRange(records);
                //await Task.Delay(10000);

                //var xoa = await _context.TraLoiVong2s.Where(x => x.PhongDauId == phongdauid).ToListAsync();
                //if(playerid == 1)
                //{
                //    if (xoa.Count > 0)
                //    {
                //        _context.RemoveRange(xoa);

                //        await _context.SaveChangesAsync();
                //    }
                //}   
                try
                {
                    lock (_lockObject)
                    {
                        _context.TraLoiVong2s.RemoveRange(_context.TraLoiVong2s);
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi
                }


                //if (playerid == 2)
                //{
                //    _context.TraLoiVong2s.RemoveRange(_context.TraLoiVong2s);
                //    await _context.SaveChangesAsync();
                //}    
                
                // Lưu thay đổi vào database
                //await _context.SaveChangesAsync();

                int hangngang = Convert.ToInt32(answer);
                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateQuestion", cauhoi2, hangngang);
                
            }

            //else khác
            else if (chon == 3)
            {
                //var getAnswerDung = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "dung").FirstOrDefaultAsync();
                //var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
                //var dung = "dung";
                //var sai = "sai";
                //var dapandung = getAnswerDung.Answer;
                //await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", getAnswerDung.NguoiDungId, dapandung, dung);
                //if (getAnswerSai.Count > 0)
                //{
                //    foreach (TraLoiCNV an in getAnswerSai)
                //    {
                //        var dapansai = an.Answer;
                //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
                //    }
                //}
                //await Task.Delay(5000);
                var cnvnguoidungid = await _context.TraLoiCNVs
                             .Where(x => x.PhongDauId == phongdauid && x.TrangThaiNguoiChoi == "dung")
                             .Select(x => x.NguoiDungId).FirstOrDefaultAsync();

                if (cnvnguoidungid == playerid)
                {
                    var ctpdcnv = await _context.ChiTietPhongDaus
                            .Where(x => x.PhongDauId == phongdauid && x.NguoiDungId == cnvnguoidungid)
                            .FirstOrDefaultAsync();
                    ctpdcnv.TongDiem += 50;
                    var diemmoi = ctpdcnv.TongDiem;
                    await _context.SaveChangesAsync();
                }
                //string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
                //await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
            }
            else if (chon == 4)
            {
                //var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
                //foreach (TraLoiCNV an in getAnswerSai)
                //{
                //    var dapansai = an.Answer;
                //    var sai = "sai";
                //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
                //    an.TrangThaiNguoiChoi = "loai";
                //    await _context.SaveChangesAsync();
                //    string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
                //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
                //}
                //string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
                //await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);

            }
            else if (chon == 5)
            {
                //string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
                //await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
            }
            return Ok();
        }


        //[HttpPost]
        //public async Task<IActionResult> CheckAnswerVong2(int chon, int phongdauid, int playerid, int cauhoiid, string answer, int solangoi)
        //{
        //    var cauhoiv2 = await _context.CauHois
        //        .Include(x => x.LinhVuc)
        //        .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1).ToListAsync();
        //    var cauhoi2 = System.Text.Json.JsonSerializer.Serialize(cauhoiv2);
        //    if (chon == 1)
        //    {
        //        //int check = 2;

        //        //if (Solangoi.Ids.Any(x => x.idcauhoiid == cauhoiid))
        //        //{
        //        //    check = 1;
        //        //}

        //        //else if (cauhoiid != 0)
        //        //    {
        //        //        var listcauhoi = await _context.CauHois
        //        //        .Include(x => x.LinhVuc)
        //        //        .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1 && x.CauHoiId == cauhoiid).FirstOrDefaultAsync();
        //        //        if (listcauhoi != null)
        //        //        {
        //        //            //check = 0;
        //        //            //usedQuestions.Add(listcauhoi.CauHoiId);
        //        //            //usedQuestionsByPlayer[playerid] = usedQuestions;
        //        //            solan sl = new solan();
        //        //            sl.idcauhoiid = cauhoiid;
        //        //            Solangoi.Ids.Add(sl);
        //        //        }
        //        //    }

        //        //checkCNV
        //        //string checkcnv = await TraLoiCNV(phongdauid);
        //        //if (checkcnv == "dung")
        //        //{
        //        //    var getAnswerDung = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "dung").FirstOrDefaultAsync();
        //        //    var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
        //        //    var dung = "dung";
        //        //    var sai = "sai";
        //        //    var dapandung = getAnswerDung.Answer;
        //        //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", getAnswerDung.NguoiDungId, dapandung, dung);
        //        //    if (getAnswerSai.Count > 0)
        //        //    {
        //        //        foreach (TraLoiCNV an in getAnswerSai)
        //        //        {
        //        //            var dapansai = an.Answer;
        //        //            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
        //        //        }
        //        //    }
        //        //    await Task.Delay(5000);
        //        //    var cnvnguoidungid = await _context.TraLoiCNVs
        //        //                 .Where(x => x.PhongDauId == phongdauid && x.TrangThaiNguoiChoi == "dung")
        //        //                 .Select(x => x.NguoiDungId).FirstOrDefaultAsync();

        //        //    if (cnvnguoidungid == playerid)
        //        //    {
        //        //        var ctpdcnv = await _context.ChiTietPhongDaus
        //        //                .Where(x => x.PhongDauId == phongdauid && x.NguoiDungId == cnvnguoidungid)
        //        //                .FirstOrDefaultAsync();
        //        //        ctpdcnv.TongDiem += 50;
        //        //        var diemmoi = ctpdcnv.TongDiem;
        //        //        await _context.SaveChangesAsync();
        //        //    }
        //        //    string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
        //        //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
        //        //}

        //        //else if (checkcnv == "sai")
        //        //{
        //        //    var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
        //        //    if (getAnswerSai.Count > 0)
        //        //    {
        //        //        foreach (TraLoiCNV an in getAnswerSai)
        //        //        {
        //        //            var dapansai = an.Answer;
        //        //            var sai = "sai";
        //        //            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
        //        //            an.TrangThaiNguoiChoi = "loai";
        //        //            await _context.SaveChangesAsync();
        //        //        }
        //        //    }

        //        //}

        //        var listbiloai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "loai").ToListAsync();

        //        var nguoibiloai = listbiloai.Where(x => x.NguoiDungId == playerid).FirstOrDefault();

        //        var ctpd = await _context.ChiTietPhongDaus
        //                .Where(x => x.NguoiDungId == playerid && x.PhongDauId == phongdauid)
        //                .FirstOrDefaultAsync();

        //        var cauhoi = await _context.CauHois
        //                        .Where(x => x.CauHoiId == cauhoiid)
        //                        .FirstOrDefaultAsync();

        //        TraLoiVong2 tlv2 = new TraLoiVong2();
        //        tlv2.PlayerId = playerid;
        //        tlv2.PhongDauId = phongdauid;
        //        //if(listbiloai.Count > 0)
        //        //{
        //        //    if (nguoibiloai != null)
        //        //    {
        //        //        answer = "ĐÃ BỊ LOẠI";
        //        //    }
        //        //}    
        //        //else
        //        //{

        //        //}

        //        if (answer != null)
        //        {
        //            if(answer == "")
        //            {
        //                tlv2.Answer = "chưa trả lời";
        //            }    
        //            else
        //            {
        //                tlv2.Answer = answer;
        //            }      
        //        }
        //        else
        //        {
        //            tlv2.Answer = "chưa trả lời";
        //        }

        //        if (answer == cauhoi.DapAn)
        //        {
        //            //NguoiChoiTraLoiDung nc = new NguoiChoiTraLoiDung();
        //            //nc.PlayerId = (int)ctpd.NguoiDungId;
        //            //nctld.Add(nc);
        //            ctpd.TongDiem += 10;
        //            tlv2.Result = 1;
        //            await _context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            tlv2.Result = 0;
        //        }

        //        var checktrungid = await _context.TraLoiVong2s.Where(x => x.PlayerId == playerid).FirstOrDefaultAsync();
        //        if(checktrungid != null)
        //        {

        //        }   
        //        else
        //        {
        //            tlv2.Id = 0;
        //            _context.TraLoiVong2s.Add(tlv2);
        //            await _context.SaveChangesAsync();
        //        }    


        //        CauDaHoi cdh = new CauDaHoi();
        //        cdh.PhongDauId = phongdauid;
        //        cdh.CauHoiId = cauhoi.CauHoiId;
        //        _context.Add(cdh);
        //        await _context.SaveChangesAsync();



        //        var answerv2 = await _context.TraLoiVong2s.Where(x=> x.PhongDauId == phongdauid).ToListAsync();
        //        //hiển thị answer
        //        var html2 = "";
        //        foreach (var a in answerv2)
        //        {
        //            html2 += "<div class=\"box\" id=\"player" + a.PlayerId + "\">";
        //            if (a.Answer != "")
        //            {
        //                html2 += "<p>" + a.Answer.ToUpper() + "</p>";
        //            }    
        //            else
        //            {
        //                html2 += "<p>" + "CHƯA TRẢ LỜI" + "</p>";
        //            }
        //            html2 += "</div>";
        //        }

        //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateAnswer", html2);

        //        await Task.Delay(3000);

        //        //hienthiketqua
        //        var html1 = "";

        //        foreach (var a in answerv2)
        //        {
        //            if (a.Result == 1)
        //            {
        //                html1 += "<div class=\"box\" id=\"player" + a.PlayerId + "\" style=\"background: rgb(35, 147, 66);\">";
        //            }
        //            else
        //            {
        //                html1 += "<div class=\"box\" id=\"player" + a.PlayerId + "\" style=\"background: rgb(229, 61, 48);\">";
        //            }
        //            if (a.Answer != "")
        //            {
        //                html1 += "<p>" + a.Answer.ToUpper() + "</p>";
        //            }
        //            else
        //            {
        //                html1 += "<p>" + "CHƯA TRẢ LỜI" + "</p>";
        //            }
        //            html1 += "</div>";
        //        }

        //        var results = answerv2.Where(x => x.Result == 1).FirstOrDefault();

        //        int result;

        //        if (results != null) result = 1;
        //        else result = 0;

        //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateResult", html1, cauhoiid, result);

        //        await Task.Delay(5000);

        //        var players = await _context.ChiTietPhongDaus
        //                            .Include(x => x.NguoiDung)
        //                            .Where(x => x.PhongDauId == phongdauid)
        //                            .Select(x => new PlayerScore
        //                            {
        //                                Name = x.NguoiDung.HoVaTen,
        //                                Score = (int)x.TongDiem,
        //                                Id = (int) x.NguoiDungId
        //                            })
        //                            .ToListAsync();
        //        var html = "";
        //        foreach (var player in players)
        //        {
        //            //html += "<li>" + player.Name + ", Score: " + player.Score + "</li>";
        //            html += "<div class=\"box\" id=\"player" + player.Id + "\">";
        //            html += "<p>" + player.Name.ToUpper() + "</p>";
        //            html += "<span>" + player.Score + "</span>";
        //            html += "</div>";
        //        }

        //        var listnc = System.Text.Json.JsonSerializer.Serialize(nctld);

        //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateScore2", html, listnc);

        //        //if (check == 0)
        //        //{
        //        //    // Kiểm tra thời gian giữa các lần gọi SignalR
        //        //    if (DateTime.Now - lastSignalRSendTime < TimeSpan.FromSeconds(10))
        //        //    {
        //        //        return Ok(); // Nếu thời gian giữa các lần gọi SignalR nhỏ hơn 10 giây, bỏ qua yêu cầu SignalR này
        //        //    }
        //        //    lastSignalRSendTime = DateTime.Now; // Lưu trữ thời gian cuối cùng khi phương thức SignalR được gọi
        //            var solangoii = solangoi;
        //            // Gọi phương thức SignalR
        //            var listnguoibiloai = System.Text.Json.JsonSerializer.Serialize(listbiloai);
        //            var nguoichoi = await _context.ChiTietPhongDaus
        //            .Include(p => p.NguoiDung)
        //            .Where(p => p.PhongDauId == phongdauid)
        //            .ToListAsync();
        //            var listnguoichoi = System.Text.Json.JsonSerializer.Serialize(nguoichoi);

        //            List<int> idbiloai = new List<int>();
        //            for(int i = 0; i < listbiloai.Count; i++)
        //            {
        //                for(int j = 0; j < nguoichoi.Count; j++)
        //                {
        //                    if(listbiloai[i].NguoiDungId == nguoichoi[j].NguoiDungId)
        //                    {
        //                        idbiloai.Add(j);
        //                    }    
        //                }    
        //            }
        //            if(idbiloai.Count > 0)
        //            {
        //                idbiloai.Sort();
        //            }

        //            var list = System.Text.Json.JsonSerializer.Serialize(idbiloai);

        //            var cdhs = await _context.CauDaHois.Where(x => x.PhongDauId == phongdauid).Select(x=> x.CauHoiId).ToListAsync();

        //            var cauhoichuahoi = await _context.CauHois
        //            .Include(x => x.LinhVuc)
        //            .Where(x => x.VongCauHoi == 2 && x.ChuongNgaiVatId == 1 && !cdhs.Contains(x.CauHoiId)).ToListAsync();

        //            var indexList = cauhoiv2
        //            .Select((value, index) => index)
        //            .Where(i => cauhoichuahoi.Any(c => c.Equals(cauhoiv2[i])))
        //            .ToList();

        //            var listchch = System.Text.Json.JsonSerializer.Serialize(indexList);
        //            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("NextQuestionne", solangoi, list, listchch);
        //        //}

        //    }

        //    //nếu chọn ==0
        //    else if (chon == 0)
        //    {
        //        nctld.Clear();
        //        // Lấy DbSet của bảng MyTable từ DbContext
        //        var myTable = _context.Set<TraLoiVong2>();

        //        // Lấy tất cả các bản ghi trong bảng MyTable
        //        var records = myTable.Where(x=> x.PhongDauId == phongdauid).ToList();

        //        // Xóa tất cả các bản ghi trong bảng MyTable
        //        myTable.RemoveRange(records);

        //        // Lưu thay đổi vào database
        //        await _context.SaveChangesAsync();

        //        int hangngang = Convert.ToInt32(answer);
        //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("UpdateQuestion",  cauhoi2, hangngang);
        //    }
        //    //else if (chon == 3)
        //    //{
        //    //    var getAnswerDung = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "dung").FirstOrDefaultAsync();
        //    //    var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
        //    //    var dung = "dung";
        //    //    var sai = "sai";
        //    //    var dapandung = getAnswerDung.Answer;
        //    //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", getAnswerDung.NguoiDungId, dapandung, dung);
        //    //    if (getAnswerSai.Count > 0)
        //    //    {
        //    //        foreach (TraLoiCNV an in getAnswerSai)
        //    //        {
        //    //            var dapansai = an.Answer;
        //    //            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
        //    //        }
        //    //    }
        //    //    await Task.Delay(5000);
        //    //    var cnvnguoidungid = await _context.TraLoiCNVs
        //    //                 .Where(x => x.PhongDauId == phongdauid && x.TrangThaiNguoiChoi == "dung")
        //    //                 .Select(x => x.NguoiDungId).FirstOrDefaultAsync();

        //    //    if (cnvnguoidungid == playerid)
        //    //    {
        //    //        var ctpdcnv = await _context.ChiTietPhongDaus
        //    //                .Where(x => x.PhongDauId == phongdauid && x.NguoiDungId == cnvnguoidungid)
        //    //                .FirstOrDefaultAsync();
        //    //        ctpdcnv.TongDiem += 50;
        //    //        var diemmoi = ctpdcnv.TongDiem;
        //    //        await _context.SaveChangesAsync();
        //    //    }
        //    //    string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
        //    //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
        //    //}
        //    //else if (chon == 4)
        //    //{
        //    //    var getAnswerSai = await _context.TraLoiCNVs.Where(x => x.TrangThaiNguoiChoi == "sai").ToListAsync();
        //    //    foreach (TraLoiCNV an in getAnswerSai)
        //    //    {
        //    //        var dapansai = an.Answer;
        //    //        var sai = "sai";
        //    //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", an.NguoiDungId, dapansai, sai);
        //    //        an.TrangThaiNguoiChoi = "loai";
        //    //        await _context.SaveChangesAsync();
        //    //        string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
        //    //        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
        //    //    }
        //    //}
        //    //else if (chon == 5)
        //    //{
        //    //    string absoluteUrl = Url.Action("Index", "PhongCho", Request.Scheme);
        //    //    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("navigateToPage1", absoluteUrl);
        //    //}
        //    return Ok();
        //}


        [HttpPost]
        public async Task<IActionResult> LuuTraLoiCNV(int phongdauid, int playerid, int chuongngaivatid, string answer)
        {
            
                TraLoiCNV cnv = new TraLoiCNV();
                cnv.PhongDauId = phongdauid;
                cnv.NguoiDungId = playerid;
                cnv.HoVaTen = _context.NguoiDungs.Where(x => x.NguoiDungId == playerid).Select(x => x.HoVaTen).FirstOrDefault();
                cnv.ChuongNgaiVatId = chuongngaivatid;
                cnv.Answer = "";
                cnv.ThoiGian = DateTime.Now;
                cnv.TrangThaiNguoiChoi = "chuatraloi";
                _context.Add(cnv);
                await _context.SaveChangesAsync();

                var listcnv = await _context.TraLoiCNVs.Where(x => x.PhongDauId == phongdauid).ToListAsync();
                var list = System.Text.Json.JsonSerializer.Serialize(listcnv);

            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("NguoiBamCNV", list);
               
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> CheckDapAnCNV(int phongdauid, int playerid, int chuongngaivatid, string answer)
        {
            await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("HienThiCauTraLoiCNV", playerid, answer);
            var updatetrangthaicnv = await _context.TraLoiCNVs.Where(x => x.NguoiDungId == playerid && x.PhongDauId == phongdauid).FirstOrDefaultAsync();
            updatetrangthaicnv.Answer = answer;
            await _context.SaveChangesAsync();
            var dapan = await _context.ChuongNgaiVats.Where(x => x.ChuongNgaiVatId == chuongngaivatid).Select(x => x.DapAn).FirstOrDefaultAsync();
            if(dapan == answer)
            {
                updatetrangthaicnv.TrangThaiNguoiChoi = "dung";
                await _context.SaveChangesAsync();
                var dung = "dung";
                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", playerid, answer, dung);
            }    
            else
            {
                updatetrangthaicnv.TrangThaiNguoiChoi = "sai";
                await _context.SaveChangesAsync();
                var sai = "sai";
                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ResutlTraLoiCNV", playerid, answer, sai);
            }    
            return Ok();
        }



        public async Task<String> TraLoiCNV(int phongdauid)
        {
            var bamcnv = await _context.TraLoiCNVs.Where(x => x.PhongDauId == phongdauid).OrderBy(x=> x.ThoiGian).ToListAsync();
            if (bamcnv.Count > 0)
            {
                foreach (TraLoiCNV cnv in bamcnv)
                {
                    if (cnv.TrangThaiNguoiChoi == "chuatraloi")
                    {
                        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ChonNguoiTraLoiCNV", cnv.NguoiDungId);
                        await Task.Delay(20000);
                        //await _context.SaveChangesAsync();
                        var cnvcopy = await _context.TraLoiCNVs
                            .Where(x => x.PhongDauId == phongdauid && x.NguoiDungId == cnv.NguoiDungId)
                            .Select(x => x.TrangThaiNguoiChoi).FirstOrDefaultAsync();
                        if (cnvcopy == "dung")
                        {
                            return "dung";
                        }
                    }
                }
                return "sai";
            }
            return "khongco";
        }

        [HttpPost]
        public async Task<ActionResult> CheckSau10s(int phongdauid)
        {
            int loai;
            var bamcnv = await _context.TraLoiCNVs
                .Where(x => x.PhongDauId == phongdauid && x.TrangThaiNguoiChoi == "chuatraloi").ToListAsync();
            if(bamcnv.Count > 0)
            {
                foreach (TraLoiCNV cnv in bamcnv)
                {
                    await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("ChonNguoiTraLoiCNV", cnv.NguoiDungId);
                    await Task.Delay(20000);
                    //await _context.SaveChangesAsync();
                    var cnvcopy = await _context.TraLoiCNVs
                        .Where(x => x.PhongDauId == phongdauid && x.NguoiDungId == cnv.NguoiDungId)
                        .Select(x => x.TrangThaiNguoiChoi).FirstOrDefaultAsync();
                    if (cnvcopy == "dung")
                    {
                        loai = 3;
                        await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("GoiCheckLanCuoi", loai);
                        return Ok();
                    }
                }
                loai = 4;
                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("GoiCheckLanCuoi", loai);
                return Ok();
            }    
            else
            {
                loai = 5;
                await _signalrHub.Clients.Group(phongdauid.ToString()).SendAsync("GoiCheckLanCuoi", loai);
                return Ok();
            }    
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongCho = await _context.PhongChos
                .Include(p => p.NguoiDung)
                .FirstOrDefaultAsync(m => m.PhongChoId == id);
            if (phongCho == null)
            {
                return NotFound();
            }

            return View(phongCho);
        }

        // GET: PhongCho/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId");
            return View();
        }

        // POST: PhongCho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhongChoId,NguoiDungId,SoLuongNguoi,ThoiGianLap")] PhongCho phongCho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phongCho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", phongCho.NguoiDungId);
            return View(phongCho);
        }

        // GET: PhongCho/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongCho = await _context.PhongChos.FindAsync(id);
            if (phongCho == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", phongCho.NguoiDungId);
            return View(phongCho);
        }

        // POST: PhongCho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhongChoId,NguoiDungId,SoLuongNguoi,ThoiGianLap")] PhongCho phongCho)
        {
            if (id != phongCho.PhongChoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongCho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongChoExists(phongCho.PhongChoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", phongCho.NguoiDungId);
            return View(phongCho);
        }

        // GET: PhongCho/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phongCho = await _context.PhongChos
                .Include(p => p.NguoiDung)
                .FirstOrDefaultAsync(m => m.PhongChoId == id);
            if (phongCho == null)
            {
                return NotFound();
            }

            return View(phongCho);
        }

        // POST: PhongCho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phongCho = await _context.PhongChos.FindAsync(id);
            _context.PhongChos.Remove(phongCho);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongChoExists(int id)
        {
            return _context.PhongChos.Any(e => e.PhongChoId == id);
        }
    }
}
