using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Models.ViewModels;

namespace web.Pages.Players;

public class CreateModel : PageModel
{
    private IRepository<Player> _playersRepository;
    private IRepository<Team> _teamsRepository;
    private ApplicationDbContext _context;

    public CreateModel(IRepository<Player> playerRepo, IRepository<Team> teamRepo, ApplicationDbContext context)
    {
        _playersRepository = playerRepo;
        _teamsRepository = teamRepo;
        _context = context;
    }

    public async Task OnGetAsync()
    {
        ViewData["existingTeams"] = await _teamsRepository.GetAll();
    }

    [BindProperty] public CreatePlayerVM ViewModel { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ViewData["existingTeams"] = await _teamsRepository.GetAll();
            return Page();
        }
            

        switch (ViewModel)
        {
            case { IsNewTeam: true, TeamName: not null }:
                try
                {
                    var newTeam = new Team
                    {
                        Name = ViewModel.TeamName
                    };
                    var newPlayer = new Player
                    {
                        FirstName = ViewModel.FirstName,
                        LastName = ViewModel.LastName,
                        Gender = ViewModel.Gender,
                        Country = ViewModel.Country,
                        DateOfBirth = ViewModel.DateOfBirth,
                    };
                    await _context.Database.BeginTransactionAsync();
                    await _teamsRepository.AddAsync(newTeam);
                    newPlayer.Team = newTeam;
                    await _playersRepository.AddAsync(newPlayer);
                    await _context.Database.CommitTransactionAsync();
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await _context.Database.RollbackTransactionAsync();
                }

                break;
            case { IsNewTeam: false, TeamID: not null }:
            {
                var newPlayer = new Player
                {
                    FirstName = ViewModel.FirstName,
                    LastName = ViewModel.LastName,
                    Gender = ViewModel.Gender,
                    Country = ViewModel.Country,
                    DateOfBirth = ViewModel.DateOfBirth,
                    TeamID = ViewModel.TeamID.Value
                };
                await _playersRepository.AddAsync(newPlayer);
                await _context.SaveChangesAsync();
                break;
            }
        }
    
        return RedirectToPage("./Index");
    }
}