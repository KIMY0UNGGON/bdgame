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
        Rectangle rect = new Rectangle(0, 0, 540, 500); //카드의 사각형.
        public List<string> Cardtext = new List<String>(); //카드들의 텍스트들을 저장하는 리스트.
        public int color { get; set; } //건물카드의 구역. 구역의 색깔이 무엇인지를 나타냄.
        public int loca { get; set; } //건물카드의 위치. 즉 어떤 지역인지를 표시.
        public int city_address { get; set; }

        public string[] Cards;


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


        GameMain gm = Application.OpenForms["GameMain"] as GameMain; //열려있는 메인 게임의 폼을 가지고 옴.



        private void card_input() 
        {
            //GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            
            if (gm != null)
            {
                gm.input_carddata(this);
            }

        }

        private void card_sell()
        {
            if (gm != null)
               gm.card_sell(this);
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
            init(); //그래픽 변수들을 선언해서 카드를 그릴 수 있게 함.
            Image(); //이미지 파일들을 가지고 옴.
             
            locacard(); // 지역 및 건물 카드의 내용들을 txt파일에서 긁어옴.

            textbuild(loca); //카드의 상세 정보들을 적음. loca 변수는 CardMain_Build.cs에서 정해서 form을 열때 값을 줌.
            Invalidate();
        }

        private void locacard() // 지역 및 건물 카드의 내용들을 txt파일에서 긁어옴.
        {

            //StreamReader sr = new StreamReader(new FileStream("build.txt", FileMode.OpenOrCreate));

            //while (sr.EndOfStream == false)
            //{
            //    Cardtext.Add(sr.ReadLine());

            //}
            //sr.Close();
            string Card_Text = Properties.Resources.build;
            foreach(var Text in Card_Text.Split('\n'))
            {
                Cardtext.Add(Text);
            }

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
                this.Location = new Point(Cursor.Position.X - Card_Point.X, Cursor.Position.Y - Card_Point.Y);
            }
        }
        bool mouseclick;
        private void Form5_MouseUp(object sender, MouseEventArgs e)
        {

            mouseclick = false; //마우스 클릭시 mouseclick 중이라는 것.

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
            card_sell();
            this.Close();
        }

        private void putToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter(new FileStream("buildSave.txt", FileMode.Append));
            //sw.WriteLine(this.Text + "|" + loca.ToString()+"|"+color.ToString());
            //sw.Close();
            card_input();
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
        Point Card_Point;
        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {
            //if (MouseButtons.Right == e.Button) { }

            if (MouseButtons.Left == e.Button) //마우스 버튼을 왼쪽 클릭했을 경우에 왼쪽 클릭중이라고 부울 변수로 표현.
            {
                mouseclick = true;
                Card_Point = e.Location; //마우스의 현재 위치. 카드를 클릭했을 때 마우스를 따라가게 하기 위함.
            }
        }
        private Font Ft(int size) //카드들의 폰트.
        {
            return new Font("Bold", size);
        }

        private void textbuild(int num) //건물 카드의 텍스트들의 위치들을 설정해서 그림.
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
