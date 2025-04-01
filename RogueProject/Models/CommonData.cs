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
        { "Amulet", '♀' }
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
        { "RoomIsDark", 10 }
    };

    public static readonly Dictionary<int, List<int>> RegionBoundaries = new Dictionary<int, List<int>>
    {
        { 1, new List<int> { 1, 24, 6, 1 }},
        { 2, new List<int> { 1, 50, 6, 27}},
        { 3, new List<int> { 1, 76, 6, 53 }},
        { 4, new List<int> { 9, 24, 14, 1 }},
        { 5, new List<int> { 9, 50, 14, 27 }},
        { 6, new List<int> { 9, 76, 14, 53 }},
        { 7, new List<int> { 17, 24, 22, 1 }},
        { 8, new List<int> { 17, 50, 22, 27 }},
        { 9, new List<int> { 17, 76, 22, 53 }},
    };

    public static readonly Dictionary<string, short> ItemValues = new Dictionary<string, short> {
        { "MinGoldValue", 10 },
        { "MaxGoldValue", 125 }
    };
}
