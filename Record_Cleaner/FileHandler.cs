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
        public string fpath { get; private set; }
        //文件名
        public string fname { get; private set; }
        //文件流
        public FileStream fs { get; set; }
        //IO流
        public StreamReader sr { get; set; }
        public StreamWriter sw { get; set; }
        //计数器
        public int total_lines { get; private set; }
        public int influenced_lines { get; private set; }
        //list用于存储有效发言
        private List<string> contents = new List<string>();
        //用于文本匹配
        private Regex regex_comments = new Regex(@"^\s{4}（.*[）|\w]$");
        private Regex regex_charactor = new Regex(@"^.*?\s\d{4}(/\d{2}){2}\s(\d{2}:){2}\d{2}$");
        //用于自动获取文件名
        private Regex regex_fname = new Regex(@"\w*[.]\w*$");

        public FileHandler(string fpath, string fname)
        {
            this.fpath = fpath;
            this.fname = fname;
        }

        //匹配场外发言
        //private string regex_comments = @"^\s{4}（.*）$";
        //匹配发言者记录（id yyyy/MM/dd HH:mm:ss）
        //private string regex_charactor = @"^.*?\s\d{4}(/\d{2}){2}\s(\d{2}:){2}\d{2}$";

        ///从文件中筛选有效发言
        public bool ReadFile()
        {
            //打开文件流
            fs = new FileStream(fpath + fname, FileMode.Open, FileAccess.Read);
            //开启文件读取流
            sr = new StreamReader(fs, Encoding.Default);
            string cha = "";
            string cont = "";
            total_lines = 0;
            influenced_lines = 0;

            //获取文件名
            Match match = regex_fname.Match(fpath);
            if (match.Success) { fname = match.Value; }

            //逐行读取文件
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
            //关闭流
            sr.Close();
            fs.Close();

            if (total_lines == 0)
                return false;

            return true;
        }

        ///将有效信息写入新文件
        public bool WriteToFile()
        {
            fs = new FileStream(fpath + "New_" + fname, FileMode.Create, FileAccess.Write);
            //开启文件写入流
            sw = new StreamWriter(fs, Encoding.Default);
            for(int i = 0; i < contents.Count; i++)
                sw.WriteLine(contents[i]);
            sw.Close();
            fs.Close();
            return true;
        }
    }
}