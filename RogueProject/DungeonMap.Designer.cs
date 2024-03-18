namespace RogueProject
{
    partial class DungeonMain
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
            lblStatus = new Label();
            btnStart = new Button();
            lblArray = new Label();
            PlayerNamePanel = new Panel();
            label1 = new Label();
            PlayerNameBox = new TextBox();
            PlayerNamePanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(16, 21);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(220, 28);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Welcome to Rogue";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.None;
            btnStart.BackColor = Color.Black;
            btnStart.ForeColor = Color.FromArgb(255, 128, 0);
            btnStart.Location = new Point(702, 61);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(111, 33);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnNext_Click;
            // 
            // lblArray
            // 
            lblArray.Dock = DockStyle.Fill;
            lblArray.Location = new Point(0, 0);
            lblArray.Name = "lblArray";
            lblArray.Size = new Size(1097, 711);
            lblArray.TabIndex = 4;
            lblArray.TextAlign = ContentAlignment.TopCenter;
            lblArray.Click += lblArray_Click;
            // 
            // PlayerNamePanel
            // 
            PlayerNamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlayerNamePanel.Controls.Add(label1);
            PlayerNamePanel.Controls.Add(PlayerNameBox);
            PlayerNamePanel.Controls.Add(btnStart);
            PlayerNamePanel.Location = new Point(96, 198);
            PlayerNamePanel.Name = "PlayerNamePanel";
            PlayerNamePanel.Size = new Size(881, 149);
            PlayerNamePanel.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(63, 61);
            label1.Name = "label1";
            label1.Size = new Size(350, 28);
            label1.TabIndex = 3;
            label1.Text = "What is your rogue's name?";
            // 
            // PlayerNameBox
            // 
            PlayerNameBox.Anchor = AnchorStyles.None;
            PlayerNameBox.BackColor = SystemColors.InfoText;
            PlayerNameBox.BorderStyle = BorderStyle.FixedSingle;
            PlayerNameBox.ForeColor = SystemColors.Window;
            PlayerNameBox.Location = new Point(413, 61);
            PlayerNameBox.Name = "PlayerNameBox";
            PlayerNameBox.Size = new Size(283, 35);
            PlayerNameBox.TabIndex = 0;
            // 
            // DungeonMain
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1097, 711);
            Controls.Add(PlayerNamePanel);
            Controls.Add(lblStatus);
            Controls.Add(lblArray);
            Font = new Font("Consolas", 14F, FontStyle.Bold);
            ForeColor = Color.FromArgb(255, 128, 0);
            KeyPreview = true;
            Margin = new Padding(5);
            Name = "DungeonMain";
            Text = "Rogue";
            Load += Form1_Load;
            KeyDown += DungeonMain_KeyDown;
            KeyPress += DungeonMain_KeyPress;
            KeyUp += DungeonMain_KeyUp;
            PlayerNamePanel.ResumeLayout(false);
            PlayerNamePanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Button btnStart;
        private Label lblArray;
        private Panel PlayerNamePanel;
        private TextBox PlayerNameBox;
        private Label label1;
    }
}
