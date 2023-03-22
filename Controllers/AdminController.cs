using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinhDoanhThuCung.Models;
using PagedList;
using PagedList.Mvc;

namespace KinhDoanhThuCung.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ThuCungDataContext db = new ThuCungDataContext();
        public ActionResult Index()
        {
            if(Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
                return View();
        }
        public ActionResult Loaithucung(int ? page)
         
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            return View(db.LOAIs.ToList().OrderBy(n=>n.MALOAI).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Themmoiloai()
        {

                return View();
        }
        [HttpPost]
        public ActionResult Themmoiloai(LOAI loai)
        {
            try
            {
                db.LOAIs.InsertOnSubmit(loai);
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Themmoiloai");
            }
            return RedirectToAction("Loaithucung");
        }
       public ActionResult Xoaloai(string id)
        {
            LOAI loai = db.LOAIs.SingleOrDefault(n => n.MALOAI == id);
            ViewBag.MALOAI = loai.MALOAI;
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loai);
        }
        [HttpPost,ActionName("Xoaloai")]
        public ActionResult Xacnhanxoa(string id)
        {
            try
            {
                LOAI loai = db.LOAIs.SingleOrDefault(n => n.MALOAI == id);
                ViewBag.MALOAI = loai.MALOAI;
                if (loai == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.LOAIs.DeleteOnSubmit(loai);
                db.SubmitChanges();
                
            }
            catch(Exception ex)
            {
                ViewBag.Loi = "Không thể xóa";
                return RedirectToAction("Xoaloai");
            }
            return RedirectToAction("Loaithucung");
        }
        [HttpGet]
        public ActionResult Sualoai(string id)
        {

            LOAI loai = db.LOAIs.SingleOrDefault(n => n.MALOAI == id);
            ViewBag.MALOAI = loai.MALOAI;
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loai);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sualoai(string id,LOAI loai)
        {
            loai = db.LOAIs.SingleOrDefault(n => n.MALOAI == id);
            ViewBag.MALOAI = loai.MALOAI;
            UpdateModel(loai);
            db.SubmitChanges();
            return RedirectToAction("Loaithucung");
        }
        //Nhà cung cấp
        public ActionResult Nhacungcap(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(db.NCCGIONGs.ToList().OrderBy(n=>n.MANCC).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Themmoincc()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoincc(NCCGIONG ncc)
        {
            try
            {
                db.NCCGIONGs.InsertOnSubmit(ncc);
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Themmoincc");
            }
            return RedirectToAction("Nhacungcap");
        }
        public ActionResult Xoancc(string id)
        {
            NCCGIONG ncc = db.NCCGIONGs.SingleOrDefault(n => n.MANCC == id);
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        [HttpPost, ActionName("Xoancc")]
        public ActionResult Xacnhanxoancc(string id)
        {
            try
            {
                NCCGIONG ncc = db.NCCGIONGs.SingleOrDefault(n => n.MANCC == id);
                ViewBag.MANCC = ncc.MANCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                db.NCCGIONGs.DeleteOnSubmit(ncc);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Xoancc");
            }
            
            return RedirectToAction("Nhacungcap");
        }
        [HttpGet]
        public ActionResult Suancc(string id)
        {

            NCCGIONG ncc = db.NCCGIONGs.SingleOrDefault(n => n.MANCC.Equals(id));   
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        [HttpPost, ActionName("Suancc")]
        [ValidateInput(false)]
        public ActionResult Xacnhansuancc(string id)
        {
            try
            {
                NCCGIONG ncc = db.NCCGIONGs.SingleOrDefault(n => n.MANCC.Equals(id));
                ViewBag.MANCC = ncc.MANCC;
                UpdateModel(ncc);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Suancc");
            }
            

            return RedirectToAction("Nhacungcap");
        }
        public ActionResult Chitietncc(string id)
        {
            NCCGIONG ncc = db.NCCGIONGs.SingleOrDefault(n => n.MANCC.Equals(id));
            ViewBag.MANCC = ncc.MANCC;
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        public ActionResult ThuCung(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;

            return View(db.THUCUNGs.ToList().OrderBy(n=>n.MATC).ToPagedList(pageNumber, pageSize));
        }
        //Khách hàng
        public ActionResult KhachHang(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MAKH).ToPagedList(pageNumber, pageSize));

        }
        public ActionResult HoaDon(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.HOADONs.ToList().OrderBy(n => n.MAHD).ToPagedList(pageNumber, pageSize));
        }
     
        public ActionResult NhanVien(int ? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.NHANVIENs.ToList().OrderBy(n=>n.MANV).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Themmoinhanvien()
        {
            ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.MACV), "MACV", "MACV");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoinhanvien(NHANVIEN nhanvien)
        {
            
            try
            {
                ViewBag.MACV = new SelectList(db.CHUCVUs.ToList().OrderBy(n => n.MACV), "MACV", "MACV");

                db.NHANVIENs.InsertOnSubmit(nhanvien);
                db.SubmitChanges();
          

            }
            catch (Exception ex)
            {       
                
                    ViewData["Loi1"] = "Chức vụ sai"; 
               
                return RedirectToAction("Themmoinhanvien");
            }

            return RedirectToAction("NhanVien");
        }
        
        public ActionResult Chitietnhanvien(int id)
        {
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            ViewBag.MANV = nhanvien.MANV;
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }
        [HttpGet]
        public ActionResult Xoanhanvien(int id)
        {
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            ViewBag.MANV = nhanvien.MANV;
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanvien);
        }
        [HttpPost, ActionName("Xoanhanvien")]
        public ActionResult Xacnhanxoa(int id)
        {
            try
            {
                NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
                ViewBag.MANV = nhanvien.MANV;
                if (nhanvien == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.NHANVIENs.DeleteOnSubmit(nhanvien);
                db.SubmitChanges(); 

            }
            catch (Exception ex)
            {
                return RedirectToAction("Xoanhanvien");
            }
            
            return RedirectToAction("NhanVien");
        }
        [HttpGet]
        public ActionResult Suanhanvien(int id)
        {
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            ViewBag.MANV = nhanvien.MANV;
            if (nhanvien == null)
            {
                Response.StatusCode = 404;
                return null;
            }



            return View(nhanvien);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suanhanvien(int id,NHANVIEN nhanvien)
        {
            nhanvien = db.NHANVIENs.SingleOrDefault(n => n.MANV == id);
            try
            {
                UpdateModel(nhanvien);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Suanhanvien");
            }
           
            return RedirectToAction("NhanVien");
        }
        [HttpGet]
        public ActionResult Themmoikhachhang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoikhachhang(KHACHHANG khachhang)
        {
            try
            {
                db.KHACHHANGs.InsertOnSubmit(khachhang);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Themmoikhachhang");
            }
            
            return RedirectToAction("KhachHang");
        }
        public ActionResult Chitietkhachhang(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = khachhang.MAKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpGet]
        public ActionResult Xoakhachhang(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = khachhang.MAKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);

        }
        [HttpPost, ActionName("Xoakhachhang")]
        public ActionResult XacnhanxoaKH(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            ViewBag.MAKH = khachhang.MAKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KHACHHANGs.DeleteOnSubmit(khachhang);
            db.SubmitChanges();
            return RedirectToAction("KhachHang");
        }
        //Chỉnh sửa khách hàng
        //Bị lỗi-------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult SuaKhachHang(int id)
        {
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost]
        public ActionResult SuaKhachHang(KHACHHANG khachhang)
        {
            UpdateModel(khachhang);
            db.SubmitChanges();
            return RedirectToAction("KhachHang");
        }
        //Nhân viên
      
        public ActionResult Giong(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(db.GIONGs.ToList().OrderBy(n => n.MAGIONG).ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                TAIKHOAN tk = db.TAIKHOANs.SingleOrDefault(n => n.TK == tendn && n.MK == matkhau);
                if (tk != null)
                {
                    Session["Taikhoanadmin"] = tk;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themmoigiong()
        {
            ViewBag.MALOAI = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoigiong(GIONG giong)
        {
            try
            {
                ViewBag.MALOAI = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                db.GIONGs.InsertOnSubmit(giong);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Themmoigiong");
            }
           
            
            return RedirectToAction("Giong");
        
        }
        public ActionResult Chitietgiong(string id)
        {
            GIONG giong = db.GIONGs.SingleOrDefault(n => n.MAGIONG == id);
            ViewBag.MAGIONG = giong.MAGIONG;
            if(giong==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giong);


        }
        [HttpGet]
        public ActionResult Xoagiong(string id)
        {

            GIONG giong = db.GIONGs.SingleOrDefault(n => n.MAGIONG == id);
            ViewBag.MAGIONG = giong.MAGIONG;
            if(giong==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giong);
        }
        [HttpPost,ActionName("Xoagiong")]
        public ActionResult Xacnhanxoag(string id)
        {
            try
            {
                GIONG giong = db.GIONGs.SingleOrDefault(n => n.MAGIONG == id);
                ViewBag.MATC = giong.MAGIONG;
                if (giong == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.GIONGs.DeleteOnSubmit(giong);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Xoagiong");
            }
            return RedirectToAction("Giong");
        }
        [HttpGet]
        public ActionResult Suagiong(string id)
        {
            GIONG giong = db.GIONGs.SingleOrDefault(n => n.MAGIONG == id);

            if (giong == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MALOAI = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "MALOAI",giong.MALOAI);
            return View(giong);

        }
      
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suagiong( string id ,GIONG giong)
        {
            try
            {
                giong = db.GIONGs.SingleOrDefault(n => n.MAGIONG == id);
                ViewBag.MAGIONG = giong.MAGIONG;
                ViewBag.MALOAI = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "MALOAI", giong.MALOAI);
                UpdateModel(giong);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Suagiong");
            }
            
                
            return RedirectToAction("Giong");
            
        }
        [HttpGet]
        public ActionResult Themmoithucung()
        {
    
            ViewBag.MAGIONG = new SelectList(db.GIONGs.ToList().OrderBy(n => n.MAGIONG), "MAGIONG","MAGIONG");
            ViewBag.MANCC = new SelectList(db.NCCGIONGs.ToList().OrderBy(n => n.MANCC), "MANCC", "MANCC");

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoithucung(THUCUNG thucung,HttpPostedFileBase fileuploat)
        {
            ViewBag.MANCC = new SelectList(db.NCCGIONGs.ToList().OrderBy(n => n.TEN), "MANCC", "TEN");
            ViewBag.MANCC = new SelectList(db.NCCGIONGs.ToList().OrderBy(n => n.MANCC), "MANCC", "MANCC");

            if (fileuploat==null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();          
            }
            else
            {
                if(ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileuploat.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhThuCung"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileuploat.SaveAs(path);
                    }
                    thucung.ANH = fileName;
                    db.THUCUNGs.InsertOnSubmit(thucung);
                    db.SubmitChanges();
                }
                return RedirectToAction("ThuCung");
            }
        }
        [HttpGet]
        public ActionResult Suathucung(int id)
        {
            THUCUNG thucung = db.THUCUNGs.SingleOrDefault(n => n.MATC == id);
            if (thucung == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MAGIONG = new SelectList(db.GIONGs.ToList().OrderBy(n => n.MAGIONG), "MAGIONG", "MAGIONG");
            ViewBag.MANCC = new SelectList(db.NCCGIONGs.ToList().OrderBy(n => n.TEN), "MANCC", "TEN",thucung.MANCC);
            return View(thucung);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suathucung(int id,THUCUNG thucung, HttpPostedFileBase fileuploat)
        {
            thucung = db.THUCUNGs.SingleOrDefault(n => n.MATC == id);
            ViewBag.MAGIONG = new SelectList(db.GIONGs.ToList().OrderBy(n => n.MAGIONG), "MAGIONG", "MAGIONG");
            ViewBag.MANCC = new SelectList(db.NCCGIONGs.ToList().OrderBy(n => n.TEN), "MANCC", "TEN");
            if (fileuploat == null)
            {
                THUCUNG thucung1 = db.THUCUNGs.SingleOrDefault(n => n.MATC == id);
                thucung.ANH = thucung1.ANH;
                UpdateModel(thucung);
                db.SubmitChanges();
                return RedirectToAction("ThuCung");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileuploat.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhThuCung"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileuploat.SaveAs(path);
                    }
                    thucung.ANH = fileName;
                    UpdateModel(thucung);
                    db.SubmitChanges();
                }
                return RedirectToAction("ThuCung");
            }
        }
        public ActionResult Chitietthucung(int id)
        {
            THUCUNG thucung = db.THUCUNGs.SingleOrDefault(n => n.MATC == id);
            ViewBag.MATC = thucung.MATC;
            if(thucung==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thucung);
        }
        public ActionResult Xoathucung(int id)
        {
            THUCUNG thucung = db.THUCUNGs.SingleOrDefault(n => n.MATC == id);
            ViewBag.MATC = thucung.MATC;
            if(thucung==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thucung);
        }
        [HttpPost,ActionName("Xoathucung")]
        public ActionResult Xacnhanxoatc(int id)
        {
            try
            {
                THUCUNG thucung = db.THUCUNGs.SingleOrDefault(n => n.MATC == id);
                ViewBag.MATC = thucung.MATC;
                if (thucung == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.THUCUNGs.DeleteOnSubmit(thucung);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                return RedirectToAction("ThuCung");
            }
            
            return RedirectToAction("ThuCung");
        }

        public ActionResult ChiTietHoaDon(int id)
        {
            List<CTHOADONTHUCUNG> ct = db.CTHOADONTHUCUNGs.Where(n => n.MAHD == id).ToList();
            
            return View(ct);
        }
    }
    
}