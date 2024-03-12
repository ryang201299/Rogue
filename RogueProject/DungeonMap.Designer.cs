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
            lblStats = new Label();
            btnNext = new Button();
            btnGenerate = new Button();
            lblArray = new Label();
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
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStats.AutoSize = true;
            lblStats.Location = new Point(16, 586);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(90, 28);
            lblStats.TabIndex = 1;
            lblStats.Text = "Stats:";
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.LightGray;
            btnNext.ForeColor = Color.Black;
            btnNext.Location = new Point(644, 580);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(148, 39);
            btnNext.TabIndex = 2;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.LightGray;
            btnGenerate.ForeColor = Color.Black;
            btnGenerate.Location = new Point(810, 580);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(173, 39);
            btnGenerate.TabIndex = 3;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = false;
            // 
            // lblArray
            // 
            lblArray.Dock = DockStyle.Fill;
            lblArray.Location = new Point(0, 0);
            lblArray.Name = "lblArray";
            lblArray.Size = new Size(995, 635);
            lblArray.TabIndex = 4;
            lblArray.TextAlign = ContentAlignment.TopCenter;
            lblArray.Click += lblArray_Click;
            // 
            // DungeonMain
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(995, 635);
            Controls.Add(btnGenerate);
            Controls.Add(btnNext);
            Controls.Add(lblStats);
            Controls.Add(lblStatus);
            Controls.Add(lblArray);
            Font = new Font("Consolas", 14F, FontStyle.Bold);
            ForeColor = Color.FromArgb(255, 128, 0);
            Margin = new Padding(5, 5, 5, 5);
            Name = "DungeonMain";
            Text = "Rogue";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Label lblStats;
        private Button btnNext;
        private Button btnGenerate;
        private Label lblArray;
    }
}
