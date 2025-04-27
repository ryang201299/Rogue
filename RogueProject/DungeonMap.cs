using System.Diagnostics;

namespace RogueProject
{
    public partial class DungeonMain : Form
    {
        private Game? currentGame;
        private MapSpace[,]? levelMap;

        public DungeonMain()
        {
            InitializeComponent();

            tileSet = new Bitmap("Tilesheet/test-tilesheet.png"); // Make sure this path is correct

            // Conditionally remove this when changing levels, so the new level has a wiping animation? 
            // There will be a better way to do this though I'm sure
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, panelMap, new object[] { true });

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (PlayerNameBox.TextLength > 0)
            {
                currentGame = new Game(PlayerNameBox.Text);
                levelMap = currentGame.CurrentMap.LevelMap;
                PlayerNamePanel.Visible = false;
                lblStatus.Text = currentGame.StatusMessage;
                lblStats.Text = currentGame.Stats;
                panelMap.Invalidate();
            }
            else
            {
                MessageBox.Show("Please enter a name for your character.");
            }
        }

        private void DungeonMain_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key Down - " + e.KeyValue);

            if (this.currentGame != null)
            {
                if (e.KeyCode == Keys.I)
                {
                    InventoryPanel.Visible = !InventoryPanel.Visible;
                }

                currentGame.KeyHandler(e.KeyValue, e.Shift);
                levelMap = currentGame.CurrentMap.LevelMap;
                lblStatus.Text = currentGame.StatusMessage;
                lblStats.Text = currentGame.Stats;

                panelMap.Invalidate();
            }
        }
    }
}
