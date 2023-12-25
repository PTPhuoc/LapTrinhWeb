using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBaoTanNgheThuat.Models;

namespace WebBaoTanNgheThuat.Controllers
{
    public class CoVatController : Controller
    {
        // GET: CoVat
        public ActionResult DangBaiCoVat()
        {
            return View();
        }

        public ActionResult DangBai(DangBaiViewModel m, String SRC)
        {
            DateTime Ngay = DateTime.Now;
            CoVatDataContext Mt = new CoVatDataContext();
            COVAT M = new COVAT();
            M.SRC = SRC;
            M.TEN = m.Ten;
            M.TEN_TAC_GIA = m.TenTacGia;
            M.NOI_DUNG = m.NoiDung;
            M.THOI_GIAN = "Ngày " + Ngay.Day.ToString() + ", Tháng " + Ngay.Month.ToString() + ", Vào lúc " + Ngay.Hour.ToString() + ":" + Ngay.Minute.ToString();
            Mt.COVATs.InsertOnSubmit(M);
            Mt.SubmitChanges();
            return RedirectToAction("CoVat", "Home");
        }

        public ActionResult XoaBai(String SRC)
        {
            CoVatDataContext Mt = new CoVatDataContext();
            COVAT M = new COVAT();
            var ViTri = from m in Mt.COVATs where m.SRC == SRC select m;
            foreach (var v in ViTri)
            {
                Mt.COVATs.DeleteOnSubmit(v);
            }
            Mt.SubmitChanges();
            return RedirectToAction("CoVat", "Home");
        }
    }
}