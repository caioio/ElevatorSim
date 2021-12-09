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
        private BackProcess bck;
        private ElevatorLogic logic;
        private Task backProcTask;
        private Dictionary<uint, ButtonBase> elevatorButtons = new Dictionary<uint, ButtonBase>();

        public delegate MethodInvoker InvokeLogic();


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
                bck.RunRandomCaller();
            }
            else
            {
                tBmodeText.Text = "Modo manual";
                tBmodeText.BackColor = Color.Orange;
                bck.PauseRandomCaller();
            }
            tBDebugText.Text = "Modo de controle alterado.";
        }

        private void DebugTextColorChanger(object sender, EventArgs e)
        {
            if (tBDebugText.BackColor == Color.LimeGreen)
            {
                Invoke((MethodInvoker)delegate ()
               {
                   tBDebugText.BackColor = Color.LightBlue;
               });
            }
            else
            {
                Invoke((MethodInvoker)delegate ()
                {
                    tBDebugText.BackColor = Color.LimeGreen;
                });
            }
        }

        private void FormElevatorLogicRunner(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                logic.RunElevatorLogic();

                if (logic.FloorRequested == logic.CloserFloor)
                {
                    elevatorButtons[logic.FloorRequested].BackColor = Color.LightGray;
                }
            });

        }

        private void CheckedChangedEvent(object sender, EventArgs e)
        {
            DisplayMode();
        }


        private void Button0_Click(object sender, EventArgs e)
        {
            button0.BackColor = Color.Yellow;
            tBDebugText.Text = "Botão 0 clicado.";
            logic.AddPannelRequest(0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Yellow;
            tBDebugText.Text = "Botão 1 clicado.";
            logic.AddPannelRequest(1);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Yellow;
            tBDebugText.Text = "Botão 2 clicado.";
            logic.AddPannelRequest(2);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Yellow;
            tBDebugText.Text = "Botão 3 clicado.";
            logic.AddPannelRequest(3);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Yellow;
            tBDebugText.Text = "Botão 4 clicado.";
            logic.AddPannelRequest(4);
        }

        /* Carrega a thread */
        private void Form1_Load(object sender, EventArgs e)
        {
            elevatorButtons.Add(0, button0);
            elevatorButtons.Add(1, button1);
            elevatorButtons.Add(2, button2);
            elevatorButtons.Add(3, button3);
            elevatorButtons.Add(4, button4);

            tBDebugText.Text = "Form carregado.";
            logic = new ElevatorLogic(5, 3.0d, 0.12d);
            bck = new BackProcess(100, 1000, 5000);
            backProcTask = new Task(bck.CallProc);

            bck.RandomCallEvent += this.DebugTextColorChanger;
            bck.ProcessEvent += this.FormElevatorLogicRunner;

            backProcTask.Start();
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            bck.CloseBackProcess();
            backProcTask.Wait();
            backProcTask.Dispose();
        }
    }
}
