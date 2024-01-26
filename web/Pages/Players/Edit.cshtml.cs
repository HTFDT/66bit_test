using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Models.ViewModels;

namespace web.Pages.Players;

public class EditModel : PageModel
{
    private IRepository<Player> _playersRepository;
    private IRepository<Team> _teamsRepository;
    
    public EditModel(IRepository<Player> playersRepo, IRepository<Team> teamsRepo)
    {
        _playersRepository = playersRepo;
        _teamsRepository = teamsRepo;
    }
    [BindProperty] public EditPlayerVM ViewModel { get; set; } = new();
    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id is null)
            return NotFound();

        var player = await _playersRepository.FindById(id.Value);
        if (player is null)
            return NotFound();

        ViewModel = new EditPlayerVM
        {
            FirstName = player.FirstName,
            LastName = player.LastName,
            Country = player.Country,
            DateOfBirth = player.DateOfBirth,
            Gender = player.Gender,
            TeamID = player.TeamID
        };

        ViewData["existingTeams"] = await _teamsRepository.GetAll();
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        if (!ModelState.IsValid)
        {
            ViewData["existingTeams"] = await _teamsRepository.GetAll();
            return Page();
        }
        
        var player = await _playersRepository.FindById(id, true);
        if (player is null)
            return NotFound();
        player.FirstName = ViewModel.FirstName;
        player.LastName = ViewModel.LastName;
        player.Country = ViewModel.Country;
        player.DateOfBirth = ViewModel.DateOfBirth;
        player.Gender = ViewModel.Gender;
        player.TeamID = ViewModel.TeamID;
        await _playersRepository.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}