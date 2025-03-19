using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Models
{
    //[TODO:] Maybe add example return objects per https://medium.com/@niteshsinghal85/multiple-request-response-examples-for-swagger-ui-in-asp-net-core-864c0bdc6619 
    
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
        [SwaggerSchema("The name of the geological interval as defined by the International Commission on Stratigraphy.")]
        public string? IntervalName { get; set; }
        // [ForeignKey("interval_name")]
 //       [ForeignKey("EarlyIntervalNo")]
        
        [SwaggerSchema("Fossil occurrences that are associated with the interval(s).")]
        public IEnumerable<Occurrence> Occurrences { get; set; } = new List<Occurrence>();

        
        [Column("min_ma")]
        [JsonPropertyName("min_ma")]
        [SwaggerSchema("The start of the interval in millions of years ago.")]
        public int? MinMya { get; set; }

        [Column("max_ma")]
        [JsonPropertyName("max_ma")]
        [SwaggerSchema("The end of the interval in millions of years ago.")]
        public int? MaxMya { get; set; }

        [Column("color"), StringLength(255)]
        [JsonPropertyName("color")]
        [JsonProperty("color")]
        [SwaggerSchema("The color associated with the interval.")]
        public string? Color { get; set; }

         [Column("parent_no")]
        public string? ParentNo { get; set; }
        // [ForeignKey("ParentNo")]
        // public Interval Parent { get; set; }

        [Column("record_type"), StringLength(255)]
        [JsonPropertyName("record_type")]
        [JsonProperty("record_type")]
        [SwaggerSchema("The type of the interval. Either 'eon', 'era', 'period', 'epoch', 'age', 'int'")]
        public string? RecordType { get; set; }

        [JsonPropertyName("reference_no")]
        [JsonProperty("reference_no")]
        [Column("reference_no")]
        public int? ReferenceNo { get; set; }

        [Column("scale_no")]
        [JsonPropertyName("scale_no")]
        [JsonProperty("scale_no")]
        public int? ScaleNo { get; set; }
    }
}