using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace boardgame
{
    public partial class GameMain : Form
    {

        public Point[] C1 = { new Point(500, 300), new Point(400, 200), new Point(200, 400), new Point(300, 500) };
        public GameMain()
        {
            InitializeComponent();
        }

        Bitmap bit;
        Graphics DC, dices, Card, nameGp;
        Bitmap map, dice1, key, cardg, player, player2, nameBt, card_red;
        //Area area;
        cityArea[] city = new cityArea[4];


        Bitmap areacard, areacard1, areacard2, areacard3;
        Bitmap social;
        public List<buildCard> bclist_infor = new List<buildCard>();
        List<buildCard> bclist_all = new List<buildCard>();

        public int island { get; set; } = 0; //무인도에 들어가있는 턴

        public bool Uninhabited { get; set; } = false; //무인도에 있는지

        public List<Rectangle> Dicelist = new List<Rectangle>();
        public List<Rectangle> CardList = new List<Rectangle>();
        public List<String> CardText = new List<String>();
        List<Player>[] players = new List<Player>[4];

        List<Button> buttonTravel = new List<Button>();

        bool Carddr = false;
        //Label label = new Label();
        //public int money = 0;
        Money money = new Money();

        bool RED = false;
        bool BLUE = false;
        bool GRAY = false;
        bool BLACK = false;
        public int nowcity { get; set; } = 9;
        public int nowblock { get; set; } = 3;
        int bfcity { get; set; } = 9;
        int bfblock { get; set; } = 3;
        //bool xgt = false;
        bool start = true;

        public int cardnum;
        public int[] buildnum = new int[36];
        Rectangle[][] playerrect = new Rectangle[4][];
        Rectangle[][][] buildrect = new Rectangle[4][][];
        //public int flatform = 0;
        public int money_so { get; set; } = 100;
        Rectangle[][] buyground = new Rectangle[4][];


        information inform = new information();







        private void ground()
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == 2)
                {
                    buyground[i] = new Rectangle[7];
                }
                else
                {
                    buyground[i] = new Rectangle[8];
                }
                for (int j = 0; j < 8; j++)
                {
                    if (i == 2)
                    {
                        if (j == 1)
                        {
                            continue;
                        }
                        buyground[i][j] = City.four(city[i].cities[j].X, city[i].cities[j].Y);

                    }
                    else
                    {
                        if (i < 3)
                        {
                            if (j == 1 || j == 6)
                            {
                                continue;
                            }

                        }
                        else
                        {
                            if (j == 4 || j == 7)
                            {
                                continue;
                            }
                        }
                        buyground[i][j] = City.four(city[i].cities[j].X, city[i].cities[j].Y);
                    }
                }
            }
        }



        public bool confirm_ground(int nowblock, int nowcity)
        {
            if (city[nowblock].cityRect[nowcity].Count > 0)
                return true;
            else
                return false;
        }
        public bool confirm_Villa(int nowblock, int nowcity)
        {
            if (city[nowblock].Rect_Villa[nowcity].X != 0)
                return true;
            else
                return false;
        }
        public bool confirm_Building(int nowblock, int nowcity)
        {
            if (city[nowblock].Rect_Building[nowcity].X != 0)
                return true;
            else
                return false;
        }
        public bool confirm_Hotel(int nowblock, int nowcity)
        {
            if (city[nowblock].Rect_Hotel[nowcity].X != 0)
                return true;
            else
                return false;
        }
        Button bt;
        private void Form1_Load(object sender, EventArgs e)
        {
         
            Server_connect();

          
            this.Size = new Size(320, 232);

            //button3.Visible = false;
            button1.Visible = false;
            initDC();

            //close cl = new close();
            //cl.Show();

            //timer1.Start();
            timer3.Start();
            //Invalidate();


        }

        private void Form_show()
        {
            //bt.Visible = false;
            this.Size = new Size(cityArea.side - 1, cityArea.down - 1);
            initDC();
           
            //Number_ser();
            //money = 1000;
            
            init();
            imagea();
            //playerimage();
            image();
            rectcity();
            //timer1.Start();
            timer2.Start();
            StreamWriter s1 = new StreamWriter(new FileStream("buildSave.txt", FileMode.Create));
            s1.Close();

            StreamReader sr = new StreamReader(new FileStream("Card.txt", FileMode.Open));

            while (sr.EndOfStream == false)
            {
                CardText.Add(sr.ReadLine());
            }
            //MessageBox.Show(sr.ReadToEnd());

            sr.Close();

            StreamWriter sw = new StreamWriter(new FileStream("Save.txt", FileMode.Create));
            sw.Close();
            //StreamWriter sw1 = new StreamWriter(new FileStream("money.txt", FileMode.Create));
            //sw1.Write("1000");
            //sw.Close();
            //la = new Label();
            //la.AutoSize = true;
            //la.Location = new System.Drawing.Point(700, 367);
            //la.Name = "label25";
            //la.Size = new System.Drawing.Size(38, 20);
            //la.TabIndex = 1;
            //la.Text = money.m.ToString();
            //la.Visible = true;
            //Controls.Add(la);


            //int dr = 45;
            //Matrix m = new Matrix();
            //Point[] C = { new Point(500, 300), new Point(400, 200), new Point(300, 500) };

            //m.RotateAt(dr, C);
            // DC.Transform = m;
            //Rectangle Card1 = new Rectangle(0, 0, 182, 393); //key
            //Rectangle Card2 = new Rectangle(200, 0, 182, 393); //Card
            Card.DrawImage(cardg, C, CardList[0], GraphicsUnit.Pixel);

            // DC.DrawImage(pl, new Rectangle(city[4].cities[0].Location, new Size(75, 64)));
            Player player = players[3][9];
            city[3].play[9].Add(player);

            city[3].update(nowblock);
            Invalidate();
            for (int i = 0; i < 4; i++)
            {
                name[i] = new List<string>();
                name[i] = city[i].name_str;
            }
            //city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);



            //DC.FillRectangle(new SolidBrush(Color.Red), a.cities[1][3]);


            //button3.Visible = true;
            button1.Visible = true;
            //color / nowblock,nowcity, money.m
        }





        public List<string>[] name = new List<string>[4];



        int skip = 0;

        private void GameMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //sock.Close();
        }

        int loopconfirm = 0;
        bool Turn = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (Turn)
                button1.Enabled = true;
            else
                button1.Enabled = false;
            


        }

        private void bu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void butt_Click(object sender, EventArgs e)
        {
            loopconfirm++;
            string butt_name = ((Button)sender).ToString();
            string[] bname = butt_name.Split(' ');
            string[] number = bname[2].Split('|');
            int block = Convert.ToInt32(number[0]);
            int city = Convert.ToInt32(number[1]);
            for (int i = 0; i < buttonTravel.Count; i++)
            {
                Controls.Remove(buttonTravel[i]);
            }
            if (loopconfirm <= 1)
            {
                spacemove(block, city);
                if (nowblock == 3 && nowcity == 7)
                {
                    //money -= 15;
                    money.m -= 15;
                    money_so += 15;
                }
                if (nowblock == 1 && nowcity == 9)
                {
                    if (money_so != 0)
                    {
                        money.m += money_so;
                        MessageBox.Show(money_so.ToString() + "만원이 지급되었습니다.");
                        money_so = 0;
                    }
                }
            }


        }



        private static DateTime Delay(int MS)

        {

            DateTime ThisMoment = DateTime.Now;

            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);

            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)

            {

                System.Windows.Forms.Application.DoEvents();

                ThisMoment = DateTime.Now;

            }

            return DateTime.Now;

        }

        // = new Form3();

        //Card frm2;// = new Form2();


        //}

        private bool containkey(Vector2 point, Vector2[] polygon)
        {

            int polygonlength = polygon.Length, i = 0;
            bool inside = false;

            float pointX = point.X, pointY = point.Y;
            float startX, startY, endX, endY;
            Vector2 endPoint = polygon[polygonlength - 1];
            endX = endPoint.X;
            endY = endPoint.Y;

            while (i < polygonlength)
            {
                startX = endX; startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.X; endY = endPoint.Y;

                inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                && /* if so, test if it is under the segment */
                ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }
            return inside;
        }




        private void DataReceiveEvent(bool data) //무인도 정보 받음
        {
            if (data == false)
            {
                island = 3;
                Uninhabited = false;
                //timer2.Stop();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            Card_red();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //string[] butt = button3.Text.Split('|');
            //Select_build sb = new Select_build();
            //sb.ShowDialog();

        }

        //public Vector2 v2(this Point point)
        //{
        //    return new Vector2(point.X, point.Y);
        //}
        //public Vector2[] Vect(this Point[] point)
        //{
        //    return new Vector2[] { new Vector2(point[0].X, point[0].Y), new Vector2(point[1].X, point[1].Y), new Vector2(point[2].X, point[2].Y) };
        //}




        //private void te()
        //{
        //    //93,69 번호 //70 119 제목 //21 167 내용 // 37,201 행동
        //    Random rand = new Random();
        //    int a = rand.Next(0, 30);
        //    string[] Text = CardText[a].Split(':');


        //    //string Test = "1";


        //    //this.label1.AutoSize = true;
        //    //this.label1.Location = new System.Drawing.Point(723, 367);
        //    //this.label1.Name = "label1";
        //    //this.label1.Size = new System.Drawing.Size(38, 12);
        //    //this.label1.TabIndex = 1;
        //    //this.label1.Text = "label1";

        //    Card.DrawString(Text[0],new Font("Bold",10) , new SolidBrush(Color.Black),  new Point(691, 475)); //번호 85 75     원래 크기 189, 0, 200, 280 120 168    
        //    Card.DrawString(Text[1], new Font("Bold", 7), new SolidBrush(Color.Black), new Point(655, 520)); //제목 55 120
        //    Card.DrawString(Text[2], new Font("Bold", 7), new SolidBrush(Color.Black), new Point(634, 558)); //내용 34 164  700, 500, 120, 168 3/5 3/5
        //                                                                                                     //Card.DrawString(Text[3], new Font("Bold", 7), new SolidBrush(Color.Black), new Point(643, 604)); //설명 43 204


        //    label.Name = "label";
        //    label.Text = "test";
        //    //label.Visible = false;
        //    label.AutoSize = true;
        //    label.TabIndex = 1;
        //    label.BackColor = Color.White;
        //    label.Location = new Point(643, 604);
        //    label.Size = new System.Drawing.Size(38, 12);
        //    label.Text = Text[3];
        //    label.Visible = true;



        //    Invalidate();
        //}
        MySocket server;
        private void timer1_Tick(object sender, EventArgs e)
        {
        }
        private void backmove(int movec)
        {
            int fq;
            if (nowcity < movec)
            {
                fq = 10 + (nowcity - movec);

           
            }
            else fq = nowcity - movec;

            for (int i = 0; i < movec; i++)
            {

                // area.Emptyrect();

                //i = nowblock
                
                if (nowcity == 0)
                {
                    if (nowblock > 0)
                        nowblock -= 1;
                    else
                        nowblock = 3;
                }
                nowcity -= 1;
                if(nowcity < 0)
                {
                    nowcity += 10;
                }
                //DC.DrawImage(player, new Rectangle(area.cities[flatform][i].Location, new Size(75, 64))); 
                if (nowblock == 3 && nowcity == 9) money.m += 20;
                city[bfblock].play[bfcity].Remove(players[bfblock][bfcity]);
                
                
                reset();
                city[nowblock].play[nowcity].Add(players[nowblock][nowcity]);
                city[nowblock].update(nowblock);
                Delay(100);
                
                Invalidate();
                bfblock = nowblock;
                bfcity = nowcity;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            city[0].play[0].Add(players[0][0]);
            city[0].update(0);
            Invalidate();
        }
        public void test_data(buildCard bc)
        {
            inform.Card_put(bc);
            bclist_infor.Add(bc);
        }

        public void input_bclist(buildCard bc)
        {
            bclist_all.Add(bc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // inform = new information();
            inform.money = money.m;
            inform.ShowDialog();

        }
    }

}
