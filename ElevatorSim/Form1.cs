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
        VisualEffects vfx;
        RandomCaller rnd;
        ElevatorLogic logic;
        Task blink;
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
                rnd.RunRandomCaller();
            }
            else
            {
                tBmodeText.Text = "Modo manual";
                tBmodeText.BackColor = Color.Orange;
                rnd.PauseRandomCaller();
            }
            tBDebugText.Text = "Modo de controle alterado.";
        }

        private void DebugTextColorChanger(object sender, EventArgs e)
        {
            if(tBDebugText.BackColor == Color.LimeGreen)
            {
                Invoke((MethodInvoker) delegate() 
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

        private void CheckedChangedEvent(object sender, EventArgs e)
        {
            DisplayMode();
        }


        private void Button0_Click(object sender, EventArgs e)
        {
            button0.BackColor = Color.Yellow;
            tBDebugText.Text = "Botão 0 clicado.";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tBDebugText.Text = "Botão 1 clicado.";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tBDebugText.Text = "Botão 2 clicado.";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            tBDebugText.Text = "Botão 3 clicado.";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            tBDebugText.Text = "Botão 4 clicado.";
        }

        /* Carrega a thread */
        private void Form1_Load(object sender, EventArgs e)
        {
            tBDebugText.Text = "Form carregado.";
            logic = new ElevatorLogic(5, 3.0d, 0.12d);
            rnd = new RandomCaller(logic, 1000, 20000);
            blink = new Task(rnd.CallRandomly);

            rnd.RandomCallEvent += this.DebugTextColorChanger;

            blink.Start();
            // iniciar task
            // iniciar objeto de processo
            // inscrever eventos
            // rodar task

        }

        private void Form1_Close(object sender, EventArgs e)
        {
            //vfx.runEfx = false;
            rnd.CloseRandomCaller();
            blink.Wait();
            blink.Dispose();
        }
        class VisualEffects
        {
            public bool runEfx;
            public delegate void ChangeColorDelegate(object sender, EventArgs e);
            public event ChangeColorDelegate ChangeColor;
            public VisualEffects()
            {
                runEfx = true;
            }
            public void DoVisualEffect()
            {
                while (runEfx)
                {
                    if (ChangeColor != null)    // alugém está inscrito nesse evento?
                    {
                        ChangeColor(this, EventArgs.Empty); // se sim chama ele
                    }
                    Thread.Sleep(500);
                }
            }
        }

    }

}
