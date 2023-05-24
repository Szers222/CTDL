using System;
using System.IO;
using System.Text;
using DemoDoAnCTDLvsGiaiThuat.Services;
using static System.Net.WebRequestMethods;

namespace DemoDoAnCTDLvsGiaiThuat.Entities
{
    class HangHoaDonHang
    {
        //field
        const string FILE = "DB/HangHoaDonHang.txt";
        private static LinkedList<HangHoaDonHang> listHangHoaDonHang = new LinkedList<HangHoaDonHang>();
        private int idDonHang;
        private int idHangHoa;
        private int soLuong;
        //Properties
        public static LinkedList<HangHoaDonHang> ListHangHoaDonHang { get => listHangHoaDonHang; set => listHangHoaDonHang = value; }
        public int IdDonHang { get => idDonHang; set => idDonHang = value; }
        public int IdHangHoa { get => idHangHoa; set => idHangHoa = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }

        //Constructor
        public HangHoaDonHang(int idDonHang, int idHangHoa,int soLuong, bool isFile = false)
        {
            HangHoa hangHoa = null;
            DonHang donHang = null;


            for (int i = 0; i < HangHoa.ListHangHoa.Count; i++)
            {
                if (HangHoa.ListHangHoa.Get(i).Id == idHangHoa)
                {
                    hangHoa = HangHoa.ListHangHoa.Get(i);
                    break;
                }
            }
            for (int i = 0; i < DonHang.ListDonHang.Count; i++)
            {
                if (DonHang.ListDonHang.Get(i).Id == idDonHang)
                {
                    donHang = DonHang.ListDonHang.Get(i);
                    break;
                }
            }

            if (hangHoa==null)
            {
                throw new Exception("Khong tim thay hang hoa");
            }
            if (donHang == null)
            {
                throw new Exception("Khong tim thay don hang");
            }
            if (hangHoa.SoLuong < soLuong && !isFile)
            {
                throw new Exception("So Luong Trong Kho Khong Du");
            }
            if (hangHoa.SoLuong<=0 && !isFile)
            {
                throw new Exception("Het Hang");
            }
            //Tinh Tien
            donHang.TongTien += (soLuong * hangHoa.GiaBan);
            // them so luong hang hoa vao trong so luong don hang
            donHang.SoLuong += soLuong;
            // giam so luong hang hoa trong kho
            hangHoa.SoLuong -= soLuong;

            hangHoa.ListDonHang.AddLast(donHang);
            donHang.ListHangHoa.AddLast(hangHoa);

            this.soLuong = soLuong;
            this.idDonHang = idDonHang;
            this.idHangHoa = idHangHoa;

            listHangHoaDonHang.AddLast(this);
        }


        public static string[] ToFile()
        {
            LinkedList<string> strs = new LinkedList<string>();
            HangHoaDonHang temp;
            for (int i = 0; i < listHangHoaDonHang.Count; i++)
            {
                temp = listHangHoaDonHang.Get(i);
                strs.AddLast($"{temp.idDonHang}-{temp.idHangHoa}-{temp.soLuong}");
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
                    string[] hangHoaData = hangHoaListData.Get(i).Split("-");
                    try
                    {
                        _ = new HangHoaDonHang(idDonHang: int.Parse(hangHoaData[0]), idHangHoa: int.Parse(hangHoaData[1]), soLuong: int.Parse(hangHoaData[2]),isFile:true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }/// <summary>
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

