using System.Diagnostics;
using System.Text;
using Rogueproject;
using RogueProject.Models;

namespace RogueProject;
public class MapLevel {
    // Map measurements
    private const short _REGION_WIDTH = 26;
    private const short _REGION_HEIGHT = 8;
    private const short _MAP_WIDTH = 78;
    private const short _MAP_HEIGHT = 24;
    private const short _MAX_ROOM_WIDTH = 22;
    private const short _MAX_ROOM_HEIGHT = 5;
    private const short _MIN_ROOM_WIDTH = 4;
    private const short _MIN_ROOM_HEIGHT = 4;

    private readonly Dictionary<int, List<MapSpace>> _allDoorways;

    public MapSpace[,] levelMap { get; set; }

    public List<MapRegion> MapRegions { get; set; }

    private Random RandomObject { get; set; }

    public MapLevel() {
        do
        {
            this.levelMap = new MapSpace[80, 25];

            this.MapRegions = new List<MapRegion>();

            // Can probably change to region objects instead of ints
            this._allDoorways = new Dictionary<int, List<MapSpace>>()
            {
                { 1, new List<MapSpace>() },
                { 2, new List<MapSpace>() },
                { 3, new List<MapSpace>() },
                { 4, new List<MapSpace>() },
                { 5, new List<MapSpace>() },
                { 6, new List<MapSpace>() },
                { 7, new List<MapSpace>() },
                { 8, new List<MapSpace>() },
                { 9, new List<MapSpace>() }
            };

            RandomObject = new Random();

            MapGeneration();
            /*Debug.WriteLine(MapText());*/
            /*                Application.DoEvents();
            */

            foreach (MapSpace space in levelMap) {
                space.Visible = false;
            }

        } while (!MapVerification());
    }

    public MapSpace PlayersLocation() {
        MapSpace playerLocation = new MapSpace();

        foreach (MapSpace space in levelMap) {
            if (space.DisplayCharacter == '☺') {
                playerLocation = space;
                break;
            }
        }

        return playerLocation;
    }

    public List<MapSpace> SpacesSurroundingPlayer(MapSpace playerLocation) {
        List<MapSpace> surroundingSpaces =
        [
            levelMap[playerLocation.X + 1, playerLocation.Y],
            levelMap[playerLocation.X - 1, playerLocation.Y],
            levelMap[playerLocation.X, playerLocation.Y + 1],
            levelMap[playerLocation.X, playerLocation.Y - 1],

            levelMap[playerLocation.X + 1, playerLocation.Y - 1],
            levelMap[playerLocation.X - 1, playerLocation.Y - 1],

            levelMap[playerLocation.X + 1, playerLocation.Y + 1],
            levelMap[playerLocation.X - 1, playerLocation.Y + 1]
        ];

        return surroundingSpaces;
    }

