namespace YouTubeVTTLinesSeparator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox_youtubeVTT = new System.Windows.Forms.ListBox();
            this.button_doit = new System.Windows.Forms.Button();
            this.listBox_assSubtitle = new System.Windows.Forms.ListBox();
            this.listBox_timePairs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_youtubeVTT
            // 
            this.listBox_youtubeVTT.FormattingEnabled = true;
            this.listBox_youtubeVTT.HorizontalScrollbar = true;
            this.listBox_youtubeVTT.ItemHeight = 12;
            this.listBox_youtubeVTT.Items.AddRange(new object[] {
            "Drop your YouTube VTT file here!"});
            this.listBox_youtubeVTT.Location = new System.Drawing.Point(22, 17);
            this.listBox_youtubeVTT.Margin = new System.Windows.Forms.Padding(2);
            this.listBox_youtubeVTT.Name = "listBox_youtubeVTT";
            this.listBox_youtubeVTT.Size = new System.Drawing.Size(388, 136);
            this.listBox_youtubeVTT.TabIndex = 1;
            // 
            // button_doit
            // 
            this.button_doit.Location = new System.Drawing.Point(620, 253);
            this.button_doit.Margin = new System.Windows.Forms.Padding(2);
            this.button_doit.Name = "button_doit";
            this.button_doit.Size = new System.Drawing.Size(87, 60);
            this.button_doit.TabIndex = 3;
            this.button_doit.Text = "Do it!";
            this.button_doit.UseVisualStyleBackColor = true;
            this.button_doit.Click += new System.EventHandler(this.button_doit_Click);
            // 
            // listBox_assSubtitle
            // 
            this.listBox_assSubtitle.FormattingEnabled = true;
            this.listBox_assSubtitle.HorizontalScrollbar = true;
            this.listBox_assSubtitle.ItemHeight = 12;
            this.listBox_assSubtitle.Items.AddRange(new object[] {
            "Drop your ASS timeline file here!"});
            this.listBox_assSubtitle.Location = new System.Drawing.Point(22, 177);
            this.listBox_assSubtitle.Margin = new System.Windows.Forms.Padding(2);
            this.listBox_assSubtitle.Name = "listBox_assSubtitle";
            this.listBox_assSubtitle.Size = new System.Drawing.Size(388, 136);
            this.listBox_assSubtitle.TabIndex = 18;
            // 
            // listBox_timePairs
            // 
            this.listBox_timePairs.FormattingEnabled = true;
            this.listBox_timePairs.HorizontalScrollbar = true;
            this.listBox_timePairs.ItemHeight = 12;
            this.listBox_timePairs.Location = new System.Drawing.Point(453, 17);
            this.listBox_timePairs.Margin = new System.Windows.Forms.Padding(2);
            this.listBox_timePairs.Name = "listBox_timePairs";
            this.listBox_timePairs.Size = new System.Drawing.Size(388, 136);
            this.listBox_timePairs.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(471, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "Shift YouTube VVT Time:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(620, 199);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(58, 21);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "600";
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(754, 253);
            this.button_save.Margin = new System.Windows.Forms.Padding(2);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(87, 60);
            this.button_save.TabIndex = 24;
            this.button_save.Text = "Save to ASS";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 334);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_timePairs);
            this.Controls.Add(this.listBox_assSubtitle);
            this.Controls.Add(this.button_doit);
            this.Controls.Add(this.listBox_youtubeVTT);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "YouTube VTT Lines Separator v0.0.1 by Sofronio.cn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_youtubeVTT;
        private System.Windows.Forms.Button button_doit;
        private System.Windows.Forms.ListBox listBox_assSubtitle;
        private System.Windows.Forms.ListBox listBox_timePairs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_save;
    }
}

