namespace RogueProject;

public class MapSpace {
    public char MapCharacter { get; set; }
    public char? ItemCharacter { get; set; } = null;
    public char? DisplayCharacter { get; set; } = null;
    public bool SearchRequired { get; set; }    // Certain items like trap doors and exists need a 
    public bool Visible { get; set; } = true;
    public char InvisibleCharacter { get; set; } = EMPTY;
    public int X { get; set; }                  // special key before they can be seen
    public int Y { get; set; }
    public int Region { get; set; } = 0;
    public int? GCost { get; set; }             // Cost from the start to this node
    public int? HCost { get; set; }             // Heuristic cost to the goal
    public int? FCost { get; set; }             // Total cost (g + h)
    public MapSpace? Parent { get; set; }       // Parent node in the path

    public MapSpace() {
        // Create blank space for map
        this.MapCharacter = ' ';
        this.SearchRequired = false;
        X = 0;
        Y = 0;
    }

    public MapSpace(char mapChar, int X, int Y) {
        // Create a non-blank space
        this.MapCharacter = mapChar;
        this.SearchRequired = false;
        this.X = X;
        this.Y = Y;
        this.Region = GetRegionNumber(X, Y);
    }

    public MapSpace(char mapChar, MapSpace oldSpace) {
        // Update value for an existing space
        this.MapCharacter = mapChar;
        this.SearchRequired = oldSpace.SearchRequired;
        this.X = oldSpace.X;
        this.Y = oldSpace.Y;
        this.Region = oldSpace.Region;
    }

    public MapSpace(char mapChar, bool search, int X, int Y) {
        // Allows for setting objects to be displayed or hidden
        this.MapCharacter = mapChar;
        this.SearchRequired = search;
        this.X = X;
        this.Y = Y;
        this.Region = GetRegionNumber(X, Y);
    }
}