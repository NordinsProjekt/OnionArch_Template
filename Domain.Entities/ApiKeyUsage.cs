using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class ApiKeyUsage : IEntity
{
    public Guid Id { get; set; }
    public Guid ApiKeyId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Endpoint { get; set; } = string.Empty;
    public string HttpMethod { get; set; } = string.Empty;
}