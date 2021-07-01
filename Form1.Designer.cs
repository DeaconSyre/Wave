namespace Wave
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrawArea = new System.Windows.Forms.Panel();
            this.DebugTxtBx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DrawArea
            // 
            this.DrawArea.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DrawArea.Location = new System.Drawing.Point(12, 224);
            this.DrawArea.Name = "DrawArea";
            this.DrawArea.Size = new System.Drawing.Size(776, 214);
            this.DrawArea.TabIndex = 0;
            // 
            // DebugTxtBx
            // 
            this.DebugTxtBx.Location = new System.Drawing.Point(12, 12);
            this.DebugTxtBx.Multiline = true;
            this.DebugTxtBx.Name = "DebugTxtBx";
            this.DebugTxtBx.Size = new System.Drawing.Size(776, 206);
            this.DebugTxtBx.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DebugTxtBx);
            this.Controls.Add(this.DrawArea);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel DrawArea;
        private System.Windows.Forms.TextBox DebugTxtBx;
    }
}

