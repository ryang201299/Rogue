using RogueProject.Models;
using RogueProject;
using System.Drawing.Drawing2D;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            btnStart.Location = new Point(1005, 222);
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
            lblArray.Size = new Size(1702, 1033);
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
            PlayerNamePanel.Size = new Size(1486, 471);
            PlayerNamePanel.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(366, 222);
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
            PlayerNameBox.Location = new Point(716, 222);
            PlayerNameBox.Name = "PlayerNameBox";
            PlayerNameBox.Size = new Size(283, 35);
            PlayerNameBox.TabIndex = 0;
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
            panelMap.Location = new Point(29, 52);
            panelMap.Name = "panelMap";
            panelMap.Size = new Size(1644, 848);
            panelMap.TabIndex = 7;
            panelMap.Paint += panelMap_Paint;
            // 
            // DungeonMain
            // 
            AutoScaleDimensions = new SizeF(13F, 27F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1702, 1033);
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
            int result = DrawTiles(e);

            if (result == 0) return; // No tiles to draw

            ApplyLightingOverlay(e.Graphics);
        }

        // Needs refactoring.
        private int DrawTiles(PaintEventArgs e)
        {
            const int tileSize = 64;
            const int viewRadius = 64;

            if (levelMap == null) return 0;

            int mapW = levelMap.GetLength(0);
            int mapH = levelMap.GetLength(1);
            int px   = currentGame.CurrentPlayer.Location.X;
            int py   = currentGame.CurrentPlayer.Location.Y;

            // 1) Visible‐tile bounds
            int minX = Math.Max(px - viewRadius, 0);
            int maxX = Math.Min(px + viewRadius, mapW - 1);
            int minY = Math.Max(py - viewRadius, 0);
            int maxY = Math.Min(py + viewRadius, mapH - 1);

            // 2) Pixel offset
            int centerX = panelMap.Width  / 2;
            int centerY = panelMap.Height / 2;
            int worldX  = px * tileSize + tileSize/2;
            int worldY  = py * tileSize + tileSize/2;
            int offsetX = centerX - worldX;
            int offsetY = centerY - worldY;

            // 3) Draw
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    char symbol = levelMap[x, y].MapCharacter;
                    if (levelMap[x, y].ItemCharacter    != null) symbol = (char)levelMap[x, y].ItemCharacter;
                    if (levelMap[x, y].DisplayCharacter != null) symbol = (char)levelMap[x, y].DisplayCharacter;
                    if (!levelMap[x, y].Visible) symbol = CommonData.MapCharacters["Empty"];

                    if (tileMap.TryGetValue(symbol, out var srcRect))
                    {
                        var dest = new Rectangle(
                            x * tileSize + offsetX,
                            y * tileSize + offsetY,
                            tileSize, tileSize
                        );
                        e.Graphics.DrawImage(tileSet, dest, srcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            return 1;
        }

        private void ApplyLightingOverlay(Graphics g)
        {
            const int tileSize       = 64;    // must match your DrawTiles
            const int torchRadius    = 2;     // in tiles
            const int gradientRadius = torchRadius * tileSize;

            // 1) Player/world offsets (same as DrawTiles)
            int px = currentGame.CurrentPlayer.Location.X;
            int py = currentGame.CurrentPlayer.Location.Y;
            int centerX = panelMap.Width  / 2;
            int centerY = panelMap.Height / 2;
            int worldX  = px * tileSize + tileSize/2;
            int worldY  = py * tileSize + tileSize/2;
            int offsetX = centerX - worldX;
            int offsetY = centerY - worldY;

            // torch in SCREEN coords
            int torchX = worldX + offsetX;
            int torchY = worldY + offsetY;

            byte lightAlpha      = 75;
            byte darkAlpha       = 150;
            byte reallyDarkAlpha = 225;

            int width  = panelMap.Width;
            int height = panelMap.Height;

            using (var mask = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            using (var dg   = Graphics.FromImage(mask))
            {
                var rect      = new Rectangle(0, 0, width, height);
                var bd        = mask.LockBits(rect, ImageLockMode.WriteOnly, mask.PixelFormat);
                int stride    = bd.Stride;
                int byteCount = Math.Abs(stride) * height;
                var buffer    = new byte[byteCount];

                for (int y = 0; y < height; y++)
                {
                    int row = y * stride;
                    for (int x = 0; x < width; x++)
                    {
                        // 2) Convert screen → world pixel, then to map cell
                        int worldPixelX = x - offsetX;
                        int worldPixelY = y - offsetY;
                        int mapX = worldPixelX / tileSize;
                        int mapY = worldPixelY / tileSize;

                        bool isLightRoom       = false;
                        bool isDarkSpace       = false;
                        bool isReallyDarkSpace = false;

                        if (mapX >= 0 && mapX < levelMap.GetLength(0) &&
                            mapY >= 0 && mapY < levelMap.GetLength(1))
                        {
                            var cell = levelMap[mapX, mapY];

                            // 3-tier classification
                            if (cell.MapRoom != null)
                            {
                                if (cell.MapRoom.IsDark)
                                    isReallyDarkSpace = true;
                                else
                                    isLightRoom = true;
                            }
                            else if (cell.MapCharacter == CommonData.MapCharacters["Hallway"])
                            {
                                isDarkSpace = true;
                            }
                        }
                        else
                        {
                            // off-map = darkest
                            isReallyDarkSpace = true;
                        }

                        // 3) Distance falloff from the torch center
                        double dx   = x - torchX;
                        double dy   = y - torchY;
                        double dist = Math.Sqrt(dx*dx + dy*dy);
                        double t    = Math.Min(1.0, Math.Max(0.0, dist / gradientRadius));

                        byte alpha = 0;
                        if (isReallyDarkSpace)  alpha = (byte)(t * reallyDarkAlpha);
                        else if (isDarkSpace)   alpha = (byte)(t * darkAlpha);
                        else if (isLightRoom)   alpha = (byte)(t * lightAlpha);

                        // 4) Write ARGB
                        int idx = row + x * 4;
                        buffer[idx + 0] = 0;    // B
                        buffer[idx + 1] = 0;    // G
                        buffer[idx + 2] = 0;    // R
                        buffer[idx + 3] = alpha;
                    }
                }

                // 5) Push mask into the bitmap and draw it
                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, bd.Scan0, byteCount);
                mask.UnlockBits(bd);
                g.DrawImage(mask, 0, 0);
            }
        }


    }
}
