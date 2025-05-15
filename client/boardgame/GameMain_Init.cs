using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class GameMain
    {
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
            for (int i = 0; i < MultyPlayers.Length; i++)
            {
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


        private void BCCard_init()
        {
            for (int i = 0; i < name.Length; i++)
            {
                name[i] = new List<string>();
            }
            string Card_Text = Properties.Resources.build;
            int count = 0;
            foreach (var Text in Card_Text.Split('\n'))
            {
                string[] txtSplit = Text.Split(','); //도시 이름
                name[count].Add(txtSplit[0]);
                if (count != 3 && name[count].Count == 7)
                    count++;
                else if (count == 3 && name[count].Count == 8)
                    count++;
            }

        }

        private void Self_init(int color) //자기자신의 토큰 넘버와 비트맵컬러 저장.
        {
            MultyPlayers[0].Add(player_1[color]);
            MultyPlayers[0].Add(player_2[color]);
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

        private void init()
        {

            int Client_num = Multy_Num - 1;

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

            city[0] = new cityArea(0, DC, bit, (City.s2.Height * 10), (City.s1.Width * 9 + City.fours.Width), 0, -City.s1.Width, Card, areacard, social, nameGp, Players_Token, Arch_GP);
            city[1] = new cityArea(1, DC, bit, 0, (City.s2.Height * 10), 1, -City.s2.Height, Card, areacard1, social, nameGp, Players_Token, Arch_GP);
            city[2] = new cityArea(2, DC, bit, City.s1.Width * 2, 0, 2, City.s1.Width, Card, areacard2, social, nameGp, Players_Token, Arch_GP);
            city[3] = new cityArea(3, DC, bit, (City.s2.Height * 9 + City.fours.Height), City.s2.Height * 2, 3, City.s2.Height, Card, areacard3, social, nameGp, Players_Token, Arch_GP);
            city[0].play.ForEach(x => x.Activate(3,9));



            city[0].drawcity(Color.Red, 0);
            city[1].drawcity(Color.DarkGreen, 1);
            city[2].drawcity(Color.Brown, 2);
            city[3].drawcity(Color.Gray, 3);

            for (int i = 0; i < 4; i++)
            {
                city[i].drawname(i); //구역들의 도시 이름을 그리는 메소드.
            }
        }

        private void initPlace_Multy()
        {
            for (int j = 0; j < Multy_Count; j++) //클라이언트의 숫자 만큼 반복.
            {
                  
                for (int i = 0; i < 4; i++) //구역만큼 반복. 플레이어 토큰의 위치를 추가.
                {
                    playerrect[i] = new Rectangle[10]; //구역간 플레이어가 갈 수 있는 열개의 칸
                    if (i % 2 == 0) //0번구역과 2번구역일때.
                    {
                        for (int a = 0; a < 9; a++) //10개의 칸 중 9개의 칸만 반복. 마지막은 칸의 크기가 달라 따로 지정.
                        {

                            Rectangle pl = new Rectangle(new Point(city[i].cities[a].X, city[i].cities[a].Y + (j * 35)), new Size(75, 64));
                            playerrect[i][a] = pl;

                        }
                        Rectangle Last_Block; //마지막 블럭에서의 위치.
                        if (j < 2)
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X + (Player.Width_1 * j), city[i].cities[9].Y), new Size(75, 64));
                        }
                        else
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X + (Player.Width_1 * (j - 2)), city[i].cities[9].Y + (Player.Height_1 * (j - 1) + 10)), new Size(75, 64));
                        }
                        playerrect[i][9] = Last_Block;
                    }
                    else //1,3번구역.
                    {
                        for (int a = 0; a < 9; a++) //10개의 칸 중 9개의 칸만 반복. 마지막은 칸의 크기가 달라 따로 지정.
                        {
                            Rectangle pl = new Rectangle(new Point(city[i].cities[a].X + (j * 35), city[i].cities[a].Y), new Size(64, 75));
                            playerrect[i][a] = pl;
                        }
                        Rectangle Last_Block; //마지막 블럭에서의 위치.
                        if (j < 2)
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X + (Player.Width_1 * j), city[i].cities[9].Y), new Size(75, 64));
                        }
                        else
                        {
                            Last_Block = new Rectangle(new Point(city[i].cities[9].X, city[i].cities[9].Y + (Player.Height_1 + 10)), new Size(75, 64));
                        }
                        playerrect[i][9] = Last_Block;
                    }
                }
                var player = new Player(playerrect, MultyPlayers[j].ToArray());

                Players_Token.Add(player);
            }
        }
        private void Place_Solo() //플레이어의 말 크기와 좌표, 도시의 구역들의 좌표 지정.
        {
            for (int i = 0; i < 4; i++) //플레이어 토큰의 위치를 추가.
            {
                playerrect[i] = new Rectangle[10];
                if (i == 0 || i == 2)
                {
                    for (int a = 0; a < 10; a++)
                    {

                        Rectangle pl = new Rectangle(new Point(city[i].cities[a].X, city[i].cities[a].Y), new Size(75, 64));
                        playerrect[i][a] = pl;

                    }
                }
                else
                {
                    for (int a = 0; a < 10; a++)
                    {
                        Rectangle pl = new Rectangle(new Point(city[i].cities[a].X, city[i].cities[a].Y), new Size(64, 75));
                        playerrect[i][a] = pl;


                    }
                }
                Players_Token.Add(new Player(playerrect, MultyPlayers.First().ToArray()));
            }
        }
    }
}
