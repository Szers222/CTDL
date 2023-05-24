using DemoDoAnCTDLvsGiaiThuat.Entities;
using DemoDoAnCTDLvsGiaiThuat.Service;
using DemoDoAnCTDLvsGiaiThuat.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections;

namespace DemoDoAnCTDLvsGiaiThuat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            View.Length = 50;
            Functions.GetAllData();
            Screen.Menu();
            Functions.SaveAllData();

        }
    }
}
