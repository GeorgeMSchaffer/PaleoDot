namespace Backend.Models.DTOs;

public interface IPaginationDTO
{
    public int skip { get; set; }
    public int limit { get; set; }
    public string orderBy { get; set; }
    public string sortDir { get; set; }
    public int getPageStart();
    public int getPageEnd();
}
public class PaginationDTO
{
    public int skip { get; set; }
    public int limit { get; set; }
    //Do not allow for more than 999 records to avoid performance issues and DoS attacks
    public void setLimit(int limit)
    {
        if(limit > 999)
        {
            this.limit = 999;
            return;
        }
        this.limit = limit;
    }
    public required string sortBy { get; set; }
    public required string orderDir { get; set; }
    public int getPageEnd()
    {
        return skip * limit;
    }
    public int getPageStart()
    {
        var page =  (skip - 1) * limit;
        return page;
    }
    public string getOrderBy()
    {
        if(sortBy == null){
            return "ASC";
        }
        return sortBy;
    }
    
}