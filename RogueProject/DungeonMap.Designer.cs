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
            lblStatus = new Label();
            btnStart = new Button();
            PlayerNamePanel = new Panel();
            label1 = new Label();
            PlayerNameBox = new TextBox();
            lblStats = new Label();
            panelMap = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            PlayerNamePanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(16, 21);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(170, 22);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Welcome to Rogue";
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.None;
            btnStart.BackColor = Color.Black;
            btnStart.ForeColor = Color.FromArgb(255, 128, 0);
            btnStart.Location = new Point(1005, 222);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(111, 33);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnNext_Click;
            // 
            // PlayerNamePanel
            // 
            PlayerNamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PlayerNamePanel.Controls.Add(tableLayoutPanel1);
            PlayerNamePanel.Controls.Add(label1);
            PlayerNamePanel.Controls.Add(PlayerNameBox);
            PlayerNamePanel.Controls.Add(btnStart);
            PlayerNamePanel.Location = new Point(96, 198);
            PlayerNamePanel.Name = "PlayerNamePanel";
            PlayerNamePanel.Size = new Size(1486, 471);
            PlayerNamePanel.TabIndex = 5;
            PlayerNamePanel.Paint += PlayerNamePanel_Paint;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(366, 222);
            label1.Name = "label1";
            label1.Size = new Size(270, 22);
            label1.TabIndex = 3;
            label1.Text = "What is your rogue's name?";
            // 
            // PlayerNameBox
            // 
            PlayerNameBox.Anchor = AnchorStyles.None;
            PlayerNameBox.BackColor = SystemColors.InfoText;
            PlayerNameBox.BorderStyle = BorderStyle.FixedSingle;
            PlayerNameBox.ForeColor = SystemColors.Window;
            PlayerNameBox.Location = new Point(716, 222);
            PlayerNameBox.Name = "PlayerNameBox";
            PlayerNameBox.Size = new Size(283, 29);
            PlayerNameBox.TabIndex = 0;
            // 
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStats.AutoSize = true;
            lblStats.Location = new Point(29, 961);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(0, 22);
            lblStats.TabIndex = 6;
            // 
            // panelMap
            // 
            panelMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMap.Location = new Point(29, 79);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(1644, 821);
            panelMap.TabIndex = 7;
            panelMap.Paint += panelMap_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 133F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutPanel1.Location = new Point(299, 21);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 42.6160355F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 57.3839645F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 111F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tableLayoutPanel1.Size = new Size(501, 407);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // DungeonMain
            // 
            AutoScaleDimensions = new SizeF(10F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1540, 1005);
            Controls.Add(PlayerNamePanel);
            Controls.Add(panelMap);
            Controls.Add(lblStats);
            Controls.Add(lblStatus);
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

        private TableLayoutPanel tableLayoutPanel1;
    }
}
