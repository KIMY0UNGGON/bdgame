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
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e) 
        {
           
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            Card_red();//열쇠 칸인지 확인.
            if (Turn)//자신의 턴이 돌아왔는지 확인하는 용도.
                button1.Enabled = true;
            if (Is_Uninhabited()) //지금 위치가 무인도인지.
            {
                int n = 4 - island; 
                if (island == 0)
                    n = 3;
                MessageBox.Show("무인도에 도착하였습니다. 앞으로 " + n.ToString() + "턴동안 무인도에서 나갈수 없습니다.");
                if (nowcity == 9 && nowblock == 0 && island < 3) //턴이 넘어갔을 때 아직 탈출하지 못하면 턴을 줄들게 함.
                    island++;

            }

        }
    }
}
