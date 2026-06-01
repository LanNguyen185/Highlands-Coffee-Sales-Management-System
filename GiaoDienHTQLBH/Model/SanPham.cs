using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoDienHTQLBH.Model
{
    internal class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
        public bool IsActive { get; set; }
        public string LoaiSP { get; set; }
    }
}
