using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KinhDoanhThuCung.Models;

namespace KinhDoanhThuCung.Controllers
{
    public class NguoidungController : Controller
    {
        ThuCungDataContext dt = new ThuCungDataContext();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult Dangky()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        //{
            
        //    var hoten = collection["HotenKH"];
        //    var tendn = collection["TenDN"];
        //    var matkhau = collection["Matkhau"];
        //    var matkhaunhaplai = collection["Matkhaunhaplai"];
        //    var gioitinh = collection["Gioitinh"];
        //    var diachi = collection["Diachi"];
        //    var dienthoai = collection["Dienthoai"];
        //    var cmnd = collection["Chungminh"];
        //    var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
        //    var mkmh = ToMD5(matkhau);
   
        //    if (String.IsNullOrEmpty(hoten))
        //    {
        //        ViewData["Loi1"] = "Họ tên khách hàng không để trống";
        //    }
        //    else if (String.IsNullOrEmpty(tendn))
        //    {
        //        ViewData["Loi2"] = "Phải nhập tên đăng nhập";

        //    }
        //    else if (String.IsNullOrEmpty(matkhau))
        //    {
        //        ViewData["Loi3"] = "Phải nhập mật khẩu";
        //    }
        //    else if (String.IsNullOrEmpty(matkhaunhaplai))
        //    {
        //        ViewData["Loi4"] = "Phải nhập lại mật khẩu";
        //    }
        //    if (String.IsNullOrEmpty(dienthoai))
        //    {
        //        ViewData["Loi5"] = "Phải nhập số điện thoại";
        //    }
        //    if (dienthoai.Length != 11 || dienthoai.Length != 12)
        //    {
        //        ViewData["Loi8"] = "Nhập lại số điện thoại";
        //    }
        //    if (cmnd.Length != 10)
        //    {
        //        ViewData["Loi10"] = "Nhập lại chứng minh";
        //    }
        //    if (String.IsNullOrEmpty(cmnd))
        //    {
        //        ViewData["Loi6"] = "Phải nhập chứng minh";
        //    }
      
        //    if (matkhau != matkhaunhaplai)
        //    {
        //        ViewData["Loi7"] = "Mật khẩu nhập lại không trùng khớp";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            kh.HOTEN = hoten;
        //            kh.TAIKHOAN = tendn;
        //            kh.MATKHAU = mkmh;
        //            kh.GIOITINH = gioitinh;
        //            kh.DIACHI = diachi;
        //            kh.SDT = dienthoai;
        //            kh.CMND = cmnd;
        //            kh.NGAYSINH = DateTime.Parse(ngaysinh);
        //            dt.KHACHHANGs.InsertOnSubmit(kh);
        //            dt.SubmitChanges();
        //        }
        //        catch(Exception ex)
        //        {
        //            ViewData["Loi9"] = "Chứng minh, sdt hoặc tên đăng nhập đã được sử dụng";
        //            return RedirectToAction("Dangky");
        //        }
                
        //        return RedirectToAction("Dangnhap");

        //    }
        //    return this.Dangky();
        //}
        //[HttpGet]
        //public ActionResult Dangnhap()
        //{
        //    return View();
        //}
        //public ActionResult Dangnhap(FormCollection collection)
        //{
        //    var tendn = collection["TenDN"];
        //    var matkhau = ToMD5(collection["Matkhau"]);


        //    if (String.IsNullOrEmpty(tendn))
        //    {
        //        ViewData["Loi1"] = "Phải nhập tên đăng nhập";
        //    }
        //    else if (String.IsNullOrEmpty(matkhau))
        //    {
        //        ViewData["Loi2"] = "Phải nhập mật khẩu";
        //    }
        //    else
        //    {
        //        KHACHHANG kh = dt.KHACHHANGs.SingleOrDefault(k => k.TAIKHOAN == tendn && k.MATKHAU == matkhau);
        //        if (kh != null)
        //        {
        //            ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
        //            Session["Taikhoan"] = kh;
        //            return RedirectToAction("GioHang", "GioHang");
        //        }
        //        else
        //            ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
               
        //    }
        //    return View();
        //}
        [HttpGet]
        public ActionResult Dangnhap1()
        {
            return View();
        }
        public ActionResult Dangnhap1(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = ToMD5(collection["Matkhau"]);


            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = dt.KHACHHANGs.SingleOrDefault(k => k.TAIKHOAN == tendn && k.MATKHAU == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("GioHang", "GioHang");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
            return View();
        }
        [HttpGet]
        public ActionResult Dangky1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky1(FormCollection collection, KHACHHANG kh)
        {
            Boolean loi = false;
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var gioitinh = collection["Gioitinh"];
            var diachi = collection["Diachi"];
            var dienthoai = collection["Dienthoai"];
            var cmnd = collection["Chungminh"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            var mkmh = ToMD5(matkhau);

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi5"] = "Phải nhập số điện thoại";
            }
            if (dienthoai.Length != 11 || dienthoai.Length != 12)
            {
                ViewData["Loi8"] = "Nhập lại số điện thoại";
            }
            if (cmnd.Length != 10)
            {
                ViewData["Loi10"] = "Nhập lại chứng minh";
            }
            if (String.IsNullOrEmpty(cmnd))
            {
                ViewData["Loi6"] = "Phải nhập chứng minh";
            }

            if (matkhau != matkhaunhaplai)
            {
                ViewData["Loi7"] = "Mật khẩu nhập lại không trùng khớp";
            }
            else
            {
                try
                {
                    kh.HOTEN = hoten;
                    kh.TAIKHOAN = tendn;
                    kh.MATKHAU = mkmh;
                    kh.GIOITINH = gioitinh;
                    kh.DIACHI = diachi;
                    kh.SDT = dienthoai;
                    kh.CMND = cmnd;
                    kh.NGAYSINH = DateTime.Parse(ngaysinh);
                    dt.KHACHHANGs.InsertOnSubmit(kh);
                    dt.SubmitChanges();
                }
                catch (Exception ex)
                {
                    loi = true;
                    ViewData["Loi9"] = "Chứng minh, sdt hoặc tên đăng nhập đã được sử dụng";
                    return RedirectToAction("Dangky1");
                    
                }

                return RedirectToAction("Dangnhap1");

            }
            return this.Dangky1();
        }
        public string ToMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {

                sbHash.Append(String.Format("{0:x2}", b));

            }
            return sbHash.ToString();

        }
    }
}