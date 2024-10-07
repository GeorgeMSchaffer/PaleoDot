using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Backend.Models;
[Table("occurrences", Schema = "paleo")]
public class Occurrence
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("occurrence_no")]
    [JsonProperty("occurrence_no")]
    //[ForeignKey("occurrence_no")]
    public int OccurrenceNo { get; set; }

    [Column("record_type")]
public string RecordType { get; set; }

// //
// // [Column("collection_no")]
// // public string CollectionNo { get; set; }
//
//
// // [Column("catalog_no")]
// // public string CatalogNo { get; set; }
// [Column("identified_name")]
// public string IdentifiedName { get; set; }

[Column("identified_rank")]
public string IdentifiedRank { get; set; }

[Column("identified_no")]
public int IdentifiedNo { get; set; }

[Column("accepted_name")]
public string AcceptedName { get; set; }

[Column("accepted_rank")]
public string AcceptedRank { get; set; }

[Column("accepted_no")]
public int AcceptedNo { get; set; }

[Column("early_interval")]
public string? EarlyInterval { get; set; }

[Column("early_interval_no")]
public int? EarlyIntervalNo { get; set; }

[Column("late_interval_no")]
public int? LateIntervalNo { get; set; }
//
// [ForeignKey("occurrence_no")]
//
[Column("max_ma")]
public double? MaxMa { get; set; }

[Column("min_ma")]
public double? MinMa { get; set; }

// [Column("reference_no")]
// public int ReferenceNo { get; set; }
//
// // [Column("cc")]
// // public string Cc { get; set; }
//
// [Column("state")]
// public string State { get; set; }
//
// [Column("county")]
// public string County { get; set; }
//
// // [Column("latlng_basis")]
// // public string LatlngBasis { get; set; }
//
// // [Column("latlng_precision")]
// // public string LatlngPrecision { get; set; }
//
// // [Column("geogscale")]
// // public string Geogscale { get; set; }
//
// // [Column("geogcomments")]
// // public string Geogcomments { get; set; }

[Column("phylum")]
public string Phylum { get; set; }
[Column("class")]
public string? Class { get; set; }

[Column("order")]
public string? Order { get; set; }

[Column("family")]
public string? Family { get; set; }

[Column("genus")]
public string? Genus { get; set; }

// [Column("abund_value")]
// public double AbundValue { get; set; }
//
// [Column("abund_unit")]
// public string AbundUnit { get; set; }
//
[Column("environment")]
public string? Environment { get; set; }
//
// // [Column("paleomodel")]
// // public string Paleomodel { get; set; }
//
// // [Column("geoplate")]
// // public string Geoplate { get; set; }
//
[Column("paleoage")]
public string? Paleoage { get; set; }

[Column("paleolng")]
public double? PaleoLng { get; set; }

[Column("paleolat")]
public double? PaleoLat { get; set; }

// // [Column("research_group")]
// // public string ResearchGroup { get; set; }
//
[Column("lng")]
public double? Lng { get; set; }

[Column("lat")]
public double? Lat { get; set; }

[Column("composition")]
public string Composition { get; set; }

// [Column("cx_int_no")]
// public int CxIntNo { get; set; }


}