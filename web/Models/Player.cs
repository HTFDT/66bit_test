using System.ComponentModel.DataAnnotations.Schema;
using web.Data;

namespace web.Models;

public class Player : EntityWithTypedId
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Country Country { get; set; }
    public Guid TeamID { get; set; }
    public Team Team { get; set; }
}