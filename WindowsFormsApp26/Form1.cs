using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp26
{
    public partial class Form1 : Form
    {
        MyTree<int> myfirstTree = new MyTree<int>();
        List<MyEllipse> myfirstEllipse = new List<MyEllipse>();
        Graphics g;
        int panelWidth;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            label4.Text = "0";
            label5.Text = "0";
            panelWidth = panel1.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int out1))
            {
                if (myfirstTree.Find(out1) == null)
                {
                    if (myfirstTree.GetNodes() == 0)
                    {
                        myfirstTree.Add(out1);
                        myfirstEllipse.Add(new MyEllipse(panelWidth / 2, 10, 30, out1));
                    }
                    else
                    {
                        myfirstTree.Add(out1);
                        int r = FindIndex(myfirstTree.FindRoot(out1).Value);
                        if (myfirstTree.FindRoot(out1).Right != null && myfirstTree.FindRoot(out1).Right.Value == out1)
                            myfirstEllipse.Add(new MyEllipse(myfirstEllipse[r].StartX + panelWidth / GetLevel(myfirstEllipse[r].StartY), myfirstEllipse[r].StartY + 50, 30, out1));
                        else
                            myfirstEllipse.Add(new MyEllipse(myfirstEllipse[r].StartX - panelWidth / GetLevel(myfirstEllipse[r].StartY), myfirstEllipse[r].StartY + 50, 30, out1));
                    }
                }
                else
                {
                    MessageBox.Show(
                                    "Значение уже есть в дереве.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(
                                    "Проверьте введённое значение.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
            DrawEllipse();
            DrawLines();
            GetLeafandDeep();
        }
        private void DrawEllipse()
        {
            g.Clear(Color.White);
            foreach (var item in myfirstEllipse)
            {
                item.Draw(panel1);
            }
        }
        private int GetLevel(int a)
        {
            switch (a){
                case 10:
                    return 4;
                case 60:
                    return 8;
                case 110:
                    return 16;
                case 160:
                    return 32;
                case 210:
                    return 64;
                case 260:
                    return 128;
                default:
                    return 128;
            }
        }
        private int FindIndex(int z)
        {
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstEllipse[i].Znach == z)
                    return i;
            }
            return -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (myfirstTree.RootNode != null)
            {
                richTextBox1.Text += "LNR" + myfirstTree.LNR() + "\r\n";
            }
            else
            {
                MessageBox.Show(
                                    "Дерево - пусто.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (myfirstTree.RootNode != null)
            {
                richTextBox1.Text += "LRN" + myfirstTree.LRN() + "\r\n";
            }
            else
            {
                MessageBox.Show(
                                    "Дерево - пусто.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (myfirstTree.RootNode != null)
            {
                richTextBox1.Text += "NLR" + myfirstTree.NLR() + "\r\n";
            }
            else
            {
                MessageBox.Show(
                                    "Дерево - пусто.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
        }
        private void GetLeafandDeep()
        {
            label4.Text = Convert.ToString(myfirstTree.GetDeep());
            label5.Text = Convert.ToString(myfirstTree.GetLeafs());
        }

        private void DrawLines()
        {
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstEllipse[i].Znach != myfirstTree.Root)
                {
                    int r = FindIndex(myfirstTree.FindRoot(myfirstEllipse[i].Znach).Value);
                    g.DrawLine(new Pen(Color.Black), myfirstEllipse[r].StartX + 10, myfirstEllipse[r].StartY + 10, myfirstEllipse[i].StartX + 10, myfirstEllipse[i].StartY + 10);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(myfirstTree.RootNode != null && int.TryParse(textBox1.Text, out int out1))
            {
                if(myfirstTree.Find(out1) != null)
                {
                    myfirstEllipse.RemoveAt(FindIndex(out1));
                    //myfirstTree.Remove(out1);
                    myfirstTree.RemoveTree(out1);
                }
                else
                {
                    MessageBox.Show(
                                    "Элемент не найден.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show(
                                    "Проверьте введённое значение и кол-во элементов дерева.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
            ChangeRoot();
            DrawEllipse();
            DrawLines();
            GetLeafandDeep();
        }
        private void ChangeRoot()
        {
            for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if (myfirstTree.Root == myfirstEllipse[i].Znach)
                {
                    myfirstEllipse[i].StartX = panelWidth / 2;
                    myfirstEllipse[i].StartY = 10;
                    myfirstEllipse[i].Rerect();
                }
            }
                for (int i = 0; i < myfirstEllipse.Count; i++)
            {
                if(myfirstTree.Root != myfirstEllipse[i].Znach)
                {
                    int r = FindIndex(myfirstTree.FindRoot(myfirstEllipse[i].Znach).Value);
                    if (myfirstTree.FindRoot(myfirstEllipse[i].Znach).Right != null && myfirstTree.FindRoot(myfirstEllipse[i].Znach).Right.Value == myfirstEllipse[i].Znach)
                        myfirstEllipse[i].StartX = myfirstEllipse[r].StartX + panelWidth / GetLevel(myfirstEllipse[r].StartY);
                    else
                        myfirstEllipse[i].StartX = myfirstEllipse[r].StartX - panelWidth / GetLevel(myfirstEllipse[r].StartY);
                    myfirstEllipse[i].StartY = myfirstEllipse[r].StartY + 50;
                    myfirstEllipse[i].Rerect();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (myfirstTree.RootNode != null && int.TryParse(textBox1.Text, out int out1) && myfirstTree.Find(out1) != null)
            {
                richTextBox1.Text += "Поиск: " + out1 + " Родитель: " + myfirstTree.FindRoot(myfirstTree.Find(out1).Value).Value + "\r\n";
            }
            else
            {
                MessageBox.Show(
                                    "Проверьте введённое значение и кол-во элементов дерева.\r\n",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
