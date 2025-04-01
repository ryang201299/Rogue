using RogueProject.Models;
using RogueProject.Models.Enums;

namespace RogueProject;
internal class Game
{
    public MapLevel CurrentMap { get; set; }
    public int CurrentLevel { get; set; }
    public List<MapLevel> VisitedLevels { get; set; }
    public Player CurrentPlayer { get; }
    public int CurrentTurn { get; }
    public string StatusMessage { get; set; }
    public string Stats { get; set; }
    public bool GameWon {  get; set; }
    public const int MAX_LEVEL = 9;

    private static Random rand = new Random();

    public Game(string playerName)
    {
        // Setup a new game
        this.CurrentLevel = 1;
        this.VisitedLevels = new List<MapLevel>();
        this.CurrentMap = new MapLevel();
        this.CurrentPlayer = new Player(playerName);
        this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
        InitialTorch();
        this.CurrentTurn = 0;
        this.StatusMessage = $"Welcome to the Dungeon, {this.CurrentPlayer.PlayerName} ...";
        this.Stats = $"Level: {CurrentLevel}   Gold: {CurrentPlayer.Gold}";
        this.GameWon = false;
    }

    public void KeyHandler(int keyValue, bool shiftKeyPressed)
    {
        KeyBindings key;

        // If the user enters an unrecognised key binding nothing will happen.
        if (Enum.IsDefined(typeof(KeyBindings), keyValue)) {
            key = (KeyBindings)keyValue;
        }
        else {
            Console.WriteLine("Unhandled key binding");

            return;
        }

        if (key == KeyBindings.MOVE_UP || key == KeyBindings.MOVE_RIGHT || key == KeyBindings.MOVE_DOWN || key == KeyBindings.MOVE_LEFT) {
            MoveCharacter(CurrentPlayer, key);
        }

        if (shiftKeyPressed)
        {
            switch (key)
            {
                case KeyBindings.UPSTAIRS:
                    if (CurrentPlayer.Location!.MapCharacter == CommonData.MapCharacters["Stairway"])
                        ChangeLevel(StaircaseDirection.UP);
                    else
                        this.StatusMessage = "There's no stairway here.";
                    break;
                case KeyBindings.DOWNSTAIRS:
                    if (CurrentPlayer.Location!.MapCharacter == CommonData.MapCharacters["Stairway"])
                        ChangeLevel(StaircaseDirection.DOWN);
                    else
                        this.StatusMessage = "There's no stairway here.";
                    break;
                default:
                    break;
            }
        }
    }

    private void ChangeLevel(StaircaseDirection staircaseDirection)
    {
        bool allowPass = false;

        // If the player is trying to move up the stairs
        if (staircaseDirection == StaircaseDirection.UP)
        {
            allowPass = CurrentPlayer.HasAmulet && CurrentLevel > 1;
            GameWon = CurrentPlayer.HasAmulet && CurrentLevel == 1;

            if (!allowPass) {
                if (GameWon)
                {
                    this.StatusMessage = "You have won!";
                }
                else {
                    this.StatusMessage = "You must first find the Amulet!";
                }
            }
        }
        // If the player is trying to move down the stairs
        else if (staircaseDirection == StaircaseDirection.DOWN)
        {
            allowPass = CurrentLevel < MAX_LEVEL;
            this.StatusMessage = allowPass ? "" : "You have reached the bottom level.You must go the other way.";  
        }

        if (allowPass)
        {
            CurrentMap.levelMap[CurrentPlayer.Location!.X, CurrentPlayer.Location.Y].DisplayCharacter = null;

            if (staircaseDirection == StaircaseDirection.DOWN)
            {
                CurrentMap.levelMap[CurrentPlayer.Location.X!, CurrentPlayer.Location.Y].DisplayCharacter = null;

                if (!VisitedLevels.Contains(CurrentMap)) {
                    VisitedLevels.Add(CurrentMap);
                }
                if (VisitedLevels.Count >= CurrentLevel + 1)
                {
                    CurrentMap = VisitedLevels[CurrentLevel];
                }
                else {
                    CurrentMap = new MapLevel();
                }
                CurrentLevel += (byte) staircaseDirection;

            }
            else {
                if (!VisitedLevels.Contains(CurrentMap)) {
                    VisitedLevels.Add(CurrentMap);
                }
                CurrentMap = VisitedLevels[CurrentLevel - 2];
                CurrentLevel += (byte) staircaseDirection;
            }

            CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
            this.StatusMessage = $"Welcome to level {CurrentLevel} rogue";
            UpdateStatsMessage();

            if (CurrentLevel == MAX_LEVEL && !CurrentPlayer.HasAmulet)
            {
                CurrentMap.PlaceMapCharacter(CommonData.MapCharacters["Amulet"], false);
            }
        }
    }

