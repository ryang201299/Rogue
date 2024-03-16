using RogueProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
            this.CurrentTurn = 0;
            this.StatusMessage = $"Welcome to the Dungeon, {this.CurrentPlayer.PlayerName} ...";
        }

        public void KeyHandler(int KeyVal)
        {
            switch (KeyVal)
            {
                case KEY_WEST:
                    this.StatusMessage = "You moved west.";
                    break;
                case KEY_NORTH:
                    this.StatusMessage = "You moved north.";
                    break;
                case KEY_EAST:
                    this.StatusMessage = "You moved east.";
                    break;
                case KEY_SOUTH:
                    this.StatusMessage = "You moved south.";
                    break;
            }
        }
    }
}
