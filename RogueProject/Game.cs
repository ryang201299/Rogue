using RogueProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static RogueProject.MapLevel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RogueProject
{
    internal class Game
    {
        // Directional inputs
        private const int KEY_WEST = 37;                
        private const int KEY_NORTH = 38;
        private const int KEY_EAST = 39;
        private const int KEY_SOUTH = 40;

        private const int KEY_UPLEVEL = 188;
        private const int KEY_DOWNLEVEL = 190;
        private const int MAX_LEVEL = 2;

        public MapLevel CurrentMap { get; set; }
        public int CurrentLevel { get; set; }
        public List<MapLevel> VisitedLevels { get; set; }
        public Player CurrentPlayer { get; }
        public int CurrentTurn { get; }
        public string StatusMessage { get; set; }
        public string Stats { get; set; }

        private static Random rand = new Random();

        public Game(string playerName)
        {
            // Setup a new game
            this.CurrentLevel = 1;
            this.VisitedLevels = new List<MapLevel>();
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(playerName);
            this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
            this.CurrentTurn = 0;
            this.StatusMessage = $"Welcome to the Dungeon, {this.CurrentPlayer.PlayerName} ...";
            this.Stats = $"Level: {CurrentLevel}   Gold: {CurrentPlayer.Gold}";
        }

        public void KeyHandler(int keyVal, bool shift)
        {
            if (keyVal == KEY_NORTH || keyVal == KEY_EAST || keyVal == KEY_SOUTH || keyVal == KEY_WEST) {
                MoveCharacter(CurrentPlayer, keyVal);
            }

            if (shift)
            {
                switch (keyVal)
                {
                    case KEY_DOWNLEVEL:
                        if (CurrentPlayer.Location.MapCharacter == MapLevel.STAIRWAY)
                            ChangeLevel(1);
                        else
                            this.StatusMessage = "There's no stairway here.";
                        break;
                    case KEY_UPLEVEL:
                        if (CurrentPlayer.Location.MapCharacter == MapLevel.STAIRWAY)
                            ChangeLevel(-1);
                        else
                            this.StatusMessage = "There's no stairway here.";
                        break;
                    default:
                        break;
                }
            }
        }

        private void ChangeLevel(int change)
        {
            bool allowPass = false;
            bool gameWon = false;

            // If the player is trying to move up the stairs
            if (change < 0)
            {
                allowPass = CurrentPlayer.HasAmulet && CurrentLevel > 1;
                gameWon = CurrentPlayer.HasAmulet && CurrentLevel == 1;

                if (!allowPass) {
                    if (gameWon)
                    {
                        this.StatusMessage = "You have won!";
                    }
                    else {
                        this.StatusMessage = "You must first find the Amulet!";
                    }
                }
            }
            // If the player is trying to move down the stairs
            else if (change > 0)
            {
                allowPass = CurrentLevel < MAX_LEVEL;
                this.StatusMessage = allowPass ? "" : "You have reached the bottom level.You must go the other way.";  
            }

            if (allowPass)
            {
                CurrentMap.levelMap[CurrentPlayer.Location.X, CurrentPlayer.Location.Y].DisplayCharacter = null;

                if (change == 1)
                {
                    CurrentMap.levelMap[CurrentPlayer.Location.X, CurrentPlayer.Location.Y].DisplayCharacter = null;

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
                    CurrentLevel += change;

                }
                else {
                    if (!VisitedLevels.Contains(CurrentMap)) {
                        VisitedLevels.Add(CurrentMap);
                    }
                    CurrentMap = VisitedLevels[CurrentLevel - 2];
                    CurrentLevel += change;
                }

                CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
                this.StatusMessage = $"Welcome to level {CurrentLevel} rogue";
                UpdateStatsMessage();

                if (CurrentLevel == MAX_LEVEL && !CurrentPlayer.HasAmulet)
                {
                    CurrentMap.PlaceMapCharacter(MapLevel.AMULET, false);
                }
            }
        }

        private void UpdateStatsMessage() {
            this.Stats = $"Level: {CurrentLevel}   Gold: {this.CurrentPlayer.Gold}";
        }

        private void PickUpGold()
        {
            int goldAmt = rand.Next(MapLevel.MIN_GOLD_AMOUNT, MapLevel.MAX_GOLD_AMOUNT);
            CurrentPlayer.Gold += goldAmt;

            this.StatusMessage = $"You picked up {goldAmt} pieces of gold.";

            UpdateStatsMessage();
        }

        private string AddInventory() {
            string message = "";

            if (CurrentPlayer.Location.ItemCharacter == MapLevel.AMULET) {
                CurrentPlayer.HasAmulet = true;
                message = "You have found the Amulet of Yendor! Time to escape!";
            }

            return message;
        }

        public void MoveCharacter(Player player, int keyInput)
        {
            int desiredX = player.Location.X;
            int desiredY = player.Location.Y;

            switch (keyInput) {
                case KEY_NORTH:
                    desiredY = player.Location.Y - 1;
                    break;
                case KEY_EAST:
                    desiredX = player.Location.X + 1;
                    break;
                case KEY_SOUTH:
                    desiredY = player.Location.Y + 1;
                    break;
                case KEY_WEST:
                    desiredX = player.Location.X - 1;
                    break;
            }

            MapSpace desiredLocation = CurrentMap.levelMap[desiredX, desiredY];

            // List of characters a living character can move onto.
            List<char> charsAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,
                    MapLevel.ROOM_DOOR, MapLevel.HALLWAY, MapLevel.GOLD};

            // If the map character in the chosen direction is habitable 
            // and if there's no monster there, move the character there.

            if (charsAllowed.Contains(desiredLocation.MapCharacter) && desiredLocation.DisplayCharacter == null) {
                CurrentMap.MoveDisplayItem(player, desiredLocation);

                if (desiredLocation.ItemCharacter == MapLevel.GOLD)
                {
                    PickUpGold();
                }
                else if (desiredLocation.ItemCharacter == MapLevel.AMULET) {
                    this.StatusMessage = AddInventory();
                }
            }
        }
    }
}
