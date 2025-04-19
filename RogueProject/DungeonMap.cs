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

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, panelMap, new object[] { true });

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblArray_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (PlayerNameBox.TextLength > 0)
            {
                currentGame = new Game(PlayerNameBox.Text);
                levelMap = currentGame.CurrentMap.LevelMap;
                PlayerNamePanel.Visible = false;
                lblArray.Text = currentGame.CurrentMap.MapText();
                lblStatus.Text = currentGame.StatusMessage;
                lblStats.Text = currentGame.Stats;
                panelMap.Invalidate();
            }
            else
            {
                MessageBox.Show("Please enter a name for your character.");
            }
        }

        private void DungeonMain_KeyUp(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key Up - " + e.KeyValue);
        }

        // Means pressing any key down - does not mean pressing the down arrow key 
        private void DungeonMain_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key Down - " + e.KeyValue);

            if (this.currentGame != null)
            {
                currentGame.KeyHandler(e.KeyValue, e.Shift);

                lblArray.Text = currentGame.CurrentMap.MapText();
                lblStatus.Text = currentGame.StatusMessage;
                lblStats.Text = currentGame.Stats;

                panelMap.Invalidate();
            }
        }

        private void DungeonMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("Key Press - " + e.KeyChar);
        }
    }
}
