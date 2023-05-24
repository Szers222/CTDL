using DemoDoAnCTDLvsGiaiThuat.Entities;
using DemoDoAnCTDLvsGiaiThuat.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoDoAnCTDLvsGiaiThuat
{
    class Screen
    {
        /// <summary>
        /// Hien thi Menu lua chon
        /// lua chon 1: hien thi thong tin tat ca hang hoa
        /// lua chon 2: hien thi thong tin cac hang hoa can tim
        /// lua chon 3: dat hang
        /// lua chon 4: hien thi man hinh dang nhap de quan ly hang hoa va don hang
        /// lua chon ESC: thoat chuong  trinh
        /// </summary>
        public static void Menu()
        {
            Console.Clear();
            char chosen;
            LinkedList<IKeyValue> itemsMenu = new LinkedList<IKeyValue>();
            itemsMenu.AddLast(new KeyValue<string>(key: "1", value: "Hien thi thong tin hang hoa"));
            itemsMenu.AddLast(new KeyValue<string>(key: "2", value: "Tim kiem thong tin hang hoa"));
            itemsMenu.AddLast(new KeyValue<string>(key: "3", value: "Dat hang"));
            itemsMenu.AddLast(new KeyValue<string>(key: "4", value: "Quan Ly"));
            itemsMenu.AddLast(new KeyValue<string>(key: "ESC", value: "Thoat chuong trinh"));
            Dictionary<string, IKeyValue> inputChosen = new Dictionary<string, IKeyValue>();
            inputChosen.Add("chosen", new KeyValue<char>(key: "Lua chon"));

            do
            {
                View.HeaderTitle("menu");
                View.WriteLines(itemsMenu);
                View.ReadLines(inputChosen);
                chosen = (char)inputChosen["chosen"].GetValue();
            } while (chosen != '1' && chosen != '2' && chosen != '3' && chosen != '4' && chosen != (char)ConsoleKey.Escape);

            switch (chosen)
            {
                case '1':
                    HienThiThongTinHangHoa();
                    break;
                case '2':
                    TimKiemThongTinHangHoa();
                    break;
                case '3':
                    DatHang();
                    break;
                case '4':
                    QuanLy();
                    break;

            }

        }
        /// <summary>
        /// Hien thi tat ca  thon tin hang hoa
        /// </summary>
        private static void HienThiThongTinHangHoa()
        {
            LinkedList<IKeyValue> keyValues = null;
            keyValues = Functions.LayTatCaHangHoa();
            Console.Clear();
            View.HeaderTitle("Thong Tin Hang Hoa");
            View.WriteLines(keyValues);
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
        /// <summary>
        /// Tìm kiếm thông tin hàng hóa theo tên
        /// </summary>
        private static void TimKiemThongTinHangHoa()
        {
            Console.Clear();

            Dictionary<string, IKeyValue> inputSearch = new Dictionary<string, IKeyValue>();
            inputSearch.Add("tuKhoa", new KeyValue<string>(key: "Nhap Ten Can Tim", string.Empty));
            View.HeaderTitle("Tim Kiem Hang Hoa");
            View.ReadLines(inputSearch);
            string tuKhoa = (string)inputSearch["tuKhoa"].GetValue();
            LinkedList<IKeyValue> keyValues = null;
            try
            {
                keyValues = Functions.TimKiemHangHoa(tuKhoa);
                Console.Clear();
                View.HeaderTitle("Thong Tin Hang Hoa Da Tim Thay");
                View.WriteLines(keyValues);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                View.Warning(e.Message);
            }
            Console.Clear();
            Menu();
            return;
        }
        /// <summary>
        /// Cho phép đặt hàng
        /// lựa chọn 1 đăng ký khách hàng  mới và cho phép đặt hàng
        /// lựa chọn 2 Cho phép khách hàng cũ đặt hàng
        /// Nhấn ESC để quay về màn hình chính
        /// </summary>
        private static void DatHang()
        {
            char chosen;
            LinkedList<IKeyValue> itemsMenu = new LinkedList<IKeyValue>();
            itemsMenu.AddLast(new KeyValue<string>(key: "1", value: "Khach Hang Moi"));
            itemsMenu.AddLast(new KeyValue<string>(key: "2", value: "Khach hang Da Dang Ky"));
            itemsMenu.AddLast(new KeyValue<string>(key: "ESC", value: "Quay Ve Man Hinh Chinh"));
            Dictionary<string, IKeyValue> inputChosen = new Dictionary<string, IKeyValue>();
            inputChosen.Add("chosen", new KeyValue<char>(key: "Lua chon"));

            do
            {
                View.HeaderTitle("Dat Hang");
                View.WriteLines(itemsMenu);
                View.ReadLines(inputChosen);
                chosen = (char)inputChosen["chosen"].GetValue();
            } while (chosen != '1' && chosen != '2' && chosen != (char)ConsoleKey.Escape);

            switch (chosen)
            {
                case '1':
                    DangKyKhachHang();
                    break;
                case '2':
                    DatHangChoKhachHangCu();
                    break;
            }
            Menu();
        }
        private static void QuanLy()
        {
           
        }
        /// <summary>
        /// Đăng ký khách hàng mới
        /// </summary>
        private static void DangKyKhachHang()
        {
            Dictionary<string, IKeyValue> input = new Dictionary<string, IKeyValue>();
            input.Add("tenKhachHang", new KeyValue<string>(key: "Nhap Ten Khach Hang", value: ""));
            input.Add("diaChiKhachHang", new KeyValue<string>(key: "Nhap Dia Chi Khach Hang", value: ""));
            input.Add("soDienThoai", new KeyValue<string>(key: "Nhap SDT Khach Hang", value: ""));
            View.HeaderTitle("thong tin dang ky");
            View.ReadLines(input);
            string tenKhachHang = (string)input["tenKhachHang"].GetValue();
            string diaChiKhachHang = (string)input["diaChiKhachHang"].GetValue();
            string soDienThoai = (string)input["soDienThoai"].GetValue();
            KhachHang khachHang = new KhachHang(tenKhachHang, diaChiKhachHang, soDienThoai);
            View.Success("Dang ky thanh comg, ma khach hang cua ban la " + khachHang.Id);
            DonHang donHang = new DonHang(khachHang.Id);
            DatHang(donHang.Id);
            Menu();
        }
        /// <summary>
        /// Dùng để đặt hàng
        /// </summary>
        /// <param name="idDonHang">truyền vô idDonHang lấy ra đơn hàng để thêm hàng hóa vào</param>
        private static void DatHang(int idDonHang)
        {
            Console.Clear();
            Dictionary<string, IKeyValue> input = new Dictionary<string, IKeyValue>();
            input.Add("idHangHoa", new KeyValue<int>(key: "Nhap ma  Hang Hoa"));
            input.Add("soLuongHangHoa", new KeyValue<int>(key: "Nhap So Luong Hang Hoa"));
            View.HeaderTitle("Dat Hang");
            View.ReadLines(input);
            int idHangHoa = (int)input["idHangHoa"].GetValue();
            int soLuongHangHoa = (int)input["soLuongHangHoa"].GetValue();
            try
            {
                Functions.DatHang(idDonHang, idHangHoa, soLuongHangHoa);
                View.Success("Don hang dang cho xu ly");
            }
            catch (Exception e)
            {
                View.Warning(e.Message);
            }
            char chosen;
            Dictionary<string, IKeyValue> inputChosen = new Dictionary<string, IKeyValue>();
            inputChosen.Add("chosen", new KeyValue<char>(key: "Nhan ESC de ngung dat hang"));
            View.ReadLines(inputChosen);
            chosen = (char)inputChosen["chosen"].GetValue();
            if (chosen == ((char)ConsoleKey.Escape))
            {
                Menu();
            }
            DatHang(idDonHang);
            Menu();
        }
        /// <summary>
        /// Cho khách hàng nhập id  khi đã có id trên hệ thống sau đó cho phép khách hàng cũ được đặt hàng
        /// </summary>
        private static void DatHangChoKhachHangCu()
        {
            Dictionary<string, IKeyValue> input = new Dictionary<string, IKeyValue>();
            input.Add("idKhacHang", new KeyValue<int>(key: "Nhap id  Khach Hang"));
            View.HeaderTitle("Dat Hang");
            View.ReadLines(input);
            int idKhacHang = (int)input["idKhacHang"].GetValue();

            try
            {
            DonHang donHang = new DonHang(idKhacHang);
            DatHang(donHang.Id);
            }
            catch (Exception e)
            {

                View.Warning(e.Message);
            }
            Menu();
        }
    }
}

