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
        { "Player", '☺'},
        { "Goblin", 'G' }
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
        { 1, new List<int> {  8,  15,   1,   1 }},  // row=0, col=0
        { 2, new List<int> {  8,  31,   1,  17 }},  // row=0, col=1
        { 3, new List<int> {  8,  47,   1,  33 }},  // row=0, col=2

        { 4, new List<int> { 17,  15,  10,   1 }},  // row=1, col=0
        { 5, new List<int> { 17,  31,  10,  17 }},  // row=1, col=1
        { 6, new List<int> { 17,  47,  10,  33 }},  // row=1, col=2

        { 7, new List<int> { 26,  15,  19,   1 }},  // row=2, col=0
        { 8, new List<int> { 26,  31,  19,  17 }},  // row=2, col=1
        { 9, new List<int> { 26,  47,  19,  33 }}   // row=2, col=2
    };

    public static readonly Dictionary<string, short> ItemValues = new Dictionary<string, short> {
        { "MinGoldValue", 10 },
        { "MaxGoldValue", 125 }
    };
}
