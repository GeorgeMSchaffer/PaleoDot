public class PaginationParams
{
    public PaginationParams(int limit = 25, int skip = 0, string orderBy = "", string sortDir = "ASC")
    {
        this.limit = limit;
        this.skip = skip;
        this.orderBy = orderBy;
        this.sortDir = sortDir;
    }
    public int limit { get; set; }
    public int skip { get; set; }
    public string orderBy { get; set; }

    public void ValidateSortBy<T>()
    {
        var property = typeof(T).GetProperty(orderBy);
        if (property == null)
        {
            Console.WriteLine($"Warning: The property '{orderBy}' does not exist on type '{typeof(T).Name}'.");
        }
    }
    public string sortDir { get; set; }
}