using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienHTQLBH.Model
{
    internal class HoaDon
    {
        public string MaHD { get; set; }
        public DateTime NgayLapHD { get; set; }
        public decimal ThueVAT { get; set; }      // 0.1 = 10%
        public decimal ThanhTienHD { get; set; }
        public decimal SoTienKhachDua { get; set; }
        public decimal SoTienDu { get; set; }
        public string MaDH { get; set; }
    }
}
