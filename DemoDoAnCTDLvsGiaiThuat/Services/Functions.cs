/***********************************************************************
 * 1. Ham lay du lieu tu file
 * 2. Ham luu du lieu vao file
 * 3. Ham user dang nhap
 * 4. Ham hien thi thong tin hang hoa
 * 5. Ham tim kiem hang hoa theo ten
 * 6. Hang dat hang san pham
 * 7. Hang them hang hoa
 * 8. Ham xoa hang hoa
 * 9. Ham cap nhat so luong hang hoa
 ***********************************************************************/

using DemoDoAnCTDLvsGiaiThuat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DemoDoAnCTDLvsGiaiThuat.Service
{
    public static class Functions
    {
        /// <summary>
        /// Ham lay du lieu
        /// </summary>
        public static void GetAllData()
        {
            KhachHang.GetObjectsFromFile();
            Admin.GetObjectsFromFile();
            DonHang.GetObjectsFromFile();
            HangHoa.GetObjectsFromFile();
            HangHoaDonHang.GetObjectsFromFile();
        }

        /// <summary>
        /// Ham luu du lieu
        /// </summary>
        public static void SaveAllData()
        {
            Admin.SaveObjectsToFile();
            DonHang.SaveObjectsToFile();
            HangHoa.SaveObjectsToFile();
            HangHoaDonHang.SaveObjectsToFile();
            KhachHang.SaveObjectsToFile();
        }
        /// <summary>
        /// Lấy tất cả hàng hóa trong hệ thống đơn hang
        /// </summary>
        /// <returns>trả về một list đơn hàng</returns>
        public static LinkedList<IKeyValue> LayTatCaHangHoa()
        {
            LinkedList<IKeyValue> result = new LinkedList<IKeyValue>();
            HangHoa item;
            for (int i = 0; i < HangHoa.ListHangHoa.Count; i++)        
            {
                item = HangHoa.ListHangHoa.Get(i);
                result.AddLast(new KeyValue<string>(key: $"{i}------------------------{i}", value: $"{i}------------------------{i}"));
                result.AddLast(new KeyValue<string>(key: "Id san pham", value: $"{item.Id}"));
                result.AddLast(new KeyValue<string>(key: "Ma san pham", value: $"{item.MaHang}"));
                result.AddLast(new KeyValue<string>(key: "Ten san pham", value: $"{item.TenHang}"));
                result.AddLast(new KeyValue<string>(key: "Mau san pham", value: $"{item.MauSac}"));
                result.AddLast(new KeyValue<string>(key: "Noi san xuat", value: $"{item.NoiSanXuat}"));
                result.AddLast(new KeyValue<string>(key: "Ngay nhap kho", value: $"{item.NgayNhapKho.ToShortDateString()}"));
                result.AddLast(new KeyValue<string>(key: "So luong ton kho", value: $"{item.SoLuong}"));
                result.AddLast(new KeyValue<string>(key: "Gia san pham", value: $"{item.GiaBan} VND"));
            }

            return result;
        }
        /// <summary>
        /// Tìm kiếm hàng hóa bằng trừ khóa truyền vào
        /// </summary>
        /// <param name="tuKhoa">từ khóa để hàm tìm kiếm hàng hóa</param>
        /// <returns> </returns>
        public static LinkedList<IKeyValue> TimKiemHangHoa(string tuKhoa)
        {
            LinkedList<IKeyValue> result = new LinkedList<IKeyValue>();
            var listHangHoaTimKiem = HangHoa.TimKiemHangHoa(tuKhoa);
            HangHoa item;
            for (int i = 0; i < listHangHoaTimKiem.Count; i++)
            {
                item = listHangHoaTimKiem.Get(i);
                result.AddLast(new KeyValue<string>(key: $"{i}------------------------{i}", value: $"{i}------------------------{i}"));
                result.AddLast(new KeyValue<string>(key: "Ma san pham", value: $"{item.Id}"));
                result.AddLast(new KeyValue<string>(key: "Ma san pham", value: $"{item.MaHang}"));
                result.AddLast(new KeyValue<string>(key: "Ten san pham", value: $"{item.TenHang}"));
                result.AddLast(new KeyValue<string>(key: "Mau san pham", value: $"{item.MauSac}"));
                result.AddLast(new KeyValue<string>(key: "Noi san xuat", value: $"{item.NoiSanXuat}"));
                result.AddLast(new KeyValue<string>(key: "Ngay nhap kho", value: $"{item.NgayNhapKho.ToShortDateString()}"));
                result.AddLast(new KeyValue<string>(key: "So luong ton kho", value: $"{item.SoLuong}"));
                result.AddLast(new KeyValue<string>(key: "Gia san pham", value: $"{item.GiaBan} VND"));
            }

            return result;
        }
        public static void DatHang(int idDonHang,int idHangHoa,int soLuongHangHoa)
        {
            _ = new HangHoaDonHang(idDonHang,idHangHoa,soLuongHangHoa);
        }

    }
}
