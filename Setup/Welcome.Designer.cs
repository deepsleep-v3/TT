namespace Setup
{
    partial class Welcome
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WelcomeCaption = new Label();
            WelcomeNext = new Button();
            SuspendLayout();
            // 
            // WelcomeCaption
            // 
            WelcomeCaption.AutoSize = true;
            WelcomeCaption.Font = new Font("Y式筆書CL", 19.9999981F, FontStyle.Regular, GraphicsUnit.Point, 136);
            WelcomeCaption.Location = new Point(178, 58);
            WelcomeCaption.Name = "WelcomeCaption";
            WelcomeCaption.Size = new Size(390, 72);
            WelcomeCaption.TabIndex = 0;
            WelcomeCaption.Text = "Welcome to TT Setup Wizard!\n歡迎來到 TT 安裝精靈！";
            WelcomeCaption.TextAlign = ContentAlignment.TopCenter;
            // 
            // WelcomeNext
            // 
            WelcomeNext.Font = new Font("致一圓體_傳承形", 15F, FontStyle.Regular, GraphicsUnit.Point, 136);
            WelcomeNext.Location = new Point(291, 200);
            WelcomeNext.Name = "WelcomeNext";
            WelcomeNext.Size = new Size(156, 31);
            WelcomeNext.TabIndex = 1;
            WelcomeNext.Text = "Next / 下一步";
            WelcomeNext.UseVisualStyleBackColor = true;
            WelcomeNext.Click += WelcomeNext_Click;
            // 
            // Welcome
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 300);
            Controls.Add(WelcomeNext);
            Controls.Add(WelcomeCaption);
            Margin = new Padding(2);
            Name = "Welcome";
            Text = "TT Setup Wizard";
            Load += Welcome_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label WelcomeCaption;
        private Button WelcomeNext;
    }
}
