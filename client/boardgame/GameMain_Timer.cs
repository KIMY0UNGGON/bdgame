using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
