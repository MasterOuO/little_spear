using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace game
{
    public partial class Form2 : Form
    {
        string[] arr = new string[4];
        string[] arrStr = null;
        string[] arrtime = null;
        string[] stringlist = new string[100];
        string tmp = null;
        int[,] list = new int[100, 2];
        int i = 0, j, k;
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Clear();
            //Add column header
            //listView1.Columns.Add("Rank", 50);
            listView1.Columns.Add("Score", 50);
            listView1.Columns.Add("PlayTime", 70);
            listView1.Columns.Add("Date", 70);
            listView1.Columns.Add("Time", 70);

            //Add items in the listview
             
            ListViewItem itm;
            //Add first item
            string fileName = "myRank.csv";
            FileStream fsr = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fsr, Encoding.UTF8);
            //string strLine = "";
            i = 0;
            while ((stringlist[i] = sr.ReadLine()) != null) i++;               
            for(j=0;j< i-1;j++)
            {
                arrStr = stringlist[j].Split(',');
                list[j, 0] = Int32.Parse(arrStr[0]);

                arrtime = arrStr[1].Split(':');
                list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                for (k = j+1; k < i; k++)
                {
                    arrStr = stringlist[k].Split(',');
                    list[k, 0] = Int32.Parse(arrStr[0]);
                    arrtime = arrStr[1].Split(':');
                    list[k, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                    if (list[j,0] < list[k, 0])
                    {
                        tmp = stringlist[k];
                        stringlist[k] = stringlist[j];
                        stringlist[j] = tmp;

                        arrStr = stringlist[j].Split(',');
                        list[j, 0] = Int32.Parse(arrStr[0]);
                        arrtime = arrStr[1].Split(':');
                        list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                    }
                    //分數一樣看時間
                    if (list[j, 0] == list[k, 0])
                    {
                        if (list[j, 1] > list[k, 1])
                        {
                            tmp = stringlist[k];
                            stringlist[k] = stringlist[j];
                            stringlist[j] = tmp;

                            arrStr = stringlist[j].Split(',');
                            list[j, 0] = Int32.Parse(arrStr[0]);
                            arrtime = arrStr[1].Split(':');
                            list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                        }
                    }
                }
            }
            for( j = 0; j < i; j++)
            {
                arrStr = stringlist[j].Split(',');
                itm = new ListViewItem(arrStr);
                listView1.Items.Add(itm);
            }
            sr.Close();
            fsr.Close();
        }

        private void enter(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            Hide();
            f.Show();
        }

        private void Hard_click(object sender, EventArgs e)
        {
            listView1.Clear();
            //Add column header
            //listView1.Columns.Add("Rank", 50);
            listView1.Columns.Add("Score", 50);
            listView1.Columns.Add("PlayTime", 70);
            listView1.Columns.Add("Date", 70);
            listView1.Columns.Add("Time", 70);

            //Add items in the listview
            string[] arr = new string[4];
            ListViewItem itm;
            string fileName = "HmyRank.csv";
            FileStream fsr = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fsr, Encoding.UTF8);
            i = 0;
            while ((stringlist[i] = sr.ReadLine()) != null) i++;
            for (j = 0; j < i - 1; j++)
            {
                arrStr = stringlist[j].Split(',');
                list[j, 0] = Int32.Parse(arrStr[0]);

                arrtime = arrStr[1].Split(':');
                list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                for (k = j + 1; k < i; k++)
                {
                    arrStr = stringlist[k].Split(',');
                    list[k, 0] = Int32.Parse(arrStr[0]);
                    arrtime = arrStr[1].Split(':');
                    list[k, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                    if (list[j, 0] < list[k, 0])
                    {
                        tmp = stringlist[k];
                        stringlist[k] = stringlist[j];
                        stringlist[j] = tmp;

                        arrStr = stringlist[j].Split(',');
                        list[j, 0] = Int32.Parse(arrStr[0]);
                        arrtime = arrStr[1].Split(':');
                        list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                    }
                    //分數一樣看時間
                    if (list[j, 0] == list[k, 0])
                    {
                        if (list[j, 1] > list[k, 1])
                        {
                            tmp = stringlist[k];
                            stringlist[k] = stringlist[j];
                            stringlist[j] = tmp;

                            arrStr = stringlist[j].Split(',');
                            list[j, 0] = Int32.Parse(arrStr[0]);
                            arrtime = arrStr[1].Split(':');
                            list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                        }
                    }
                }
            }
            for (j = 0; j < i; j++)
            {
                arrStr = stringlist[j].Split(',');
                itm = new ListViewItem(arrStr);
                listView1.Items.Add(itm);
            }
            sr.Close();
            fsr.Close();
        }

        private void Normal_click(object sender, EventArgs e)
        {
            listView1.Clear();
            
            listView1.Columns.Add("Score", 50);
            listView1.Columns.Add("PlayTime", 70);
            listView1.Columns.Add("Date", 70);
            listView1.Columns.Add("Time", 70);  
            ListViewItem itm;
            string fileName = "NmyRank.csv";
            FileStream fsr = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fsr, Encoding.UTF8);
            i = 0;
            while ((stringlist[i] = sr.ReadLine()) != null) i++;
            for (j = 0; j < i - 1; j++)
            {
                arrStr = stringlist[j].Split(',');
                list[j, 0] = Int32.Parse(arrStr[0]);

                arrtime = arrStr[1].Split(':');
                list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                for (k = j + 1; k < i; k++)
                {
                    arrStr = stringlist[k].Split(',');
                    list[k, 0] = Int32.Parse(arrStr[0]);
                    arrtime = arrStr[1].Split(':');
                    list[k, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                    if (list[j, 0] < list[k, 0])
                    {
                        tmp = stringlist[k];
                        stringlist[k] = stringlist[j];
                        stringlist[j] = tmp;

                        arrStr = stringlist[j].Split(',');
                        list[j, 0] = Int32.Parse(arrStr[0]);
                        arrtime = arrStr[1].Split(':');
                        list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                    }
                    //分數一樣看時間
                    if (list[j, 0] == list[k, 0])
                    {
                        if (list[j, 1] > list[k, 1])
                        {
                            tmp = stringlist[k];
                            stringlist[k] = stringlist[j];
                            stringlist[j] = tmp;

                            arrStr = stringlist[j].Split(',');
                            list[j, 0] = Int32.Parse(arrStr[0]);
                            arrtime = arrStr[1].Split(':');
                            list[j, 1] = Int32.Parse(arrtime[0]) * 60 + Int32.Parse(arrtime[1]);
                        }
                    }
                }
            }
            for (j = 0; j < i; j++)
            {
                arrStr = stringlist[j].Split(',');
                itm = new ListViewItem(arrStr);
                listView1.Items.Add(itm);
            }
            sr.Close();
            fsr.Close();
        }
    }
}