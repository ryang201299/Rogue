using RogueProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static RogueProject.MapLevel;

namespace RogueProject
{
    internal class Game
    {
        private const int KEY_WEST = 37;                // Player controls following key inputs
        private const int KEY_NORTH = 38;
        private const int KEY_EAST = 39;
        private const int KEY_SOUTH = 40;

        public MapLevel CurrentMap { get; set; }
        public int CurrentLevel { get; }
        public Player CurrentPlayer { get; }
        public int CurrentTurn { get; }
        public string StatusMessage { get; set; }

        public Game(string playerName)
        {
            // Setup a new game
            this.CurrentLevel = 0;
            this.CurrentMap = new MapLevel();
            this.CurrentPlayer = new Player(playerName);
            this.CurrentPlayer.Location = CurrentMap.PlaceMapCharacter(Player.CHARACTER, true);
            this.CurrentTurn = 0;
            this.StatusMessage = $"Welcome to the Dungeon, {this.CurrentPlayer.PlayerName} ...";
        }

        public string KeyHandler(int KeyVal)
        {
            switch (KeyVal)
            {
                case KEY_WEST:
                    this.StatusMessage = "You moved west.";
                    return "west";
                case KEY_NORTH:
                    this.StatusMessage = "You moved north.";
                    return "north";
                case KEY_EAST:
                    this.StatusMessage = "You moved east.";
                    return "east";
                case KEY_SOUTH:
                    this.StatusMessage = "You moved south.";
                    return "south";
                default:
                    return "undefined";
            }
        }

        public void MoveCharacter(Player player, int keyInput)
        {
            string direction = KeyHandler(keyInput);
            int desiredX = player.Location.X;
            int desiredY = player.Location.Y;

            switch (direction) {
                case "north":
                    desiredY = player.Location.Y - 1;
                    break;
                case "south":
                    desiredY = player.Location.Y + 1;
                    break;
                case "east":
                    desiredX = player.Location.X + 1;
                    break;
                case "west":
                    desiredX = player.Location.X - 1;
                    break;
            }

            MapSpace desiredLocation = CurrentMap.levelMap[desiredX, desiredY];

            // List of characters a living character can move onto.
            List<char> charsAllowed =
                new List<char>(){MapLevel.ROOM_INT, MapLevel.STAIRWAY,
                    MapLevel.ROOM_DOOR, MapLevel.HALLWAY};

            // If the map character in the chosen direction is habitable 
            // and if there's no monster there, move the character there.

            if (charsAllowed.Contains(desiredLocation.MapCharacter) && desiredLocation.DisplayCharacter == null) {
                CurrentMap.MoveDisplayItem(player, desiredLocation);
            }
        }
    }
}
