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
        bool test_mode = true;
        bool player_stop = true;
        public Point[] C1 = { new Point(500, 300), new Point(400, 200), new Point(200, 400), new Point(300, 500) };


        Bitmap bit, map, key, nameBt, Arch;
        Graphics DC, dices, Card, nameGp, Arch_GP;
        Bitmap dice1, cardg, card_red;
        //Area area;
        cityArea[] city = new cityArea[4]; //cityArea 들의 리스트 도시들의 구역들을 지정하고 그리기 위한 클래스 들임.


        Bitmap areacard, areacard1, areacard2, areacard3;
        Bitmap social;
        public List<buildCard> bclist_infor = new List<buildCard>();
        List<buildCard> bclist_all = new List<buildCard>(); //건물 카드들이 들어있는 리스트

        static int Multy_Count { get; set; } = 2;
        public int island { get; set; } = 0; //무인도에 들어가있는 턴

        public bool Uninhabited { get; set; } = false; //무인도에 있는지

        public List<Rectangle> Dicelist = new List<Rectangle>();
        public List<Rectangle> CardList = new List<Rectangle>();
        public List<String> CardText = new List<String>();
        List<Player>[] players = new List<Player>[4];
        List<Player>[][] Multy_Token = new List<Player>[Multy_Count][]; //[][][] 각각 클라이언트 번호, 구역, 블럭 순.
        Dictionary<Player,KeyValuePair<Point,Point>> MultyTest = new Dictionary<Player, KeyValuePair<Point, Point>>();
        
        List<Button> buttonTravel = new List<Button>();

        bool Carddr = false;
        int Multy_Num { get; set; } = -1; //현재 이 클라이언트의 번호를 나타내는 변수.
        Money money = new Money();
        bool Multy_Wait = true;
        bool Multi = false;

        public int nowcity { get; set; } = 9;//현재 구역의 도시 번호
        public int nowblock { get; set; } = 3; //현재 구역의 번호.
        int bfcity { get; set; } = 9; //전의 구역의 도시 번호
        int bfblock { get; set; } = 3; //전에 있던 구역의 번호.

        List<int> Nowcity_List = new List<int>();
        List<int> Nowblock_List = new List<int>();
        List<int> Before_city = new List<int>();
        List<int> Before_block = new List<int>();

        public int cardnum;
        public int[] buildnum = new int[36];
        Rectangle[][] playerrect = new Rectangle[4][];
        Rectangle[][][] buildrect = new Rectangle[4][][];

        public int money_so { get; set; } = 100;
        Rectangle[][] buyground = new Rectangle[4][];


        information inform = new information();

        public List<string>[] name = new List<string>[4];

        int skip = 0;

        MySocket server;  //서버 연결시 사용하는 소켓



        public bool b_building, b_villa, b_ground, b_hotel; //현재 땅에 어떤 건물이 지어졌는지 확인하는 bool 변수.


        int color_num { get; set; } = -1; //토큰의 색. 0번 하늘색, 1번 검정색, 2번 빨간색, 3번 회색
        List<Bitmap> player_1 = new List<Bitmap>(); //플레이어 말의 가로.
        List<Bitmap> player_2 = new List<Bitmap>(); //플레이어 말의 세로
        List<Bitmap>[] MultyPlayers = new List<Bitmap>[Multy_Count]; //플레이어들의 비트맵을 저장.

        public Point[] C = { new Point(500, 300), new Point(400, 200), new Point(300, 500) }; //카드의 크기.
        bool card_clicked = false;


        public List<Card> cards = new List<Card>(); //카드들이 들어있는 리스트.



        int hotel = 0;
        int building = 0;
        int bul = 0;

        bool start_confirm = false; //게임이 시작을 했는지 확인하는변수.

        int loopconfirm = 0; //  보드를 몇바퀴 돌았는 확인하는 변수.
        enum Build_Confirm { GROUND, VILLA, BUILDING, HOTEL };


        bool Turn = false;
        byte[] receiverBuff = new byte[8192];


        bool Test_Turn = false;
        bool Inhabit_move = false;
    }
}
