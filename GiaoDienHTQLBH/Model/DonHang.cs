using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienHTQLBH.Model
{
    internal class DonHang
    {
        public string MaDH { get; set; }           // Khóa chính
        public DateTime NgayTaoDH { get; set; }    // Ngày tạo đơn
        public decimal ThanhTienDH { get; set; }   // Tổng tiền hàng
        public string HinhThucThanhToan { get; set; } // "Tiền mặt", "Chuyển khoản", "Thẻ"
        public string MaNV { get; set; }           // Nhân viên lập đơn
        public string MaBan { get; set; }          // Bàn
        public string MaKH { get; set; }           // Khách hàng (nếu có)

    }
}
