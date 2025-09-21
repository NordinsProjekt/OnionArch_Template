using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class ApiKey : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ComponentId { get; set; }

    public string EncryptedKey { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}