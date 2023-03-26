﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace game
{

    public partial class Normal1: Form
    {
        Player player;
        Ball[] myballs;
        List<Ball> myThreadedBalls;
        Enemy[] enemys;
        List<Enemy> myThreadedEnemys;
        FastEnemy[] fenemys;
        List<FastEnemy> myThreadedFastEnemys;
        //目前子彈
        int i = 0;
        //一般怪物數量
        int j = 0;
        //快速怪物
        int l = 0;
        int k,eci;
        int dight = 0;
        int Invincible = 20, changebullt = 25, shooti = 2, fspeed=2;
        int look = 2;
        int finish = 0;
        bool islookup, islookdown;
        bool fin = true;
        bool up = false;
        bool left = false;
        bool right = false;
        bool down = false;
        public int h = 3, m = 0, n = 3000, sum = 0;
        public bool have = false;
        //Color[] color = { Color.Red, Color.Blue, Color.Black, Color.Aqua, Color.Chocolate };
        //private object formGraphics;
        public Normal1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        private void frmBouncingBall_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 0, 40, 600, 40);
            e.Graphics.FillRectangle(Brushes.LightBlue, 0, 0, 600, 40);
            e.Graphics.DrawRectangle(new Pen(Color.Black), player.x, player.y, player.size, player.size);
            e.Graphics.FillRectangle(Brushes.Blue, player.hx, player.hy, player.hsize, player.hsize);
            finish = 0;
            eci = 0;
            //怪物移動
            foreach (Enemy enemy in myThreadedEnemys)
            {
                if (enemy.have)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.Red), enemy.x, enemy.y, enemy.size, enemy.size);
                    e.Graphics.FillRectangle(Brushes.Red, enemy.hx, enemy.hy, enemy.hsize, enemy.hsize);
                    //怪物往玩家移動
                    if (enemy.y < player.y)
                    {
                        if (enemy.y <= (player.y + player.size) && player.y <= enemy.y + enemy.size && (enemy.x - player.size) <= player.x && player.x <= enemy.x + enemy.size)
                            enemy.yspeed = 0;
                        else
                            enemy.yspeed = 1;
                        //對準後不動
                        if (enemy.x == player.x) enemy.xspeed = 0;

                    }
                    if (enemy.y > player.y)
                    {

                        if (enemy.y <= (player.y + player.size) && player.y <= enemy.y + enemy.size && (enemy.x - player.size) <= player.x && player.x <= enemy.x + enemy.size)
                            enemy.yspeed = 0;
                        else
                            enemy.yspeed = -1;
                        if (enemy.x == player.x) enemy.xspeed = 0;

                    }
                    if (enemy.x < player.x)
                    {
                        if (enemy.y <= (player.y + player.size) && player.y <= enemy.y + enemy.size && (enemy.x - player.size) <= player.x && player.x <= enemy.x + enemy.size)
                            enemy.xspeed = 0;
                        else
                            enemy.xspeed = 1;
                        if (enemy.y == player.y) enemy.yspeed = 0;

                    }
                    if (enemy.x > player.x)
                    {
                        if (enemy.y <= (player.y + player.size) && player.y <= enemy.y + enemy.size && (enemy.x - player.size) <= player.x && player.x <= enemy.x + enemy.size)
                            enemy.xspeed = 0;
                        else
                            enemy.xspeed = -1;
                        if (enemy.y == player.y) enemy.yspeed = 0;
                    }
                    //怪物撞玩家
                    if (enemy.y <= (player.y + player.size) && player.y <= enemy.y + enemy.size && (enemy.x - player.size) <= player.x && player.x <= enemy.x + enemy.size)
                    {
                        if (Invincible == 20)
                        {
                            //look = 0;
                            player.heath--;
                            islookup = false;
                            islookdown = false;
                            //被打方向
                            //怪上玩家下
                            if (enemy.y < player.y)
                            {
                                if (player.y + player.size + 10 < ClientSize.Height) player.y += 10;
                                else player.y = ClientSize.Height - player.size;
                                //看上
                                look = 0;
                                islookup = true;
                                player.hy = player.y + 3;
                            }
                            //怪下玩家上
                            if (enemy.y > player.y)
                            {
                                if (player.y - 10 > 40) player.y -= 10;
                                else player.y = 41;
                                //看下
                                look = 4;
                                islookdown = true;
                                player.hy = player.y + 7;
                            }
                            //怪左玩家右
                            if (enemy.x < player.x)
                            {
                                if (player.x + player.size + 10 < ClientSize.Width) player.x += 10;
                                else player.x = ClientSize.Width - player.size;
                                //看左
                                look += 6;
                                //看左下
                                if (islookdown) look = 5;
                                //看左上
                                if (islookup) look = 7;
                                player.hx = player.x + 3;
                            }
                            if (enemy.x > player.x)
                            {
                                if (player.x - 10 > 0) player.x -= 10;
                                else player.x = 0;
                                //看右
                                look += 2;
                                //看右下
                                if (islookdown) look = 3;
                                //右上
                                if (islookup) look = 1;
                                player.hx = player.x + 7;
                            }
                            if (player.heath == 0)
                            {
                                end();
                            }
                            Invincible = 0;
                            hp.Text = player.heath.ToString();
                        }
                    }
                    
                    //怪物撞怪物
                    for (k = eci; k >= 0; k--)
                    {
                        if (enemys[k].have)
                        {
                            if (enemy.y <= (enemys[k].y + enemys[k].size) && enemys[k].y <= enemy.y + enemy.size && (enemy.x - enemys[k].size) <= enemys[k].x && enemys[k].x <= enemy.x + enemy.size)
                            {
                                if (enemy.y < enemys[k].y)
                                {
                                    enemy.xspeed = 0;
                                    //對準後不動
                                    //if (enemy.x == enemys[k].x) enemy.xspeed = 0;
                                }
                                if (enemy.y > enemys[k].y)
                                {
                                    enemy.xspeed = 0;
                                    //if (enemy.x == enemys[k].x) enemy.xspeed = 0;
                                }
                                if (enemy.x < enemys[k].x)
                                {
                                    enemy.yspeed = 0;
                                    //if (enemy.y == enemys[k].y) enemy.yspeed = 0;
                                }
                                if (enemy.x > enemys[k].x)
                                {
                                    enemy.yspeed = 0;
                                    // if (enemy.y == enemys[k].y) enemy.yspeed = 0;
                                }

                            }
                        }
                    }
                    //怪物撞快速怪物
                    for (k = 0; k < l; k++)
                    {
                        if (fenemys[k].have)
                        {
                            if (enemy.y <= (fenemys[k].y + fenemys[k].size) && fenemys[k].y <= enemy.y + enemy.size && (enemy.x - fenemys[k].size) <= fenemys[k].x && fenemys[k].x <= enemy.x + enemy.size)
                            {
                                if (enemy.y < fenemys[k].y)
                                {
                                    enemy.xspeed = 0;
                                    //對準後不動
                                    //if (enemy.x == enemys[k].x) enemy.xspeed = 0;
                                }
                                if (enemy.y > fenemys[k].y)
                                {
                                    enemy.xspeed = 0;
                                    //if (enemy.x == enemys[k].x) enemy.xspeed = 0;
                                }
                                if (enemy.x < fenemys[k].x)
                                {
                                    enemy.yspeed = 0;
                                    //if (enemy.y == enemys[k].y) enemy.yspeed = 0;
                                }
                                if (enemy.x > fenemys[k].x)
                                {
                                    enemy.yspeed = 0;
                                    // if (enemy.y == enemys[k].y) enemy.yspeed = 0;
                                }

                            }

                        }
                    }
                    finish++;
                    eci++;
                }
            }
            eci = 0;
            foreach (FastEnemy fenemy in myThreadedFastEnemys)
            {
                if (fenemy.have)
                {
                    //e.Graphics.DrawRectangle(new Pen(Color.Green), fenemy.x, fenemy.y, fenemy.size, fenemy.size);
                    //e.Graphics.FillRectangle(Brushes.Green, fenemy.hx, fenemy.hy, fenemy.hsize, fenemy.hsize);
                    //怪物撞玩家
                    if (fenemy.y <= (player.y + player.size) && player.y <= fenemy.y + fenemy.size && (fenemy.x - player.size) <= player.x && player.x <= fenemy.x + fenemy.size)
                    {
                        if (Invincible == 20)
                        {
                            //look = 0;
                            player.heath--;
                            islookup = false;
                            islookdown = false;
                            //被打方向
                            //怪上玩家下
                            if (fenemy.y < player.y)
                            {
                                if (player.y + player.size + 10 < ClientSize.Height) player.y += 10;
                                else player.y = ClientSize.Height - player.size;
                                //看上
                                look = 0;
                                islookup = true;
                                player.hy = player.y + 3;
                            }
                            //怪下玩家上
                            if (fenemy.y > player.y)
                            {
                                if (player.y - 10 > 40) player.y -= 10;
                                else player.y = 41;
                                //看下
                                look = 4;
                                islookdown = true;
                                player.hy = player.y + 7;
                            }
                            //怪左玩家右
                            if (fenemy.x < player.x)
                            {
                                if (player.x + player.size + 10 < ClientSize.Width) player.x += 10;
                                else player.x = ClientSize.Width - player.size;
                                //看左
                                look += 6;
                                //看左下
                                if (islookdown) look = 5;
                                //看左上
                                if (islookup) look = 7;
                                player.hx = player.x + 3;
                            }
                            if (fenemy.x > player.x)
                            {
                                if (player.x - 10 > 0) player.x -= 10;
                                else player.x = 0;
                                //看右
                                look += 2;
                                //看右下
                                if (islookdown) look = 3;
                                //右上
                                if (islookup) look = 1;
                                player.hx = player.x + 7;
                            }
                            if (player.heath == 0)
                            {
                                end();
                            }
                            Invincible = 0;
                            hp.Text = player.heath.ToString();
                        }
                    }
                    //怪物往玩家移動
                    //衝刺冷卻到 開始計時衝刺時間
                    if (fenemy.fasti == 70)
                    {
                        fenemy.fasttime=40;
                        fenemy.fasti = 0;
                    }
                    //衝刺時間
                    if (fenemy.fasttime > 0)
                    {
                        fspeed = 4;
                        e.Graphics.DrawRectangle(new Pen(Color.Lime), fenemy.x, fenemy.y, fenemy.size, fenemy.size);
                        e.Graphics.FillRectangle(Brushes.Lime, fenemy.hx, fenemy.hy, fenemy.hsize, fenemy.hsize);
                    }
                    else
                    {
                        fspeed = 1;
                        e.Graphics.DrawRectangle(new Pen(Color.Aqua), fenemy.x, fenemy.y, fenemy.size, fenemy.size);
                        e.Graphics.FillRectangle(Brushes.Aqua, fenemy.hx, fenemy.hy, fenemy.hsize, fenemy.hsize);
                    }
                    if (fenemy.y < player.y)
                    {
                        if (fenemy.y <= (player.y + player.size) && player.y <= fenemy.y + fenemy.size && (fenemy.x - player.size) <= player.x && player.x <= fenemy.x + fenemy.size)
                            fenemy.yspeed = 0;
                        else
                            fenemy.yspeed = fspeed;
                        //對準後不動
                        if (fenemy.x == player.x) fenemy.xspeed = 0;
                        
                    }
                    if (fenemy.y > player.y)
                    {

                        if (fenemy.y <= (player.y + player.size) && player.y <= fenemy.y + fenemy.size && (fenemy.x - player.size) <= player.x && player.x <= fenemy.x + fenemy.size)
                            fenemy.yspeed = 0;
                        else
                            fenemy.yspeed = -fspeed;
                        if (fenemy.x == player.x) fenemy.xspeed = 0;
                        //fenemy.hy = fenemy.y + 1;
                    }
                    if (fenemy.x < player.x)
                    {
                        if (fenemy.y <= (player.y + player.size) && player.y <= fenemy.y + fenemy.size && (fenemy.x - player.size) <= player.x && player.x <= fenemy.x + fenemy.size)
                            fenemy.xspeed = 0;
                        else
                            fenemy.xspeed = fspeed;
                        if (fenemy.y == player.y) fenemy.yspeed = 0;
                        //fenemy.hx = fenemy.x + 9;
                    }
                    if (fenemy.x > player.x)
                    {
                        if (fenemy.y <= (player.y + player.size) && player.y <= fenemy.y + fenemy.size && (fenemy.x - player.size) <= player.x && player.x <= fenemy.x + fenemy.size)
                            fenemy.xspeed = 0;
                        else
                            fenemy.xspeed = -fspeed;
                        if (fenemy.y == player.y) fenemy.yspeed = 0;
                        //fenemy.hx = fenemy.x + 1;
                    }
                    for (k = eci; k >= 0; k--)
                    {
                        if (fenemys[k].have)
                        {
                            if (fenemy.y <= (fenemys[k].y + fenemys[k].size) && fenemys[k].y <= fenemy.y + fenemy.size && (fenemy.x - fenemys[k].size) <= fenemys[k].x && fenemys[k].x <= fenemy.x + fenemy.size)
                            {
                                if (fenemy.y < fenemys[k].y)
                                {
                                    fenemy.xspeed = 0;
                                    //對準後不動
                                    //if (enemy.x == enemys[k].x) enemy.xspeed = 0;
                                }
                                if (fenemy.y > fenemys[k].y)
                                {
                                    fenemy.xspeed = 0;
                                    //if (enemy.x == enemys[k].x) enemy.xspeed = 0;
                                }
                                if (fenemy.x < fenemys[k].x)
                                {
                                    fenemy.yspeed = 0;
                                    //if (enemy.y == enemys[k].y) enemy.yspeed = 0;
                                }
                                if (fenemy.x > fenemys[k].x)
                                {
                                    fenemy.yspeed = 0;
                                    // if (enemy.y == enemys[k].y) enemy.yspeed = 0;
                                }

                            }
                        }
                    }
                    finish++;
                    eci++;
                }
            }
            label3.Text = finish.ToString() + "/" + dight;
            if (finish == 0) fin = true;
            else fin = false;
            foreach (Ball myBall in myThreadedBalls)
            {
                if (myBall.have)
                {
                    e.Graphics.FillEllipse(Brushes.Black, myBall.x, myBall.y, myBall.radius, myBall.radius);
                    for (k = 0; k < j; k++)
                    {
                         if (enemys[k].have && myBall.have)
                        {
                            //碰撞怪物的前後左右
                            if (enemys[k].y <= (myBall.y + myBall.radius) && myBall.y <= enemys[k].y + enemys[k].size && (enemys[k].x - myBall.radius) <= myBall.x && myBall.x <= enemys[k].x + enemys[k].size)
                            {
                                enemys[k].heath -= 1;
                                if (enemys[k].heath == 0)
                                {
                                    enemys[k].have = false;
                                    sum += 10;
                                    label4.Text = sum.ToString();
                                }
                                myBall.have = false;
                            }
                        }  
                    }
                    for (k = 0; k < l; k++)
                    {
                        if (fenemys[k].have && myBall.have)
                        {
                            //碰撞快速怪物的前後左右
                            if (fenemys[k].y <= (myBall.y + myBall.radius) && myBall.y <= fenemys[k].y + fenemys[k].size && (fenemys[k].x - myBall.radius) <= myBall.x && myBall.x <= fenemys[k].x + fenemys[k].size)
                            {
                                fenemys[k].heath -= 1;
                                if (fenemys[k].heath == 0)
                                {
                                    fenemys[k].have = false;
                                    sum += 10;
                                    label4.Text = sum.ToString();
                                }
                                myBall.have = false;
                            }
                        }
                    }
                }
            }
        }
        //結束
        private void end()
        {
            timer1.Enabled = false;
            end f = new end();
            f.f2 = this;
            Hide();
            f.Show();
        }
        private void timer_tick(object sender, EventArgs e)
        {
            if (fin)
            {
                //幾關卡
                if (dight == 7)
                {
                    end();
                }
                Random random = new Random();
                //召喚怪物 數量
                for (j = 0; j <= dight; j++)
                {
                    //第6關卡
                    enemys[j] = new Enemy(this);
                    //生成位置  上右下左 0-3
                    int r = random.Next(0, 4);
                    if (r == 0)
                    {
                        enemys[j].x = random.Next(ClientSize.Width / 5, ClientSize.Width / 5 * 4 - enemys[j].size);
                        enemys[j].y = random.Next(41, ClientSize.Height / 5 - enemys[j].size);
                    }
                    else if (r == 1)
                    {
                        enemys[j].x = random.Next(ClientSize.Width / 5 * 4, ClientSize.Width - enemys[j].size);
                        enemys[j].y = random.Next(ClientSize.Height / 5, ClientSize.Height / 5 * 4 - enemys[j].size);
                    }
                    else if (r == 2)
                    {
                        enemys[j].x = random.Next(ClientSize.Width / 5, ClientSize.Width / 5 * 4 - enemys[j].size);
                        enemys[j].y = random.Next(ClientSize.Height / 5 * 4, ClientSize.Height - enemys[j].size);
                    }
                    else
                    {
                        enemys[j].x = random.Next(0, ClientSize.Width / 5 - enemys[j].size);
                        enemys[j].y = random.Next(ClientSize.Height / 5, ClientSize.Height / 5 * 4 - enemys[j].size);
                    }
                    enemys[j].hx = enemys[j].x + 7;
                    enemys[j].hy = enemys[j].y + 5;
                    myThreadedEnemys.Add(enemys[j]);
                    Thread tid2 = new Thread(new ThreadStart(enemys[j].move));
                    tid2.Start();
                }
                if (dight == 5|| dight == 2)
                {
                    l = 0;
                    fenemys[l] = new FastEnemy(this);
                    //生成位置  上右下左 0-3
                    int r = random.Next(0, 4);
                    if (r == 0)
                    {
                        fenemys[l].x = random.Next(ClientSize.Width / 5, ClientSize.Width / 5 * 4 - fenemys[l].size);
                        fenemys[l].y = random.Next(41, ClientSize.Height / 5 - fenemys[l].size);
                    }
                    else if (r == 1)
                    {
                        fenemys[l].x = random.Next(ClientSize.Width / 5 * 4, ClientSize.Width - fenemys[l].size);
                        fenemys[l].y = random.Next(ClientSize.Height / 5, ClientSize.Height / 5 * 4 - fenemys[l].size);
                    }
                    else if (r == 2)
                    {
                        fenemys[l].x = random.Next(ClientSize.Width / 5, ClientSize.Width / 5 * 4 - fenemys[l].size);
                        fenemys[l].y = random.Next(ClientSize.Height / 5 * 4, ClientSize.Height - fenemys[l].size);
                    }
                    else
                    {
                        fenemys[l].x = random.Next(0, ClientSize.Width / 5 - fenemys[l].size);
                        fenemys[l].y = random.Next(ClientSize.Height / 5, ClientSize.Height / 5 * 4 - fenemys[l].size);
                    }
                    fenemys[l].hx = fenemys[l].x + 7;
                    fenemys[l].hy = fenemys[l].y + 5;
                    myThreadedFastEnemys.Add(fenemys[l]);
                    Thread tid2 = new Thread(new ThreadStart(fenemys[l].move));
                    tid2.Start();
                    l++;
                }
                if (dight == 6)
                {
                    for (l = 0; l < 2; l++)
                    {
                        fenemys[l] = new FastEnemy(this);
                        //生成位置  上右下左 0-3
                        int r = random.Next(0, 4);
                        if (r == 0)
                        {
                            fenemys[l].x = random.Next(ClientSize.Width / 5, ClientSize.Width / 5 * 4 - fenemys[l].size);
                            fenemys[l].y = random.Next(41, ClientSize.Height / 5 - fenemys[l].size);
                        }
                        else if (r == 1)
                        {
                            fenemys[l].x = random.Next(ClientSize.Width / 5 * 4, ClientSize.Width - fenemys[l].size);
                            fenemys[l].y = random.Next(ClientSize.Height / 5, ClientSize.Height / 5 * 4 - fenemys[l].size);
                        }
                        else if (r == 2)
                        {
                            fenemys[l].x = random.Next(ClientSize.Width / 5, ClientSize.Width / 5 * 4 - fenemys[l].size);
                            fenemys[l].y = random.Next(ClientSize.Height / 5 * 4, ClientSize.Height - fenemys[l].size);
                        }
                        else
                        {
                            fenemys[l].x = random.Next(0, ClientSize.Width / 5 - fenemys[l].size);
                            fenemys[l].y = random.Next(ClientSize.Height / 5, ClientSize.Height / 5 * 4 - fenemys[l].size);
                        }
                        fenemys[l].hx = fenemys[l].x + 7;
                        fenemys[l].hy = fenemys[l].y + 5;
                        myThreadedFastEnemys.Add(fenemys[l]);
                        Thread tid2 = new Thread(new ThreadStart(fenemys[l].move));
                        tid2.Start();
                    }
                     
                }
                dight++;
            }
            n--;
            if (n == 0)
            {
                timer1.Enabled = false;
                end();
            }
            for (k = 0; k < l; k++) {
                //快速怪物冷卻
                if (fenemys[k].fasti < 70) fenemys[k].fasti++;
                if (fenemys[k].fasttime > 0) fenemys[k].fasttime--;
                //快速怪物衝刺時間
            }
            //無敵
            if (Invincible < 20) Invincible++;
            //換子彈
            if (changebullt < 25)
            {
                changebullt++;
                if (changebullt == 25) label2.Text = "30" + "/" + player.fillbullet.ToString();
            }
            //射子彈
            if (shooti < 3) shooti++;
            //顯示剩餘時間
            label1.Text = h.ToString() + ":";
            if (m < 10)
            {
                label1.Text += "0" + m.ToString();
            }
            else
            {
                //if (m == 60) label1.Text += "00";
                label1.Text += m.ToString();
            }
            if (m == 0 && n % 10 == 0)
            {
                h--;
                m = 60;
            }
            if (n % 10 == 0) m--;
            this.Invalidate();
        }
        //玩家控制
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) up = true;
            if (e.KeyCode == Keys.Right) right = true;
            if (e.KeyCode == Keys.Down) down = true;
            if (e.KeyCode == Keys.Left) left = true;
            if (timer1.Enabled)
            {
                if (up)
                {
                    if (left)
                    {
                        //左上
                        if (player.x > 0)
                            player.x -= player.speed;
                        if (player.y > 41)
                            player.y -= player.speed;
                        player.hx = player.x + 3;
                        player.hy = player.y + 3;
                        look = 7;
                    }
                    else if (right)
                    {
                        //右上
                        if (player.x + player.size < this.ClientSize.Width)
                            player.x += player.speed;
                        if (player.y > 41)
                            player.y -= player.speed;

                        player.hx = player.x + 7;
                        player.hy = player.y + 3;
                        look = 1;
                    }
                    else if (down) { }
                    else
                    {
                        //上
                        if (player.y > 41)
                            player.y -= player.speed;
                        player.hx = player.x + 5;
                        player.hy = player.y + 3;
                        look = 0;
                    }
                }
                else if (down)
                {
                    if (left)
                    {
                        //左下
                        if (player.x > 0)
                            player.x -= player.speed;
                        if (player.y + player.size < this.ClientSize.Height)
                            player.y += player.speed;
                        player.hx = player.x + 3;
                        player.hy = player.y + 7;
                        look = 5;
                    }
                    else if (right)
                    {
                        //右下
                        if (player.x + player.size < this.ClientSize.Width)
                            player.x += player.speed;
                        if (player.y + player.size < this.ClientSize.Height)
                            player.y += player.speed;
                        player.hx = player.x + 7;
                        player.hy = player.y + 7;
                        look = 3;
                    }
                    else
                    {
                        //下
                        if (player.y + player.size < this.ClientSize.Height)
                            player.y += player.speed;
                        player.hx = player.x + 5;
                        player.hy = player.y + 7;
                        look = 4;
                    }
                }
                else if (left)
                {
                    if (right) { }
                    else
                    {
                        //左
                        if (player.x > 0)
                            player.x -= player.speed;
                        player.hx = player.x + 3;
                        player.hy = player.y + 5;
                        look = 6;
                    }
                }
                else if (right)
                {
                    //右
                    if (player.x + player.size < this.ClientSize.Width)
                        player.x += player.speed;
                    player.hx = player.x + 7;
                    player.hy = player.y + 5;
                    look = 2;
                }
                if (e.KeyCode == Keys.Space)
                {
                    //射子彈冷卻時間
                    if (shooti == 3)
                    {
                        shoot();
                        shooti = 0;
                    }
                }
            }
        }
        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) up = false;
            if (e.KeyCode == Keys.Right) right = false;
            if (e.KeyCode == Keys.Down) down = false;
            if (e.KeyCode == Keys.Left) left = false;
        }
        //射子彈
        private void shoot()
        {
            //裝子彈
            if (changebullt == 25)
            {
                myballs[i] = new Ball(this);
                //8方向射擊
                if (look == 0)
                {
                    myballs[i].x = player.x + 7;
                    myballs[i].y = player.y - 6;
                    myballs[i].yspeed = -4;
                    myballs[i].xspeed = 0;

                }
                if (look == 1)
                {
                    myballs[i].x = player.x + 20;
                    myballs[i].y = player.y - 6;
                    myballs[i].yspeed = -4;
                    myballs[i].xspeed = 4;
                }
                if (look == 2)
                {
                    myballs[i].x = player.x + 20;
                    myballs[i].y = player.y + 7;
                    myballs[i].yspeed = 0;
                    myballs[i].xspeed = 4;
                }
                if (look == 3)
                {
                    myballs[i].x = player.x + 20;
                    myballs[i].y = player.y + 20;
                    myballs[i].yspeed = 4;
                    myballs[i].xspeed = 4;
                }
                if (look == 4)
                {
                    myballs[i].x = player.x + 7;
                    myballs[i].y = player.y + 20;
                    myballs[i].yspeed = 4;
                    myballs[i].xspeed = 0;
                }
                if (look == 5)
                {
                    myballs[i].x = player.x - 6;
                    myballs[i].y = player.y + 20;
                    myballs[i].yspeed = 4;
                    myballs[i].xspeed = -4;
                }
                if (look == 6)
                {
                    myballs[i].x = player.x - 6;
                    myballs[i].y = player.y + 7;
                    myballs[i].yspeed = 0;
                    myballs[i].xspeed = -4;
                }
                if (look == 7)
                {
                    myballs[i].x = player.x - 6;
                    myballs[i].y = player.y - 6;
                    myballs[i].yspeed = -4;
                    myballs[i].xspeed = -4;
                }

                myThreadedBalls.Add(myballs[i]);
                Thread tid1 = new Thread(new ThreadStart(myballs[i].move));
                tid1.Start();
                i++;
                //顯示子彈數量
                label2.Text = (player.bullet - i).ToString() + "/" + player.fillbullet.ToString();
            }
            //換子彈
            if (i == player.bullet && player.bullet > 0)
            {
                changebullt = 0;
                i = 0;
                //子彈空了
                if (player.fillbullet == 0) end();
                player.fillbullet -= player.bullet;

            }
        }
        private void click(object sender, MouseEventArgs e)
        {
            //暫停
            timer1.Enabled = !timer1.Enabled;
            foreach (Ball myBall in myThreadedBalls)
                myBall.threadPause = !myBall.threadPause;
            foreach (Enemy enemy in myThreadedEnemys)
                enemy.threadPause = !enemy.threadPause;
            foreach (FastEnemy fenemy in myThreadedFastEnemys)
                fenemy.threadPause = !fenemy.threadPause;
        }
        private void Normal1_Load(object sender, EventArgs e)
        {
            player = new Player(this);

            enemys = new Enemy[10];
            myThreadedEnemys = new List<Enemy>();

            myballs = new Ball[player.bullet];
            myThreadedBalls = new List<Ball>();

            fenemys = new FastEnemy[2];
            myThreadedFastEnemys = new List<FastEnemy>();
            timer1.Interval = 100;
            timer1.Enabled = true;
            hp.Text = player.heath.ToString();
            label1.Text = "3:00";
            label2.Text = "30/"+ player.fillbullet.ToString();
            have = true;
        }
        class Ball
        {
            public int radius = 7;
            public int x, y;
            public int xspeed, yspeed;
            //public Color color;
            public bool have = true;
            Control form;
            public Boolean threadStop = false;
            public Boolean threadPause = false;
            public Ball(Control form)
            {
                this.form = form;
            }
            public void move()
            {
                threadStop = false;
                threadPause = false;
                while (!threadStop)
                {
                    if (!threadPause)
                    {
                        y = y + yspeed;
                        if (y < 40)
                            have = false;
                        ///if y is less than 0 then we change direction}
                        else if (y + radius > form.ClientSize.Height)
                            have = false;
                        //if(y==)
                        x = x + xspeed;
                        if (x < 0)
                            have = false;
                        ///if y is less than 0 then we change direction}
                        else if (x + radius > form.ClientSize.Width)
                            have = false;
                    }
                    Thread.Sleep(50);
                }
            }
        }
        class Enemy
        {
            public int heath = 2;
            public int x, y;
            public int hx, hy;
            public int xspeed = -1, yspeed = -1;
            public int size = 20;
            public int hsize = 10;
            public bool have = true;
            Control form;
            public Boolean threadStop = false;
            public Boolean threadPause = false;
            public Enemy(Control form)
            {
                this.form = form;
            }
            public void move()
            {
                threadStop = false;
                threadPause = false;
                while (!threadStop)
                {
                    if (!threadPause)
                    {
                        y = y + yspeed;
                        x = x + xspeed;
                        //hy = EnemyHeady(y, hy, yspeed);
                        if (yspeed < 0) hy = y + 3;
                        else if (yspeed > 0) hy = y + 7;
                        else hy = y + 5;
                        if (xspeed < 0) hx = x + 3;
                        else if (xspeed > 0) hx = x + 7;
                        else hx = x + 5;
                    }
                    Thread.Sleep(50);
                }

            }
        }
        class FastEnemy
        {
            public int heath = 3;
            public int x, y;
            public int hx, hy;
            public int fasti = 40;
            public int fasttime = 0;
            public int xspeed = -2, yspeed = -2;
            public int size = 20;

            public int hsize = 10;
            public bool have = true;
            Control form;
            public Boolean threadStop = false;
            public Boolean threadPause = false;
            public FastEnemy(Control form)
            {
                this.form = form;
            }
            public void move()
            {
                threadStop = false;
                threadPause = false;
                while (!threadStop)
                {
                    if (!threadPause)
                    {
                        y = y + yspeed;
                        x = x + xspeed;
                        if (yspeed < 0) hy = y + 3;
                        else if (yspeed > 0) hy = y + 7;
                        else hy = y + 5;
                        if (xspeed < 0) hx = x + 3;
                        else if (xspeed > 0) hx = x + 7;
                        else hx = x + 5;
                    }
                    Thread.Sleep(50);
                }
            }

        }
        class Player
        {
            public int heath = 5;
            public int bullet = 30, fillbullet = 60;
            public int x, y;
            public int hx, hy;
            public int size = 20, hsize = 11;
            public int speed = 2;
            //public bool nocollision = true;
            Control form;

            public Player(Control form)
            {
                this.form = form;
                x = form.Width / 2;
                y = (form.Height - 40) / 2;
                hx = x + 7;
                hy = y + 5;
            }
        }
    }
}