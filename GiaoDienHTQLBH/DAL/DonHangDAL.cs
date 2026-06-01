using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiaoDienHTQLBH.Model;

namespace GiaoDienHTQLBH.DAL
{
    internal class DonHangDAL
    {
        public void InsertDonHang(DonHang dh)
        {
            string q = @"INSERT INTO DonHang(MaDH, MaBan, NgayTaoDH, ThanhTienDH, MaNV) 
             VALUES(@MaDH, @MaBan, @Ngay, @ThanhTien, @MaNV)";

            DatabaseHelper.ExecuteNonQuery(q, new SqlParameter[] {
    new SqlParameter("@MaDH", dh.MaDH),
    new SqlParameter("@MaBan", dh.MaBan),
    new SqlParameter("@Ngay", dh.NgayTaoDH),
    new SqlParameter("@ThanhTien", dh.ThanhTienDH),
    new SqlParameter("@MaNV", dh.MaNV)
});
        }

        public void UpdateThanhTien(string maDH)
        {
            string q = @"UPDATE DonHang SET ThanhTienDH = (SELECT SUM(SoLuong*DonGia) FROM ChiTietDonHang WHERE MaDH=@MaDH) WHERE MaDH=@MaDH";
            DatabaseHelper.ExecuteNonQuery(q, new SqlParameter[] { new SqlParameter("@MaDH", maDH) });
        }

        public DataTable GetByBan(string maBan)
        {
            string q = @"SELECT * FROM DonHang WHERE MaBan = @MaBan ORDER BY NgayTaoDH DESC";
            return DatabaseHelper.ExecuteQuery(q, new SqlParameter[] { new SqlParameter("@MaBan", maBan) });
        }
    }
}
