using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using KinhDoanhThuCung.Models;

namespace KinhDoanhThuCung.Controllers
{
    public class SearchController : Controller
    {
        ThuCungDataContext db = new ThuCungDataContext();
        // GET: Search
        [HttpPost]
        public ActionResult KetQuaTimKiem(int? page, FormCollection f)
        {
            int pagesize = 10;
            int pagenum = (page ?? 1);
            string tukhoa = f["SearchString"].ToString();
            ViewBag.TuKhoa = tukhoa;
            List<THUCUNG> thucungtim = db.THUCUNGs.Where(tct => tct.GIONG.TENGIONG.ToUpper().Contains(tukhoa.ToUpper())).ToList();
            if(thucungtim.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm phù hợp";
                return View(db.THUCUNGs.OrderBy(tc => tc.SLTON).ToPagedList(pagenum, pagesize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + thucungtim.Count + " kết quả!";
            return View(thucungtim.OrderBy(tc => tc.SLTON).ToPagedList(pagenum, pagesize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page, string tukhoa)
        {
            ViewBag.TuKhoa = tukhoa;
            int pagesize = 10;
            int pagenum = (page ?? 1);
            List<THUCUNG> thucungtim = db.THUCUNGs.Where(tct => tct.GIONG.TENGIONG.ToUpper().Contains(tukhoa.ToUpper())).ToList();
            if (thucungtim.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm phù hợp";
                return View(db.THUCUNGs.OrderBy(tc => tc.SLTON).ToPagedList(pagenum, pagesize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + thucungtim.Count + " kết quả!";
            return View(thucungtim.OrderBy(tc => tc.SLTON).ToPagedList(pagenum, pagesize));
        }
    }
}