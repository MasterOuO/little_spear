using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            Hide();
            f.FormClosing += secondForm_FormClosing;
            f.Show();

            //Strin
        }
        private void easy(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            Hide();
            f.FormClosing += secondForm_FormClosing;
            f.Show();
        }

        private void normal(object sender, EventArgs e)
        {
            Normal1 f = new Normal1();
            Hide();
            f.FormClosing += secondForm_FormClosing;
            f.Show();
        }

        private void hard(object sender, EventArgs e)
        {
            Hard f = new Hard();
            Hide();
            f.FormClosing += secondForm_FormClosing;
            f.Show();
        }
        private void secondForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Show();
        }
    }
}
