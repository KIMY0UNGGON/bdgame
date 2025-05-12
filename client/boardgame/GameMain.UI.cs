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
            server.send("T" + color.ToString()); //선택한 컬러를 서버에 다시 전송.
            //server.send($"{Multy_Num}READY"); //현재 클라이언트 토큰 선택 완료.
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(key, 0, 0);
            e.Graphics.DrawImageUnscaled(bit, 0, 0);
            

            e.Graphics.DrawImageUnscaled(nameBt, 0, 0);
            e.Graphics.DrawImageUnscaled(map, 0, 0);
            

        }

        private bool LOSE_CONDITION()
        {
            if (money.m.CompareTo(0) <= 0) return true;
            return false;
        }



        private void Self_init(int color) //자기자신의 토큰 넘버와 비트맵컬러 저장.
        {
            MultyPlayers[Multy_Num - 1].Add(player_1[color]);
            MultyPlayers[Multy_Num - 1].Add(player_2[color]);
            ColorSet(Multy_Num - 1, color);
        }
        Color[] TokenColor = new Color[Multy_Count];
        private void Multy_init(int Num, int color)
        {

            MultyPlayers[Num].Add(player_1[color]);
            MultyPlayers[Num].Add(player_2[color]);
            ColorSet(Num, color);
            //TokenColor[Num] = 
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
        private void init()
        {

            int Client_num = Multy_Num - 1;


            //List<Bitmap> thisplayer = new List<Bitmap>();
        
            if (!Multi) //솔로 플레이.
            {
                MultyPlayers[0].Add(new Bitmap(player_1[0]));
                MultyPlayers[0].Add(new Bitmap(player_2[0]));
                Client_num = 0;
            }
            else
            {
                for (int i = 0; i < Multy_Count; i++)
                {
                    Nowcity_List.Add(9);
                    Nowblock_List.Add(3);
                    Before_city.Add(9);
                    Before_block.Add(3);
                }
            }
            Image cardspace = Properties.Resources.areacard;
            areacard = new Bitmap(cardspace);
            Image cardspace1 = Properties.Resources.areacard2;
            areacard1 = new Bitmap(cardspace1);
            Image cardspace2 = Properties.Resources.areacard3;
            areacard2 = new Bitmap(cardspace2);
            Image cardspace3 = Properties.Resources.areacard1;
            areacard3 = new Bitmap(cardspace3);
            Image img = Properties.Resources.사회복지기금;
            social = new Bitmap(img);
            
            city[0] = new cityArea(0, DC, bit, (City.s2.Height * 10), (City.s1.Width * 9 + City.fours.Width), 0, -City.s1.Width, Card, areacard, social, nameGp, MultyPlayers, Arch_GP,Client_num);
            city[1] = new cityArea(1, DC, bit, 0, (City.s2.Height * 10), 1, -City.s2.Height, Card, areacard1, social, nameGp, MultyPlayers, Arch_GP, Client_num);
            city[2] = new cityArea(2, DC, bit, City.s1.Width * 2, 0, 2, City.s1.Width, Card, areacard2, social, nameGp, MultyPlayers, Arch_GP, Client_num);
            city[3] = new cityArea(3, DC, bit, (City.s2.Height * 9 + City.fours.Height), City.s2.Height * 2, 3, City.s2.Height, Card, areacard3, social, nameGp, MultyPlayers, Arch_GP, Client_num);

            city[0].drawcity(Color.Red, 0);
            city[1].drawcity(Color.DarkGreen, 1);
            city[2].drawcity(Color.Brown, 2);
            city[3].drawcity(Color.Gray, 3);

            for (int i = 0; i < 4; i++)
            {
                city[i].drawname(i); //구역들의 도시 이름을 그리는 메소드.
            }
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

                            buildrect[i][l][j + 1] = City.citybuildA(city[i].cities[l].X + n, city[i].cities[l].Y);
                            n += 25;
                        }
                        else if (i == 2)
                        {
                            buildrect[i][l][j + 1] = City.citybuildA(city[i].cities[l].X + n, 125);
                            n += 25;
                        }
                        else if (i == 1)
                        {
                            buildrect[i][l][j + 1] = City.citybuildB(125, city[i].cities[l].Y + n);
                            n += 25;
                        }
                        else if (i == 3)
                        {
                            buildrect[i][l][j + 1] = City.citybuildB(city[i].cities[l].X, city[i].cities[l].Y + n);
                            n += 25;
                        }
                        //buildrect[i][l][0] = new Rectangle(new Point(1, 1), new Size(1, 0));
                    }
                }
            }
        }

        //다른 말들도 설정할 수 있게 건드려야 함.
        private void Place_Multy()
        {

                
            for (int i = 0; i < Multy_Count; i++) //리스트의 개수를 추가.
            {
                Multy_Token[i] = new List<Player>[4]; //4개의 배열안에 각각 4개의 구역에 해당하는 배열 초기화
                for (int l = 0; l < 4; l++)
                { //초기화된 구역안에 리스트 인스턴스 생성.
                    Multy_Token[i][l] = new List<Player>();
                }

            }

            for (int j = 0; j < Multy_Count; j++) //클라이언트의 숫자 만큼 반복.
            {
                for (int i = 0; i < 4; i++) //구역만큼 반복. 플레이어 토큰의 위치를 추가.
                {
                    playerrect[i] = new Rectangle[10]; //구역간 플레이어가 갈 수 있는 열개의 칸
                    if (i % 2 == 0) //0번구역과 2번구역일때.
                    {
                        for (int a = 0; a < 9; a++) //10개의 칸 중 9개의 칸만 반복. 마지막은 칸의 크기가 달라 따로 지정.
                        {

                            Rectangle pl = new Rectangle(new Point(city[i].cities[a].X , city[i].cities[a].Y + (j * 35)), new Size(75, 64));
                            playerrect[i][a] = pl;
                            Player player = new Player(playerrect, i, a);
                            Multy_Token[j][i].Add(player);
                        }
                        Rectangle Last_Block; //마지막 블럭에서의 위치.
                        if (j < 2)
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X + (Player.Width_1*j), city[i].cities[9].Y), new Size(75, 64));
                        }
                        else
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X + ( Player.Width_1 * (j-2)), city[i].cities[9].Y + (Player.Height_1*(j-1)+10)), new Size(75, 64));
                        }
                        playerrect[i][9] = Last_Block;
                        Player player2 = new Player(playerrect, i, 9);
                        Multy_Token[j][i].Add(player2);

                    }
                    else //1,3번구역.
                    {
                        for (int a = 0; a < 9; a++) //10개의 칸 중 9개의 칸만 반복. 마지막은 칸의 크기가 달라 따로 지정.
                        {
                            Rectangle pl = new Rectangle(new Point(city[i].cities[a].X + (j *35), city[i].cities[a].Y ), new Size(64, 75));
                            playerrect[i][a] = pl;
                            Player player = new Player(playerrect, i, a);
                            Multy_Token[j][i].Add(player);

                        }
                        Rectangle Last_Block; //마지막 블럭에서의 위치.
                        if (j < 2)
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X  + (Player.Width_1 * j), city[i].cities[9].Y ), new Size(75, 64));
                        }
                        else
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X , city[i].cities[9].Y + (Player.Height_1 + 10)), new Size(75, 64));
                        }
                        playerrect[i][9] = Last_Block;
                        Player player2 = new Player(playerrect, i, 9);
                        Multy_Token[j][i].Add(player2);
                    }
                }
            }
        }
        private void Place_Solo() //플레이어의 말 크기와 좌표, 도시의 구역들의 좌표 지정.
        {
            for (int i = 0; i < 4; i++) //리스트의 개수를 추가.
            {

                players[i] = new List<Player>();


            }

            for (int i = 0; i < 4; i++) //플레이어 토큰의 위치를 추가.
            {
                playerrect[i] = new Rectangle[10];
                if (i == 0 || i == 2)
                {
                    for (int a = 0; a < 10; a++)
                    {

                        Rectangle pl = new Rectangle(new Point(city[i].cities[a].X, city[i].cities[a].Y), new Size(75, 64));
                        playerrect[i][a] = pl;
                        Player player = new Player(playerrect, i, a);
                        players[i].Add(player);
                    }
                }
                else
                {
                    for (int a = 0; a < 10; a++)
                    {
                        Rectangle pl = new Rectangle(new Point(city[i].cities[a].X, city[i].cities[a].Y), new Size(64, 75));
                        playerrect[i][a] = pl;
                        Player player = new Player(playerrect, i, a);
                        players[i].Add(player);

                    }
                }
            }
        }
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

        private void initDC()
        {

            bit = new Bitmap(this.Width, this.Height);
            DC = Graphics.FromImage(bit);
            map = new Bitmap(this.Width, this.Height);
            dices = Graphics.FromImage(map);
            key = new Bitmap(this.Width, this.Height);
            Card = Graphics.FromImage(key);
            nameBt = new Bitmap(this.Width, this.Height);
            nameGp = Graphics.FromImage(nameBt);
            Arch = new Bitmap(this.Width, this.Height);
            Arch_GP = Graphics.FromImage(Arch);
        }
        private void initMultyImage() //멀티플레이어의 이미지가 담긴 리스트의 인스턴스들 생성.
        {
            for (int i = 0; i < MultyPlayers.Length; i++) {
                MultyPlayers[i] = new List<Bitmap>();
            }
        }
        private void initImage()
        {
            Image play = Properties.Resources.p1;
            player_1.Add(new Bitmap(play));

            Image play1 = Properties.Resources.p1_2;
            player_2.Add(new Bitmap(play1));

            play = Properties.Resources.p2;
            player_1.Add(new Bitmap(play));
            play1 = Properties.Resources.p2_2;
            player_2.Add(new Bitmap(play1));

            play = Properties.Resources.p3;
            player_1.Add(new Bitmap(play));
            play1 = Properties.Resources.p3_2;
            player_2.Add(new Bitmap(play1));

            play = Properties.Resources.p4;
            player_1.Add(new Bitmap(play));
            play1 = Properties.Resources.p4_2;
            player_2.Add(new Bitmap(play1));
        }

    }
}
