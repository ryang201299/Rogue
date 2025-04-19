using RogueProject.Models;
using RogueProject;

namespace RogueProject
{
    partial class DungeonMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Bitmap tileSet;

        private Dictionary<char, Rectangle> tileMap = new()
        {
            { CommonData.MapCharacters["Horizontal"], new Rectangle(0, 0, 16, 16) },
            { CommonData.MapCharacters["Vertical"], new Rectangle(16, 0, 16, 16) },
            { CommonData.MapCharacters["CornerNorthWest"], new Rectangle(32, 0, 16, 16) },
            { CommonData.MapCharacters["CornerSouthEast"], new Rectangle(48, 0, 16, 16) },
            { CommonData.MapCharacters["CornerNorthEast"], new Rectangle(0, 16, 16, 16) },
            { CommonData.MapCharacters["CornerSouthWest"], new Rectangle(16, 16, 16, 16) },
            { CommonData.MapCharacters["RoomFloor"], new Rectangle(32, 16, 16, 16) },
            { CommonData.MapCharacters["RoomDoor"], new Rectangle(48, 16, 16, 16) },
            { CommonData.MapCharacters["Empty"], new Rectangle(0, 32, 16, 16) },
            { CommonData.MapCharacters["Stairway"], new Rectangle(16, 32, 16, 16) },
            { CommonData.MapCharacters["Hallway"], new Rectangle(32, 32, 16, 16) },
            { CommonData.MapCharacters["Gold"], new Rectangle(48, 32, 16, 16) },
            { CommonData.MapCharacters["Amulet"], new Rectangle(0, 48, 16, 16) },
            { CommonData.MapCharacters["Player"], new Rectangle(16, 48, 16, 16) },
        };

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
            lblStats = new Label();
            panelMap = new Panel();
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
            btnStart.Location = new Point(795, 89);
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
            lblArray.Size = new Size(1282, 766);
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
            PlayerNamePanel.Size = new Size(1066, 204);
            PlayerNamePanel.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(156, 89);
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
            PlayerNameBox.Location = new Point(506, 89);
            PlayerNameBox.Name = "PlayerNameBox";
            PlayerNameBox.Size = new Size(283, 35);
            PlayerNameBox.TabIndex = 0;
            // 
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStats.AutoSize = true;
            lblStats.Location = new Point(29, 694);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(0, 28);
            lblStats.TabIndex = 6;
            // 
            // panelMap
            // 
            panelMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMap.Location = new Point(162, 104);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(960, 560);
            panelMap.TabIndex = 7;
            panelMap.Paint += panelMap_Paint;
            // 
            // DungeonMain
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1282, 766);
            Controls.Add(PlayerNamePanel);
            Controls.Add(panelMap);
            Controls.Add(lblStats);
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
        private Label lblStats;
        private Panel panelMap;

        private void panelMap_Paint(object sender, PaintEventArgs e)
        {
            int tileSize = 16;

            if (levelMap != null)
            {
                for (int y = 0; y < levelMap.GetLength(0); y++)
                {
                    for (int x = 0; x < levelMap.GetLength(1); x++)
                    {
                        // Get the character representing this tile
                        char symbol = levelMap[y, x].MapCharacter; // Adjust if needed

                        // Try to get the matching tile rectangle
                        if (tileMap.TryGetValue(symbol, out var srcRect))
                        {
                            // Destination on the screen
                            Rectangle destRect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);

                            // Draw the part of the tileSet image for this tile
                            e.Graphics.DrawImage(tileSet, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                }
            }

        }

    }
}
