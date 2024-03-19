using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace RogueProject
{
    internal class MapLevel {
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
        private const byte REGION_WD = 26;
        private const byte REGION_HT = 8;
        private const byte MAP_WD = 78;                 // Based on screen width and height of 80 x 25
        private const byte MAP_HT = 24;                 // these values keep the map within the borders
        private const byte MAX_ROOM_WT = 22;
        private const byte MAX_ROOM_HT = 5;
        private const byte MIN_ROOM_WT = 4;
        private const byte MIN_ROOM_HT = 4;

        private const byte ROOM_CREATE_PCT = 90;        // Probability that a room will be created
        private const byte ROOM_EXIT_PCT = 90;          // Probablity that a room has an exit
        private const int ROOM_GOLD_PCT = 65;           // Probability that gold spawns in a room

        public const int MIN_GOLD_AMOUNT = 10;
        public const int MAX_GOLD_AMOUNT = 125;

        // Regional boundaries for room generation in order of north_y, east_x, south_y, and west_x
        private Dictionary<int, List<int>> regionBoundaries = new Dictionary<int, List<int>>
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

        public Dictionary<string, int[]> directions = new Dictionary<string, int[]> {
            { "East",      new int[] {  1,  0 } },
            { "West",      new int[] { -1,  0 } },
            { "North",     new int[] {  0, -1 } },
            { "South",     new int[] {  0, +1 } },
            { "NorthEast", new int[] {  1, -1 } },
            { "NorthWest", new int[] { -1, -1 } },
            { "SouthWest", new int[] { -1, +1 } },
            { "SouthEast", new int[] {  1, +1 } }
        };

        List<MapSpace> spacesSurroundingPlayer;

        // Dictionary to hold hallway endings during map generation
        // Previously was MapSpace and Direction, but I care more about region than direction
        private Dictionary<int, List<MapSpace>> allDoorways;
        public MapSpace[,] levelMap;

        public MapLevel() {
            do
            {
                this.levelMap = new MapSpace[80, 25];
                this.allDoorways = new Dictionary<int, List<MapSpace>>()
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
                levelMap[playerLocation.X - 1, playerLocation.Y + 1],
            ];

            return surroundingSpaces;
        }

        private void MapGeneration()
        {
            var rand = new Random();
            int roomWidth = 0;
            int roomHeight = 0;
            int roomAnchorX = 0;
            int roomAnchorY = 0;
            int region = 1;

            levelMap = new MapSpace[80, 25];

            for (int y = 1; y < 18; y += REGION_HT) {
                for (int x = 1; x < 54; x += REGION_WD) {
                    if (rand.Next(1, 101) <= ROOM_CREATE_PCT) {
                        // Room size
                        roomHeight = rand.Next(MIN_ROOM_HT, MAX_ROOM_HT + 1);
                        roomWidth = rand.Next(MIN_ROOM_WT, MAX_ROOM_WT + 1);

                        // Center room in region
                        roomAnchorY = (int)((REGION_HT - roomHeight) / 2) + y;
                        roomAnchorX = (int)((REGION_WD - roomWidth) / 2) + x;

                        // Create room
                        RoomGeneration(roomAnchorX, roomAnchorY, roomWidth, roomHeight, region);
                    }

                    region++;
                }
            }

            for (int y = 0; y <= levelMap.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= levelMap.GetUpperBound(0); x++)
                {
                    if (levelMap[x, y] is null)
                        levelMap[x, y] = new MapSpace(EMPTY, false, x, y);
                }
            }

            HallwayGeneration();

            AddStairway();
        }

        private void RoomGeneration(int westWallX, int northWallY, int roomWidth, int roomHeight, int region) {
            int eastWallX = westWallX + roomWidth;
            int southWallY = northWallY + roomHeight;

            // If room dimensions exceed region boundaries, set said dimenion back to the boundary limit
            if (northWallY < regionBoundaries[region][0]) {
                northWallY = regionBoundaries[region][0];
            }

            if (eastWallX > regionBoundaries[region][1]) {
                eastWallX = regionBoundaries[region][1];
            }

            if (southWallY > regionBoundaries[region][2]) {
                southWallY = regionBoundaries[region][2];
            }

            if (westWallX < regionBoundaries[region][3]) {
                westWallX = regionBoundaries[region][3];
            }

            int regionNumber = GetRegionNumber(westWallX, northWallY);
            int doorway = 0;
            int doorCount = 0;
            var rand = new Random();

            // Create horizontal and vertical walls for a room. Not including corners or exits
            for (int y = northWallY; y <= southWallY; y++) {
                for (int x = westWallX; x <= eastWallX; x++) {
                    if (y == northWallY || y == southWallY)
                    {
                        levelMap[x, y] = new MapSpace(HORIZONTAL, false, x, y);
                    }
                    else if (x == westWallX || x == eastWallX)
                    {
                        levelMap[x, y] = new MapSpace(VERTICAL, false, x, y);
                    }
                    else if (levelMap[x, y] == null) {
                        levelMap[x, y] = new MapSpace(ROOM_INT, false, x, y);
                    }
                }
            }

            while (doorCount == 0) {
                // North doorways
                if (regionNumber >= 4 && rand.Next(101) <= ROOM_EXIT_PCT) {
                    // calculate random wall along the north wall. Add 1 to the start, and subtract 1
                    // from the end to avoid corners
                    doorway = rand.Next(westWallX + 1, eastWallX);

                    // create new door space
                    levelMap[doorway, northWallY] = new MapSpace(ROOM_DOOR, false, doorway, northWallY);

                    // create new hallway space one square further away in same direction
                    levelMap[doorway, northWallY - 1] = new MapSpace(EMPTY, false, doorway, northWallY - 1);

                    // add to deadends dictionary
                    allDoorways[regionNumber].Add(levelMap[doorway, northWallY - 1]);

                    // Increment door count
                    doorCount += 1;
                }

                // South doorways
                if (regionNumber <= 6 && rand.Next(101) <= ROOM_EXIT_PCT) {
                    doorway = rand.Next(westWallX + 1, eastWallX);

                    levelMap[doorway, southWallY] = new MapSpace(ROOM_DOOR, false, doorway, southWallY);

                    levelMap[doorway, southWallY + 1] = new MapSpace(EMPTY, false, doorway, southWallY + 1);

                    allDoorways[regionNumber].Add(levelMap[doorway, southWallY + 1]);

                    doorCount += 1;
                }

                // East doorways
                if ("147258".Contains(regionNumber.ToString()) && rand.Next(101) <= ROOM_EXIT_PCT) {
                    doorway = rand.Next(northWallY + 1, southWallY);

                    levelMap[eastWallX, doorway] = new MapSpace(ROOM_DOOR, false, eastWallX, doorway);

                    levelMap[eastWallX + 1, doorway] = new MapSpace(EMPTY, false, eastWallX + 1, doorway);

                    allDoorways[regionNumber].Add(levelMap[eastWallX + 1, doorway]);

                    doorCount += 1;
                }

                // West doorways
                if ("258369".Contains(regionNumber.ToString()) && rand.Next(101) <= ROOM_EXIT_PCT) {
                    doorway = rand.Next(northWallY + 1, southWallY);

                    levelMap[westWallX, doorway] = new MapSpace(ROOM_DOOR, false, westWallX, doorway);

                    levelMap[westWallX - 1, doorway] = new MapSpace(EMPTY, false, westWallX - 1, doorway);

                    allDoorways[regionNumber].Add(levelMap[westWallX - 1, doorway]);

                    doorCount += 1;
                }
            }

            // Lastly, the corners are filled in
            levelMap[westWallX, northWallY] = new MapSpace(CORNER_NW, false, westWallX, northWallY);
            levelMap[eastWallX, northWallY] = new MapSpace(CORNER_NE, false, eastWallX, northWallY);
            levelMap[westWallX, southWallY] = new MapSpace(CORNER_SW, false, westWallX, southWallY);
            levelMap[eastWallX, southWallY] = new MapSpace(CORNER_SE, false, eastWallX, southWallY);

            // Evaluate for a gold stash
            int goldX = westWallX; 
            int goldY = northWallY;

            if (rand.Next(1, 101) > ROOM_GOLD_PCT)
            {
                // Search the room randomly for an empty interior room space
                // and mark it as a gold stash.
                while (levelMap[goldX, goldY].MapCharacter != ROOM_INT)
                {
                    goldX = rand.Next(westWallX + 1, eastWallX);
                    goldY = rand.Next(northWallY + 1, southWallY);
                }

                levelMap[goldX, goldY].ItemCharacter = GOLD;
            }
        }

        private void AddStairway()
        {
            var rand = new Random();
            int x = 1; int y = 1;

            // Search the array randomly for an interior room space
            // and mark it as a hallway.
            while (levelMap[x, y].MapCharacter != ROOM_INT)
            {
                x = rand.Next(1, MAP_WD);
                y = rand.Next(1, MAP_HT);
            }

            levelMap[x, y] = new MapSpace(STAIRWAY, x, y);
        }

        private Tuple<MapSpace, MapSpace> ClosestDoorway(List<MapSpace> doorwaysWithoutCorridorsInCurrentRegion, Dictionary<int, List<MapSpace>> allDoorwaysWithoutCorridors)
            {
            MapSpace closestDoorwayInCurrentRegion = null;
            MapSpace closestDoorwayInOtherRegion = null;
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

            if (newX > 0 && newX < MAP_WD && newY > 0 && newY < MAP_HT)
            {
                MapSpace possibleSuccessor = new MapSpace(levelMap[newX, newY].MapCharacter, newX, newY);

                if ((!closedSet.Any(space => space.X == possibleSuccessor.X && space.Y == possibleSuccessor.Y))
                    && possibleSuccessor.MapCharacter == EMPTY
                    && levelMap[possibleSuccessor.X, possibleSuccessor.Y + 1].MapCharacter != HALLWAY
                    && levelMap[possibleSuccessor.X + 1, possibleSuccessor.Y].MapCharacter != HALLWAY
                    && levelMap[possibleSuccessor.X, possibleSuccessor.Y - 1].MapCharacter != HALLWAY
                    && levelMap[possibleSuccessor.X - 1, possibleSuccessor.Y].MapCharacter != HALLWAY)
                {
                    int verticalWeight = 3;

                    int g = Math.Abs(startingPosition.X - possibleSuccessor.X) + Math.Abs(startingPosition.Y - possibleSuccessor.Y) * verticalWeight;

                    // Applies manhattan for heuristic
                    int h = Math.Abs(possibleSuccessor.X - goalPosition.X) + Math.Abs(possibleSuccessor.Y - goalPosition.Y) * verticalWeight;

                    int f = g + h;

                    MapSpace existingNode = openSet.Find(n => n.X == possibleSuccessor.X && n.Y == possibleSuccessor.Y);
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
                        path.Insert(0, currentNode);
                        currentNode = currentNode.Parent;
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

            foreach (var entry in allDoorways)
            {
                doorwaysWithoutCorridors.Add(entry.Key, new List<MapSpace>(entry.Value));
            }

            for (int region = 1; region <= 9; region++)
            {
                // Get the list of doorways for the current region
                if (doorwaysWithoutCorridors.TryGetValue(region, out List<MapSpace> regionDoorways))
                {
                    while (regionDoorways.Count > 0)
                    {
                        Tuple<MapSpace, MapSpace> closestDoorAndTargetDoor = ClosestDoorway(regionDoorways, doorwaysWithoutCorridors);

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
                            space.MapCharacter = HALLWAY;

                            levelMap[space.X, space.Y] = space;
                        }

                        doorwaysWithoutCorridors[region].Remove(closestDoorAndTargetDoor.Item1);
                        doorwaysWithoutCorridors[closestDoorAndTargetDoor.Item2.Region].Remove(closestDoorAndTargetDoor.Item2);

                        regionDoorways.Remove(closestDoorAndTargetDoor.Item1);
                    }
                }
            }
        }

        public MapSpace GetStartingSpace()
        {
            foreach (MapSpace space in levelMap)
            {
                if (space.MapCharacter == ROOM_INT)
                {
                    return space;
                }
            }

            return null;
        }

        public bool IsValidSpace(MapSpace space) {
            return space.MapCharacter == ROOM_INT || space.MapCharacter == HALLWAY || space.MapCharacter == ROOM_DOOR;
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

            foreach (KeyValuePair<int, List<MapSpace>> region in allDoorways)
            {
                if (region.Value.Count > 0)
                {
                    regionsWithRooms.Add(region.Key);
                }
            }

            // Get the starting cell coordinates
            MapSpace startingSpace = GetStartingSpace();

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

            Random rand = new Random();
            int xPos = 1, yPos = 1;
            bool freeSpace = false;

            while (!freeSpace)
            {
                xPos = rand.Next(1, MAP_WD);
                yPos = rand.Next(1, MAP_HT);

                freeSpace = (levelMap[xPos, yPos].MapCharacter == ROOM_INT)
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
            newLocation.DisplayCharacter = player.Location.DisplayCharacter;

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

            for (int y = 0; y <= MAP_HT; y++)
            {
                for (int x = 0; x <= MAP_WD; x++)
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

            int regionX = ((int)RoomAnchorX / REGION_WD) + 1;
            int regionY = ((int)RoomAnchorY / REGION_HT) + 1;

            returnVal = (regionX) + ((regionY - 1) * 3);

            return returnVal;
        }

        internal class MapSpace {
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
    }
}