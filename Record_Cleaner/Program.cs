using System;

namespace Record_Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            //string fp = @"D:\文档\跑团相关\模组\逆转的环\Reversed_Ring_R1.txt";
            string fp = @"D:\文档\跑团相关\模组\逆转的环\test.txt";
            FileHandler handler = new FileHandler();
            handler.path = fp;

            handler.ReadFile();
            handler.DelComment();
        }
    }
}
