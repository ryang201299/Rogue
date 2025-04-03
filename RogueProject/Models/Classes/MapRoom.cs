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
    public MapSpace[,] LevelMap { get; set; }
    public Dictionary<int, List<MapSpace>> AllDoorways { get; set; } 

    public MapRoom(int northWallYAxis, int southWallYAxis, int westWallXAxis, int eastWallXAxis, int regionNumber, MapSpace[,] levelMap, Dictionary<int, List<MapSpace>> allDoorways) {
        AllDoorways = allDoorways;
        
        RandomObject = new Random();
        
        this.NorthWallYAxis = northWallYAxis;
        this.SouthWallYAxis = southWallYAxis;
        this.WestWallXAxis = westWallXAxis;
        this.EastWallXAxis = eastWallXAxis;
        this.RegionNumber = regionNumber;
        this.LevelMap = levelMap;

        ValidateMapRoomDimensions();
        PopulateRoom();
    }

    private void PopulateRoom() {
        // Create horizontal and vertical walls for a room. Not including corners or exits
        for (int y = SouthWallYAxis; y <= NorthWallYAxis; y++) {
            for (int x = WestWallXAxis; x <= EastWallXAxis; x++) {
                if (y == SouthWallYAxis || y == NorthWallYAxis)
                {
                    LevelMap[x, y] = new MapSpace(CommonData.MapCharacters["Horizontal"], false, x, y, RegionNumber);
                }
                else if (x == WestWallXAxis || x == EastWallXAxis)
                {
                    LevelMap[x, y] = new MapSpace(CommonData.MapCharacters["Vertical"], false, x, y, RegionNumber);
                }
                else if (LevelMap[x, y] == null) {
                    LevelMap[x, y] = new MapSpace(CommonData.MapCharacters["RoomFloor"], false, x, y, RegionNumber);
                }
            }
        }

        GenerateDoors();

        // Lastly, the corners are filled in
        LevelMap[WestWallXAxis, SouthWallYAxis] = new MapSpace(CommonData.MapCharacters["CornerNorthWest"], false, WestWallXAxis, SouthWallYAxis, RegionNumber);
        LevelMap[EastWallXAxis, SouthWallYAxis] = new MapSpace(CommonData.MapCharacters["CornerNorthEast"], false, EastWallXAxis, SouthWallYAxis, RegionNumber);
        LevelMap[WestWallXAxis, NorthWallYAxis] = new MapSpace(CommonData.MapCharacters["CornerSouthWest"], false, WestWallXAxis, NorthWallYAxis, RegionNumber);
        LevelMap[EastWallXAxis, NorthWallYAxis] = new MapSpace(CommonData.MapCharacters["CornerSouthEast"], false, EastWallXAxis, NorthWallYAxis, RegionNumber); 

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

            // Note for future Ryan - This is failing because levelMap hasn't been instantiated yet in MapLevel.cs
            while (LevelMap[goldX, goldY].MapCharacter != CommonData.MapCharacters["RoomFloor"])
            {
                goldX = RandomObject.Next(WestWallXAxis + 1, EastWallXAxis);
                goldY = RandomObject.Next(SouthWallYAxis + 1, NorthWallYAxis);
            }

            LevelMap[goldX, goldY].ItemCharacter = CommonData.MapCharacters["Gold"];
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
            space.Visible = true;
        }
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
                LevelMap[doorway, SouthWallYAxis] = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, doorway, SouthWallYAxis, RegionNumber);

                // create new hallway space one square further away in same direction
                LevelMap[doorway, SouthWallYAxis - 1] = new MapSpace(CommonData.MapCharacters["Empty"], false, doorway, SouthWallYAxis - 1, RegionNumber);

                // add to deadends dictionary
                AllDoorways[RegionNumber].Add(LevelMap[doorway, SouthWallYAxis - 1]);

                // Increment door count
                doorCount += 1;
            }

            // South doorways
            if (RegionNumber <= 6 && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                doorway = RandomObject.Next(WestWallXAxis + 1, EastWallXAxis);

                LevelMap[doorway, NorthWallYAxis] = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, doorway, NorthWallYAxis, RegionNumber);

                LevelMap[doorway, NorthWallYAxis + 1] = new MapSpace(CommonData.MapCharacters["Empty"], false, doorway, NorthWallYAxis + 1, RegionNumber);

                AllDoorways[RegionNumber].Add(LevelMap[doorway, NorthWallYAxis + 1]);

                doorCount += 1;
            }

            // East doorways
            if ("147258".Contains(RegionNumber.ToString()) && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                doorway = RandomObject.Next(SouthWallYAxis + 1, NorthWallYAxis);

                LevelMap[EastWallXAxis, doorway] = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, EastWallXAxis, doorway, RegionNumber);

                LevelMap[EastWallXAxis + 1, doorway] = new MapSpace(CommonData.MapCharacters["Empty"], false, EastWallXAxis + 1, doorway, RegionNumber);

                AllDoorways[RegionNumber].Add(LevelMap[EastWallXAxis + 1, doorway]);

                doorCount += 1;
            }

            // West doorways
            if ("258369".Contains(RegionNumber.ToString()) && RandomObject.Next(101) <= CommonData.Probabilities["DoorwayCreation"]) {
                doorway = RandomObject.Next(SouthWallYAxis + 1, NorthWallYAxis);

                LevelMap[WestWallXAxis, doorway] = new MapSpace(CommonData.MapCharacters["RoomDoor"], false, WestWallXAxis, doorway, RegionNumber);

                LevelMap[WestWallXAxis - 1, doorway] = new MapSpace(CommonData.MapCharacters["Empty"], false, WestWallXAxis - 1, doorway, RegionNumber);

                AllDoorways[RegionNumber].Add(LevelMap[WestWallXAxis - 1, doorway]);

                doorCount += 1;
            }
        }
    }
}