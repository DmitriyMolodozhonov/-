using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Полет_2._1
{
    public partial class Form1 : Form
    {
        System.Drawing.Bitmap sky, plane, gold, skynight, bum, plane1, plane0;
        Graphics g; //рабочая графическая область
        int speed = 1; // скорость полета
        Rectangle rct; //область в которой находится самолет
        Rectangle rctgold;
        Rectangle rctplane1;
       // Rectangle rctplane0;
        int money = 0;

        private void button3_Click(object sender, EventArgs e) //начать игру
        {
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            money = -1;
            button3.Visible = false;
            speed = 1;
            rctplane1.X = 680 + rnd.Next(this.ClientSize.Width - 10 - plane1.Width); //определение координат самолета
            rctplane1.Y = 58 + rnd.Next(this.ClientSize.Height - 90 - plane1.Height); // в рандомном месте
            label1.Text = money.ToString();
        }

        Boolean demo = true; // true - самолет скрывается в облаках


        System.Random rnd;



        public Form1()
        {
            InitializeComponent();
            KeyPreview = true; // клавиши
            KeyDown += (s, e) =>
            {
                if (e.KeyValue == (char)Keys.W)
                {
                    rct.Y += -10;
                    g.DrawImage(plane0, rct.X, rct.Y);
                }
                else if (e.KeyValue == (char)Keys.S)
                {
                    rct.Y += +10;
                }
            };
            try
            {

                sky = new Bitmap(Полет_2._1.Properties.Resources.sky); // небо
                bum = new Bitmap(Полет_2._1.Properties.Resources.bum); // авария
                skynight = new Bitmap(Полет_2._1.Properties.Resources.night); // ночь
                plane = new Bitmap(Полет_2._1.Properties.Resources.plane); // самолет
                gold = new Bitmap(Полет_2._1.Properties.Resources.gold); // монета
                plane1 = new Bitmap(Полет_2._1.Properties.Resources.plane1); // самолет2
                plane0 = new Bitmap(Полет_2._1.Properties.Resources.plane0); // вверх с

                this.BackgroundImage = new Bitmap(Полет_2._1.Properties.Resources.sky);


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Полет", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // закрыть приложение
                return;
            }
            //сделать прозрачный фон во круг объектов
            plane.MakeTransparent();
            gold.MakeTransparent();
            plane1.MakeTransparent();
            plane0.MakeTransparent();
            //задать размер формы в зависимости от картинки
            this.ClientSize = new System.Drawing.Size(new Point(BackgroundImage.Width, BackgroundImage.Height));
            //задать вид границы окна
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            g = Graphics.FromImage(BackgroundImage);
            rnd = new System.Random();
            //-----------------------------------
            rct.X = -40;
            rct.Y = 170 + rnd.Next(20);
            //-----------------------------------
            rctplane1.X = 680;
            rctplane1.Y = 150 + rnd.Next(30);
            //-----------------------------------
            rctgold.X = -40;
            rctgold.Y = 170 + rnd.Next(20);
            //-----------------------------------
            rct.Width = plane.Width;
            rct.Height = plane.Height;
            //-----------------------------------
            rctgold.Width = gold.Width;
            rctgold.Height = gold.Height;
            //-----------------------------------
            rctplane1.Width = plane1.Width;
            rctplane1.Height = plane1.Height;
            //-----------------------------------
            timer2.Interval = 20;
            timer2.Enabled = true;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            g.DrawImage(sky, new Point(0, 0));

            if (rct.X < this.ClientRectangle.Width)
            {
                rct.X += speed;
            }

            else
            {
                rct.X = -40;
                rct.Y = 20 + rnd.Next(this.ClientSize.Height - 40 - plane.Height);
            }
            if (rctplane1.X < this.ClientRectangle.Width)
            {
                rctplane1.X -= speed;
            }

            else
            {
                rctplane1.X = 680;
                rctplane1.Y = 60 + rnd.Next(this.ClientSize.Width - 20 - plane1.Height);
            }
            //----------------столкновение--------------------------------------------
            if (rct.IntersectsWith(rctgold)) //столкновение с монетой
            {
                money++;
                speed++;
                label1.Text = money.ToString();
                    rctgold.X = 30 + rnd.Next(this.ClientSize.Width - 10 - gold.Width); //определение координат монеты
                    rctgold.Y = 58 + rnd.Next(this.ClientSize.Height - 90 - gold.Height); // в рандомном месте
                rctplane1.X = 680 + rnd.Next(this.ClientSize.Width - 10 - plane1.Width); //определение координат самолета
                rctplane1.Y = 58 + rnd.Next(this.ClientSize.Height - 90 - plane1.Height); // в рандомном месте
            }
            if (rct.IntersectsWith(rctplane1))//столкновение с самолетом
            {
                this.timer2.Tick -= new System.EventHandler(this.timer2_Tick);
                g.DrawImage(bum, new Point(0, 0));
                button3.Visible = true;
            }
            //----------------------------------------------------------------------------
            if ((money > 10)) //смена фона
            {
                g.DrawImage(skynight, new Point(0, 0));
            }
            //рисовка объекта
            g.DrawImage(plane, rct.X, rct.Y);
            g.DrawImage(plane1, rctplane1.X, rctplane1.Y);
            g.DrawImage(gold, rctgold.X, rctgold.Y);
            if (!demo) //обновить область формы в которой находится рисунок
            {
                this.Invalidate(rct);
                this.Invalidate(rctplane1);
            }
            else
            {
                Rectangle reg = new Rectangle(0, 0, sky.Width - 0, sky.Height - 0);
                // g.DrawRectangle(Pens.Black, reg.X, reg.Y, reg.Width - 1, reg.Height - 1);

                this.Invalidate(reg); // обновить область
            }

        }
    }
}