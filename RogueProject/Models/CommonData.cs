    namespace RogueProject.Models;
    
public abstract class CommonData {
    public static readonly Dictionary<string, char> MapCharacters = new Dictionary<string, char>
    {
        { "Horizontal", '═' },
        { "Vertical", '║' },
        { "CornerNorthWest", '╔' },
        { "CornerSouthEast", '╝' },
        { "CornerNorthEast", '╗' },
        { "CornerSouthWest", '╚' },
        { "RoomFloor", '.' },
        { "RoomDoor", '╬' },
        { "Hallway", '▓' },
        { "Stairway", '≣' },
        { "Empty", ' ' },
        { "Gold", '*' },
        { "Amulet", '♀' },
        { "Player", '☺'}
    };

    public static readonly Dictionary<string, int[]> PlayerDirections = new Dictionary<string, int[]> {
        { "East",      new int[] {  1,  0 } },
        { "West",      new int[] { -1,  0 } },
        { "North",     new int[] {  0, -1 } },
        { "South",     new int[] {  0, +1 } },
        { "NorthEast", new int[] {  1, -1 } },
        { "NorthWest", new int[] { -1, -1 } },
        { "SouthWest", new int[] { -1, +1 } },
        { "SouthEast", new int[] {  1, +1 } }
    };

    public static readonly Dictionary<string, short> Probabilities = new Dictionary<string, short> {
        { "RoomCreation", 90 },
        { "DoorwayCreation", 90 },
        { "GoldGeneration", 65 },
        { "RoomIsDark", 50 }
    };

    // Needs refactoring, I don't want to use magic numbers here. Calculate them from map height and width
    public static readonly Dictionary<int, List<int>> RegionBoundaries = new Dictionary<int, List<int>>
    {
        { 1, new List<int> { 10, 22,  1,  1 }},
        { 2, new List<int> { 10, 45,  1, 24 }},
        { 3, new List<int> { 10, 68,  1, 47 }},
        { 4, new List<int> { 21, 22, 12,  1 }},
        { 5, new List<int> { 21, 45, 12, 24 }},
        { 6, new List<int> { 21, 68, 12, 47 }},
        { 7, new List<int> { 32, 22, 23,  1 }},
        { 8, new List<int> { 32, 45, 23, 24 }},
        { 9, new List<int> { 32, 68, 23, 47 }},
    };

    public static readonly Dictionary<string, short> ItemValues = new Dictionary<string, short> {
        { "MinGoldValue", 10 },
        { "MaxGoldValue", 125 }
    };
}
