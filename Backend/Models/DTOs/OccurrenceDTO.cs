namespace Backend.Models.DTOs;
public class OccurrenceDTO
{
    public int OccurrenceNo { get; set; }
    public string? EarlyInterval { get; set; }
    public int? EarlyIntervalNo { get; set; }
    public int? LateIntervalNo { get; set; }
    public double? MaxMa { get; set; }
    public double? MinMa { get; set; }
    public string Phylum { get; set; }
    public string? Class { get; set; }
    public string? Order { get; set; }
    public string? Family { get; set; }
    public string? Genus { get; set; }
    public string? Environment { get; set; }
    public string? Paleoage { get; set; }
    public double? PaleoLng { get; set; }
    public double? PaleoLat { get; set; }
    public double? Lng { get; set; }
    public double? Lat { get; set; }
}