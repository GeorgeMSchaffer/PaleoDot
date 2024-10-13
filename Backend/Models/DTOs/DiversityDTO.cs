namespace Backend.Models.DTOs;

public class DiversityDTO
{
  
  public string intervalName { get; set; }
  public string? color { get;set; }
  public double? minMya { get; set; }
  public double? maxMya { get; set; }
  public int CountOfPhylum { get; set; }
  public int CountOfClasses { get; set; }
  public int CountOfOrders { get; set; }
  public int CountOfFamilies { get; set; }

  public int CountOfGenera { get; set; }
  public int CountOfOccurrences { get; set; }
  // public DiversityCountsDTO(string? intervalName, int? countOfPhylum, int? countOfClasses, int? countOfOrders, int countOfFamilies, int countOfGenera)
  // {
  //   this.intervalName = intervalName;
  //   CountOfPhylum = countOfPhylum;
  //   CountOfClasses = countOfClasses;
  //   CountOfOrders = countOfOrders;
  //   CountOfFamilies = countOfFamilies;
  //   CountOfGenera = countOfGenera;
  // }
}
