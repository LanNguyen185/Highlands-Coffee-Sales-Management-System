using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienHTQLBH.DAL
{
    internal class BanDAL
    {
        public DataTable GetAll()
        {
            string q = "SELECT MaBan, SoBan, ViTriBan, TinhTrangBan FROM Ban";
            return DatabaseHelper.ExecuteQuery(q);
        }
    }
}
