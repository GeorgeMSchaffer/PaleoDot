using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Backend.Models;

[Table("intervals")]
public class IntervalDTO
{
    public int IntervalNo { get; set; }
    public String? RecordType { get; set; }
    public String? IntervalName { get; set; }
    public String? Abbrev { get; set; }
    public String? Type { get; set; }
    public int? ParentNo { get; set; }
    public String? Color { get; set; }
    public int? MinMya { get; set; }
    public int? MaxMYA { get; set; }
    public int? referenceNo { get; set; }
    public int? ScaleNo { get; set; }
}