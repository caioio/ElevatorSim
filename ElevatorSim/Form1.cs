using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ElevatorSim
{
    public partial class Form1 : Form
    {
        private bool[] btnArray = new bool[5];
        private bool[] lampArray = new bool[5];
        public Form1()
        {
            InitializeComponent();            
        }

        public delegate void FormUpdateEventHandler(Object source, EventArgs args);
        public event FormUpdateEventHandler FormUpdateEvent;

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
            tBDebugText.Text = "Modo de controle alterado.";
        }

        private void CheckedChangedEvent(object sender, EventArgs e)
        {
            DisplayMode();
        }

        /*private void ElevatorLogicRunTask()
        {
            while (true)
            {
                for (int i = 0; i < (int)logic.FloorsNumber; i++)
                {
                    if (btnArray[i]) // btnArray
                    {
                        //bw.ReportProgress(0, String.Format("O botão {0} foi pressionado.", i));
                        //logic.AddFloorRequest((uint)i);
                        btnArray[i] = false;
                        Thread.Sleep(10);
                    }
                }
                //logic.RunElevatorLogic(30);
            }
        }*/

        private void button0_Click(object sender, EventArgs e)
        {
            btnArray[0] = true;     /* seta boão */ 
            lampArray[0] = true;    /* seta lâmpada */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnArray[1] = true;
            lampArray[1] = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnArray[2] = true;
            lampArray[2] = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnArray[3] = true;
            lampArray[3] = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btnArray[4] = true;
            lampArray[4] = true;
        }
        
        /* Carrega a thread */
        private void Form1_Load(object sender, EventArgs e)
        {
            LogicProcess process = new LogicProcess();

            process.LogicUpdateEvent += process.OnUpdate;
            process.LogicUpdateEvent += process.ElevatorLogicRunTask;

        }

    }

    public class LogicProcess
    {
        public uint closer_floor;
        public bool is_moving;
        public double position;

        public LogicProcess()
        {
            closer_floor = 0;
            is_moving = false;
            position = 0.0d;
        }

        static private ElevatorLogic logic = new ElevatorLogic(5, 3.0d, 0.12d);

        public delegate void LogicUpdateEventHandler(Object source, EventArgs args);

        public event LogicUpdateEventHandler LogicUpdateEvent;

        private bool[] btnArray = new bool[5];

        public void OnUpdate(Object source, EventArgs e)
        {
            closer_floor = logic.CloserFloor;
            is_moving = logic.IsMoving;
            position = logic.Position;
        }
        public void ElevatorLogicRunTask(object sender, EventArgs e)
        {
            while (true)
            {
                for (int i = 0; i < (int)logic.FloorsNumber; i++)
                {
                    if (btnArray[i]) // btnArray
                    {
                        logic.AddPannelRequest((uint)i);
                        btnArray[i] = false;
                        Debug.WriteLine("O botão {0} foi pressionado.", i);
                        Thread.Sleep(10);
                    }
                }
                logic.RunElevatorLogic(30);
                if (logic.CloserFloor == logic.FloorRequested)
                {
                    
                }
            }
        }
    }
}
