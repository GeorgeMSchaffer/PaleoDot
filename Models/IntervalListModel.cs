using Backend.Contexts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class IntervalListModel : PageModel
{
    private readonly AppDbContext _context;

    public IntervalListModel(AppDbContext context)
    {
        _context = context;
    }

    public Interval Interval { get; set; }
    public List<Interval> Intervals { get;set; }

    public async Task OnGetAsync()
    {
        await _context.Intervals.ToListAsync();
    }
}