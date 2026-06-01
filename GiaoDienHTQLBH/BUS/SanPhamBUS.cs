using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiaoDienHTQLBH.DAL;

namespace GiaoDienHTQLBH.BUS
{
    internal class SanPhamBUS
    {
        private SanPhamDAL dal = new SanPhamDAL();
        public DataTable GetAll() => dal.GetAll();
        public DataTable GetByLoai(string loai) => dal.GetByLoai(loai);
        public decimal GetDonGia(string maSP) => dal.GetDonGia(maSP);
        public string GetTenSP(string maSP) => dal.GetTenSP(maSP);
    }
}
