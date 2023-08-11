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
    public partial class socialfund : Form
    {
        public socialfund()
        {
            InitializeComponent();
        }
        public int money;
    
        private void Form4_Load(object sender, EventArgs e)
        {
            if (money == 0)
            {
                textBox1.Text = money.ToString() + "원" ;
            }
            else
            {
                textBox1.Text = money.ToString() + "만원" ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
