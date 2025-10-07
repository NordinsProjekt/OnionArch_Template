using Domain.Entities;

namespace Application.Services.Factories.Interfaces;

public interface IComponentFactory
{
    IComponentObject Begin(Guid userId);
    Component Build();
}