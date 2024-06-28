namespace RogueProject;

public static class MapInfo {
    // Box drawing constants and other symbols.
    public const char HORIZONTAL = '═';
    public const char VERTICAL = '║';
    public const char CORNER_NW = '╔';
    public const char CORNER_SE = '╝';
    public const char CORNER_NE = '╗';
    public const char CORNER_SW = '╚';
    public const char ROOM_INT = '.';
    public const char ROOM_DOOR = '╬';
    public const char HALLWAY = '▓';
    public const char STAIRWAY = '≣';
    public const char EMPTY = ' ';
    public const char GOLD = '*';
    public const char AMULET = '♀';

    // Map element boundaries
    public const byte REGION_WD = 26;
    public const byte REGION_HT = 8;
    public const byte MAP_WD = 78;                 // Based on screen width and height of 80 x 25
    public const byte MAP_HT = 24;                 // these values keep the map within the borders
    public const byte MAX_ROOM_WT = 22;
    public const byte MAX_ROOM_HT = 5;
    public const byte MIN_ROOM_WT = 4;
    public const byte MIN_ROOM_HT = 4;

    public const byte ROOM_CREATE_PCT = 90;        // Probability that a room will be created
    public const byte ROOM_EXIT_PCT = 90;          // Probablity that a room has an exit
    public const int ROOM_GOLD_PCT = 65;           // Probability that gold spawns in a room

    public const int MIN_GOLD_AMOUNT = 10;
    public const int MAX_GOLD_AMOUNT = 125;
}