using Domain.Entities;

namespace Application.Services.Factories.Interfaces;

public interface IApiComponentFactory
{
    IApiComponentObject Begin(Guid userId);
    ApiComponent Build();
}