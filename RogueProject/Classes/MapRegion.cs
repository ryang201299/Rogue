using Rogueproject;

namespace RogueProject;

public class MapRegion {
    public byte RegionNumber { get ; set; }
    public MapRoom Room { get; set; }

    public MapRegion(byte regionNumber) {
        this.RegionNumber = regionNumber;
    }
}