using System;

namespace Record_Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            string fpath = @"D:\文档\跑团相关\模组\逆转的环\";
            //string fname = @"test.txt";
            string fname = @"Reversed_Ring_R1.txt";
            
            FileHandler handler = new FileHandler(fpath,fname);

            handler.ReadFile();
            handler.WriteToFile();
        }
    }
}
