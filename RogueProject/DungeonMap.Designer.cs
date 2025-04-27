using RogueProject.Models;
using RogueProject;
using System.Drawing.Drawing2D;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

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
            { CommonData.MapCharacters["CornerNorthEast"], new Rectangle(0, 96, 32, 32) },
            { CommonData.MapCharacters["CornerNorthWest"], new Rectangle(32, 96, 32, 32) },
            // { CommonData.MapCharacters["CornerNorthWest"], new Rectangle(32, 0, 16, 16) },
            // { CommonData.MapCharacters["CornerSouthEast"], new Rectangle(48, 0, 16, 16) },
            { CommonData.MapCharacters["Vertical"], new Rectangle(0, 64, 32, 32) },
            // { CommonData.MapCharacters["RoomDoor"], new Rectangle(16, 16, 16, 16) },
            { CommonData.MapCharacters["Hallway"], new Rectangle(64, 32, 32, 32) },
            { CommonData.MapCharacters["Gold"], new Rectangle(96, 64, 32, 32) },
            { CommonData.MapCharacters["CornerSouthEast"], new Rectangle(0, 32, 32, 32) },
            { CommonData.MapCharacters["CornerSouthWest"], new Rectangle(32, 32, 32, 32) },
            { CommonData.MapCharacters["RoomFloor"], new Rectangle(64, 32, 32, 32) },
            { CommonData.MapCharacters["Empty"], new Rectangle(96, 32, 32, 32) },
            { CommonData.MapCharacters["Player"], new Rectangle(0, 0, 32, 32) },
            { CommonData.MapCharacters["RoomDoor"], new Rectangle(32, 0, 32, 32) },
            { CommonData.MapCharacters["Horizontal"], new Rectangle(96, 0, 32, 32) }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DungeonMain));
            lblStatus = new Label();
            btnStart = new Button();
            PlayerNamePanel = new Panel();
            label1 = new Label();
            PlayerNameBox = new TextBox();
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
            lblStats = new Label();
            panelMap = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            PlayerNamePanel.SuspendLayout();
            InventoryPanel.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
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
            btnStart.Location = new Point(739, 63);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(111, 33);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnNext_Click;
            // 
            // PlayerNamePanel
            // 
            PlayerNamePanel.AllowDrop = true;
            PlayerNamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlayerNamePanel.Controls.Add(label1);
            PlayerNamePanel.Controls.Add(PlayerNameBox);
            PlayerNamePanel.Controls.Add(btnStart);
            PlayerNamePanel.Location = new Point(274, 388);
            PlayerNamePanel.Name = "PlayerNamePanel";
            PlayerNamePanel.Size = new Size(955, 153);
            PlayerNamePanel.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(100, 63);
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
            PlayerNameBox.Location = new Point(450, 63);
            PlayerNameBox.Name = "PlayerNameBox";
            PlayerNameBox.Size = new Size(283, 35);
            PlayerNameBox.TabIndex = 0;
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
            InventoryPanel.Location = new Point(555, 210);
            InventoryPanel.Name = "InventoryPanel";
            InventoryPanel.Size = new Size(450, 550);
            InventoryPanel.TabIndex = 0;
            InventoryPanel.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(261, 188);
            label6.Name = "label6";
            label6.Size = new Size(129, 28);
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
            label9.Size = new Size(311, 28);
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
            label8.Size = new Size(25, 28);
            label8.TabIndex = 17;
            label8.Text = "5";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 80);
            label4.Name = "label4";
            label4.Size = new Size(90, 28);
            label4.TabIndex = 11;
            label4.Text = "Hunger";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 127);
            label3.Name = "label3";
            label3.Size = new Size(90, 28);
            label3.TabIndex = 10;
            label3.Text = "Armour";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 36);
            label2.Name = "label2";
            label2.Size = new Size(90, 28);
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
            label5.Size = new Size(142, 28);
            label5.TabIndex = 14;
            label5.Text = "Consumable";
            // 
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStats.AutoSize = true;
            lblStats.Location = new Point(29, 961);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(0, 28);
            lblStats.TabIndex = 6;
            // 
            // panelMap
            // 
            panelMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMap.Location = new Point(29, 79);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(1475, 821);
            panelMap.TabIndex = 7;
            panelMap.Paint += panelMap_Paint;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.IndianRed;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(200, 100);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = Color.IndianRed;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(200, 100);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = Color.IndianRed;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(200, 100);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.IndianRed;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(200, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // DungeonMain
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1540, 1005);
            Controls.Add(InventoryPanel);
            Controls.Add(PlayerNamePanel);
            Controls.Add(lblStats);
            Controls.Add(lblStatus);
            Controls.Add(panelMap);
            Font = new Font("Consolas", 14F, FontStyle.Bold);
            ForeColor = Color.FromArgb(255, 128, 0);
            KeyPreview = true;
            Margin = new Padding(5);
            Name = "DungeonMain";
            Text = "Rogue";
            KeyDown += DungeonMain_KeyDown;
            PlayerNamePanel.ResumeLayout(false);
            PlayerNamePanel.PerformLayout();
            InventoryPanel.ResumeLayout(false);
            InventoryPanel.PerformLayout();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Button btnStart;
        private Panel PlayerNamePanel;
        private TextBox PlayerNameBox;
        private Label label1;
        private Label lblStats;
        private Panel panelMap;

        private void panelMap_Paint(object sender, PaintEventArgs e)
        {
            int result = DrawTiles(e);

            if (result == 0) return; // No tiles to draw

            ApplyLightingOverlay(e.Graphics);
        }

        // Needs refactoring.
        private int DrawTiles(PaintEventArgs e) 
        {
            int tileSize = 32;

            if (levelMap != null)
            {
                for (int x = 0; x < levelMap.GetLength(0); x++)
                {
                    for (int y = 0; y < levelMap.GetLength(1); y++)
                    {
                        // Get the character representing this tile
                        char symbol = levelMap[x, y].MapCharacter;

                        if (levelMap[x, y].ItemCharacter != null)
                        {
                            symbol = (char)levelMap[x, y].ItemCharacter;
                        }

                        if (levelMap[x, y].DisplayCharacter != null) 
                        {
                            symbol = (char)levelMap[x, y].DisplayCharacter;
                        }

                        if (!levelMap[x, y].Visible)
                        {
                            symbol = CommonData.MapCharacters["Empty"];
                        }

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
            else 
            {
                return 0;
            }

            return 1;
        }

        private void ApplyLightingOverlay(Graphics g)
        {
            int tileSize  = 32;
            int width     = panelMap.Width;
            int height    = panelMap.Height;
            int torchX    = currentGame.CurrentPlayer.Location.X * tileSize + tileSize / 2;
            int torchY    = currentGame.CurrentPlayer.Location.Y * tileSize + tileSize / 2;
            int radius    = tileSize * 2;
            byte lightAlpha = 75;
            byte darkAlpha = 150;
            byte reallyDarkAlpha = 225;

            // 1) Create a 32bpp ARGB mask
            using (var mask = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            using (var dg   = Graphics.FromImage(mask))
            {
                var rect = new Rectangle(0, 0, width, height);
                var bd   = mask.LockBits(rect, ImageLockMode.WriteOnly, mask.PixelFormat);
                int stride   = bd.Stride;
                int byteCount = Math.Abs(stride) * height;
                var buffer   = new byte[byteCount];

                // 2) Fill per‐pixel based on room darkness + torch gradient
                for (int y = 0; y < height; y++)
                {
                    int row    = y * stride;
                    int mapY   = y / tileSize;

                    for (int x = 0; x < width; x++)
                    {
                        int mapX = x / tileSize;
                        bool isLightRoom = false;
                        bool isDarkSpace = false;
                        bool isReallyDarkSpace = false;

                        // check bounds + room darkness flag
                        if ( mapX >= 0 && mapX < levelMap.GetLength(0)
                        && mapY >= 0 && mapY < levelMap.GetLength(1)
                        && (levelMap[mapX, mapY].MapRoom != null && levelMap[mapX, mapY].MapRoom.IsDark))
                        {
                            isReallyDarkSpace = true;
                        }
                        else if (mapX >= 0 && mapX < levelMap.GetLength(0)
                        && mapY >= 0 && mapY < levelMap.GetLength(1)
                        && levelMap[mapX, mapY].MapCharacter == CommonData.MapCharacters["Hallway"])
                        {
                            isDarkSpace = true;
                        }
                        if ( mapX >= 0 && mapX < levelMap.GetLength(0)
                        && mapY >= 0 && mapY < levelMap.GetLength(1)
                        && (levelMap[mapX, mapY].MapRoom != null && !levelMap[mapX, mapY].MapRoom.IsDark))
                        {
                            isLightRoom = true;
                        }

                        byte alpha = 0;

                        double dx   = x - torchX;
                        double dy   = y - torchY;
                        double dist = Math.Sqrt(dx * dx + dy * dy);
                        double t    = dist / radius;
                        if (t < 0) t = 0;
                        if (t > 1) t = 1;

                        if (isDarkSpace)
                        {
                            // distance‐based alpha: transparent at torch center → maxAlpha at radius
                            alpha = (byte)(t * darkAlpha);
                        }

                        else if (isReallyDarkSpace) {
                            alpha = (byte)(t * reallyDarkAlpha);
                        }

                        else if (isLightRoom) {
                            
                            alpha = (byte)(t * lightAlpha);
                        }

                        int idx = row + x * 4;
                        buffer[idx + 0] = 0;      // B
                        buffer[idx + 1] = 0;      // G
                        buffer[idx + 2] = 0;      // R
                        buffer[idx + 3] = alpha;  // A
                    }
                }

                // 3) Copy back and unlock
                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, bd.Scan0, byteCount);
                mask.UnlockBits(bd);

                // 4) Blit over the scene
                g.DrawImage(mask, 0, 0);
            }
        }
        private Panel InventoryPanel;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label2;
        private ProgressBar progressBar1;
        private Label label3;
        private Label label8;
        private Label label5;
        private Label label4;
        private Label label9;
        private ProgressBar progressBar2;
        private Panel panel2;
        private Label label6;
        private TableLayoutPanel tableLayoutPanel5;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
