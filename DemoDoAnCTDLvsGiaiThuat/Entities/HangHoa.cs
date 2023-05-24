using DemoDoAnCTDLvsGiaiThuat.Services;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace DemoDoAnCTDLvsGiaiThuat.Entities
{
    internal class HangHoa
    {
        // fields
        const string FILE = "DB/HangHoa.txt";
        private static int autoIncrement = 0;
        private static LinkedList<HangHoa> listHangHoa = new LinkedList<HangHoa>();

        private int id;
        private string maHang;
        private string tenHang;
        private string noiSanXuat;
        private string mauSac;
        private int giaBan;
        private DateTime ngayNhapKho;
        private int soLuong;
        private LinkedList<DonHang> listDonHang = new LinkedList<DonHang>();

        // Properties
        public static int AutoIncrement { get => autoIncrement; }
        internal static LinkedList<HangHoa> ListHangHoa { get => listHangHoa; set => listHangHoa = value; }
        public int Id { get => id; }
        public string TenHang { get => tenHang; set => tenHang = value; }
        public string NoiSanXuat { get => noiSanXuat; set => noiSanXuat = value; }
        public string MauSac { get => mauSac; set => mauSac = value; }
        public int GiaBan { get => giaBan; set => giaBan = value; }
        public DateTime NgayNhapKho { get => ngayNhapKho; set => ngayNhapKho = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        internal LinkedList<DonHang> ListDonHang { get => listDonHang; }
        public string MaHang { get => maHang; set => maHang = value; }

        //Constructor
        public HangHoa(string tenHang, string noiSanXuat, string mauSac, int giaBan, DateTime ngayNhapKho, int soLuong)
        {
            this.tenHang = tenHang;
            this.noiSanXuat = noiSanXuat;
            this.mauSac = mauSac;
            this.giaBan = giaBan;
            this.ngayNhapKho = ngayNhapKho;
            this.soLuong = soLuong;
            id = ++autoIncrement;
            string temp = "";
            for (int i = 0; i < 4- $"{this.id}".Length; i++)
            {
                temp += "0";
            }
            this.MaHang = temp + $"{this.id}";
            listHangHoa.AddLast(this);
        }
        public HangHoa(int id, string tenHang, string noiSanXuat, string mauSac, int giaBan, DateTime ngayNhapKho, int soLuong,string maHang)
        {
            this.tenHang = tenHang;
            this.noiSanXuat = noiSanXuat;
            this.mauSac = mauSac;
            this.giaBan = giaBan;
            this.ngayNhapKho = ngayNhapKho;
            this.soLuong = soLuong;
            this.MaHang = maHang;
            this.id = id;
            listHangHoa.AddLast(this);
        }
        //method
        public static LinkedList<HangHoa> TimKiemHangHoa(string tenHang)
        {

            LinkedList<HangHoa> hangHoa = new LinkedList<HangHoa>();
            HangHoa itemHangHoa;
            for (int i = 0; i < HangHoa.ListHangHoa.Count; i++)
            {
                itemHangHoa = HangHoa.ListHangHoa.Get(i);
                if (itemHangHoa.tenHang.Contains(tenHang))
                {
                    hangHoa.AddLast(itemHangHoa);
                }
            }
            if (hangHoa.Count == 0)
            {
                throw new Exception("Hang hoa khong tim thay");
            }
            return hangHoa;
        }
        //public static bool ThemHangHoa()
        //{
        //}

        /// <summary>
        /// hàm xóa hàng hóa
        /// </summary>
        /// <param name="id">id của hàng hóa cần xóa</param>
        /// <returns>thành công nếu tìm được hàng hóa, thất bại nếu không tìm được</returns>
        public static bool XoaHangHoa(int id)
        {
            bool result = false;
            
            for (int i = 0; i < HangHoa.ListHangHoa.Count; i++)
            {
                if (HangHoa.ListHangHoa.Get(i).Id==id)
                {
                    HangHoa.ListHangHoa.Remove(i);
                    result = true;
                }
            }
            
            return result;
        }

        public override string ToString()
        {
            return $"Hang Hóa: \n\tID:{this.id} \n\tTên Hàng: {tenHang}\n\tGiá Bán: {giaBan}\n\tMàu Sắc: {mauSac}\n\tNơi Sản Xuất: {noiSanXuat}\n\tNgày Nhập Kho: {ngayNhapKho.ToString("dd/MM/yyyy")}\n\tSố Lượng: {soLuong}";
        }
        public static string[] ToFile()
        {
            LinkedList<string> strs = new LinkedList<string>();
            strs.AddLast($"{autoIncrement}");
            HangHoa temp;
            for (int i = 0; i < listHangHoa.Count; i++)
            {
                temp = listHangHoa.Get(i);
                strs.AddLast($"{temp.Id}-{temp.tenHang}-{temp.noiSanXuat}-{temp.mauSac}-{temp.giaBan}-{temp.ngayNhapKho.ToString("dd/MM/yyyy")}-{temp.soLuong}-{temp.MaHang}");
            }
            return strs.ToArray();
        }
        /// <summary>
        /// đọc dữ liệu file
        /// </summary>
        public static void GetObjectsFromFile()
        {
            LinkedList<string> hangHoaListData
                = new LinkedList<string>();
            hangHoaListData.ParseLinkedList(IOFIle.ReadFile(FILE));
            if (hangHoaListData.Count > 0)
            {
                for (int i = 0; i < hangHoaListData.Count; i++)
                {
                    if (i == 0)
                    {
                        autoIncrement = int.Parse(hangHoaListData.Get(i));
                    }
                    else
                    {
                        string[] hangHoaData = hangHoaListData.Get(i).Split("-");
                        try
                        {
                            _ = new HangHoa(id: int.Parse(hangHoaData[0]), tenHang: hangHoaData[1], noiSanXuat: hangHoaData[2], mauSac: hangHoaData[3], giaBan: int.Parse(hangHoaData[4]), ngayNhapKho: DateTime.ParseExact($"{hangHoaData[5]}/{hangHoaData[6]}/{hangHoaData[7]}", "dd/MM/yyyy", null),soLuong: int.Parse(hangHoaData[8]), maHang: hangHoaData[9]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// lưu dữ liệu vào file
        /// </summary>
        public static void SaveObjectsToFile()
        {
            if (!Directory.Exists("DB"))
            {
                Directory.CreateDirectory("DB");
            }
            IOFIle.WriteFile(FILE, ToFile());
        }
    }
}

