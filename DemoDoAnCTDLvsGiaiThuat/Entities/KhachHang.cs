using DemoDoAnCTDLvsGiaiThuat.Services;
using System;
using System.IO;
using System.Text;

namespace DemoDoAnCTDLvsGiaiThuat.Entities
{
     class KhachHang
    {
        //fields
        const string FILE = "DB/KhachHang.txt";
        private static int autoIncrement = 0;
        private static LinkedList<KhachHang> listKhachHang = new LinkedList<KhachHang>();

        private int id;
        private string tenKhachHang;
        private string diaChiKhachHang;
        private string soDienThoai;

        private LinkedList<DonHang> listDonHang = new LinkedList<DonHang>();

        //Properties
        public static int AutoIncrement { get => autoIncrement; }
        public static LinkedList<KhachHang> ListKhachHang { get => listKhachHang; set => listKhachHang = value; }

        public int Id { get => id; }
        public string TenKhachHang { get => tenKhachHang; set => tenKhachHang = value; }
        public string DiaChiKhachHang { get => diaChiKhachHang; set => diaChiKhachHang = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public LinkedList<DonHang> ListDonHang { get => listDonHang; set => listDonHang = value; }
        //Constructor
        public KhachHang(string tenKhachHang, string diaChiKhachHang, string soDienThoai)
        {
            this.tenKhachHang = tenKhachHang;
            this.diaChiKhachHang = diaChiKhachHang;
            this.soDienThoai = soDienThoai;
            this.id = ++autoIncrement;

            listKhachHang.AddLast(this);
        }
        public KhachHang(int id,string tenKhachHang, string diaChiKhachHang, string soDienThoai)
        {
            this.tenKhachHang = tenKhachHang;
            this.diaChiKhachHang = diaChiKhachHang;
            this.soDienThoai = soDienThoai;
            this.id = id;

            listKhachHang.AddLast(this);
        }
        //Method
        public override string ToString()
        {
            return $"KhachHang: \n\tID:{this.Id}\n\tTen Khach Hang:{this.tenKhachHang}\n\tDia Chi:{this.diaChiKhachHang}\n\tSo Dien Thoai:{this.soDienThoai}";
        }
        public static string[] ToFile()
        {
            LinkedList<string> strs = new LinkedList<string>();
            strs.AddLast($"{autoIncrement}");
            KhachHang temp;
            for (int i = 0; i < listKhachHang.Count; i++)
            {
                temp = listKhachHang.Get(i);
                strs.AddLast($"{temp.Id}-{temp.tenKhachHang}-{temp.diaChiKhachHang}-{temp.soDienThoai}");
            }
            return strs.ToArray();
        }
        /// <summary>
        /// đọc dữ liệu file
        /// </summary>
        public static void GetObjectsFromFile()
        {
            LinkedList<string> khachHangListData
                = new LinkedList<string>();
            khachHangListData.ParseLinkedList(IOFIle.ReadFile(FILE));
            if (khachHangListData.Count > 0)
            {
                for (int i = 0; i < khachHangListData.Count; i++)
                {
                    if (i == 0)
                    {
                        autoIncrement = int.Parse(khachHangListData.Get(i));
                    }
                    else
                    {
                        string[] khachHangData = khachHangListData.Get(i).Split("-");
                        try
                        {
                            _ = new KhachHang (id: int.Parse(khachHangData[0]), tenKhachHang: khachHangData[1], diaChiKhachHang: khachHangData[2], soDienThoai: khachHangData[3]);
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

