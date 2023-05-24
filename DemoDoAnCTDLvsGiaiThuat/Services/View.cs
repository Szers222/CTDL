using System;
using System.Collections.Generic;

namespace DemoDoAnCTDLvsGiaiThuat.Service
{
    public interface IKeyValue
    {
        string GetKey();
        string GetValueType();
        object GetValue();
    }

    public class KeyValue<T> : IKeyValue
    {
        private T _value;
        private string _key;

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        public T Value
        {
            get => _value; set => _value = value;
        }
        public bool IsPassword
        {
            get; set;
        }

        public KeyValue(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public KeyValue(string key)
        {
            Key = key;
        }

        public KeyValue(string key, T value, bool isPassword)
        {
            Key = key;
            Value = value;
            IsPassword = isPassword;
        }

        public string GetKey()
        {
            return Key;
        }

        public string GetValueType()
        {
            return _value.GetType().Name;
        }

        public object GetValue()
        {
            return Value;
        }
    }
    public static class View
    {
        private static int length = 50;
        private static ConsoleColor titleColor = ConsoleColor.Green;
        private static ConsoleColor borderColor = ConsoleColor.Yellow;
        private static ConsoleColor keyColor = ConsoleColor.DarkGreen;
        private static ConsoleColor valueColor = ConsoleColor.White;
        private static ConsoleColor warningColor = ConsoleColor.Red;
        private static ConsoleColor successColor = ConsoleColor.Green;

        public static int Length
        {
            set
            {
                length = value;
            }
        }
        public static ConsoleColor TitleColor
        {
            set
            {
                titleColor = value;
            }
        }
        public static ConsoleColor BorderColor
        {
            set
            {
                borderColor = value;
            }
        }
        public static ConsoleColor KeyColor
        {
            set
            {
                keyColor = value;
            }
        }
        public static ConsoleColor ValueColor
        {
            set
            {
                valueColor = value;
            }
        }
        public static ConsoleColor WarningColor
        {
            set
            {
                warningColor = value;
            }
        }
        public static ConsoleColor SuccessColor
        {
            set
            {
                successColor = value;
            }
        }

        /// <summary>
        /// chức năng nhập mật khẩu
        /// </summary>
        /// <returns></returns>
        private static string InputPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass.Remove(pass.Length - 1, 1);
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Environment.Exit(1);
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            return pass;
        }

        /// <summary>
        /// tự động tạo chuỗi theo số lượng
        /// </summary>
        /// <param name="n">số lượng</param>
        /// <param name="str">chuỗi</param>
        /// <returns>chuỗi đã tạo</returns>
        private static string GenStr(int n, string str)
        {
            string strs = "";
            for (int i = 0; i < n; i++)
            {
                strs += str;
            }
            return strs;
        }

