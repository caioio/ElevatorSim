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
        private Random rand;
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

        private void ChamaAndarAleatorio(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                uint r = (uint)rand.Next() % 5;
                logic.AddPannelRequest(r);
                tBDebugText.Text = "Andar aleatório chamado: " + r.ToString();

                elevatorButtons[r].BackColor = Color.Yellow;
            });


        }

        private void FormElevatorLogicRunner(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate ()
            {
                logic.RunElevatorLogicOnce();

                if (logic.IsMoving)
                {
                    tBDebugText.Text = "Position: " + logic.Position.ToString("0.00");
                }

                if (logic.HasReachedFloor())
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
            logic.AddPannelRequest(0);
            tBDebugText.Text = "Floor 0 request: " + logic.HasPannelRequest().ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Yellow;
            logic.AddPannelRequest(1);
            tBDebugText.Text = "Floor 1 request: " + logic.HasPannelRequest().ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Yellow;
            logic.AddPannelRequest(2);
            tBDebugText.Text = "Floor 2 request: " + logic.HasPannelRequest().ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Yellow;
            logic.AddPannelRequest(3);
            tBDebugText.Text = "Floor 3 request: " + logic.HasPannelRequest().ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.Yellow;
            logic.AddPannelRequest(4);
            tBDebugText.Text = "Floor 4 request: " + logic.HasPannelRequest().ToString();
        }

        /* Carrega a thread */
        private void Form1_Load(object sender, EventArgs e)
        {
            elevatorButtons.Add(0, button0);
            elevatorButtons.Add(1, button1);
            elevatorButtons.Add(2, button2);
            elevatorButtons.Add(3, button3);
            elevatorButtons.Add(4, button4);

            rand = new Random();

            tBDebugText.Text = "Form carregado.";
            logic = new ElevatorLogic(5, 3.0d, 0.12d);
            bck = new BackProcess(50, 1000, 5000);
            backProcTask = new Task(bck.CallProc);

            bck.RandomCallEvent += this.ChamaAndarAleatorio;
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
