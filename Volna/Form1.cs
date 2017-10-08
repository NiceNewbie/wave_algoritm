using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int[,] Map;
        int MapWidht = 20;//ширина
        int MapHeight = 20;//высота
        bool flag = true;
        public Form1()
        {
            InitializeComponent();
        }
        int[,] cMap;
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 20;
            dataGridView1.ColumnCount = 20;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                   
                    }
                }
            

            Map = new int[,]{
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,1,0,0,0,0,1,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,1,0,1,1,1,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,1,0,1,1,1,0,1,1,0,0,0,0,0,0,1,0,1},           //карта
                {1,0,0,1,0,1,1,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,0,1,1,1,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,1},  
                {1,0,0,0,1,0,1,1,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,1,1,1,1,1,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,1,0,1,1,1,0,1,1,0,0,0,0,0,0,1,0,1},           //карта
                {1,0,0,1,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,0,1,0,1,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,1},
                {1,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            };
            cMap = new int[MapHeight, MapWidht];

            for (int y = 0; y < MapHeight; y++)
            {
                dataGridView1.Columns[y].Width = 25;  //задаю ширину столбцов
                dataGridView1.Rows[y].Cells[0].Value = "";
                for (int x = 0; x < MapWidht; x++)
                    if (Map[y, x] == 1)
                        dataGridView1.Rows[y].Cells[x].Value = "+";
                    else
                        dataGridView1.Rows[y].Cells[x].Value = " ";
            }

            FindWave(Convert.ToInt16(textBox1.Text), Convert.ToInt16(textBox2.Text), Convert.ToInt16(textBox3.Text), Convert.ToInt16(textBox4.Text));//стартовая и финишная позиции

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == " ")
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Black;
                    }
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "S")
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.Red;
                    }
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "F")
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.SeaGreen;

                    }
                }
            }
        }

        public void FindWave(int startX, int startY, int targetX, int targetY)
        {
            bool add = true;
            int x, y, step = 0;
            for (y = 0; y < MapHeight; y++)
                for (x = 0; x < MapWidht; x++)
                {
                    if (Map[y, x] == 1)
                        cMap[y, x] = -2;//индикатор стены
                    else
                        cMap[y, x] = -1;//индикатор еще не ступали сюда
                }
            cMap[targetY, targetX] = 0;//Начинаем с финиша
            while (add == true)
            {
               
                for (y = 0; y < MapWidht; y++)
                    for (x = 0; x < MapHeight; x++)
                    {
                        if (cMap[x, y] == step)
                        {
                            //Ставим значение шага+1 в соседние ячейки (если они проходимы)
                            if (y - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
                                cMap[x, y - 1] = step + 1;
                            if (x - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
                                cMap[x - 1, y] = step + 1;

                            if (y + 1 < MapWidht && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
                                cMap[x, y + 1] = step + 1;
                            if (x + 1 < MapHeight && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
                                cMap[x + 1, y] = step + 1;
                        }
                    }
                step++;
                add = true;
                if (cMap[startY, startX] != -1)//решение найдено
                    add = true;
                if (step > MapWidht * MapHeight)//решение не найдено
                    add = false;
            }
            //Отрисовываем карты
            for (y = 0; y < MapHeight; y++)
            {
                for (x = 0; x < MapWidht; x++)
                    if (cMap[y, x] == -1)
                        dataGridView1.Rows[y].Cells[x].Value = "-1";
                    else
                        if (cMap[y, x] == -2)
                            dataGridView1.Rows[y].Cells[x].Value = " ";
                        else
                            if (y == startY && x == startX)
                                dataGridView1.Rows[y].Cells[x].Value = "S";
                            else
                                if (y == targetY && x == targetX)
                                    dataGridView1.Rows[y].Cells[x].Value = "F";
                                else
                                    if (cMap[y, x] > -1)
                                        dataGridView1.Rows[y].Cells[x].Value = cMap[y, x];
            }
        }
        private Point getminneighbour(Point cell)
        {
            Point[] cells = new Point[8];
            int cur = 0;
            int x = 0;
            int y = 0;
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0: //up
                        x = cell.X;
                        y = cell.Y - 1;
                        break;
                    case 1: // right
                        x = cell.X + 1;
                        y = cell.Y;
                        break;
                    case 2: // down
                        x = cell.X;
                        y = cell.Y + 1;
                        break;
                    case 3: // left
                        x = cell.X - 1;
                        y = cell.Y;
                        break;
                    case 4:
                        x = cell.X + 1;
                        y = cell.Y - 1;
                        break;
                    case 5:
                        x = cell.X + 1;
                        y = cell.Y + 1;
                        break;
                    case 6:
                        x = cell.X - 1;
                        y = cell.Y + 1;
                        break;
                    case 7:
                        x = cell.X - 1;
                        y = cell.Y - 1;
                        break;
                }
                if ((x > 0) && (y > 0) && (x < MapWidht) && (y < MapHeight) && (Map[y, x] == 0))
                    cells[cur++] = new Point(x, y);
            }
 


            Point res = cells[0];
            for (int i = 1; i < cur; i++)
            {
                if (cMap[cells[i].Y, cells[i].X] < cMap[res.Y, res.X])
                    res = cells[i];
            }
            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point cur = new Point(Convert.ToInt16(textBox1.Text), Convert.ToInt16(textBox2.Text));
            while (dataGridView1.Rows[cur.Y].Cells[cur.X].Value.ToString() != "F")
            {
                int x = cur.X;
                int y = cur.Y;
                cur = getminneighbour(cur);
                dataGridView1.Rows[y].Cells[x].Value = "o";
                dataGridView1.Rows[Convert.ToInt16(textBox1.Text)].Cells[Convert.ToInt16(textBox2.Text)].Value = "S";
                if (dataGridView1.Rows[y].Cells[x].Value.ToString() == "o")
                { dataGridView1.Rows[y].Cells[x].Style.BackColor = System.Drawing.Color.Orange; };
                
            }
           
            for (int y = 0; y < MapWidht; y++)
                for (int x = 0; x < MapHeight; x++)
                { dataGridView1.Rows[y].Cells[x].Value = " "; }
            FindWave(Convert.ToInt16(textBox1.Text), Convert.ToInt16(textBox2.Text), Convert.ToInt16(textBox3.Text), Convert.ToInt16(textBox4.Text));//стартовая и финишная позиции
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender,e);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (flag == true)
            {
                textBox2.Text = Convert.ToString(dataGridView1.CurrentCell.RowIndex);
                textBox1.Text = Convert.ToString(dataGridView1.CurrentCell.ColumnIndex);
            }
            else
            {
                textBox4.Text = Convert.ToString(dataGridView1.CurrentCell.RowIndex);
                textBox3.Text = Convert.ToString(dataGridView1.CurrentCell.ColumnIndex);
            }
            flag = !flag;
        }
    }
}