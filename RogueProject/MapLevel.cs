using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RogueProject
{
    internal class MapLevel {
        private enum Direction {
            None = 0,
            North = 1,
            East = 2,
            South = -1,
            West = -2
        }

        // Dictionary to hold hallway endings during map generation
        private Dictionary<MapSpace, Direction> deadEnds = 
            new Dictionary<MapSpace, Direction>();

        // Box drawing constants and other symbols.
        private const char HORIZONTAL = '═';
        private const char VERTICAL = '║';
        private const char CORNER_NW = '╔';
        private const char CORNER_SE = '╝';
        private const char CORNER_NE = '╗';
        private const char CORNER_SW = '╚';
        private const char ROOM_INT = '.';
        private const char ROOM_DOOR = '╬';
        private const char HALLWAY = '▓';
        private const char STAIRWAY = '≣';
        private const char EMPTY = ' ';

        // Map element boundaries
        private const byte REGION_WD = 26;
        private const byte REGION_HT = 8;
        private const byte MAP_WD = 78;                 // Based on screen width and height of 80 x 25
        private const byte MAP_HT = 24;                 // these values keep the map within the borders
        private const byte MAX_ROOM_WT = 24;
        private const byte MAX_ROOM_HT = 6;
        private const byte MIN_ROOM_WT = 4;
        private const byte MIN_ROOM_HT = 4;


        private const byte ROOM_CREATE_PCT = 90;        // Probability that a room will be created
        private const byte ROOM_EXIT_PCT = 90;          // Probablity that a room has an exit

        // Array to hold map definition. Commented out until I found out if it's needed
        /*        private MapSpace[,] levelMap {
                    get { return levelMap; }
                }*/

        private MapSpace[,] levelMap = new MapSpace[80, 25];

        public MapLevel() {
            MapGeneration();

 /*           while (!VerifyMap()) {
                MapGeneration();    
            }*/
        }

        private void MapGeneration()
        {
            var rand = new Random();
            int roomWidth = 0;
            int roomHeight = 0;
            int roomAnchorX = 0;
            int roomAnchorY = 0;

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

                        // Create room - let's section this out in its own procedure
                        RoomGeneration(roomAnchorX, roomAnchorY, roomWidth, roomHeight);
                    }
                }
            }

            for (int y = 0; y <= levelMap.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= levelMap.GetUpperBound(0); x++)
                {
                    if (levelMap[x, y] is null)
                        levelMap[x, y] = new MapSpace(' ', false, false, x, y);
                }
            }

            /*HallwayGeneration();*/

            AddStairway();
        }

        private void RoomGeneration(int westWallX, int northWallY, int roomWidth, int roomHeight) {
            int eastWallX = westWallX + roomWidth;
            int southWallY = northWallY + roomHeight;

            int regionNumber = GetRegionNumber(westWallX, northWallY);
            int doorway = 0;
            int doorCount = 0;
            var rand = new Random();

            // Create horizontal and vertical walls for a room. Not including corners or exits
            for (int y = northWallY; y <= southWallY; y++) {
                for (int x = westWallX; x <= eastWallX; x++) {
                    if (y == northWallY || y == southWallY)
                    {
                        levelMap[x, y] = new MapSpace(HORIZONTAL, false, false, x, y);
                    }
                    else if (x == westWallX || x == eastWallX)
                    {
                        levelMap[x, y] = new MapSpace(VERTICAL, false, false, x, y);
                    }
                    else if (levelMap[x, y] == null) {
                        levelMap[x, y] = new MapSpace(ROOM_INT, false, false, x, y);
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
                    levelMap[doorway, northWallY] = new MapSpace(ROOM_DOOR, false, false, doorway, northWallY);

                    // create new hallway space one square further away in same direction
                    levelMap[doorway, northWallY - 1] = new MapSpace(HALLWAY, false, false, doorway, northWallY - 1);

                    // add to deadends dictionary
                    deadEnds.Add(levelMap[doorway, northWallY - 1], Direction.North);

                    // Increment door count
                    doorCount += 1;
                }

                // South doorways
                if (regionNumber <= 6 && rand.Next(101) <= ROOM_EXIT_PCT) {
                    doorway = rand.Next(westWallX + 1, eastWallX);

                    levelMap[doorway, southWallY] = new MapSpace(ROOM_DOOR, false, false, doorway, southWallY);

                    levelMap[doorway, southWallY + 1] = new MapSpace(HALLWAY, false, false, doorway, southWallY + 1);

                    deadEnds.Add(levelMap[doorway, southWallY + 1], Direction.South);

                    doorCount += 1;
                }

                // East doorways
                if ("147258".Contains(regionNumber.ToString()) && rand.Next(101) <= ROOM_EXIT_PCT) {
                    doorway = rand.Next(northWallY + 1, southWallY);

                    levelMap[eastWallX, doorway] = new MapSpace(ROOM_DOOR, false, false, eastWallX, doorway);

                    levelMap[eastWallX + 1, doorway] = new MapSpace(HALLWAY, false, false, eastWallX + 1, doorway);

                    deadEnds.Add(levelMap[eastWallX + 1, doorway], Direction.East);

                    doorCount += 1;
                }

                // West doorways
                if ("258369".Contains(regionNumber.ToString()) && rand.Next(101) <= ROOM_EXIT_PCT) {
                    doorway = rand.Next(northWallY + 1, southWallY);

                    levelMap[westWallX, doorway] = new MapSpace(ROOM_DOOR, false, false, westWallX, doorway);

                    levelMap[westWallX - 1, doorway] = new MapSpace(HALLWAY, false, false, westWallX - 1, doorway);

                    deadEnds.Add(levelMap[westWallX - 1, doorway], Direction.West);

                    doorCount += 1;
                }
            }

            // Lastly, the corners are filled in
            levelMap[westWallX, northWallY] = new MapSpace(CORNER_NW, false, false, westWallX, northWallY);
            levelMap[eastWallX, northWallY] = new MapSpace(CORNER_NE, false, false, eastWallX, northWallY);
            levelMap[westWallX, southWallY] = new MapSpace(CORNER_SW, false, false, westWallX, southWallY);
            levelMap[eastWallX, southWallY] = new MapSpace(CORNER_SE, false, false, eastWallX, southWallY);
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

        public string MapText()
        {
            // Output the array to text for display.
            StringBuilder sbReturn = new StringBuilder();

            for (int y = 0; y <= MAP_HT; y++)
            {
                for (int x = 0; x <= MAP_WD; x++)
                    sbReturn.Append(levelMap[x, y].DisplayCharacter);

                sbReturn.Append("\n");
            }
            Debug.Write(sbReturn.ToString());
            return sbReturn.ToString();
        }

        private int GetRegionNumber(int RoomAnchorX, int RoomAnchorY) {
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
            public char DisplayCharacter { get; set; }
            public bool SearchRequired { get; set; }    // Certain items like trap doors and exists need a 
            public int X { get; set; }                  // special key before they can be seen
            public int Y { get; set; }

            public MapSpace() {
                // Create blank space for map
                this.MapCharacter = ' ';
                this.DisplayCharacter = ' ';
                this.SearchRequired = false;
                X = 0;
                Y = 0;
            }

            public MapSpace(char mapChar, int X, int Y) {
                // Create a non-blank space
                this.MapCharacter = mapChar;
                this.DisplayCharacter = mapChar;
                this.SearchRequired = false;
                this.X = X;
                this.Y = Y;
            }

            public MapSpace(char mapChar, MapSpace oldSpace) {
                // Update value for an existing space
                this.MapCharacter = mapChar;
                this.DisplayCharacter = mapChar;
                this.SearchRequired = oldSpace.SearchRequired;
                this.X = oldSpace.X;
                this.Y = oldSpace.Y;
            }

            public MapSpace(char mapChar, bool hidden, bool search, int X, int Y) {
                // Allows for setting objects to be displayed or hidden
                this.MapCharacter = mapChar;
                this.DisplayCharacter = hidden ? ' ' : mapChar;
                this.SearchRequired = search;
                this.X = X;
                this.Y = Y;
            }
        }
    }
}