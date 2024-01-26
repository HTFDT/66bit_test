using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace web.Models.ViewModels;

public class CreatePlayerVM : IValidatableObject
{
    [DisplayName("Фамилия")]
    public string LastName { get; set; }
    [DisplayName("Имя")]
    public string FirstName { get; set; }
    [DisplayName("Пол")]
    public Gender Gender { get; set; }
    [DisplayName("Дата рождения")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    [DisplayName("Страна")]
    public Country Country { get; set; }
    [DisplayName("Команда")]
    public Guid? TeamID { get; set; }
    [DisplayName("Название новой команды")] 
    public string? TeamName { get; set; }
    [DisplayName("Новая команда")] 
    public bool IsNewTeam { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsNewTeam && TeamName is null)
            yield return new ValidationResult(
                "Это обязательное поле",
                new[] { nameof(TeamName) });
    }
}