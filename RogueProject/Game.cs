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
        public MapLevel CurrentMap { get; set; }
        public int CurrentLevel { get; }
        public Player CurentPlayer { get; }
        public int CurrentTurn { get; }

        public Game()
        {
            // Setup a new game
            this.CurrentLevel = 0;
            this.CurrentMap = new MapLevel();
            this.CurentPlayer = new Player("Rogue");
            this.CurrentTurn = 0;
        }
    }
}
