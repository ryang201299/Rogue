namespace RogueProject
{
    partial class InventoryMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryMenu));
            InventoryPanel = new Panel();
            label6 = new Label();
            panel3 = new Panel();
            tableLayoutPanel5 = new TableLayoutPanel();
            label9 = new Label();
            progressBar2 = new ProgressBar();
            label8 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            progressBar1 = new ProgressBar();
            panel2 = new Panel();
            tableLayoutPanel6 = new TableLayoutPanel();
            label5 = new Label();
            InventoryPanel.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // InventoryPanel
            // 
            InventoryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InventoryPanel.BackColor = Color.DimGray;
            InventoryPanel.BackgroundImage = (Image)resources.GetObject("InventoryPanel.BackgroundImage");
            InventoryPanel.Controls.Add(label6);
            InventoryPanel.Controls.Add(panel3);
            InventoryPanel.Controls.Add(label9);
            InventoryPanel.Controls.Add(progressBar2);
            InventoryPanel.Controls.Add(label8);
            InventoryPanel.Controls.Add(label4);
            InventoryPanel.Controls.Add(label3);
            InventoryPanel.Controls.Add(label2);
            InventoryPanel.Controls.Add(progressBar1);
            InventoryPanel.Controls.Add(panel2);
            InventoryPanel.Location = new Point(390, 199);
            InventoryPanel.Name = "InventoryPanel";
            InventoryPanel.Size = new Size(450, 550);
            InventoryPanel.TabIndex = 1;
            InventoryPanel.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(261, 188);
            label6.Name = "label6";
            label6.Size = new Size(98, 25);
            label6.TabIndex = 23;
            label6.Text = "Equipment";
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkGray;
            panel3.Controls.Add(tableLayoutPanel5);
            panel3.Location = new Point(236, 177);
            panel3.Name = "panel3";
            panel3.Size = new Size(183, 291);
            panel3.TabIndex = 24;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.IndianRed;
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 57F));
            tableLayoutPanel5.Location = new Point(0, 42);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 4;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 48.031498F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 51.968502F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 57F));
            tableLayoutPanel5.Size = new Size(183, 249);
            tableLayoutPanel5.TabIndex = 22;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(75, 493);
            label9.Name = "label9";
            label9.Size = new Size(215, 25);
            label9.TabIndex = 21;
            label9.Text = "[ENTER] Equip / Consume";
            // 
            // progressBar2
            // 
            progressBar2.BackColor = Color.Black;
            progressBar2.ForeColor = Color.Black;
            progressBar2.Location = new Point(156, 80);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(244, 29);
            progressBar2.TabIndex = 18;
            progressBar2.Value = 50;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.ForeColor = Color.Black;
            label8.Location = new Point(156, 127);
            label8.Name = "label8";
            label8.Size = new Size(22, 25);
            label8.TabIndex = 17;
            label8.Text = "5";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 80);
            label4.Name = "label4";
            label4.Size = new Size(71, 25);
            label4.TabIndex = 11;
            label4.Text = "Hunger";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 127);
            label3.Name = "label3";
            label3.Size = new Size(73, 25);
            label3.TabIndex = 10;
            label3.Text = "Armour";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 36);
            label2.Name = "label2";
            label2.Size = new Size(63, 25);
            label2.TabIndex = 9;
            label2.Text = "Health";
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.Black;
            progressBar1.ForeColor = Color.Black;
            progressBar1.Location = new Point(156, 35);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(244, 29);
            progressBar1.TabIndex = 6;
            progressBar1.Value = 100;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkGray;
            panel2.Controls.Add(tableLayoutPanel6);
            panel2.Controls.Add(label5);
            panel2.Location = new Point(30, 177);
            panel2.Name = "panel2";
            panel2.Size = new Size(185, 291);
            panel2.TabIndex = 19;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = Color.IndianRed;
            tableLayoutPanel6.ColumnCount = 3;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 57F));
            tableLayoutPanel6.Location = new Point(0, 44);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 4;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 48.031498F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 51.968502F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 57F));
            tableLayoutPanel6.Size = new Size(185, 247);
            tableLayoutPanel6.TabIndex = 23;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.DimGray;
            label5.Location = new Point(26, 9);
            label5.Name = "label5";
            label5.Size = new Size(111, 25);
            label5.TabIndex = 14;
            label5.Text = "Consumable";
            // 
            // InventoryMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(InventoryPanel);
            Name = "InventoryMenu";
            Size = new Size(1230, 948);
            InventoryPanel.ResumeLayout(false);
            InventoryPanel.PerformLayout();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel InventoryPanel;
        private Label label6;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label9;
        private ProgressBar progressBar2;
        private Label label8;
        private Label label4;
        private Label label3;
        private Label label2;
        private ProgressBar progressBar1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label5;
    }
}
