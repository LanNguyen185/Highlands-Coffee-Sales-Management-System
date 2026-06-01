using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienHTQLBH.DAL
{
    internal class SanPhamDAL
    {
        public DataTable GetAll()
        {
            string sql = @"SELECT sp.MaSP, sp.TenSP,
                           (SELECT TOP 1 GiaBan FROM DonGiaSanPham dg WHERE dg.MaSP = sp.MaSP ORDER BY NgayCapNhatGia DESC) AS DonGia,
                           sp.HinhAnh, sp.LoaiSP
                           FROM SanPham sp
                           WHERE sp.IsActive = 1";
            return DatabaseHelper.ExecuteQuery(sql);
        }

        public DataTable GetByLoai(string loai)
        {
            string sql = @"SELECT sp.MaSP, sp.TenSP,
                           (SELECT TOP 1 GiaBan FROM DonGiaSanPham dg WHERE dg.MaSP = sp.MaSP ORDER BY NgayCapNhatGia DESC) AS DonGia,
                           sp.HinhAnh, sp.LoaiSP
                           FROM SanPham sp
                           WHERE sp.IsActive = 1 AND sp.LoaiSP = @loai";
            return DatabaseHelper.ExecuteQuery(sql, new SqlParameter[] { new SqlParameter("@loai", loai) });
        }

        public decimal GetDonGia(string maSP)
        {
            string sql = @"SELECT TOP 1 GiaBan FROM DonGiaSanPham WHERE MaSP = @MaSP ORDER BY NgayCapNhatGia DESC";
            object o = DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@MaSP", maSP) });
            if (o == null || o == DBNull.Value) return 0;
            return Convert.ToDecimal(o);
        }

        public string GetTenSP(string maSP)
        {
            string q = "SELECT TenSP FROM SanPham WHERE MaSP=@MaSP";
            var dt = DatabaseHelper.ExecuteQuery(q, new SqlParameter[] { new SqlParameter("@MaSP", maSP) });
            if (dt.Rows.Count > 0) return dt.Rows[0]["TenSP"].ToString();
            return string.Empty;
        }
    }
}
