using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBaoTanNgheThuat.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace WebBaoTanNgheThuat.Controllers
{

    public class HomeController : Controller
    {
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

        public ActionResult HomeMain()
        {
            return View();
        }

        public ActionResult MyThuat()
        {
            var ThongTin = UserManager.FindByName(User.Identity.Name);
            if(ThongTin == null)
            {
                return RedirectToAction("DangNhap", "Home", new { ThongBao = "hãy đăng nhập để sử dụng dịch vụ của chúng tôi!" });
            }
            else
            {
                ViewBag.Gmail = ThongTin.Email;
            }
            MyThuatDataContext Mt = new MyThuatDataContext();
            List<MYTHUAT> M = Mt.MYTHUATs.ToList();
            return View(M);
        }

        public ActionResult CoVat()
        {
            CoVatDataContext Cv = new CoVatDataContext();
            List<COVAT> C = Cv.COVATs.ToList();
            var ThongTin = UserManager.FindByName(User.Identity.Name);
            if (ThongTin == null)
            {
                return RedirectToAction("DangNhap", "Home", new { ThongBao = "hãy đăng nhập để sử dụng dịch vụ của chúng tôi!" });
            }
            else
            {
                ViewBag.Gmail = ThongTin.Email;
            }
            return View(C);
        }

        public ActionResult ChungToi()
        {
            return View();
        }

        public ActionResult DangNhap(String ThongBao)
        {
            ViewBag.ThongBao = ThongBao;
            return View();
        }

        public ActionResult Taikhoan(String ThongBao, String ThongBao1, String ChucNang)
        {
            var ThongTin = UserManager.FindByName(User.Identity.GetUserName());
            ViewBag.Ten = ThongTin.UserName;
            ViewBag.Gmail = ThongTin.Email;
            ViewBag.ThongBao = ThongBao;
            ViewBag.ThongBao1 = ThongBao1;
            ViewBag.ChucNang = ChucNang;
            return View();
        }

        public ActionResult DangKy(String ThongBao, String Gmail)
        {
            ViewBag.Gmail = Gmail;
            ViewBag.ThongBao = ThongBao;
            return View();
        }
    }
}