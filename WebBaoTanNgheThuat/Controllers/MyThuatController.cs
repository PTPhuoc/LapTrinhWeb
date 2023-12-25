using System;
using System.Linq;
using System.Web.Mvc;
using WebBaoTanNgheThuat.Models;

namespace WebBaoTanNgheThuat.Controllers
{
    public class MyThuatController : Controller
    {
        // GET: MyThuat
        public ActionResult DangBaiMyThuat()
        {
            return View();
        }

        public ActionResult DangBai(DangBaiViewModel m, String SRC)
        {
            DateTime Ngay = DateTime.Now;
            MyThuatDataContext Mt = new MyThuatDataContext();
            MYTHUAT M = new MYTHUAT();
            M.SRC = SRC;
            M.TEN = m.Ten;
            M.TEN_TAC_GIA = m.TenTacGia;
            M.NOI_DUNG = m.NoiDung;
            M.THOI_GIAN = "Ngày " + Ngay.Day.ToString() + ", Tháng " + Ngay.Month.ToString() + ", Vào lúc " + Ngay.Hour.ToString() + ":" + Ngay.Minute.ToString();
            Mt.MYTHUATs.InsertOnSubmit(M);
            Mt.SubmitChanges();
            return RedirectToAction("MyThuat", "Home");
        }

        public ActionResult XoaBai(String SRC)
        {
            MyThuatDataContext Mt = new MyThuatDataContext();
            MYTHUAT M = new MYTHUAT();
            var ViTri = from m in Mt.MYTHUATs where m.SRC == SRC select m;
            foreach(var v in ViTri)
            {
                Mt.MYTHUATs.DeleteOnSubmit(v);
            }
            Mt.SubmitChanges();
            return RedirectToAction("MyThuat", "Home");
        }
    }

}