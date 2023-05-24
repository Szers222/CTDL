using System;
using DemoDoAnCTDLvsGiaiThuat.Services;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO;

namespace DemoDoAnCTDLvsGiaiThuat.Entities
{
    class DonHang
    {
        //fields
        const string FILE = "DB/DonHang.txt";
        private static int autoIncrement = 0;
        private static LinkedList<DonHang> listDonHang = new LinkedList<DonHang>();
        private static Queue<DonHang> queueDonHang = new Queue<DonHang>();

        /// <summary>
        /// id là số thứ tự đơn hàng
        /// </summary>
        private int id;
        private int soLuong;
        private int tongTien;
        private KhachHang khachHang;
        private DateTime ngayDatHang;
        private LinkedList<HangHoa> listHangHoa;



        //Properties
        public static int AutoIncreament { get => autoIncrement; }
        internal static LinkedList<DonHang> ListDonHang { get => listDonHang; set => listDonHang = value; }
        public int Id { get => id; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
        public KhachHang KhachHangCuaDonHang { get => khachHang; set => khachHang = value; }
        public DateTime NgayDatHang { get => ngayDatHang; set => ngayDatHang = value; }
        internal LinkedList<HangHoa> ListHangHoa { get => listHangHoa; set => listHangHoa = value; }
        internal static Queue<DonHang> QueueDonHang { get => queueDonHang; set => queueDonHang = value; }

        //Constructor
        public DonHang(int idKhachHang)
        {
            KhachHang khachHang = null;
            for (int i = 0; i < KhachHang.ListKhachHang.Count; i++)
            {
                if (KhachHang.ListKhachHang.Get(i).Id.Equals(idKhachHang))
                {
                    khachHang = KhachHang.ListKhachHang.Get(i);
                }
            }
            if (khachHang == null)
            {
                throw new Exception("Khach hang khong ton tai");
            }
            this.KhachHangCuaDonHang = khachHang;
            this.ngayDatHang = DateTime.Now;
            this.listHangHoa = new LinkedList<HangHoa>();
            this.id = ++autoIncrement;
            this.khachHang.ListDonHang.AddLast(this);
            ListDonHang.AddLast(this);
            queueDonHang.Enqueue(this);
        }
        public DonHang(int id, int idKhachHang, DateTime ngayDatHang)
        {
            KhachHang khachHang = null;
            for (int i = 0; i < KhachHang.ListKhachHang.Count; i++)
            {
                if (KhachHang.ListKhachHang.Get(i).Id.Equals(idKhachHang))
                {
                    khachHang = KhachHang.ListKhachHang.Get(i);
                }
            }
            if (khachHang == null)
            {
                throw new Exception("Khach hang khong ton tai");
            }
            this.KhachHangCuaDonHang = khachHang;
            this.ngayDatHang = ngayDatHang;
            this.listHangHoa = new LinkedList<HangHoa>();
            this.id = id;
            this.khachHang.ListDonHang.AddLast(this);
            ListDonHang.AddLast(this);
            queueDonHang.Enqueue(this);
        }
        public static bool XoaPTDauTien(int id)
        {
            bool reusult = false;

            return reusult;
        }
        public override string ToString()
        {
            return $"Đơn Hàng:\n \n\tSố thứ tự: {Id}\n\tSố Lượng: {SoLuong}\n\tTổng Tiền: {TongTien}\n\tKhách Hàng: {KhachHangCuaDonHang}\n\tNgày Đặt Hàng: {NgayDatHang}";
        }
        /// <summary>
        /// định dạng chuỗi lưu vào file
        /// </summary>
        /// <returns>mảng dữ liệu lưu file</returns>
        public static string[] ToFile()
        {
            LinkedList<string> strs = new LinkedList<string>();
            strs.AddLast($"{autoIncrement}");
            DonHang temp;
            for (int i = 0; i < listDonHang.Count; i++)
            {
                temp = listDonHang.Get(i);
                strs.AddLast($"{temp.Id}-{temp.KhachHangCuaDonHang.Id}-{temp.ngayDatHang.ToString("dd/MM/yyyy")}");
            }

            return strs.ToArray();
        }
        /// <summary>
        /// đọc dữ liệu file
        /// </summary>
        public static void GetObjectsFromFile()
        {
            LinkedList<string> donHangListData
                = new LinkedList<string>();
            donHangListData.ParseLinkedList(IOFIle.ReadFile(FILE));
            if (donHangListData.Count > 0)
            {
                for (int i = 0; i < donHangListData.Count; i++)
                {
                    if (i == 0)
                    {
                        autoIncrement = int.Parse(donHangListData.Get(i));
                    }
                    else
                    {
                        string[] donHangData = donHangListData.Get(i).Split("-");
                        try
                        {
                            _ = new DonHang(id: int.Parse(donHangData[0]),  idKhachHang: int.Parse(donHangData[1]), ngayDatHang: DateTime.ParseExact($"{donHangData[2]}-{donHangData[3]}-{donHangData[4]}", "dd-MM-yyyy", null));
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
