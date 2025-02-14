using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class GameMain
    {

        bool Test_Turn = false;
        bool Inhabit_move = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            Card_red();//열쇠 칸인지 확인.

            //지금 위치가 무인도인지 확인.
            if (Is_Uninhabited()) //지금 위치가 무인도인지 확인.
            {
                

                if (island <= 3 && Test_Turn &&  player_stop) //턴이 넘어갔을 때 아직 탈출하지 못하면 턴을 줄들게 함. + 현재 자신의 턴인지 확인. 턴은 말을 움직이면 변경됨.
                    //테스트 모드 및 솔로모드일때의 무인도. 멀티일때는 Turn 사용.
                {
                    Test_Turn = !Test_Turn;
                    if (!Inhabit_move)
                        Inhabit_move = true;
                    
                    island++;
                    MessageBox.Show("무인도에 도착하였습니다. 앞으로 " + (5-island).ToString() + "턴동안 무인도에서 나갈수 없습니다.");
                }
                if(island == 4)
                {

                    island = 0;
                    Inhabit_move = false;
                   
                }

            }


        }
    }
}
