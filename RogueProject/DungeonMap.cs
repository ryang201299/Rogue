using System.Diagnostics;
using System.Xml.Linq;

namespace RogueProject
{
    public partial class DungeonMain : Form
    {
        private Game? currentGame;

        public DungeonMain()
        {
            InitializeComponent();
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
                PlayerNamePanel.Visible = false;
                lblArray.Text = currentGame.CurrentMap.MapText();
                lblStatus.Text = currentGame.StatusMessage;
                lblStats.Text = currentGame.Stats;
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

        private void DungeonMain_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Key Down - " + e.KeyValue);

            if (this.currentGame != null)
            {
                currentGame.KeyHandler(e.KeyValue, e.Shift);

                lblArray.Text = currentGame.CurrentMap.MapText();
                lblStatus.Text = currentGame.StatusMessage;
                lblStats.Text = currentGame.Stats;
            }
        }

        private void DungeonMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("Key Press - " + e.KeyChar);
        }
    }
}
