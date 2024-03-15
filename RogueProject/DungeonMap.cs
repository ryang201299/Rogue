namespace RogueProject
{
    public partial class DungeonMain : Form
    {
        public DungeonMain()
        {
            InitializeComponent();
        }

/*        private void DungeonMain_Load(object sender, EventArgs e)
        {
            lblArray.Text = currentGame.CurrentMap.MapText();
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblArray_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            LoadMapLevel();
        }

        private void LoadMapLevel()
        {
            Game currentGame = new Game();

            /*MapLevel newLevel = new MapLevel();*/
            /*lblArray.Text = newLevel.MapText();*/

            lblArray.Text = currentGame.CurrentMap.MapText();
            Application.DoEvents();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 101; i++) {
                LoadMapLevel();
            }
        }
    }
}
