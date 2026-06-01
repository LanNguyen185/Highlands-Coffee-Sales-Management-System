using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiaoDienHTQLBH.Class;
using GiaoDienHTQLBH.DAL;
using GiaoDienHTQLBH.Model;

namespace GiaoDienHTQLBH.BUS
{
    internal class DonHangBUS
    {

        private DonHangDAL dal = new DonHangDAL();
        private ChiTietDonHangDAL ctDal = new ChiTietDonHangDAL();
        private HoaDonDAL hdDal = new HoaDonDAL();

        public string GenerateNewMaDH()
        {
            string q = "SELECT TOP 1 MaDH FROM DonHang ORDER BY MaDH DESC";
            var dt = DatabaseHelper.ExecuteQuery(q);
            if (dt.Rows.Count == 0) return "DH001";
            string last = dt.Rows[0]["MaDH"].ToString().Trim();
            int num = int.Parse(last.Substring(2)) + 1;
            return "DH" + num.ToString("D3");
        }

        public string GenerateNewMaHD()
        {
            string q = "SELECT TOP 1 MaHD FROM HoaDon ORDER BY MaHD DESC";
            var dt = DatabaseHelper.ExecuteQuery(q);
            if (dt.Rows.Count == 0) return "HD001";
            string last = dt.Rows[0]["MaHD"].ToString().Trim();
            int num = int.Parse(last.Substring(2)) + 1;
            return "HD" + num.ToString("D3");
        }

        public void CreateDonHang(string maDH, string maBan, string maNV = null, string maKH = null, string hinhThucThanhToan = null)
        {
            DonHang dh = new DonHang
            {
                MaDH = maDH,
                MaBan = maBan,
                NgayTaoDH = DateTime.Now,
                ThanhTienDH = 0,
                MaNV = maNV,
                MaKH = maKH,
                HinhThucThanhToan = hinhThucThanhToan
            };
            dal.InsertDonHang(dh);
        }

        public void AddChiTiet(string maDH, string maSP, int soLuong, decimal donGia, string ghiChu)
        {
            ChiTietDonHang ct = new ChiTietDonHang
            {
                MaDH = maDH,
                MaSP = maSP,
                SoLuong = soLuong,
                DonGia = donGia,
                GhiChu = ghiChu
            };
            ctDal.Insert(ct);
            dal.UpdateThanhTien(maDH);
        }

        public DataTable GetChiTiet(string maDH) => ctDal.GetByDonHang(maDH);

        // Lưu dữ liệu hoàn chỉnh: DonHang + ChiTiet + HoaDon
        public void InsertDonHang(DonHang dh)
        {
            string query = "INSERT INTO DonHang (MaDH, MaBan, NgayTaoDH, ThanhTienDH) " +
               "VALUES (@MaDH, @MaBan, @NgayTaoDH, @ThanhTienDH)";

            DatabaseHelper.ExecuteNonQuery(query, new[]
            {
    new SqlParameter("@MaDH", dh.MaDH),
    new SqlParameter("@MaBan", dh.MaBan),
    new SqlParameter("@NgayTaoDH", dh.NgayTaoDH),
    new SqlParameter("@ThanhTienDH", dh.ThanhTienDH)
});
        }

        public void InsertHoaDon(HoaDon hd)
        {
            string query = @"INSERT INTO HoaDon(MaHD, NgayLapHD, ThueVAT, ThanhTienHD, SoTienKhachDua, SoTienDu, MaDH)
                     VALUES(@MaHD, @NgayLapHD, @ThueVAT, @ThanhTienHD, @SoTienKhachDua, @SoTienDu, @MaDH)";
            DatabaseHelper.ExecuteNonQuery(query, new[]
            {
        new SqlParameter("@MaHD", hd.MaHD),
        new SqlParameter("@NgayLapHD", hd.NgayLapHD),
        new SqlParameter("@ThueVAT", hd.ThueVAT),
        new SqlParameter("@ThanhTienHD", hd.ThanhTienHD),
        new SqlParameter("@SoTienKhachDua", hd.SoTienKhachDua),
        new SqlParameter("@SoTienDu", hd.SoTienDu),
        new SqlParameter("@MaDH", hd.MaDH)
    });
        }
        
        public DataTable GetHoaDonFull(string maHD)
        {
            string query = "SELECT * FROM HoaDonFull WHERE MaHD = @MaHD";
            return DatabaseHelper.ExecuteQuery(query,
                new SqlParameter[] { new SqlParameter("@MaHD", maHD) });
        }
    }
}
