using RogueProject.Models.Enums;
using RogueProject.Models.Interfaces;

namespace RogueProject.Models.Classes
{
    public class Player : ICharacter
    {
        public CharacterType Type { get; init; } = CharacterType.PLAYER;
        private const int STARTING_HP = 12;
        private const int STARTING_STRENGTH = 16;
        public const int MAX_FOODVALUE = 1700;
        public const int MIN_FOODVALUE = 900;
        private const int HUNGER_TURNS = 150;

        public enum HungerLevel
        {
            Satisfied = 3,
            Weak = 2,
            Faint = 1,
            Dead = 0
        }

        public string PlayerName { get; set; }
        public int HP { get; set; }
        public int HPDamage { get; set; }
        public int Strength { get; set; }
        public int StrengthMod { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public HungerLevel HungerState { get; set; }
        public int HungerTurn { get; set; }
        public MapSpace? Location { get; set; }
        public bool HasAmulet { get; set; }

        public Player(string PlayerName)
        {
            var rand = new Random();
            this.PlayerName = PlayerName;
            HP = STARTING_HP;
            HPDamage = 0;
            Strength = STARTING_STRENGTH;
            StrengthMod = 0;
            Gold = 0;
            Experience = 1;
            HungerState = HungerLevel.Satisfied;
            HungerTurn = rand.Next(MIN_FOODVALUE, MAX_FOODVALUE + 1);
        }
    }
}
