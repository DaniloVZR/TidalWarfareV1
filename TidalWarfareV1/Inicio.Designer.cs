namespace TidalWarfareV1
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnJugar = new System.Windows.Forms.Button();
            this.btnRanking = new System.Windows.Forms.Button();
            this.btnControles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnJugar
            // 
            this.btnJugar.Location = new System.Drawing.Point(475, 351);
            this.btnJugar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnJugar.Name = "btnJugar";
            this.btnJugar.Size = new System.Drawing.Size(100, 28);
            this.btnJugar.TabIndex = 0;
            this.btnJugar.Text = "Jugar";
            this.btnJugar.UseVisualStyleBackColor = true;
            this.btnJugar.Click += new System.EventHandler(this.btnJugar_Click);
            // 
            // btnRanking
            // 
            this.btnRanking.Location = new System.Drawing.Point(475, 386);
            this.btnRanking.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRanking.Name = "btnRanking";
            this.btnRanking.Size = new System.Drawing.Size(100, 28);
            this.btnRanking.TabIndex = 1;
            this.btnRanking.Text = "Ranking";
            this.btnRanking.UseVisualStyleBackColor = true;
            this.btnRanking.Click += new System.EventHandler(this.btnRanking_Click);
            // 
            // btnControles
            // 
            this.btnControles.Location = new System.Drawing.Point(475, 422);
            this.btnControles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnControles.Name = "btnControles";
            this.btnControles.Size = new System.Drawing.Size(100, 28);
            this.btnControles.TabIndex = 2;
            this.btnControles.Text = "Controles";
            this.btnControles.UseVisualStyleBackColor = true;
            this.btnControles.Click += new System.EventHandler(this.btnControles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "TIDAL WARFARE";
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnControles);
            this.Controls.Add(this.btnRanking);
            this.Controls.Add(this.btnJugar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Inicio";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnJugar;
        private System.Windows.Forms.Button btnRanking;
        private System.Windows.Forms.Button btnControles;
        private System.Windows.Forms.Label label1;
    }
}

