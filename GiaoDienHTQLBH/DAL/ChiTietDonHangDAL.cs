using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiaoDienHTQLBH.Class;

namespace GiaoDienHTQLBH.DAL
{
    internal class ChiTietDonHangDAL
    {
        public void Insert(ChiTietDonHang ct)
        {
            string q = @"INSERT INTO ChiTietDonHang(MaDH, MaSP, SoLuong, DonGia, GhiChu) VALUES(@MaDH, @MaSP, @SoLuong, @DonGia, @GhiChu)";
            DatabaseHelper.ExecuteNonQuery(q, new SqlParameter[] {
                new SqlParameter("@MaDH", ct.MaDH),
                new SqlParameter("@MaSP", ct.MaSP),
                new SqlParameter("@SoLuong", ct.SoLuong),
                new SqlParameter("@DonGia", ct.DonGia),
                new SqlParameter("@GhiChu", string.IsNullOrEmpty(ct.GhiChu) ? (object)DBNull.Value : ct.GhiChu)
            });
        }

        public DataTable GetByDonHang(string maDH)
        {
            string q = @"SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.DonGia, ct.GhiChu, (ct.SoLuong*ct.DonGia) AS ThanhTien 
                         FROM ChiTietDonHang ct 
                         JOIN SanPham sp ON sp.MaSP = ct.MaSP 
                         WHERE ct.MaDH = @MaDH";
            return DatabaseHelper.ExecuteQuery(q, new SqlParameter[] { new SqlParameter("@MaDH", maDH) });
        }
    }
}
