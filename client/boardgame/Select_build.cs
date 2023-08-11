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
    public partial class Select_build : Form
    {
        public Select_build()
        {
            InitializeComponent();
        }
        bool build,villa,hotel,ground;


        private void build_b_CheckedChanged(object sender, EventArgs e)//빌딩
        {
            if (build_b.Checked)
            {
                build_g.Checked = true;
                build_g.Enabled = false;
            }
            else if(!ground)
            {
  
                build_g.Checked = false;
                if (!ground)
                    build_g.Enabled = true;
                
            }
        }

        private void build_v_CheckedChanged(object sender, EventArgs e)//별장
        {
            if (build_v.Checked)
            {
                build_g.Checked = true;
                build_g.Enabled = false;
           
            }
            else if(!ground)
            {
         
                build_g.Checked = false;
                if (!ground)
                    build_g.Enabled = true;
                
            }
        }

        private void build_h_CheckedChanged(object sender, EventArgs e)//호텔
        {
            if (build_h.Checked)
            {
                build_g.Checked = true;
                build_g.Enabled = false;
     
            }
            else 
            {
    
                build_g.Checked = false;
                if (!ground)
                    build_g.Enabled = true;
                
            }
        }

        private void Select_build_Load(object sender, EventArgs e)
        {
            receive_data();
            if (ground)
            {
                build_g.Enabled = false;
            }
            if(villa)
                build_v.Enabled = false;
            if(build)
                build_b.Enabled = false;
            if(hotel)
                build_h.Enabled = false;
            if ((((nowblock == 0 && nowcity == 4) || (nowblock == 1 && nowcity == 4) || (nowblock == 2 && (nowcity == 4 || nowcity == 7)) || (nowblock == 3 && (nowcity == 8 || nowcity == 1)))))
            {
                build_b.Enabled = false;
                build_h.Enabled = false;
                build_v.Enabled = false;

            }
            label1.Text = name_data();
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transmit_data();
            this.Close();
        }

        private void build_g_CheckedChanged(object sender, EventArgs e)//땅
        {
            if (build_g.Enabled == false)
            {
                build_g.Checked = false;
            }
        }

        private void transmit_data()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            if (gm != null)
            {
                gm.b_ground = this.build_g.Checked;
                gm.b_villa = this.build_v.Checked;
                gm.b_building = this.build_b.Checked;
                gm.b_hotel = this.build_h.Checked;
            }
        }

        private string name_data()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            return gm.name[gm.nowblock][gm.nowcity];
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        int nowblock = -1;
        int nowcity = -1;
        private void receive_data()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            if (gm != null)
            {
                this.nowblock = gm.nowblock;
                this.nowcity = gm.nowcity;

                this.ground = gm.confirm_ground(nowblock, nowcity);
                this.villa = gm.confirm_Villa(nowblock, nowcity);
                this.build = gm.confirm_Building(nowblock, nowcity);
                this.hotel = gm.confirm_Hotel(nowblock, nowcity);
                //this.build = gm.b_building;
                //this.ground = gm.b_ground;
                //this.hotel =gm.b_hotel;
                //this.villa = gm.b_villa;
            }

            
        }
    }
}
