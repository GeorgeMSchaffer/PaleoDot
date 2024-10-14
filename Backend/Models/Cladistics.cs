using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace Backend.Models;
using Backend.POJO;

public class Cladistics
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  
  [Column("id")]
  [Required]
  public int Id { get; set; }
  
  [Required]
  [Column("clade", TypeName = "varchar(255)" )]
  public string Clade { get; set; }
  
  [Column("rank", TypeName = "varchar(255)" )]
  [Required]
  public EnumCladistics Rank { get; set; }
}