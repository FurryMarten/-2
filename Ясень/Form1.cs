using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ясень
{
    public partial class Form1 : Form
    {
        Tree<string> Tr;
        Graphics gr;
        public Form1()
        {
            InitializeComponent();
            Tr = new Tree<string>();
            gr=panel1.CreateGraphics();
            textBox2.Text = "5";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tr.Add(textBox1.Text, Convert.ToInt32(textBox2.Text));
            Tr.Draw(gr, panel1.Width, panel1.Height, 50);
            textBox2.Text = Convert.ToString(Convert.ToInt32(textBox2.Text)+1);
            label4.Text = "Центрированный: " + Tr.lnr();
            label5.Text = "Прямой: " + Tr.nlr();
            label6.Text = "Обратный: " + Tr.lrn();
            label7.Text = "Высота: " + Tr.Deep();
            label10.Text = "Листья: " + Tr.List();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tr.Delete(Convert.ToInt32(textBox3.Text), Tr.root, "a");
            Tr.Draw(gr, panel1.Width, panel1.Height, 50);
            label4.Text = "Центрированный: " + Tr.lnr();
            label5.Text = "Прямой: " + Tr.nlr();
            label6.Text = "Обратный: " + Tr.lrn();
            label7.Text = "Высота: " + Tr.Deep();
            label10.Text = "Листья: " + Tr.List();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Text = "Значение: "+ Tr.Search(Convert.ToInt32(textBox4.Text)).ToString();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
