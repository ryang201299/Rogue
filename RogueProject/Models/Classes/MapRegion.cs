namespace RogueProject.Models.Classes;

public class MapRegion {
    public byte RegionNumber { get ; set; }
    public MapRoom Room { get; set; }

    public MapRegion(byte regionNumber) {
        RegionNumber = regionNumber;
    }
}