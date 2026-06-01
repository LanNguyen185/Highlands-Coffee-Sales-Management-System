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
    internal class HoaDonDAL
    {
        public void Insert(HoaDon hd)
        {
            string query = @"INSERT INTO HoaDon(MaHD, NgayLapHD, ThueVAT, ThanhTienHD, SoTienKhachDua, SoTienDu, MaDH)
                             VALUES(@MaHD, @NgayLapHD, @ThueVAT, @ThanhTienHD, @SoTienKhachDua, @SoTienDu, @MaDH)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", hd.MaHD),
                new SqlParameter("@NgayLapHD", hd.NgayLapHD),
                new SqlParameter("@ThueVAT", hd.ThueVAT),
                new SqlParameter("@ThanhTienHD", hd.ThanhTienHD),
                new SqlParameter("@SoTienKhachDua", hd.SoTienKhachDua),
                new SqlParameter("@SoTienDu", hd.SoTienDu),
                new SqlParameter("@MaDH", hd.MaDH)
            };

            DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public DataTable GetAll()
        {
            string query = "SELECT * FROM HoaDon";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public DataTable GetByDonHang(string maDH)
        {
            string query = "SELECT * FROM HoaDon WHERE MaDH = @MaDH";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDH", maDH)
            };
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
      
    }
}
