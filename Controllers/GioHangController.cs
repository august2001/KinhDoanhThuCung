using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinhDoanhThuCung.Models;
using Newtonsoft.Json.Linq;
using QLKS.Other;

namespace KinhDoanhThuCung.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        ThuCungDataContext data = new ThuCungDataContext();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;

            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int sMATC, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.sMATC == sMATC);
            if (sanpham == null)
            {
                sanpham = new GioHang(sMATC);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSOLUONGMUA++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSOLUONGMUA);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dTHANHTIEN);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "KDThuCung");

            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int matc)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang thucung = lstGioHang.SingleOrDefault(tc => tc.sMATC == matc);
            if(thucung != null)
            {
                lstGioHang.RemoveAll(tc => tc.sMATC == matc);
                return RedirectToAction("GioHang");
            }
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "KDThuCung");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int matc, FormCollection f)
        {
            List<GioHang> lstGioHang = Laygiohang();
            GioHang thucung = lstGioHang.SingleOrDefault(tc => tc.sMATC == matc);
            if (thucung != null)
            {
                thucung.iSOLUONGMUA = int.Parse(f["txtSoLuongMua"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGioHang = Laygiohang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "KDThuCung");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if(Session["Taikhoan"]==null|| Session["Taikhoan"].ToString()=="")
            {
                return RedirectToAction("Dangnhap1", "Nguoidung");

            }    
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "KDThuCung");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            HOADON hd = new HOADON();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang> gh = Laygiohang();
            hd.MAKH = kh.MAKH;
            hd.NGAYLAP = DateTime.Now;
            data.HOADONs.InsertOnSubmit(hd);
            data.SubmitChanges();
          
            foreach (var item in gh)
            {
                CTHOADONTHUCUNG cthd = new CTHOADONTHUCUNG();
                cthd.MAHD = hd.MAHD;
                cthd.MATC = item.sMATC;
                cthd.SOLUONGMUA = item.iSOLUONGMUA;
                cthd.DONGIA = (int)item.dDONGIA;
                data.CTHOADONTHUCUNGs.InsertOnSubmit(cthd);
            }
            
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Xacnhandonhang", "GioHang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Payment()
        {
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOVOU420220329";
            string accessKey = "aqzhbo3Lmg8oBqLI";
            string serectkey = "IaZxJIfOYMeiM91pzsY8uZb4HnGkGfmu";
            string orderInfo = "Thanh toán hóa đơn";
            string returnUrl = "https://localhost:44380//ThanhToanMoMo/KDThuCung";
            string notifyurl = "http://ba1adf48beba.ngrok.io/KDThuCung/SavePayment";
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            string amount = lstGiohang.Sum(gh => gh.dTHANHTIEN).ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;


            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);
            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };


            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());


            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());

        }
        public ActionResult ComfirmPaymentClient()
        {
            //hien thi thong bao cho nguoi dung
            return View();
        }
        public void SavePayment()
        {
            //cap nhat du lieu vao database
        }
    }
}
