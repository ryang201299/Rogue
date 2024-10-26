public static class GameDimensions
{
    public static readonly Dictionary<string, byte> MapDimensions = new Dictionary<string, byte>
    {
        { "REGION_WD", 26 },
        { "REGION_HT", 8 },
        { "MAP_WD", 78 },
        { "MAP_HT", 24 }
    };

    public static readonly Dictionary<string, byte> RoomDimentions = new Dictionary<string, byte>
    {
        { "MAX_ROOM_WT", 22 },
        { "MAX_ROOM_HT", 5 },
        { "MIN_ROOM_WT", 4 },
        { "MIN_ROOM_HT", 4 }
    };

    public static readonly Dictionary<int, List<int>> regionBoundaries = new Dictionary<int, List<int>>
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
}
