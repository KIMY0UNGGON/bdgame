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
            Card_red();
        }
        private void timer3_Tick(object sender, EventArgs e)//자신의 턴이 돌아왔는지 확인하는 용도.
        {
            if (Turn)
                button1.Enabled = true;
            //  else
            //      button1.Enabled = false;



        }
    }
}
