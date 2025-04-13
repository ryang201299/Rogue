using RogueProject;
using RogueProject.Models;

namespace Rogueproject;

public class MapRoom {
    private Random RandomObject { get; set; }
    // Represents whether the room has "lights" or appears dark
    public bool IsDark { get; set; }
    // Represents whether the room has been visited aka whether the room should be visible to the player
    public bool Visited { get; set;}
    public int RegionNumber { get; set; }

    public List<MapSpace> MapSpaces { get; set; }

    public int NorthWallYAxis { get; set; }
    public int SouthWallYAxis { get; set; }
    public int WestWallXAxis { get; set; }
    public int EastWallXAxis { get; set; }
    public MapLevel Level { get; set; }
    public Dictionary<int, List<MapSpace>> AllDoorways { get; set; } 

    public MapRoom(int northWallYAxis, int southWallYAxis, int westWallXAxis, int eastWallXAxis, int regionNumber, MapLevel level, Dictionary<int, List<MapSpace>> allDoorways) {
        AllDoorways = allDoorways;
        
        RandomObject = new Random();
        
        this.NorthWallYAxis = northWallYAxis;
        this.SouthWallYAxis = southWallYAxis;
        this.WestWallXAxis = westWallXAxis;
        this.EastWallXAxis = eastWallXAxis;
        this.RegionNumber = regionNumber;
        this.Level = level;
        this.MapSpaces = new List<MapSpace>();
        this.Visited = false;

        ValidateMapRoomDimensions();
        PopulateRoom();

        // Increases chance of dark room as the level increases
        int chanceRoomIsDark = CommonData.Probabilities["RoomIsDark"] * Level.GameInstance.CurrentLevel;
        this.IsDark = RandomObject.Next(1, 101) <= chanceRoomIsDark;
    }

    private void PopulateRoom() {
        // Create horizontal and vertical walls for a room. Not including corners or exits
        for (int y = SouthWallYAxis; y <= NorthWallYAxis; y++) {
            for (int x = WestWallXAxis; x <= EastWallXAxis; x++) {
                if (y == SouthWallYAxis || y == NorthWallYAxis)
                {
                    MapSpace space = new MapSpace(CommonData.MapCharacters["Horizontal"], false, x, y, RegionNumber, this);
                    Level.LevelMap[x, y] = space;
                    MapSpaces.Add(space);

                }
                else if (x == WestWallXAxis || x == EastWallXAxis)
                {
                    MapSpace space = new MapSpace(CommonData.MapCharacters["Vertical"], false, x, y, RegionNumber, this);
                    Level.LevelMap[x, y] = space;
                    MapSpaces.Add(space);
                }
                else if (Level.LevelMap[x, y] == null) {
                    MapSpace space = new MapSpace(CommonData.MapCharacters["RoomFloor"], false, x, y, RegionNumber, this);
                    Level.LevelMap[x, y] = space;
                    MapSpaces.Add(space);
                }
            }
        }

        GenerateDoors();

        // Lastly, the corners are filled in
        MapSpace cornerNorthWest = new MapSpace(CommonData.MapCharacters["CornerNorthWest"], false, WestWallXAxis, SouthWallYAxis, RegionNumber, this);
        MapSpace cornerNorthEast = new MapSpace(CommonData.MapCharacters["CornerNorthEast"], false, EastWallXAxis, SouthWallYAxis, RegionNumber, this);
        MapSpace cornerSouthWest = new MapSpace(CommonData.MapCharacters["CornerSouthWest"], false, WestWallXAxis, NorthWallYAxis, RegionNumber, this);
        MapSpace cornerSouthEast = new MapSpace(CommonData.MapCharacters["CornerSouthEast"], false, EastWallXAxis, NorthWallYAxis, RegionNumber, this);

        Level.LevelMap[WestWallXAxis, SouthWallYAxis] = cornerNorthWest;
        Level.LevelMap[EastWallXAxis, SouthWallYAxis] = cornerNorthEast;
        Level.LevelMap[WestWallXAxis, NorthWallYAxis] = cornerSouthWest;
        Level.LevelMap[EastWallXAxis, NorthWallYAxis] = cornerSouthEast;

        MapSpaces.Add(cornerNorthWest);
        MapSpaces.Add(cornerNorthEast);
        MapSpaces.Add(cornerSouthWest);
        MapSpaces.Add(cornerSouthEast);

        GenerateGold();       
    }

    public void GenerateGold() {
        // Evaluate for a gold stash
        int goldX = WestWallXAxis; 
        int goldY = SouthWallYAxis;

        if (RandomObject.Next(1, 101) > CommonData.Probabilities["GoldGeneration"])
        {
            // Search the room randomly for an empty interior room space
            // and mark it as a gold stash.

            // Note for future Ryan - This is failing because Level.LevelMap hasn't been instantiated yet in MapLevel.cs
            while (Level.LevelMap[goldX, goldY].MapCharacter != CommonData.MapCharacters["RoomFloor"])
            {
                goldX = RandomObject.Next(WestWallXAxis + 1, EastWallXAxis);
                goldY = RandomObject.Next(SouthWallYAxis + 1, NorthWallYAxis);
            }

            Level.LevelMap[goldX, goldY].ItemCharacter = CommonData.MapCharacters["Gold"];
        }
    }

