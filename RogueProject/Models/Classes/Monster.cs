namespace RogueProject.Models.Classes;

public class Monster
{
    public Enum MonsterType { get; init; }
    public MapSpace? Location { get; set; }

    public Monster(Enum monsterType, MapSpace startingLocation)
    {
        MonsterType = monsterType;
        Location = startingLocation;
    }
}
