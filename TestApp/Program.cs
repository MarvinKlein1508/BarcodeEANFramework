using BarcodeEANFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EAN13 test1 = new EAN13();
            Console.WriteLine(test1.GetEAN13BarcodeString("900000078912"));
            Console.ReadKey();
        }
    }
}
