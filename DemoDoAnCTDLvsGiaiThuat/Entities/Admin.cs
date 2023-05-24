using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using DemoDoAnCTDLvsGiaiThuat.Services;
namespace DemoDoAnCTDLvsGiaiThuat.Entities
{
    class Admin
    {
        const string FILE = "DB/Admin.txt";
        private static LinkedList<Admin> listAdmin = new LinkedList<Admin>();
        private static int autoIncrement = 0;

        private int id;
        private string user;
        private string password;
        private bool status;

        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public static LinkedList<Admin> ListAdmin { get => listAdmin; }
        public int Id { get => id; }
        public bool Status { get => status; set => status = value; }

        public Admin(string user, string password)
        {
            this.user = user;
            this.password = password;
            this.id = autoIncrement++;
            this.status = true;
            listAdmin.AddLast(this);
        }

        private Admin(int id, string user, string password, bool status)
        {
            this.id = id;
            this.user = user;
            this.password = password;
            this.status = status;
            listAdmin.AddLast(this);
        }

        public static bool CheckAdmin(string user, string password)
        {
            bool result = false;
            for (int i = 0; i < ListAdmin.Count; i++)
            {
                if (ListAdmin.Get(i).User.Equals(user) && ListAdmin.Get(i).Password.Equals(password))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// định dạng chuỗi lưu vào file
        /// </summary>
        /// <returns>mảng dữ liệu lưu file</returns>
        public static string[] ToFile()
        {
            LinkedList<string> strs = new LinkedList<string>();
            strs.AddLast($"{autoIncrement}");
            Admin temp;
            for (int i = 0; i < listAdmin.Count; i++)
            {
                temp= listAdmin.Get(i);
                strs.AddLast($"{temp.Id}-{temp.user}-{temp.password}-{temp.status}");
            }

            return strs.ToArray();
        }

        /// <summary>
        /// đọc dữ liệu file
        /// </summary>
        public static void GetObjectsFromFile()
        {
            LinkedList<string> adminListData
                = new LinkedList<string>();
            adminListData.ParseLinkedList(IOFIle.ReadFile(FILE));
            if (adminListData.Count > 0)
            {
                for (int i = 0; i < adminListData.Count; i++)
                {
                    if (i == 0)
                    {
                        autoIncrement = int.Parse(adminListData.Get(i));
                    }
                    else
                    {
                        string[] adminData = adminListData.Get(i).Split("-");
                        try
                        {
                            _ = new Admin(id: int.Parse(adminData[0]), user: adminData[1], password: adminData[2],status: bool.Parse(adminData[3]));
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
