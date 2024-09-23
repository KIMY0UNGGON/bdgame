using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boardgame
{
    public partial class GameMain
    {
        bool start_confirm = false; //게임이 시작을 했는지 확인하는변수.

        int loopconfirm = 0; //  보드를 몇바퀴 돌았는 확인하는 변수.
        enum Build_Confirm {GROUND,VILLA,BUILDING,HOTEL};


        //구매한 구역들 확인.
        public bool confirm_ground(int nowblock, int nowcity) //땅이 구매 가능한 상태인지 확인. 즉, 건물이 안지어졌는지 확인하는 용도.
        {
            if (city[nowblock].cityRect[nowcity].Count > 0)
                return true;
            else
                return false;
        }
        public bool confirm_Villa(int nowblock, int nowcity) //빌라가 지어졌는지 확인. 건물 1개
        {
            if (city[nowblock].Rect_Villa[nowcity].X != 0)
                return true;
            else
                return false;
        }
        public bool confirm_Building(int nowblock, int nowcity) //빌딩이 지어졌는지 확인. 건물 2개
        {
            if (city[nowblock].Rect_Building[nowcity].X != 0)
                return true;
            else
                return false;
        }
        public bool confirm_Hotel(int nowblock, int nowcity) //호텔이 지어졌는지 확인. 건물 3개.
        {
            if (city[nowblock].Rect_Hotel[nowcity].X != 0)
                return true;
            else
                return false;
        }

        private List<bool> Confirm_ALL_build(int nowblock, int nowcity)
        {
            List<bool> build_list = new List<bool>();
            build_list.Add(confirm_ground(nowblock, nowcity));
            build_list.Add(confirm_Villa(nowblock, nowcity));
            build_list.Add(confirm_Building(nowblock, nowcity));
            build_list.Add(confirm_Hotel(nowblock, nowcity));
            return build_list;

        }


                                                          
    }
}