        /// <summary>
        /// Hiển thị tiêu đề
        /// </summary>
        /// <param name="title">tên tiêu đề</param>
        public static void HeaderTitle(string title)
        {
            title = title.ToUpper();
            int titleLength = title.Length;
            int lenght = ((length) * 2 - 2 - 1 - titleLength) / 2;
            bool checkLength = (length * 2 - 1) == (titleLength + (lenght * 2) + 2);
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = borderColor;
            Console.Write("\t");
            for (int i = 0; i < length; i++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
            Console.Write("\t");
            Console.Write("*" + GenStr(lenght, " "));
            Console.ForegroundColor = titleColor;
            Console.Write(title);
            Console.ForegroundColor = borderColor;
            if (checkLength)
            {
                Console.WriteLine(GenStr(lenght, " ") + "*");
            }
            else
            {
                Console.WriteLine(GenStr(lenght + 1, " ") + "*");
            }

            Console.Write("\t");
            for (int i = 0; i < length; i++)
            {
                Console.Write("* ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// nhập dữ liệu
        /// </summary>
        /// <param name="data">dữ liệu cần nhập
        ///     <param name="Key">
        ///         câu xuất ra màn hình
        ///     </param>
        ///     <param name="Value">
        ///         dữ liệu cần nhập
        ///     </param>
        /// </param>
        public static void ReadLines(LinkedList<IKeyValue> data)
        {
            int maxLengthKey = GetMaxLength(data);

            int lenght = length - maxLengthKey - 2;
            int itemLenght = 0;

            bool checkInput = false;

            foreach (var dataItem in data)
            {
                do
                {
                    itemLenght = dataItem.GetKey().Length;
                    Console.ForegroundColor = keyColor;
                    Console.Write($"\t{GenStr(lenght, " ")}{dataItem.GetKey()}");
                    Console.ForegroundColor = valueColor;
                    Console.Write($"{GenStr(maxLengthKey - itemLenght, " ")} : ");

                    switch (dataItem.GetValueType())
                    {
                        case "Int32":
                            int intValue;
                            checkInput = !int.TryParse(ReadLine(), out intValue);
                            ((KeyValue<int>)dataItem).Value = intValue;
                            break;

                        case "Double":
                            double doubleValue;
                            checkInput = !double.TryParse(ReadLine(), out doubleValue);
                            ((KeyValue<double>)dataItem).Value = doubleValue;
                            break; 

                        case "String":
                            if (((KeyValue<string>)dataItem).IsPassword)
                            {
                                ((KeyValue<string>)dataItem).Value = InputPassword();
                            }
                            else
                            {
                                ((KeyValue<string>)dataItem).Value = ReadLine();
                            }
                            break;

                        case "Char":
                            ((KeyValue<char>)dataItem).Value = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                            break;
                    }
                } while (checkInput);
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// hiển thị menu
        /// </summary>
        /// <param name="data">dữ liệu cần nhập
        ///     <param name="Key">
        ///         lựa chọn
        ///     </param>
        ///     <param name="Value">
        ///         thực hiện
        ///     </param>
        /// </param>
        public static void WriteLines(LinkedList<IKeyValue> data)
        {
            int maxLengthKey = GetMaxLength(data);

            int lenght = length - maxLengthKey - 2;
            int itemLenght = 0;

            foreach (var dataItem in data)
            {
                itemLenght = dataItem.GetKey().Length;
                Console.ForegroundColor = keyColor;
                Console.Write($"\t{GenStr(lenght, " ")}{dataItem.GetKey()}");
                Console.ForegroundColor = valueColor;
                Console.WriteLine($"{GenStr(maxLengthKey - itemLenght, " ")} : {((KeyValue<string>)dataItem).Value}");
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// lây độ dài tối đa của key
        /// </summary>
        /// <param name="data">mảng KeyValue</param>
        /// <returns></returns>
        private static int GetMaxLength(LinkedList<IKeyValue> data)
        {
            int maxLength = int.MinValue;

            foreach (var item in data)
            {
                int itemLenght = item.GetKey().Length;
                if (itemLenght > maxLength)
                {
                    maxLength = itemLenght;
                }
            }

            Console.ForegroundColor = ConsoleColor.Black;
            return maxLength;
        }

        private static int GetMaxLength(Dictionary<string, IKeyValue> data)
        {
            int maxLength = int.MinValue;

            foreach (var item in data)
            {
                int itemLenght = item.Value.GetKey().Length;
                if (itemLenght > maxLength)
                {
                    maxLength = itemLenght;
                }
            }

            Console.ForegroundColor = ConsoleColor.Black;
            return maxLength;
        }

        /// <summary>
        /// cảnh báo
        /// </summary>
        /// <param name="str">chuỗi hiển thị</param>
        public static void Warning(string str)
        {
            int lenght = length - (str.Length / 2) - 1;
            Console.ForegroundColor = warningColor;
            Console.WriteLine($"\t{GenStr(lenght, " ")}{str.ToUpper()}");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Black;
        }


        /// <summary>
        /// nhập dữ liệu
        /// </summary>
        /// <param name="data">dữ liệu cần nhập
        ///     <param name="Key">
        ///         câu xuất ra màn hình
        ///     </param>
        ///     <param name="Value">
        ///         dữ liệu cần nhập
        ///     </param>
        /// </param>
        public static void ReadLines(Dictionary<string, IKeyValue> data)
        {
            int maxLengthKey = GetMaxLength(data);

            int lenght = length - maxLengthKey - 2;
            int itemLenght = 0;

            bool checkInput = false;

            foreach (var dataItem in data)
            {
                do
                {
                    itemLenght = dataItem.Value.GetKey().Length;
                    Console.ForegroundColor = keyColor;
                    Console.Write($"\t{GenStr(lenght, " ")}{dataItem.Value.GetKey()}");
                    Console.ForegroundColor = valueColor;
                    Console.Write($"{GenStr(maxLengthKey - itemLenght, " ")} : ");



                    switch (dataItem.Value.GetValueType())
                    {
                        case "Int32":
                            int intValue;
                            checkInput = !int.TryParse(ReadLine(), out intValue);
                            ((KeyValue<int>)dataItem.Value).Value = intValue;
                            break;

                        case "Double":
                            double doubleValue;
                            checkInput = !double.TryParse(ReadLine(), out doubleValue);
                            ((KeyValue<double>)dataItem.Value).Value = doubleValue;
                            break;

                        case "String":
                            if (((KeyValue<string>)dataItem.Value).IsPassword)
                            {
                                ((KeyValue<string>)dataItem.Value).Value = InputPassword();
                            }
                            else
                            {
                                ((KeyValue<string>)dataItem.Value).Value = ReadLine();
                            }
                            break;

                        case "Char":
                            ((KeyValue<char>)dataItem.Value).Value = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                            break;
                    }
                } while (checkInput);
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// thông báo
        /// </summary>
        /// <param name="str">chuỗi hiển thị</param>
        public static void Success(string str)
        {
            int lenght = length - (str.Length / 2) - 1;
            Console.ForegroundColor = successColor;
            Console.WriteLine($"\t{GenStr(lenght, " ")}{str.ToUpper()}");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// custom readline
        /// </summary>
        /// <returns>chuỗi ký tự nhập từ bàn phím</returns>
        private static string ReadLine()
        {
            var str = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && str.Length > 0)
                {
                    Console.Write("\b \b");
                    str = str.Remove(str.Length - 1, 1);
                }
                else if (key == ConsoleKey.Tab)
                {
                    break;
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write(keyInfo.KeyChar);
                    str += keyInfo.KeyChar;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Functions.SaveAllData();
                    Environment.Exit(1);
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            return str;
        }
    }
}
