using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Pages.Players;

public class IndexModel : PageModel
{
    private IRepository<Player> _repository;
    
    public IndexModel(IRepository<Player> repo)
    {
        _repository = repo;
    }

    public IList<Player> Players { get; set; } = new List<Player>();
    
    public async Task OnGetAsync()
    {
        Players = await _repository.Query().AsNoTracking().Include(e => e.Team).ToListAsync();
    }
}