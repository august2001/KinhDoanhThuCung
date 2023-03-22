using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinhDoanhThuCung.Models
{
    public class ChiTietHoaDon
    {
        ThuCungDataContext data = new ThuCungDataContext();
        public int iMAHD { set; get; }
        public int iMATC { set; get; }
        public string iTENGIONG { set; get; }
        public int iSOLUONGMUA { set; get; }
        public Double iDONGIA { set; get; }
        public Double iTHANHTIEN
        {
            get { return iSOLUONGMUA * iDONGIA; }
        }
        public ChiTietHoaDon(int MAHD)
        {
            iMAHD = MAHD;
            CTHOADONTHUCUNG chitiet = data.CTHOADONTHUCUNGs.Single(n => n.MAHD == iMAHD);
            iMATC = chitiet.MATC;
            iTENGIONG = chitiet.THUCUNG.GIONG.TENGIONG;
            iSOLUONGMUA = (int)chitiet.SOLUONGMUA;
            iDONGIA = double.Parse(chitiet.DONGIA.ToString());

        }
    }
    
}