    private void MapGeneration()
    {
        byte region = 1;

        // Change this to create regions and rooms within each region
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                // Calculate actual x,y coordinates using region dimensions
                int x = 1 + (col * _REGION_WIDTH);
                int y = 1 + (row * _REGION_HEIGHT);
                
                MapRegion mapRegion = new MapRegion(region);
                MapRegions.Add(mapRegion);

                // Random chance of a room being created in this region
                if (RandomObject.Next(1, 101) <= CommonData.Probabilities["RoomCreation"]) {
                    // Room size
                    int roomHeight = RandomObject.Next(_MIN_ROOM_HEIGHT, _MAX_ROOM_HEIGHT + 1);
                    int roomWidth = RandomObject.Next(_MIN_ROOM_WIDTH, _MAX_ROOM_WIDTH + 1);

                    // Center room in region
                    int southWallYAxis = (int)((_REGION_HEIGHT - roomHeight) / 2) + y;
                    int westWallXAxis = (int)((_REGION_WIDTH - roomWidth) / 2) + x;

                    int eastWallXAxis = westWallXAxis + roomWidth;
                    int northWallYAxis = southWallYAxis + roomHeight;

                    MapRoom mapRoom = new MapRoom(northWallYAxis, southWallYAxis, westWallXAxis, eastWallXAxis, mapRegion.RegionNumber, levelMap, _allDoorways);

                    mapRegion.Room = mapRoom;
                }

                region++;
            }
        }

        // For every pair of coordinates, if the space is not instantiated, instantiate it as empty
        for (int y = 0; y <= levelMap.GetUpperBound(1); y++)
        {
            for (int x = 0; x <= levelMap.GetUpperBound(0); x++)
            {
                if (levelMap[x, y] is null)
                    levelMap[x, y] = new MapSpace(CommonData.MapCharacters["Empty"], false, x, y, GetRegionNumber(x, y));
            }
        }

        HallwayGeneration();

        AddStairway();
    }

    private void AddStairway()
    {
        int x = 1; int y = 1;

        // Search the array randomly for an interior room space
        // and mark it as a hallway.
        while (levelMap[x, y].MapCharacter != CommonData.MapCharacters["RoomFloor"])
        {
            x = RandomObject.Next(1, _MAP_WIDTH);
            y = RandomObject.Next(1, _MAP_HEIGHT);
        }
        {
            x = RandomObject.Next(1, _MAP_WIDTH);
            y = RandomObject.Next(1, _MAP_HEIGHT);
        }

        levelMap[x, y] = new MapSpace(CommonData.MapCharacters["Stairway"], x, y, GetRegionNumber(x, y));
    }

    private Tuple<MapSpace, MapSpace>? ClosestDoorway(List<MapSpace> doorwaysWithoutCorridorsInCurrentRegion, Dictionary<int, List<MapSpace>> allDoorwaysWithoutCorridors)
        {
        MapSpace? closestDoorwayInCurrentRegion = null;
        MapSpace? closestDoorwayInOtherRegion = null;
        int shortestDistance = int.MaxValue;

        int verticalWeight = 2;

        foreach (MapSpace currentRegionDoorway in doorwaysWithoutCorridorsInCurrentRegion)
        {
            foreach (KeyValuePair<int, List<MapSpace>> otherRegionDoorways in allDoorwaysWithoutCorridors)
            {
                if (otherRegionDoorways.Key == currentRegionDoorway.Region)
                {
                    continue; // Skip the current region
                }

                foreach (MapSpace otherRegionDoorway in otherRegionDoorways.Value)
                {
                    // Applies manhattan alg to determine distance
                    int currentDistance = Math.Abs(currentRegionDoorway.X - otherRegionDoorway.X) + Math.Abs(currentRegionDoorway.Y - otherRegionDoorway.Y) * verticalWeight;
                    if (currentDistance < shortestDistance)
                    {
                        shortestDistance = currentDistance;
                        closestDoorwayInCurrentRegion = currentRegionDoorway;
                        closestDoorwayInOtherRegion = otherRegionDoorway;
                    }
                }
            }
        }

        if (closestDoorwayInCurrentRegion == null || closestDoorwayInOtherRegion == null)
        {
            // No valid path found
            return null;
        }

        return new Tuple<MapSpace, MapSpace>(closestDoorwayInCurrentRegion, closestDoorwayInOtherRegion);
    }

    private void CheckNeighbourValidty(List<MapSpace> openSet, List<MapSpace> closedSet, MapSpace currentPosition, int xDifference, int yDifference, MapSpace goalPosition, MapSpace startingPosition)
    {
        int newX = currentPosition.X + xDifference;
        int newY = currentPosition.Y + yDifference;

        if (newX > 0 && newX < _MAP_WIDTH && newY > 0 && newY < _MAP_HEIGHT)
        {
            MapSpace possibleSuccessor = new MapSpace(levelMap[newX, newY].MapCharacter, newX, newY, GetRegionNumber(newX, newY));

            if ((!closedSet.Any(space => space.X == possibleSuccessor.X && space.Y == possibleSuccessor.Y))
                && possibleSuccessor.MapCharacter == CommonData.MapCharacters["Empty"]
                && levelMap[possibleSuccessor.X, possibleSuccessor.Y + 1].MapCharacter != CommonData.MapCharacters["Hallway"]
                && levelMap[possibleSuccessor.X + 1, possibleSuccessor.Y].MapCharacter != CommonData.MapCharacters["Hallway"]
                && levelMap[possibleSuccessor.X, possibleSuccessor.Y - 1].MapCharacter != CommonData.MapCharacters["Hallway"]
                && levelMap[possibleSuccessor.X - 1, possibleSuccessor.Y].MapCharacter != CommonData.MapCharacters["Hallway"])
            {
                int verticalWeight = 3;

                int g = Math.Abs(startingPosition.X - possibleSuccessor.X) + Math.Abs(startingPosition.Y - possibleSuccessor.Y) * verticalWeight;

                // Applies manhattan for heuristic
                int h = Math.Abs(possibleSuccessor.X - goalPosition.X) + Math.Abs(possibleSuccessor.Y - goalPosition.Y) * verticalWeight;

                int f = g + h;

                MapSpace? existingNode = openSet.Find(n => n.X == possibleSuccessor.X && n.Y == possibleSuccessor.Y);
                if (existingNode == null || (existingNode.FCost.HasValue && f < existingNode.FCost.Value))
                {
                    possibleSuccessor.GCost = g;
                    possibleSuccessor.HCost = h;
                    possibleSuccessor.FCost = f;
                    possibleSuccessor.Parent = currentPosition;

                    if (existingNode != null)
                    {
                        openSet.Remove(existingNode);
                    }

                    openSet.Add(possibleSuccessor);
                }
            }
        }
    }

    private List<MapSpace> AStar(MapSpace startingPosition, MapSpace goalPosition)
    {
        List<MapSpace> openSet = new List<MapSpace>();
        List<MapSpace> closedSet = new List<MapSpace>();
        List<MapSpace> path = new List<MapSpace>();

        openSet.Add(startingPosition);

        while (openSet.Count > 0)
        {
            // Find the node with the lowest f-cost in the open set
            MapSpace currentNode = openSet.OrderBy(n => n.FCost).First();

            // If the current node is the goal, reconstruct the path and return it
            if (currentNode.X == goalPosition.X && currentNode.Y == goalPosition.Y)
            {
                while (currentNode != startingPosition)
                {
                    path.Insert(0, currentNode!);
                    currentNode = currentNode.Parent!;
                }
                path.Insert(0, startingPosition);
                return path;
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // Generate successors and add them to the open set
            CheckNeighbourValidty(openSet, closedSet, currentNode, 0, 1, goalPosition, startingPosition);
            CheckNeighbourValidty(openSet, closedSet, currentNode, 1, 0, goalPosition, startingPosition);
            CheckNeighbourValidty(openSet, closedSet, currentNode, 0, -1, goalPosition, startingPosition);
            CheckNeighbourValidty(openSet, closedSet, currentNode, -1, 0, goalPosition, startingPosition);
        }

        // If no path is found, return an empty list
        return path;
    }

    private void HallwayGeneration()
    {
        Dictionary<int, List<MapSpace>> doorwaysWithoutCorridors = new Dictionary<int, List<MapSpace>>();

        foreach (var entry in _allDoorways)
        {
            doorwaysWithoutCorridors.Add(entry.Key, new List<MapSpace>(entry.Value));
        }

        for (int region = 1; region <= 9; region++)
        {
            // Get the list of doorways for the current region
            if (doorwaysWithoutCorridors.TryGetValue(region, out List<MapSpace>? regionDoorways))
            {
                while (regionDoorways.Count > 0)
                {
                    Tuple<MapSpace, MapSpace>? closestDoorAndTargetDoor = ClosestDoorway(regionDoorways, doorwaysWithoutCorridors);

                    // Check if a door could be found, create deadend otherwise
                    if (closestDoorAndTargetDoor == null || closestDoorAndTargetDoor.Item1 == null || closestDoorAndTargetDoor.Item2 == null)
                    {
                        // Create deadend

                        break;
                    }

                    List<MapSpace> path = AStar(closestDoorAndTargetDoor.Item1, closestDoorAndTargetDoor.Item2);

                    if (path.Count > 30 || path.Count == 0) {
                        // Create deadend

                        break;
                    }

                    foreach (MapSpace space in path)
                    {
                        space.MapCharacter = CommonData.MapCharacters["Hallway"];

                        levelMap[space.X, space.Y] = space;
                    }

                    doorwaysWithoutCorridors[region].Remove(closestDoorAndTargetDoor.Item1);
                    doorwaysWithoutCorridors[closestDoorAndTargetDoor.Item2.Region].Remove(closestDoorAndTargetDoor.Item2);

                    regionDoorways.Remove(closestDoorAndTargetDoor.Item1);
                }
            }
        }
    }

    public MapSpace? GetStartingSpace()
    {
        foreach (MapSpace space in levelMap)
        {
            if (space.MapCharacter == CommonData.MapCharacters["RoomFloor"])
            {
                return space;
            }
        }

        return null;
    }

    public bool IsValidSpace(MapSpace space) {
        return space.MapCharacter == CommonData.MapCharacters["RoomFloor"] 
            || space.MapCharacter == CommonData.MapCharacters["Hallway"] 
            || space.MapCharacter == CommonData.MapCharacters["RoomDoor"];
    }

    public List<MapSpace> GetValidNeighbours(MapSpace space) {
        List<MapSpace> validNeighbours = new List<MapSpace>();

        if (IsValidSpace(levelMap[space.X, space.Y + 1])) {
            validNeighbours.Add(levelMap[space.X, space.Y + 1]);
        }

        if (IsValidSpace(levelMap[space.X + 1, space.Y]))
        {
            validNeighbours.Add(levelMap[space.X + 1, space.Y]);
        }

        if (IsValidSpace(levelMap[space.X, space.Y - 1]))
        {
            validNeighbours.Add(levelMap[space.X, space.Y - 1]);
        }

        if (IsValidSpace(levelMap[space.X - 1, space.Y]))
        {
            validNeighbours.Add(levelMap[space.X - 1, space.Y]);
        }

        return validNeighbours;
    }


    public bool MapVerification()
    {
        List<int> regionsWithRooms = new List<int>();

        foreach (KeyValuePair<int, List<MapSpace>> region in _allDoorways)
        {
            if (region.Value.Count > 0)
            {
                regionsWithRooms.Add(region.Key);
            }
        }

        // Get the starting cell coordinates
        MapSpace? startingSpace = GetStartingSpace();

        if (startingSpace == null) {
            throw new Exception("No starting point found");
        }

        Queue<MapSpace> queue = new Queue<MapSpace>();
        HashSet<MapSpace> visited = new HashSet<MapSpace>();

        queue.Enqueue(startingSpace);
        visited.Add(startingSpace);

        while (queue.Count > 0)
        {
            MapSpace currentSpace = queue.Dequeue();

            // Check if the current cell is a valid room in any region

            if (regionsWithRooms.Contains(currentSpace.Region))
            {
                regionsWithRooms.Remove(currentSpace.Region);
            }

            // Explore neighboring cells
            foreach (MapSpace neighbor in GetValidNeighbours(currentSpace))
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }
        }

        return true ? regionsWithRooms.Count == 0 : false;
    }

    public MapSpace PlaceMapCharacter(char MapChar, bool Living)
    {
        // Find a random space within one of the rooms that 
        // hasn't been occupied and return the array reference.

        int xPos = 1, yPos = 1;
        bool freeSpace = false;

        while (!freeSpace)
        {
            xPos = RandomObject.Next(1, _MAP_WIDTH);
            yPos = RandomObject.Next(1, _MAP_HEIGHT);

            freeSpace = (levelMap[xPos, yPos].MapCharacter == CommonData.MapCharacters["RoomFloor"])
                && levelMap[xPos, yPos].DisplayCharacter == null
                && levelMap[xPos, yPos].ItemCharacter == null;
        }

        // If the character is for the player or a monster, add
        // it to the Display character. Otherwise, use the item character.
        if (Living)
            levelMap[xPos, yPos].DisplayCharacter = MapChar;
        else
            levelMap[xPos, yPos].ItemCharacter = MapChar;

        return levelMap[xPos, yPos];
    }

    public void MoveDisplayItem(Player player, MapSpace newLocation) {
        newLocation.DisplayCharacter = player.Location!.DisplayCharacter;

        player.Location.DisplayCharacter = null;

        if (player.Location.ItemCharacter != null) {
            player.Location.ItemCharacter = null;
        }

        player.Location = newLocation;
    }

    public string MapText()
    {
        // Output the array to text for display.
        StringBuilder sbReturn = new StringBuilder();

        for (int y = 0; y <= _MAP_HEIGHT; y++)
        {
            for (int x = 0; x <= _MAP_WIDTH; x++)
            {
                if (levelMap[x, y].Visible == false) {
                    sbReturn.Append(levelMap[x, y].InvisibleCharacter);
                }
                else if (levelMap[x, y].DisplayCharacter != null)
                    sbReturn.Append(levelMap[x, y].DisplayCharacter);
                else if (levelMap[x, y].ItemCharacter != null)
                    sbReturn.Append(levelMap[x, y].ItemCharacter);
                else
                    sbReturn.Append(levelMap[x, y].MapCharacter);
            }

            sbReturn.Append("\n");
        }
        Debug.Write(sbReturn.ToString());
        return sbReturn.ToString();
    }

    public static int GetRegionNumber(int RoomAnchorX, int RoomAnchorY) {
        // Map is divided into a 3x3 grid of 9 equal regions
        // This function returns 1-9 depending on the region the given room exists in

        int returnVal;

        int regionX = ((int)RoomAnchorX / _REGION_WIDTH) + 1;
        int regionY = ((int)RoomAnchorY / _REGION_HEIGHT) + 1;

        returnVal = (regionX) + ((regionY - 1) * 3);

        return returnVal;
    }
}