    private void UpdateStatsMessage() {
        this.Stats = $"Level: {CurrentLevel}   Gold: {this.CurrentPlayer.Gold}";
    }

    private void PickUpGold()
    {
        int goldAmt = rand.Next(CommonData.ItemValues["MinGoldValue"], CommonData.ItemValues["MaxGoldValue"]);
        CurrentPlayer.Gold += goldAmt;

        this.StatusMessage = $"You picked up {goldAmt} pieces of gold.";

        UpdateStatsMessage();
    }

    private string AddInventory() {
        string message = "";

        if (CurrentPlayer.Location!.ItemCharacter == CommonData.MapCharacters["Amulet"]) {
            CurrentPlayer.HasAmulet = true;
            message = "You have found the Amulet of Yendor! Time to escape!";
        }

        return message;
    }

    private void InitialTorch() {
        // Consider refactoring
        List<MapSpace> spacesSurroundingPlayer = CurrentMap.SpacesSurroundingPlayer(CurrentMap.PlayersLocation());

        foreach (MapSpace space in CurrentMap.levelMap)
        {
            if (spacesSurroundingPlayer.Contains(space))
            {
                space.Visible = true;
            }
        }
    }

    private void Torch() {
        // Making x number of squares in various directions visible once the player is nearby
        foreach (KeyValuePair<string, int[]> direction in CommonData.PlayerDirections) {
            if (direction.Key == "North" || direction.Key == "South")
            {
                for (int i = 1; i < 2; i++)
                {
                    CurrentMap.levelMap[CurrentPlayer.Location!.X + direction.Value[0] * i, CurrentPlayer.Location.Y + direction.Value[1] * i].Visible = true;
                }
            }
            else if ((direction.Key.Contains("North") || direction.Key.Contains("South")) && (direction.Key != "North" && direction.Key != "South")) {
                for (int i = 1; i < 3; i++)
                {
                    CurrentMap.levelMap[CurrentPlayer.Location!.X + direction.Value[0] * i, CurrentPlayer.Location.Y + direction.Value[1] * i].Visible = true;
                }
            }
            else
            {
                for (int i = 1; i < 5; i++)
                {
                    CurrentMap.levelMap[CurrentPlayer.Location!.X + direction.Value[0] * i, CurrentPlayer.Location.Y + direction.Value[1] * i].Visible = true;
                }
            }

        }
    }

    public void MoveCharacter(Player player, KeyBindings keyInput)
    {
        int desiredX = player.Location!.X;
        int desiredY = player.Location!.Y;

        switch (keyInput) {
            case KeyBindings.MOVE_UP:
                desiredY = player.Location.Y - 1;
                break;
            case KeyBindings.MOVE_RIGHT:
                desiredX = player.Location.X + 1;
                break;
            case KeyBindings.MOVE_DOWN:
                desiredY = player.Location.Y + 1;
                break;
            case KeyBindings.MOVE_LEFT:
                desiredX = player.Location.X - 1;
                break;
        }

        MapSpace desiredLocation = CurrentMap.levelMap[desiredX, desiredY];

        // List of characters a living character can move onto.
        List<char> charsAllowed =
            new List<char>(){CommonData.MapCharacters["RoomFloor"], CommonData.MapCharacters["Stairway"],
                CommonData.MapCharacters["RoomDoor"], CommonData.MapCharacters["Hallway"], CommonData.MapCharacters["Gold"]};

        // If the map character in the chosen direction is habitable 
        // and if there's no monster there, move the character there.

        if (charsAllowed.Contains(desiredLocation.MapCharacter) && desiredLocation.DisplayCharacter == null) {
            CurrentMap.MoveDisplayItem(player, desiredLocation);

            if (desiredLocation.ItemCharacter == CommonData.MapCharacters["Gold"])
            {
                PickUpGold();
            }
            else if (desiredLocation.ItemCharacter == CommonData.MapCharacters["Amulet"]) {
                this.StatusMessage = AddInventory();
            }
        }

        // Make squares within a region of the player visible, and others not
        Torch();
    }
}
