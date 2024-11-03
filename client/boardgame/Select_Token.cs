using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class Select_Token : Form
    {
        public Select_Token()
        {
            InitializeComponent();
        }
        Bitmap bit;
        Graphics DC;
        List<Bitmap> Tokens = new List<Bitmap>();
        Rectangle[] Token_rect = new Rectangle[4];
        private void Select_Token_Load(object sender, EventArgs e)
        {
            init(); //그림들의 기본 설정.
            for (int i = 0; i < 4; i++) {
                DC.DrawImage(Tokens[i], Token_rect[i]); //Form 에 직접 그림을 그림.
            }
            Invalidate();
        }

        private int Token_Contain(Point Location) //지금 어떤 토큰을 선택했는지 확인하기 위한 메소드
        {
            for(int i = 0; i< Token_rect.Length; i++)
            {
                if (Token_rect[i].Contains(new Rectangle(Location, new Size(1, 1)))) {
                    return i;
                }
            }

            return -1;
        }

        private void init()
        {
            bit = new Bitmap(this.Width, this.Height);
            DC = Graphics.FromImage(bit); //FORM 전체를 그림그리는 판으로 만듦.

            Image img = Properties.Resources.p1; //말들의 그림 가져옴.
            Tokens.Add(new Bitmap(img)); //이미지 가져오기.
            img = Properties.Resources.p2;
            Tokens.Add(new Bitmap(img));
            img = Properties.Resources.p3;
            Tokens.Add(new Bitmap(img));
            img = Properties.Resources.p4;
            Tokens.Add(new Bitmap(img));

            for(int i =0; i < 4; i++)//말들을 그릴 좌표
            {
                if(i >= 2)
                    Token_rect[i] = new Rectangle(new Point(100* ((i - 2) * 2), 160), new Size(150, 150));
                else
                    Token_rect[i] = new Rectangle(new Point(100 * (i*2), 20), new Size(150, 150));
            }
        }
        private void Select_Token_Paint(object sender, PaintEventArgs e) //그림을 그려주는 페인트 이벤트
        {
            e.Graphics.DrawImageUnscaled(bit, 0, 0);
        }

        private void Select_Token_MouseUp(object sender, MouseEventArgs e) //마우스 클릭 이벤트
        {
            if(Token_Contain(e.Location) != -1)
            {
                MessageBox.Show(Token_Contain(e.Location).ToString());
            }
        }
    }
}
