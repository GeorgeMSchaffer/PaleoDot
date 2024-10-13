using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Backend.Models
{
    [Table("intervals", Schema = "paleo")]
    public class Interval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("interval_no")]
        [JsonPropertyName("interval_no")]
        //[ForeignKey("Occurrence")]
        public int IntervalNo { get; set; }

        // [ForeignKey("ParentNo")]
        // public virtual Interval Parent { get; set; }
        //
        // [InverseProperty("Parent")]
        // public virtual ICollection<Interval> Children { get; set; } = new HashSet<Interval>();

        [Column("interval_name"), StringLength(512)]
        [JsonPropertyName("interval_name")]
        [JsonProperty("interval_name")]
        public string? IntervalName { get; set; }
        // [ForeignKey("interval_name")]
 //       [ForeignKey("EarlyIntervalNo")]
        public IEnumerable<Occurrence> Occurrences { get; set; } = new List<Occurrence>();

        
        [Column("min_ma")]
        [JsonPropertyName("min_ma")]
        public int? MinMya { get; set; }

        [Column("max_ma")]
        [JsonPropertyName("max_ma")]
        public int? MaxMya { get; set; }

        [Column("color"), StringLength(255)]
        [JsonPropertyName("color")]
        [JsonProperty("color")]
        public string? Color { get; set; }

         [Column("parent_no")]
        public string? ParentNo { get; set; }
        // [ForeignKey("ParentNo")]
        // public Interval Parent { get; set; }

        [Column("record_type"), StringLength(255)]
        [JsonPropertyName("record_type")]
        [JsonProperty("record_type")]
        public string? RecordType { get; set; }

        [JsonPropertyName("reference_no")]
        [JsonProperty("reference_no")]
        [Column("reference_no")]
        public int? ReferenceNo { get; set; }

        [Column("scale_no")]
        [JsonPropertyName("scale_no")]
        [JsonProperty("scale_no")]
        public int? ScaleNo { get; set; }

        // public IEnumerator GetEnumerator()
        // {
        //     throw new NotImplementedException();
        // }
    }
}