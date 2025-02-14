using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace boardgame
{
    public partial class information : Form
    {
        public information()
        {
            InitializeComponent();
        }




        public double money = 0;
        public bool Multi { get; set; } = false;
        List<string> st = new List<string>();

        List<Button> cardbutt = new List<Button>();
        List<Button> buildsave = new List<Button>();
        List<buildCard> Card_b = new List<buildCard>();
        List<Card> Cards = new List<Card>();

        string name;
        private void build_retrieve()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            if (gm != null)
            {
                this.Card_b = gm.bclist_infor;
                this.Cards = gm.cards;
                this.name = gm.delete_name;
            }

        }

        private void build_remove(buildCard bc)
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            
            if (gm != null) { gm.bclist_infor.Remove(bc); }

        }

        private void Card_Remove(Card card)
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;
            
            if (gm != null) { gm.cards.Remove(card); }

        }



        private void name_init()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain;

            if (gm != null) { gm.delete_name = null; }

        }





        public void Card_put(buildCard bc)
        {
            Card_b.Add(bc);

        }


        Money mo = new Money();
        int cardloc = 0;
        int locy = 60;
        int cardx = 0;
  

        bool card;

        private void DataReceiveEvent(bool data)
        {
            card = data;
        }




        // List<string> buildstring = new List<string>();
        // GameMain frm1;

        

        private void Form3_Load(object sender, EventArgs e)
        {
            if (!Multi)
            {
                textBox1.Text = money.ToString() + "만원";

                build_save();
                Card_save();


                timer1.Start();
            }
            else //멀티 모드
            {
                textBox1.Text = money.ToString() + "만원";
            }
        }

        private void build_save()
        {
            build_retrieve();
            if (Card_b.Count > buildsave.Count)
            {
                buildsave.Add(new Button());
                int i = buildsave.Count - 1;
                int color;
                buildCard bc = Card_b[i];
                buildsave[i].Text = bc.Cards[0];
                buildsave[i].Size = new System.Drawing.Size(75, 52);
                buildsave[i].Location = new System.Drawing.Point(cardloc, locy);
                buildsave[i].Click += build_click;
                color = bc.color;
                if (color == 0)
                    buildsave[i].BackColor = Color.Red;
                else if (color == 1)
                    buildsave[i].BackColor = Color.DarkGreen;
                else if (color == 2)
                    buildsave[i].BackColor = Color.Brown;
                else
                    buildsave[i].BackColor = Color.Gray;

                if (cardloc > 500)
                {
                    locy += 60;
                    cardloc = 0;
                }
                else if (locy >= 200)
                    locy = 200;
                else
                    cardloc += 80;
             
                Controls.Add(buildsave[i]);
            }

        }

        private int numberconfirm(Button btn) //번호 확인. 
        {
            int n = -1;
            for (int i = 0; i < buildsave.Count; i++)
            {
                if (btn.Text.Equals(buildsave[i].Text))
                {
                    n = i;
                }

            }
            return n;
        }




        private void export_build(object sender) // 넣어둔 빌드 카드를 다시 꺼냄.
        {
            Button card = sender as Button;
            int b_num = numberconfirm(card);
            
            
            buildCard card_data = Card_b[b_num];


            buildCard f5 = new buildCard();
            f5.loca = card_data.loca;
            f5.color = card_data.color;


            Controls.Remove(card);
            buildsave.RemoveAt(b_num);
            build_remove(card_data);
            Card_b.Remove(card_data);
            f5.Show();
            Invalidate();


            cardloc -= 80;

            

            if (buildsave.Count != 0)
            {
                for(int i = b_num; i < buildsave.Count ; i++)
                {
                    if(buildsave[i].Location.X != 0)
                        buildsave[i].Location = new System.Drawing.Point(buildsave[i].Location.X - 80, buildsave[i].Location.Y);
                        
                    else
                        buildsave[i].Location = new System.Drawing.Point(buildsave[i-1].Location.X+80, buildsave[i].Location.Y-60);
                }
            }
    

        }
        private void build_click(object sender, EventArgs e)
        {
            export_build(sender);
        }

        private void butt_Click(object sender, EventArgs e)
        {
            Button bu = sender as Button;
            int num = -1;
            for (int i = 0; i < Cards.Count; i++)
                if (bu.Text.Equals(Cards[i].texts))
                {
                    num = i;
                    break;
                }
            
          
            
            Card F2 = new Card();
            F2.resu = Cards[num].resu;
            F2.number = Cards[num].number;
            Controls.Remove(bu);
            cardbutt.RemoveAt(num);
            Card_Remove(Cards[num]);
  
           
            F2.Show();
            cardx -= 80;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();

        }
        private void money_print()
        {
            GameMain gm = Application.OpenForms["GameMain"] as GameMain; //열려있는 메인 게임의 폼을 가지고 옴.
           

            textBox1.Text = gm.money_retur().ToString() + "만원";
 
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            money_print();
            Card_save();
            build_save();
            
        }


        private void Card_save()
        {
           
            build_retrieve();
            if (Cards.Count > cardbutt.Count)
            {
                cardbutt.Add(new Button());
                int i = cardbutt.Count - 1;
              
                Card card = Cards[i];
                cardbutt[i].Text = Cards[i].texts;
                cardbutt[i].Size = new System.Drawing.Size(75, 52);
                cardbutt[i].Location = new System.Drawing.Point(cardx, 0);
                cardbutt[i].Click += butt_Click;


                cardx += 80;

                Controls.Add(cardbutt[i]);
            }
            if(buildsave.Count > Card_b.Count)
            {
                for(int i = 0; i< buildsave.Count; i++)
                {
                    if (buildsave[i].Text.Equals(name))
                    {
                        int b_num = numberconfirm(buildsave[i]);
                        Controls.Remove(buildsave[i]);
                        buildsave.Remove(buildsave[i]);
                        cardloc -= 80;

                        if (buildsave.Count != 0)
                        {
                            for (int a = b_num; a < buildsave.Count; a++)
                            {
                                if (buildsave[a].Location.X != 0)
                                    buildsave[a].Location = new System.Drawing.Point(buildsave[a].Location.X - 80, buildsave[a].Location.Y);

                                else
                                    buildsave[a].Location = new System.Drawing.Point(buildsave[a - 1].Location.X + 80, buildsave[a].Location.Y - 60);
                            }
                        }
                        break;
                    }
                }
                name_init();
            }
        }
    }
}