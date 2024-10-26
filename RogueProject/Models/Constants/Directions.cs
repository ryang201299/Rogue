public static class Directions 
{
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
}