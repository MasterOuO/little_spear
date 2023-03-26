using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace game
{
    public partial class end : Form
    {
        public Form3 f1 = null;
        public Normal1 f2 = null;
         public Hard f3 = null;
        public end()
        {
            InitializeComponent();
        }

        private void end_Load(object sender, EventArgs e)
        {
            String rank,time;
            string fileName = "myRank.csv";
            int h=2, m=0, sum=0;
            //label1.Text = "通關時間:" + (2 - f1.h).ToString() + ":";
            if (f1!=null)
            {
                h = f1.h;
                m = f1.m;
                sum = f1.sum;
                fileName = "myRank.csv";
            }
            if (f2!= null)
            {
                h = f2.h;
                m = f2.m;
                sum = f2.sum;
                fileName = "NmyRank.csv";
            }
            if (f3!=null)
            {
                h = f3.h;
                m = f3.m;
                sum = f3.sum;
                fileName = "HmyRank.csv";
            }
            time = (2 - h).ToString() + ":";
            if (m>50)
            {
                if (m == 60)
                {
                    m = 0;
                    h--;
                }
                time += "0" + (60 - m).ToString();
                //label1.Text += "0"+(60 - f1.m).ToString();
                
            }
            else
            {
                time += (60 - m).ToString();
                //label1.Text += (60 - f1.m).ToString();
            }
            rank = sum.ToString();

            label1.Text = "遊玩時間:" + time;
            label2.Text = "最終分數:"+ rank;

            string[] arr = new string[4];
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyy-MM-dd");
            string myTimeString = myDate.ToString("HH:mm:ss");
            arr[0] = rank;
            arr[1] = time;
            arr[2] = myDateString;
            arr[3] = myTimeString;
            
            try
            {
                FileInfo fi = new FileInfo(fileName);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fsw = new FileStream(fileName, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fsw, System.Text.Encoding.UTF8);
                string csvData = arr[0] + "," + arr[1] + "," + arr[2] + "," + arr[3];
                //sw.Write("\n");
                sw.WriteLine(csvData);
                sw.Close();
                fsw.Close();
            }
            catch
            {
            }
        }

        private void enter(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            Hide();
            f.Show();
        }
    }
}
