using Backend.Contexts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class OccurrenceListModel : PageModel
{
    private readonly AppDbContext _context;

    public OccurrenceListModel(AppDbContext context)
    {
        _context = context;
    }

    public Occurrence Occurrence { get; set; }
    public List<Occurrence> Occurrences { get;set; }

    public async Task OnGetAsync()
    {
        await _context.Occurrences.ToListAsync();
    }
}