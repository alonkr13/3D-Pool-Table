namespace myOpenGL
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
            if (disposing && (components != null))
            {
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
            this.components = new System.ComponentModel.Container();
            this.timerRepaint = new System.Windows.Forms.Timer(this.components);
            this.timerRUN = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBarRotation = new System.Windows.Forms.HScrollBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.start_button = new System.Windows.Forms.Button();
            this.animation_timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerRepaint
            // 
            this.timerRepaint.Interval = 10;
            this.timerRepaint.Tick += new System.EventHandler(this.timerRepaint_Tick);
            // 
            // timerRUN
            // 
            this.timerRUN.Interval = 10;
            this.timerRUN.Tick += new System.EventHandler(this.timerRUN_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(862, 513);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownPanelFunc);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMovePanelFunc);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpPanelFunc);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(880, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "click check";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(883, 49);
            this.hScrollBar1.Maximum = 10;
            this.hScrollBar1.Minimum = -5;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(126, 17);
            this.hScrollBar1.TabIndex = 8;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_changed);
            // 
            // hScrollBarRotation
            // 
            this.hScrollBarRotation.LargeChange = 1;
            this.hScrollBarRotation.Location = new System.Drawing.Point(883, 291);
            this.hScrollBarRotation.Maximum = 359;
            this.hScrollBarRotation.Name = "hScrollBarRotation";
            this.hScrollBarRotation.Size = new System.Drawing.Size(126, 17);
            this.hScrollBarRotation.TabIndex = 12;
            this.hScrollBarRotation.Value = 359;
            this.hScrollBarRotation.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar5_Scroll);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 1;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(944, 156);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "orbit ball";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(883, 233);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(126, 40);
            this.start_button.TabIndex = 14;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // animation_timer
            // 
            this.animation_timer.Interval = 1;
            this.animation_timer.Tick += new System.EventHandler(this.animation_timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 541);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.hScrollBarRotation);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerRepaint;
        private System.Windows.Forms.Timer timerRUN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBarRotation;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Timer animation_timer;
    }
}

