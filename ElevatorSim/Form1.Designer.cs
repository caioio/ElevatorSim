
namespace ElevatorSim
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cBMode = new System.Windows.Forms.CheckBox();
            this.tBmodeText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cBMode
            // 
            this.cBMode.AutoSize = true;
            this.cBMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBMode.Location = new System.Drawing.Point(632, 42);
            this.cBMode.Name = "cBMode";
            this.cBMode.Size = new System.Drawing.Size(119, 24);
            this.cBMode.TabIndex = 0;
            this.cBMode.Text = "Automático";
            this.cBMode.UseVisualStyleBackColor = true;
            this.cBMode.CheckedChanged += new System.EventHandler(this.CheckedChangedEvent);
            // 
            // tBmodeText
            // 
            this.tBmodeText.Location = new System.Drawing.Point(632, 72);
            this.tBmodeText.Name = "tBmodeText";
            this.tBmodeText.Size = new System.Drawing.Size(119, 20);
            this.tBmodeText.TabIndex = 1;
            this.tBmodeText.Text = "Modo manual";
            this.tBmodeText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tBmodeText);
            this.Controls.Add(this.cBMode);
            this.Name = "Form1";
            this.Text = "ElevatorSim";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cBMode;
        private System.Windows.Forms.TextBox tBmodeText;
    }
}