    private void ValidateMapRoomDimensions() {
        var regionBoundaries = CommonData.RegionBoundaries;
        
        // If room dimensions exceed region boundaries, set said dimenion back to the boundary limit
        if (SouthWallYAxis < regionBoundaries[RegionNumber][2]) {
            SouthWallYAxis = regionBoundaries[RegionNumber][2];
        }

        if (EastWallXAxis > regionBoundaries[RegionNumber][1]) {
            EastWallXAxis = regionBoundaries[RegionNumber][1];
        }

        if (NorthWallYAxis > regionBoundaries[RegionNumber][0]) {
            NorthWallYAxis = regionBoundaries[RegionNumber][0];
        }

        if (WestWallXAxis < regionBoundaries[RegionNumber][3]) {
            WestWallXAxis = regionBoundaries[RegionNumber][3];
        }
    }

    public void MakeRoomVisible() {
        foreach (MapSpace space in MapSpaces) {
            // Rewrite this, because it's unnecessarily setting space to invisible, when they're invisible by default
            if (IsDark && space.MapCharacter == CommonData.MapCharacters["RoomFloor"]) {
                space.Visible = false;
            }
            else {
                space.Visible = true;
            }
        }

        this.Visited = true;
    }

    public void GenerateDoors() {
        int doorCount = 0;
        int doorway = 0;

        while (doorCount == 0) {
            // North doorways
            if (RegionNumber >= 4 && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                // calculate random wall along the north wall. Add 1 to the start, and subtract 1
                // from the end to avoid corners
                doorway = RandomObject.Next(WestWallXAxis + 1, EastWallXAxis);

                // create new door space
                MapSpace door = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, doorway, SouthWallYAxis, RegionNumber, this);
                Level.LevelMap[doorway, SouthWallYAxis] = door;
                MapSpaces.Add(door);

                // create new hallway space one square further away in same direction
                MapSpace doorwayHallway = new MapSpace(CommonData.MapCharacters["Empty"], false, doorway, SouthWallYAxis - 1, RegionNumber);
                Level.LevelMap[doorway, SouthWallYAxis - 1] = doorwayHallway;

                // Not sure if this should be added?
                // MapSpaces.Add(doorwayHallway);

                // Refactor to use doorway object instead of coords
                AllDoorways[RegionNumber].Add(doorwayHallway);

                // Increment door count
                doorCount += 1;
            }

            // South doorways
            if (RegionNumber <= 6 && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                doorway = RandomObject.Next(WestWallXAxis + 1, EastWallXAxis);

                MapSpace door = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, doorway, NorthWallYAxis, RegionNumber, this);
                Level.LevelMap[doorway, NorthWallYAxis] = door;
                MapSpaces.Add(door);

                MapSpace doorwayHallway = new MapSpace(CommonData.MapCharacters["Empty"], false, doorway, NorthWallYAxis + 1, RegionNumber);
                Level.LevelMap[doorway, NorthWallYAxis + 1] = doorwayHallway;
                // MapSpaces.Add(doorwayHallway);

                AllDoorways[RegionNumber].Add(Level.LevelMap[doorway, NorthWallYAxis + 1]);

                doorCount += 1;
            }

            // East doorways
            if ("147258".Contains(RegionNumber.ToString()) && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                doorway = RandomObject.Next(SouthWallYAxis + 1, NorthWallYAxis);

                MapSpace door = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, EastWallXAxis, doorway, RegionNumber, this);
                Level.LevelMap[EastWallXAxis, doorway] = door;
                MapSpaces.Add(door);

                MapSpace doorwayHallway = new MapSpace(CommonData.MapCharacters["Empty"], false, EastWallXAxis + 1, doorway, RegionNumber);
                Level.LevelMap[EastWallXAxis + 1, doorway] = doorwayHallway;
                // MapSpaces.Add(doorwayHallway);

                AllDoorways[RegionNumber].Add(Level.LevelMap[EastWallXAxis + 1, doorway]);

                doorCount += 1;
            }


            // Refactor to iterate over MapRegions?
            // West doorways
            if ("258369".Contains(RegionNumber.ToString()) && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                doorway = RandomObject.Next(SouthWallYAxis + 1, NorthWallYAxis);

                MapSpace door = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, WestWallXAxis, doorway, RegionNumber, this);
                Level.LevelMap[WestWallXAxis, doorway] = door;
                MapSpaces.Add(door);

                MapSpace doorwayHallway = new MapSpace(CommonData.MapCharacters["Empty"], false, WestWallXAxis - 1, doorway, RegionNumber);
                Level.LevelMap[WestWallXAxis - 1, doorway] = doorwayHallway;
                // MapSpaces.Add(doorwayHallway);

                AllDoorways[RegionNumber].Add(Level.LevelMap[WestWallXAxis - 1, doorway]);

                doorCount += 1;
            }
        }
    }
}