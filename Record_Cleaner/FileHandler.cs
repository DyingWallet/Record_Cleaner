using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Record_Cleaner
{
    class FileHandler
    {
        //文件路径
        public string path { get; set; }

        //文件流
        public FileStream fs { get; set; }

        //IO流
        public StreamReader sr { get; set; }
        public StreamWriter sw { get; set; }

        public int total_lines { get; private set; }
        public int influenced_lines { get; private set; }

        //实例化一个list用于存储有效发言
        private List<string> contents = new List<string>();

        //实例化正则类用于文本匹配
        private Regex regex_comments = new Regex(@"^\s{4}（.*[）|\w]$");
        private Regex regex_charactor = new Regex(@"^.*?\s\d{4}(/\d{2}){2}\s(\d{2}:){2}\d{2}$");


        //匹配场外发言
        //private string regex_comments = @"^\s{4}（.*）$";
        //匹配发言者记录（id yyyy/MM/dd HH:mm:ss）
        //private string regex_charactor = @"^.*?\s\d{4}(/\d{2}){2}\s(\d{2}:){2}\d{2}$";

        ///读取文件
        public void ReadFile()
        {
            //打开文件
            fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);

            //开启文件流
            sr = new StreamReader(fs, Encoding.Default);
            string cha = "";
            string cont = "";

            total_lines = 0;
            influenced_lines = 0;
            //逐行读取文件
            if (cha == null)
                return;

            while ((cont = sr.ReadLine()) != null)
            {
                total_lines++;
                //判断是否为发言者，是则更新
                if (regex_charactor.IsMatch(cont))
                    cha = cont;
                //判断是否为场外，不是则记录
                else if (!regex_comments.IsMatch(cont))
                {
                    //记录发言人
                    contents.Add(cha);
                    //记录发言
                    contents.Add(cont);
                    influenced_lines += 2;
                }
            }
            //for(int i = 0; i < contents.Count; i++)
            //{
            //    Console.WriteLine(contents[i].ToString());
            //}

            Console.WriteLine("受影响行数/总行数：" + influenced_lines + "行/" + total_lines + "行");
        }

        //删除无用部分
        public int DelComment()
        {
            return 0;
        }

    }
}