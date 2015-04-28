using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApps
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            btnStreamTesting.Click += btnStreamTesting_Click;
        }

        void btnStreamTesting_Click(object sender, EventArgs e)
        {
            StreamTest _test = new StreamTest(@"C:\StreamTest.txt", "C:\\StreamTest_Tgt.txt");
            _test.Run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I'm thinking about life, please do not disturb me");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here is the GIT Club,Are you a member yet?");
        }
    }
}
