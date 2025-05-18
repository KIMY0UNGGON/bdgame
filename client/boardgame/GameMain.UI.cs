using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace boardgame
{
    public partial class GameMain
    {

        public void Token_Color(int color) //멀티플레이에서의 색 선택 메소드.
        {
            color_num = color;
            //server.send("TOKEN_NEXT");
            
            Self_init(color_num);
            if(Multi)
                server.send("T" + color.ToString()); //선택한 컬러를 서버에 다시 전송.
            //server.send($"{Multy_Num}READY"); //현재 클라이언트 토큰 선택 완료.
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(key, 0, 0);
            e.Graphics.DrawImageUnscaled(bit, 0, 0);
            

            e.Graphics.DrawImageUnscaled(nameBt, 0, 0);
            //수정점 주사위 프린트
            e.Graphics.DrawImageUnscaled(DICES, 600, 700);
            

        }

        private bool LOSE_CONDITION()
        {
            if (money.m.CompareTo(0) <= 0) return true;
            return false;
        }




        private void ColorSet(int  Num, int color) {
            switch (color)
            {
                case 0: TokenColor[Num] = System.Drawing.Color.SkyBlue; break;
                case 1:
                    TokenColor[Num] = System.Drawing.Color.Black; break;
                case 2:
                    TokenColor[Num] = System.Drawing.Color.Red; break;
                case 3:
                    TokenColor[Num] = System.Drawing.Color.Gray; break;

            }

        }
        private bool Confirm_AllReady()
        {
            int count = 0;
            foreach(var multy in MultyPlayers)
            {
         
                if (multy.Any())
                {
                    count++;
                }
                if(count == Multy_Count)
                {
                    return true;
                }
            }
            return false;
        }
        

        private void reset(int bfblock, int bfcity)
        {

            city[bfblock].PlayerRemove(bfblock, bfcity);

        }

        private void rectcity() //건물들의 크기및 좌표 지정
        {
            for (int i = 0; i < 4; i++)
            {
                buildrect[i] = new Rectangle[9][];

                for (int l = 0; l < 9; l++)
                {
                    int n = 0;
                    buildrect[i][l] = new Rectangle[4];

                    for (int j = 0; j < 3; j++)
                    {

                        if (i == 0)
                        {

                            buildrect[i][l][j + 1] = City.citybuildA(area.cities[i][l].X + n, area.cities[i][l].Y);
                            n += 25;
                        }
                        else if (i == 2)
                        {
                            buildrect[i][l][j + 1] = City.citybuildA(area.cities[i][l].X + n, 125);
                            n += 25;
                        }
                        else if (i == 1)
                        {
                            buildrect[i][l][j + 1] = City.citybuildB(125, area.cities[i][l].Y + n);
                            n += 25;
                        }
                        else if (i == 3)
                        {
                            buildrect[i][l][j + 1] = City.citybuildB(area.cities[i][l].X, city[i].cities[l].Y + n);
                            n += 25;
                        }
                        //buildrect[i][l][0] = new Rectangle(new Point(1, 1), new Size(1, 0));
                    }
                }
            }
        }

        //다른 말들도 설정할 수 있게 건드려야 함.
      
        private void image() //주사위와 황금 카드들의 이미지 및 크기 지정.
        {
            const int Dice_X = 98;
            const int DICE_Y = 95;
            const int X_MARGIN = 10;
            const int Y_MARGIN = 4;
            Image dice2 = Properties.Resources.dice;
            dice1 = new Bitmap(dice2);
            Rectangle dice = new Rectangle(17, 13, 98, 95);
            Image card1 = Properties.Resources.keycard_3;
            cardg = new Bitmap(card1);
            Image cardred = Properties.Resources.keycard_6;
            card_red = new Bitmap(cardred);
            for (int y = 13; y <= DICE_Y * 2 + Y_MARGIN * 2 + 13; y += DICE_Y + Y_MARGIN)
            {
                for (int x = 17; x <= Dice_X + X_MARGIN + 17; x += Dice_X + X_MARGIN)
                {
                    Rectangle dice3 = new Rectangle(x, y, 98, 95);
                    Dicelist.Add(dice3);
                }
            }
            CardList.Add(new Rectangle(0, 0, 182, 393));//이미지에서의 키 카드 크기
            CardList.Add(new Rectangle(189, 0, 200, 280));//이미지에서의 황금열쇠 뒷면 크기

        }

        private void CardGraphic()
        {
            key = new Bitmap(this.Width, this.Height);
            Card = Graphics.FromImage(key);
        }

      

    }
}
