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
       // int number = 0;
        //Button[] butt;
        //Button butt;// = new Button();
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
            textBox1.Text = money.ToString() + "만원";
            //StreamReader sr = new StreamReader(new FileStream("Save.txt",FileMode.OpenOrCreate));
            //   int test = 0;
            ////butt = new Button[4];
            //while (sr.EndOfStream == false)
            //{
            //    Button butt = new Button();

            //    butt.Text = sr.ReadLine();
            //    butt.Size = new System.Drawing.Size(75, 52);
            //    butt.Location = new System.Drawing.Point(test, 0);
            //    butt.Click += butt_Click;

            //    Controls.Add(butt);
            //    //LButton.Add(butt);
            //    test += 80;
            //    //number++;

            //}
            //sr.Close();
            build_save();
            Card_save();


            timer1.Start();
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
        //private void save_build()
        //{
        //    StreamReader build = new StreamReader(new FileStream("buildSave.txt", FileMode.Open));


        //     List<int> color = new List<int>();
        //    //int color = -1;
        //    int count = buildstring.Count;
        //    while (build.EndOfStream == false)
        //    {
        //        string[] spl = build.ReadLine().Split(new char[] { '|' });
        //        buildstring.Add(spl[0]);
        //        color.Add(Convert.ToInt32(spl[2]));
        //    }
        //       // color = Convert.ToInt32(spl[2]);
        //    for (int i = count; i< buildstring.Count; i++)
        //    {
        //        buildsave.Add(new Button());

        //        buildsave[i].Text = buildstring[i];
        //        buildsave[i].Size = new System.Drawing.Size(75, 52);
        //        buildsave[i].Location = new System.Drawing.Point(cardloc, locy);
        //        buildsave[i].Click += build_click;
        //        //if (color[i] == 0)
        //        //    buildsave[i].BackColor = Color.Red;
        //        //else if (color[i] == 1)
        //        //    buildsave[i].BackColor = Color.DarkGreen;
        //        //else if (color[i] == 2)
        //        //    buildsave[i].BackColor = Color.Brown;
        //        //else
        //        //    buildsave[i].BackColor = Color.Gray;
        //        if (buildstring.Count >= 2)
        //        {
        //            if (cardloc > 500)
        //                locy += 60;
        //            else if (locy >= 200)
        //                locy = 200;
        //            else
        //                cardloc += 80;
        //        }
        //        Controls.Add(buildsave[i]);

        //        //i++;
        //    }
        //    build.Close();

        //    //for (int i = buildsave.Count; i<= buildstring.Count; i++)//string s in buildstring)
        //    //{


        //}

        private int numberconfirm(Button btn)
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




        private void export_build(object sender)
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

            // StreamReader sr = new StreamReader(new FileStream("buildSave.txt", FileMode.Open));
            //while (sr.EndOfStream == false)
            //{
            //    string cardte = sr.ReadLine();
            //    string[] spl = cardte.Split(new char[] { '|' });

            //    if (spl[0].Equals(card.Text))
            //    {
            //        num = spl[1];
            //        color = Convert.ToInt32(spl[2]);
            //        continue;
            //    }
            //    cardst.Add(cardte);
            //}

            // sr.Close();
            //StreamWriter sw = new StreamWriter(new FileStream("buildSave.txt", FileMode.Create));
            //for (int i = 0; i < cardst.Count; i++)
            //    sw.WriteLine(cardst[i]);
            //sw.Close();

            //if (f5.loca <= 6)
            //    f5.color = 0;
            //else if (f5.loca <= 13)
            //    f5.color = 1;
            //else if (f5.loca <= 21)
            //    f5.color = 2;
            //else
            //    f5.color = 3;

            //cardst.Clear();
            //Card_b.Clear();

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
            //testremove()

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
            
            //StreamReader sr = new StreamReader(new FileStream("Save.txt", FileMode.Open));
            //while (sr.EndOfStream == false)
            //{
            //    string test = sr.ReadLine();
            //    if (test.Equals(bu.Text))
            //    {
            //        continue;
            //    }
            //    st.Add(test);
            //}
            //sr.Close();
            //StreamWriter sw = new StreamWriter(new FileStream("Save.txt", FileMode.Create));
            //for (int i = 0; i < st.Count; i++)
            //    sw.WriteLine(st[i]);
            //sw.Close();
            
            Card F2 = new Card();
            F2.resu = Cards[num].resu;
            F2.number = Cards[num].number;
            Controls.Remove(bu);
            cardbutt.RemoveAt(num);
            Card_Remove(Cards[num]);
            //Cards.RemoveAt(num);
           
            F2.Show();
            cardx -= 80;

            //this.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();

        }
        private void money_print()
        {

            StreamReader sr1 = new StreamReader(new FileStream("money.txt", FileMode.Open));

            textBox1.Text = sr1.ReadLine().ToString() + "만원";
            sr1.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            money_print();
            Card_save();
            build_save();
            
        }


        private void Card_save()
        {
            //StreamReader sr = new StreamReader(new FileStream("Save.txt", FileMode.OpenOrCreate));
            //int test = 0;
            ////butt = new Button[4];
            //while (sr.EndOfStream == false)
            //{
            //    Button butt = new Button();

            //    butt.Text = sr.ReadLine();
            //    butt.Size = new System.Drawing.Size(75, 52);
            //    butt.Location = new System.Drawing.Point(test, 0);
            //    butt.Click += butt_Click;

            //    Controls.Add(butt);
            //    //LButton.Add(butt);
            //    test += 80;

            //    //number++;
            //}
            //sr.Close();
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