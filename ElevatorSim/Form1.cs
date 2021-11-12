using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DisplayMode()
        {
            if (cBMode.Checked)
            {
                tBmodeText.Text = "Modo automático";
                tBmodeText.BackColor = Color.LimeGreen;
            }
            else
            {
                tBmodeText.Text = "Modo manual";
                tBmodeText.BackColor = Color.Orange;
            }
        }

        private void CheckedChangedEvent(object sender, EventArgs e)
        {
            DisplayMode();
        }
    }
}
