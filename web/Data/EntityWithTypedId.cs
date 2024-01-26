using System.ComponentModel.DataAnnotations.Schema;

namespace web.Data;

public class EntityWithTypedId : IEntityWithTypedId<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }
}