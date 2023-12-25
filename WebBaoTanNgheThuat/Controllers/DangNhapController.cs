using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBaoTanNgheThuat.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WebBaoTanNgheThuat.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: DangNhap
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DangKyUser(DangKyViewModel D, ConfirmCodeViewModel C, String Gmail)
        {
            TaiKhoanModel Tk = new TaiKhoanModel();
            //await DeleteConfirmed("tanphuocphan372@gmail.com");
            var User = await UserManager.FindByEmailAsync(Gmail);
            if (D.MatKhau == null)
            {
                if (User != null )
                {
                    return RedirectToAction("DangKy", "Home", new { ThongBao = "Gmail Da Ton Tai!" });
                }
                else
                {
                    var r = new Random();
                    C.Code = r.Next(1000, 9999);
                    return RedirectToAction("ManagerSend", "DangNhap", new { Gmail = D.Gmail, Code = C.Code });
                }
            }
            else
            {
                var Tao = new ApplicationUser { UserName = D.Ten, Email = Gmail };
                var Kiem = await UserManager.CreateAsync(Tao, D.MatKhau);
                if (Kiem.Succeeded)
                {
                    var user = await UserManager.FindByEmailAsync(Gmail);
                    //await Them(user.Email, user.UserName, D.MatKhau, "true");
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var result = await UserManager.ConfirmEmailAsync(user.Id, code);
                    await SignInManager.SignInAsync(Tao, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("HomeMain", "Home");
                }
                else
                {
                    return RedirectToAction("DangKy", "Home", new { Thongbao = "Mật khẩu phải bao gồm chử in hoa, thường, số và ký tự đặt biệt.", Gmail = Gmail });
                }
            }
        }

        public async Task<ActionResult>  XoaTaiKhoan( DoiTenViewModel D,string Email)
        {
            if(D.Gmail == null)
            {
                return RedirectToAction("TaiKhoan", "Home", new { ThongBao = "Không được để trống!" });
            }

            var user = await UserManager.FindByEmailAsync(D.Gmail);
            if(user == null)
            {
                return RedirectToAction("TaiKhoan", "Home", new { ThongBao = "Tài khoản này vẩn chưa đăng ký!" });
            }

            var logins = user.Logins;
            var rolesForUser = await UserManager.GetRolesAsync(user.Id);
            foreach (var login in logins.ToList())
            {
                await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }
            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    // item should be the name of the role
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                }
            }
            await UserManager.DeleteAsync(user);
            return RedirectToAction("TaiKhoan", "Home", new {ThongBao = "Xóa thành công!"});
        }

        public ActionResult XatNhanEmail(String ThongBao, String Gmail, String Code)
        {
            ViewBag.Gmail = Gmail;
            ViewBag.ThongBao = ThongBao;
            ViewBag.Code = Code;
            return View();
        }

        [HttpPost]
        public ActionResult XacNhan(ConfirmCodeViewModel C, String Gmail, String Code)
        {
            if (C.Code.ToString().Equals(C.VerifyCode))
            {
                return RedirectToAction("DangKy", "Home", new { Gmail = Gmail });
            }
            return RedirectToAction("XatNhanEmail", "DangNhap", new { ThongBao = "Ma Khong Dung!" });
        }

        public async Task SendMail(String Title, String EmailUser, String NameUser, String Mess, String HtmlMess)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tanphuocphan370@gmail.com", "Admin");
            var to = new EmailAddress(EmailUser, NameUser);
            var plainTextContent = Mess;
            var htmlContent = HtmlMess;
            var msg = MailHelper.CreateSingleEmail(from, to, Title, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task<ActionResult> ManagerSend(String Gmail, int Code)
        {
            DangKyViewModel D = new DangKyViewModel();
            String title = "Xác Nhận Email";
            String Mess = "Gửi " + Gmail
                + "\nMã Xác Nhận Email Của Bạn Là: " + Code;
            await SendMail(title, Gmail, Gmail, Mess, null);
            return RedirectToAction("XatNhanEmail", "DangNhap", new { Gmail = Gmail, Code = Code.ToString() });
        }

        [HttpPost]
        public async Task<ActionResult> DangNhap(DangNhapViewModel M)
        {
            var DangNhap = await SignInManager.PasswordSignInAsync(M.Ten, M.MatKhau, false, shouldLockout: false);
            switch (DangNhap)
            {
                case SignInStatus.Success:
                    return RedirectToAction("TaiKhoan", "Home");
                case SignInStatus.Failure:
                    return RedirectToAction("DangNhap", "Home", new { ThongBao = "Tai Khoan Khong Ton Tai!" });
            }
            return RedirectToAction("DangNhap", "Home", new { ThongBao = "Tai Khoan Cua Ban Da Bi Loi!"});
        }

        public ActionResult DangNhapGoogle(String DichVu)
        {
            return new ChallengeResult(DichVu, Url.Action("LayThongTin", "DangNhap"));
        }

        [HttpPost]
        public ActionResult DangXuat()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("HomeMain", "Home");
        }

        public async Task<ActionResult> LayThongTin()
        {
            //await DeleteConfirmed("tanphuocphan372@gmail.com");
            var ThongTin = await AuthenticationManager.GetExternalLoginInfoAsync();
            var KetQua = await SignInManager.ExternalSignInAsync(ThongTin, isPersistent: false);
            switch(KetQua)
            {
                case SignInStatus.Success:
                    return RedirectToAction("TaiKhoan", "Home");
                case SignInStatus.Failure:
                    return RedirectToAction("TaoGmailMoi", "DangNhap", new {Gmail = ThongTin.Email});
            }
            return RedirectToAction("DangNhap", "Home", new { ThongBao = "Gmail Cua Ban Da Bi Loi!" });
        }

        public ActionResult SetPasswordUser( String ThongBao)
        {
            ViewBag.ThongBao = ThongBao;
            return View();
        }

        public async Task<ActionResult> SetPassword(SetPasswordViewModel Sp)
        {
            var ThongTin = await AuthenticationManager.GetExternalLoginInfoAsync();
            var Tao = new ApplicationUser { UserName = ThongTin.DefaultUserName, Email = ThongTin.Email };
            var Kiem = await UserManager.CreateAsync(Tao, Sp.MatKhau);
            if (Kiem.Succeeded)
            {
                var user = await UserManager.FindByEmailAsync(ThongTin.Email);
                //await Them(user.Email, user.UserName, Sp.MatKhau, "true");
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                await UserManager.AddLoginAsync(user.Id, ThongTin.Login);
                await UserManager.ConfirmEmailAsync(user.Id, code);
                await SignInManager.SignInAsync(Tao, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("HomeMain", "Home");
            }
            else
            {
                return RedirectToAction("SetPasswordUser", "DangNhap", new { Thongbao = "*Mật khẩu phải bao gồm chử in hoa, thường và số!"});
            }
        }

        public ActionResult DoiMk(String ThongBao)
        {
            ViewBag.ThongBao = ThongBao;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DoiMatKhau(DoiMatKhauViewModel D)
        {
            if(D.MatKhauMoi == null || D.MatKhauCu== null || D.XacNhanMk== null)
            {
                return RedirectToAction("DoiMk", "DangNhap", new { ThongBao = "*Không Được Để Trống" });
            }
            if(D.MatKhauMoi != D.XacNhanMk)
            {
                return RedirectToAction("DoiMk", "DangNhap", new { ThongBao = "*Mật Khẩu Không Trùng Nhau" });
            }else if(D.MatKhauMoi.Length < 6 || D.XacNhanMk.Length < 6)
            {
                return RedirectToAction("DoiMk", "DangNhap", new { ThongBao = "*Mật Khẩu Phải Trên 6 Ký Tự" });
            }
            else
            {
                var KetQua = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), D.MatKhauCu, D.MatKhauMoi);
                if (KetQua.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("TaiKhoan", "Home");
                }
                return RedirectToAction("DoiMk", "DangNhap", new { ThongBao = "*Mật khẩu phải có ký tự in hoa và số hoặc mật khẩu cũ sai" });
            }
        }

        public ActionResult TaoGmailMoi(String Gmail)
        {
            ViewBag.Gmail = Gmail;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SetNameUser(DoiTenViewModel D, String Gmail)
        {
            if(D.Ten == null)
            {
                return RedirectToAction("TaiKhoan", "Home", new {ThongBao1 = "Tên không được để trống!"});
            }
            var ThongTin = await UserManager.FindByEmailAsync(Gmail);
            ThongTin.UserName = D.Ten;
            await UserManager.UpdateAsync(ThongTin);
            await SignInManager.SignInAsync(ThongTin, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("TaiKhoan", "Home");
        }

        #region Helpers
        // Được sử dụng để bảo vệ XSRF khi thêm thông tin đăng nhập bên ngoài
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}