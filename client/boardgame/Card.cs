using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class Card : Form
    {
        public Card()
        {
            InitializeComponent();
        }

        Bitmap bt;
        Graphics gp;
        Bitmap cardg;
        public int minus = 0;
        public int Cardnum;
        Rectangle rect = new Rectangle(189, 0, 200, 280);
        public List<String> CardText = new List<String>();
        public List<String> Cn = new List<String>();
        List<int> CardList = new List<int>();
        public int backm = 0;
        public bool startpoint = false;
        public bool resu = false;
        public int number = 0;

        public bool islan = true;

        public delegate void DataPassEventHandler(bool data);

        // public event DataPassEventHandler DataPassEvent;



        private void save_card()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            gm.Save_Cards(this);
        }

        private void init()
        {
            bt = new Bitmap(this.Width, this.Height);
            gp = Graphics.FromImage(bt);
        }
        private void Image()
        {

            Image card1 = Properties.Resources.keycard_3;
            cardg = new Bitmap(card1);

        }
        private void Form2_Load(object sender, EventArgs e)
        {

            init();
            Image();
            for (int i = 1; i < 31; i++)
            {
                string a = i.ToString();
                Cn.Add(a);
            }
            StreamReader sr = new StreamReader(new FileStream("Card.txt", FileMode.OpenOrCreate));

            while (sr.EndOfStream == false)
            {
                CardText.Add(sr.ReadLine());
            }
            //MessageBox.Show(sr.ReadToEnd());

            sr.Close();
            //MessageBox.Show("texst");
            gp.DrawImage(cardg, new Rectangle(0, 0, 303, 450), rect, GraphicsUnit.Pixel);
            //if (resu)
            //    Draw(number);
            //else
            Draw(rando());

            Invalidate();

            Cardnum = Convert.ToInt32(this.Text);

            if ((this.Text.Equals("1") || this.Text.Equals("7")))
            {
                minus = 5;
            }
            else if (this.Text.Equals("2"))
            {
                minus = -20;
            }

            else if (this.Text.Equals("8"))
            {
                minus = 10;
            }
            else if (this.Text.Equals("9"))
            {
                minus = -5;
            }
            else if (this.Text.Equals("12") || this.Text.Equals("20"))
            {
                minus = -10;
            }
            else if (this.Text.Equals("22"))
            {
                minus = -30;
            }
            else if (this.Text.Equals("28"))
            {
                backm = 2;
            }
            else if (this.Text.Equals("10"))
            {
                backm = 3;
            }
            else if (this.Text.Equals("11"))
            {
                startpoint = true;
            }



            if ((this.Text.Equals("13") || this.Text.Equals("26") || this.Text.Equals("3")) == false)
            {
                timer1.Start();

            }

            //else if(this.Text.Equals(""))

        }

        private int rando()
        {
            Random rand = new Random();
            //return rand.Next(0, 30);
            return rand.Next(0, 1);
        }


        public string texts;

        private void Draw(int a)
        {
            //93,69 번호 //70 119 제목 //21 167 내용 // 37,201 행동

            string[] Text = CardText[a].Split(new string[] { ":" }, StringSplitOptions.None);
            this.Text = Text[0];
            texts = Text[0];


            gp.DrawString(Text[0], new Font("Bold", 12), new SolidBrush(Color.Black), new Point(138, 120)); //번호 85 75     원래 크기  200, 280    
            gp.DrawString(Text[1], new Font("Bold", 8), new SolidBrush(Color.Black), new Point(85, 185)); //제목 55 120
            TextRenderer.DrawText(gp, Text[2], new Font("Bold", 12), new Rectangle(new Point(34, 254), new Size(200, 40)), Color.Black, TextFormatFlags.WordBreak);

            TextRenderer.DrawText(gp, Text[3], new Font("Bold", 12), new Rectangle(new Point(52, 320), new Size(200, 120)), Color.Black, TextFormatFlags.WordBreak);

            //Card.DrawString(Text[3], new Font("Bold", 7), new SolidBrush(Color.Black), new Point(643, 604)); //설명 43 204
            //gp.DrawString(Text[2], new Font("Bold", 10), new SolidBrush(Color.Black), new Point(34, 254)); //내용 34 164  700, 500, 120, 168 3/5 3/5

            Invalidate();

            //Controls.Add(la);


        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(bt, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool mouseclick;
        private Point ptest;
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseclick)
            {
                this.Location = new Point(Cursor.Position.X - ptest.X, Cursor.Position.Y - ptest.Y);
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseclick = false;

        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Right == e.Button)
            {
                if ((this.Text.Equals("13") || this.Text.Equals("26") || this.Text.Equals("3")))
                {
                    mouseclick = true;
                    ptest = e.Location;
                    ContextMenuStrip menu = new ContextMenuStrip();
                    ToolStripMenuItem save = new ToolStripMenuItem("보관");
                    ToolStripMenuItem Ret = new ToolStripMenuItem("되돌리기");
                    ToolStripMenuItem use = new ToolStripMenuItem("사용하기");
                    menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { save, Ret, use });
                    menu.Size = new System.Drawing.Size(181, 70);
                    save.Size = new System.Drawing.Size(180, 22);
                    save.Text = "보관";
                    save.Click += new System.EventHandler(this.보관ToolStripMenuItem_Click);
                    Ret.Size = new System.Drawing.Size(180, 22);
                    Ret.Text = "되돌리기";
                    Ret.Click += new System.EventHandler(this.되돌리기ToolStripMenuItem_Click);
                    use.Text = "사용하기";
                    use.Size = new System.Drawing.Size(180, 22);
                    use.Click += new System.EventHandler(this.UseTool_Click);
                    menu.Show(MousePosition);
                }
            }

            else if (MouseButtons.Left == e.Button)
            {
                mouseclick = true;
                ptest = e.Location;
            }

        }
        private void USE()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            if (gm != null)
            {
                gm.uninhabit();
                this.Close();
            }
        }
        private void 되돌리기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 보관ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter(new FileStream("Save.txt", FileMode.Append));
            //sw.WriteLine(this.Text);
            //sw.Close();
            save_card();
            this.Close();
        }
        private void UseTool_Click(object sender, EventArgs e)
        {
            if (this.Text.Equals("3"))
            {
                USE();
            }

        }
    }
}