using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class ApiComponent : IEntity, IVisible
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Visible { get; set; }

    public ApiKey ApiKey { get; set; } = new();

    public ICollection<ApiKeyUsage> ApiKeyUsage { get; set; } = new List<ApiKeyUsage>();
    public ICollection<BasicComponent> BasicComponents { get; set; } = new List<BasicComponent>();
}