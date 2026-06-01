using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiaoDienHTQLBH.DAL;

namespace GiaoDienHTQLBH.BUS
{
    internal class BanBUS
    {
        private BanDAL dal = new BanDAL();
        public DataTable GetAll() => dal.GetAll();
        public void UpdateTinhTrang(string maBan, string tinhTrang)
        {
            string query = "UPDATE Ban SET TinhTrangBan = @TinhTrangBan WHERE MaBan = @MaBan";
            DatabaseHelper.ExecuteNonQuery(query, new[]
            {
        new SqlParameter("@TinhTrangBan", tinhTrang),
        new SqlParameter("@MaBan", maBan)
    });
        }

    }
}
