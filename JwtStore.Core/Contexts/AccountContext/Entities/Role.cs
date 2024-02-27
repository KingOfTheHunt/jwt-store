using JwtStore.Core.Contexts.SharedContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;
    public IList<User> Users { get; set; } = new List<User>();
}