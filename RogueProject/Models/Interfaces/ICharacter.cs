using RogueProject.Models.Classes;
using RogueProject.Models.Enums;

namespace RogueProject.Models.Interfaces;

public interface ICharacter
{
    CharacterType Type { get; init; }
    MapSpace? Location { get; set; }
}
