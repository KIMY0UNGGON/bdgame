using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    //카드 한장에 대한 정보
    public partial class buildCard : Form
    {
        public buildCard()
        {
            InitializeComponent();


        }
        bool front = true;
        Bitmap Card;
        Graphics gp;
        Bitmap Card1;
        Color co = Color.Aqua;
        SolidBrush sl;
        Rectangle rect = new Rectangle(0, 0, 540, 500);
        public List<string> Cardtext = new List<String>();
        public int color { get; set; }
        public int loca { get; set; }




        private Point house(int y)
        {
            return new Point(290, y);
        }




        private Point hotel(int y)
        {
            return new Point(40, y);
        }
        private Point building(int y)//30
        {
            return new Point(165, y);
        }






        private void test()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            
            if (gm != null)
            {
                gm.test_data(this);
            }

        }

        private void data_add()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;

            if (gm != null)
            {
                gm.input_bclist(this);
            }
        }








        private void init()
        {
            Card = new Bitmap(this.Width, this.Height);
            gp = Graphics.FromImage(Card);
        }
        private void Image()
        {
            Image img;
            if (color == 0)
            {
                if (front == true)
                    img = Properties.Resources.건물카드_빨간색_;
                else
                    img = Properties.Resources.뒷면카드_빨간색_;
                sl = new SolidBrush(Color.Blue);
            }
            else if (color == 1)
            {
                if (front == true)
                    img = Properties.Resources.건물카드_초록색_;
                else
                    img = Properties.Resources.뒷면카드_녹색_;
                sl = new SolidBrush(Color.OrangeRed);
            }
            else if (color == 2)
            {
                if (front == true)
                    img = Properties.Resources.건물카드_적색_;
                else
                    img = Properties.Resources.뒷면카드_적색_;
                sl = new SolidBrush(Color.AliceBlue);
            }
            else
            {
                if (front == true)
                    img = Properties.Resources.건물카드_회색_;
                else
                    img = Properties.Resources.뒷면카드_회색_;
                sl = new SolidBrush(Color.Black);
            }
            Card1 = new Bitmap(img);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            init();
            Image();

            //StreamReader sr = new StreamReader(new FileStream("build.txt", FileMode.OpenOrCreate));

            //while (sr.EndOfStream == false)
            //{
            //    Cardtext.Add(sr.ReadLine());
            //}
            //sr.Close();


            //gp.DrawImage(Card1, new Rectangle(0, 0, 381, 370),rect, GraphicsUnit.Pixel);
            locacard();

            textbuild(loca);
            Invalidate();
            //data_add();
        }

        private void locacard()
        {

            StreamReader sr = new StreamReader(new FileStream("build.txt", FileMode.OpenOrCreate));

            while (sr.EndOfStream == false)
            {
                Cardtext.Add(sr.ReadLine());
            
            }
            sr.Close();


            gp.DrawImage(Card1, new Rectangle(0, 0, 381, 370), rect, GraphicsUnit.Pixel);


        }

        private void Form5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(Card, 0, 0);
        }

        private void Form5_Move(object sender, EventArgs e)
        {

        }

        private void Form5_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseclick)
            {
                this.Location = new Point(Cursor.Position.X - ptest.X, Cursor.Position.Y - ptest.Y);
            }
        }
        bool mouseclick;
        private void Form5_MouseUp(object sender, MouseEventArgs e)
        {

            mouseclick = false;

            if (MouseButtons.Right == e.Button)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripMenuItem turn = new ToolStripMenuItem("뒤집기");
                ToolStripMenuItem sell = new ToolStripMenuItem("팔기");
                ToolStripMenuItem put = new ToolStripMenuItem("집어넣기");
                menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { turn, sell, put });
                menu.Size = new System.Drawing.Size(181, 70);
                turn.Text = "뒤집기";
                turn.Click += new System.EventHandler(this.TurnToolStripMenuItem_Click);

                sell.Text = "팔기";
                sell.Click += new System.EventHandler(this.sellToolStripMenuItem_Click);

                put.Text = "집어넣기";
                put.Click += new System.EventHandler(this.putToolStripMenuItem_Click);
                menu.Show(MousePosition);
            }
        }
        private void sellToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void putToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter(new FileStream("buildSave.txt", FileMode.Append));
            //sw.WriteLine(this.Text + "|" + loca.ToString()+"|"+color.ToString());
            //sw.Close();
            test();
            this.Close();
        }

        private void TurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (front == true)
                front = false;
            else
                front = true;

            gp.Dispose();
            init();
            Image();

            locacard();
            textbuild(loca);
            Invalidate();

        }
        Point ptest;
        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {
            //if (MouseButtons.Right == e.Button) { }

            if (MouseButtons.Left == e.Button)
            {
                mouseclick = true;
                ptest = e.Location;
            }
        }
        private Font Ft(int size)
        {
            return new Font("Bold", size);
        }
        public string[] Cards;
        private void textbuild(int num)
        {
            Cards = Cardtext[num].Split(new char[] { ',' });            //28장의 카드에 들어갈 텍스트들 분할 총8개의 텍스트

            this.Text = Cards[0];



            if (front == true)
            {

                //gp.DrawString(Cards[0], new Font("Bold", 12), new SolidBrush(Color.White), new Point(150, 130));
                if (Cards[0].Length > 5)
                {
                    TextRenderer.DrawText(gp, Cards[0], Ft(20), new Rectangle(new Point(100, 120), new Size(475, 59)), Color.White, TextFormatFlags.WordBreak);
                }
                else if (Cards[0].Length > 3)
                {
                    TextRenderer.DrawText(gp, Cards[0], Ft(20), new Rectangle(new Point(140, 120), new Size(475, 59)), Color.White, TextFormatFlags.WordBreak);
                }
                else
                {
                    TextRenderer.DrawText(gp, Cards[0], Ft(20), new Rectangle(new Point(150, 120), new Size(475, 59)), Color.White, TextFormatFlags.WordBreak);
                }

                gp.DrawString("호텔", Ft(12), sl, hotel(30));
                gp.DrawString("빌딩", Ft(12), sl, building(30));

                if(Cards[1].Length == 3)
                    gp.DrawString("만원", Ft(12), new SolidBrush(Color.Black), new Point(240, 270));
                else
                    gp.DrawString("만원", Ft(12), new SolidBrush(Color.Black), new Point(210, 270));
                gp.DrawString(Cards[1], Ft(36), new SolidBrush(Color.Black), new Point(150, 250)); // 땅값


                gp.DrawString("별장", Ft(12), sl, house(30));
                gp.DrawString("건설비용", Ft(24), new SolidBrush(Color.Black), new Point(110, 200));


                if ((loca == 3 || loca == 10 || loca == 17 || loca == 20 || loca == 23 || loca == 28) == false)
                {

                    gp.DrawString(Cards[2], Ft(24), sl, hotel(60)); // 호텔
                    gp.DrawString(Cards[3], Ft(24), sl, building(60)); // 빌딩               
                    gp.DrawString(Cards[4], Ft(24), sl, house(60)); //별장
                                                                    //190 200
                }
                else
                {

                    //gp.DrawString("통행요금", new Font("Bold", 12), new SolidBrush(Color.Black), new Point(150, 200));
                    gp.DrawString("이 곳에는 건물을 지을 수 없습니다.", Ft(12), sl, new Point(40, 60));
                }
            }
            else// 5 ~ 9
            {

                //250 340



                if (Cards[0].Length > 5)
                {
                    TextRenderer.DrawText(gp, Cards[0], Ft(20), new Point(110, 250), Color.Black, TextFormatFlags.WordBreak);
                }
                else if (Cards[0].Length > 3)
                {
                    TextRenderer.DrawText(gp, Cards[0], Ft(20), new Point(140, 250), Color.Black, TextFormatFlags.WordBreak);
                }
                else
                {
                    TextRenderer.DrawText(gp, Cards[0], Ft(20), new Point(150, 250), Color.Black, TextFormatFlags.WordBreak);
                }

                gp.DrawString("통행료", Ft(24), new SolidBrush(Color.Black), new Point(130, 200));

                if ((loca == 3 || loca == 10 || loca == 17 || loca == 20 || loca == 23 || loca == 28) == false)
                {


                    gp.DrawString("호텔", Ft(12), sl, hotel(30));
                    gp.DrawString("빌딩", Ft(12), sl, building(30));

                    gp.DrawString("별장", Ft(12), sl, house(30));

                    //gp.DrawString("별장2", Ft(12), sl, new Point(40, 100));

                    gp.DrawString("땅", Ft(12), sl, new Point(290, 100));

                    gp.DrawString(Cards[5], Ft(12), sl, new Point(280, 120)); //땅
                    gp.DrawString(Cards[6] + "만", Ft(12), sl, hotel(60)); // 호텔 40
                    gp.DrawString(Cards[7] + "만", Ft(12), sl, building(60)); // 빌딩 165              
                    gp.DrawString(Cards[8] + "만", Ft(12), sl, house(60)); //별장 290

                    //gp.DrawString(Cards[9] + "만", Ft(12), sl, new Point(40, 120)); //별장2                                                                                              //190 200
                }
                else
                {
                    gp.DrawString("통행료", Ft(12), sl, new Point(165, 30));
                    gp.DrawString(Cards[2], Ft(24), sl, new Point(160, 60));
                }

            }
        }
    }
}
