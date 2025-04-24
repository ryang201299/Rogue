

using Rogueproject;
using RogueProject.Models;

namespace RogueProject;

public class MapSpace {
    public MapRoom MapRoom { get; set; }
    public char MapCharacter { get; set; }
    public char? ItemCharacter { get; set; } = null;
    public char? DisplayCharacter { get; set; } = null;
    public bool SearchRequired { get; set; }    // Certain items like trap doors and exists need a 
    public bool Visible { get; set; } = true;
    public char InvisibleCharacter { get; set; } = CommonData.MapCharacters["Empty"];
    public int X { get; set; }                  // special key before they can be seen
    public int Y { get; set; }
    public int Region { get; set; } = 0;
    public int? GCost { get; set; }             // Cost from the start to this node
    public int? HCost { get; set; }             // Heuristic cost to the goal
    public int? FCost { get; set; }             // Total cost (g + h)
    public MapSpace? Parent { get; set; }       // Parent node in the path

    public MapSpace() {
    }

    /// <summary>
    /// Create empty space
    /// </summary>
    public MapSpace(int x, int y) {
        // Create blank space for map
        this.MapCharacter = CommonData.MapCharacters["Empty"];
        this.SearchRequired = false;
        this.X = x;
        this.Y = y;
    }

    // Generic create space with character, region, and room associated
    public MapSpace(char mapChar, int X, int Y, int regionNumber, MapRoom mapRoom) {
        // Create a non-blank space
        this.MapCharacter = mapChar;
        this.SearchRequired = false;
        this.X = X;
        this.Y = Y;
        this.Region = regionNumber;
        this.MapRoom = mapRoom;
    }

    // // Update an existing space? Not sure why this is needed, instead of just updating the fields
    // public MapSpace(char mapChar, MapSpace oldSpace, MapRoom mapRoom) {
    //     // Update value for an existing space
    //     this.MapCharacter = mapChar;
    //     this.SearchRequired = oldSpace.SearchRequired;
    //     this.X = oldSpace.X;
    //     this.Y = oldSpace.Y;
    //     this.Region = oldSpace.Region;
    //     this.MapRoom = mapRoom;
    // }

    // Create searchable space
    public MapSpace(char mapChar, bool search, int X, int Y, int regionNumber, MapRoom mapRoom) {
        // Allows for setting objects to be displayed or hidden
        this.MapCharacter = mapChar;
        this.SearchRequired = search;
        this.X = X;
        this.Y = Y;
        this.Region = regionNumber;
        this.MapRoom = mapRoom;
    }

    public MapSpace(char mapChar, bool search, int X, int Y, int regionNumber) {
        // Allows for setting objects to be displayed or hidden
        this.MapCharacter = mapChar;
        this.SearchRequired = search;
        this.X = X;
        this.Y = Y;
        this.Region = regionNumber;
    }
}