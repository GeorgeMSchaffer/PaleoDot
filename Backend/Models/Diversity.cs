namespace Backend.Models;

public class Diversity
{
  public string intervalName { get; set; } 
  public double? minMya { get; set; }
  public double? maxMya { get; set; }
  public string? color { get;set; }

  public int CountOfPhylum { get; set; }
  public int CountOfClasses { get; set; }
  public int CountOfOrders { get; set; }
  public int CountOfFamilies { get; set; }
  public int CountOfGenera { get; set; }
  public int CountOfOccurences { get; set; }
}