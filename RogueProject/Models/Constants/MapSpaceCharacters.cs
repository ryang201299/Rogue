public static class MapSpaceCharacters
{
    public static readonly Dictionary<string, char> Characters = new Dictionary<string, char>
    {
        { "HORIZONTAL", '═' },
        { "VERTICAL", '║' },
        { "CORNER_NW", '╔' },
        { "CORNER_SE", '╝' },
        { "CORNER_NE", '╗' },
        { "CORNER_SW", '╚' },
        { "ROOM_INT", '.' },
        { "ROOM_DOOR", '╬' },
        { "HALLWAY", '▓' },
        { "STAIRWAY", '≣' },
        { "EMPTY", ' ' },
        { "GOLD", '*' },
        { "AMULET", '♀' }
    };
}
