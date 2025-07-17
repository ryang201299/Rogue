using RogueProject.Models.Enums;
using RogueProject.Models.Interfaces;

namespace RogueProject.Models.Classes;

public class Monster : ICharacter
{
    public CharacterType Type { get; init; }
    public MapSpace? Location { get; set; }

    public Monster(CharacterType type)
    {
        Type = type;
    }
}
