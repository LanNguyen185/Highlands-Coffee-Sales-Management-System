using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienHTQLBH
{
    public class Ban
    {

        public string MaBan { get; set; }        // CHAR(10)
        public int SoBan { get; set; }           // INT
        public string ViTriBan { get; set; }     // NVARCHAR(50)
        public string TinhTrangBan { get; set; } // "Trống" / "Đang sử dụng" / "Đặt trước"

        public Ban() { }

        public Ban(string maBan, int soBan, string viTriBan, string tinhTrangBan)
        {
            MaBan = maBan;
            SoBan = soBan;
            ViTriBan = viTriBan;
            TinhTrangBan = tinhTrangBan;
        }

        public Ban(DataRow row)
        {
            MaBan = row["MaBan"].ToString();
            SoBan = row["SoBan"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoBan"]);
            ViTriBan = row["ViTriBan"] == DBNull.Value ? string.Empty : row["ViTriBan"].ToString();
            TinhTrangBan = row["TinhTrangBan"] == DBNull.Value ? "Trống" : row["TinhTrangBan"].ToString();
        }

        // Thuộc tính phụ để xử lý dễ hơn
        public bool IsUsing => TinhTrangBan == "Đang sử dụng";
        public bool IsBooked => TinhTrangBan == "Đặt trước";
        public bool IsEmpty => TinhTrangBan == "Trống";
    }
}

