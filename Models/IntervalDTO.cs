using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Backend.Models;

[Table("intervals")]
public class IntervalDTO
{
    public int intervalNo { get; set; }
    public String? recordType { get; set; }
    public String? intervalName { get; set; }
    public String? abbrev { get; set; }
    public String? type { get; set; }
    public int? parentNo { get; set; }
    public String? color { get; set; }
    public Decimal? tAge { get; set; }
    public Decimal? bAge { get; set; }
    public int? referenceNo { get; set; }
}