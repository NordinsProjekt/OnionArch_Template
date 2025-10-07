using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IComponentFactory
{
    IComponentObject Begin();
    Component Build();
}