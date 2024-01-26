using System.ComponentModel.DataAnnotations.Schema;
using web.Data;

namespace web.Models;

public class Team : EntityWithTypedId
{
    public string Name { get; set; }
    public ICollection<Player> Players { get; set; }
}