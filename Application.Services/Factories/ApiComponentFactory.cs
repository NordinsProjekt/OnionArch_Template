using Application.Services.Factories.Interfaces;
using Domain.Entities;

namespace Application.Services.Factories;

public class ApiComponentFactory : IApiComponentFactory
{
    private Guid UserId { get; set; }
    private ApiComponentObject ApiComponentObject { get; set; } = new();

    public IApiComponentObject Begin(Guid userId)
    {
        if (userId == Guid.Empty) throw new Exception("Must have a valid UserId");

        UserId = userId;

        return ApiComponentObject;
    }

    public ApiComponent Build()
    {
        return new ApiComponent { UserId = UserId };
    }
}
