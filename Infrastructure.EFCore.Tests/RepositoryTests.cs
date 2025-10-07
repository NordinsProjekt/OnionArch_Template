using Application.Services.Factories;
using Domain.Entities;
using Domain.Entities.Types;
using Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Tests;

public class RepositoryTests : IDisposable
{
    private readonly CMSDbContext _context;
    private readonly Repository<Component> _repository;

    public RepositoryTests()
    {
        // Setup In-Memory Database with a unique name for each test instance
        var options = new DbContextOptionsBuilder<CMSDbContext>()
            .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}")
            .Options;

        _context = new CMSDbContext(options);
        _repository = new Repository<Component>(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddComponentToDatabase()
    {
        // Arrange
        var component = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Component",
            Description = "Test Description",
            Visible = true,
            Height = 100,
            Width = 200,
            ZIndex = 1,
            FlexBox = true,
            FlexDirection = FlexDirectionType.Row
        };

        // Act
        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var savedComponent = await _repository.GetByIdAsync(component.Id);
        Assert.NotNull(savedComponent);
        Assert.Equal(component.Name, savedComponent.Name);
        Assert.Equal(component.Description, savedComponent.Description);
        Assert.Equal(component.Visible, savedComponent.Visible);
        Assert.Equal(component.Height, savedComponent.Height);
        Assert.Equal(component.Width, savedComponent.Width);
        Assert.Equal(component.ZIndex, savedComponent.ZIndex);
    }

    [Fact]
    public async Task AddAsync_MultipleComponents_ShouldSaveAll()
    {
        // Arrange
        var component1 = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Component 1",
            Description = "First component",
            Visible = true,
            Height = 100,
            Width = 200,
            ZIndex = 1
        };

        var component2 = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Component 2",
            Description = "Second component",
            Visible = false,
            Height = 150,
            Width = 250,
            ZIndex = 2
        };

        // Act
        await _repository.AddAsync(component1);
        await _repository.AddAsync(component2);
        await _repository.SaveChangesAsync();

        // Assert
        var allComponents = await _repository.GetAllAsync();
        Assert.Equal(2, allComponents.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateComponentInDatabase()
    {
        // Arrange
        var component = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Original Name",
            Description = "Original Description",
            Visible = true,
            Height = 100,
            Width = 200,
            ZIndex = 1
        };

        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Act
        component.Name = "Updated Name";
        component.Description = "Updated Description";
        component.Height = 300;
        await _repository.UpdateAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var updatedComponent = await _repository.GetByIdAsync(component.Id);
        Assert.NotNull(updatedComponent);
        Assert.Equal("Updated Name", updatedComponent.Name);
        Assert.Equal("Updated Description", updatedComponent.Description);
        Assert.Equal(300, updatedComponent.Height);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveComponentFromDatabase()
    {
        // Arrange
        var component = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Component to Delete",
            Description = "Will be deleted",
            Visible = true,
            Height = 100,
            Width = 200,
            ZIndex = 1
        };

        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var deletedComponent = await _repository.GetByIdAsync(component.Id);
        Assert.Null(deletedComponent);
    }

    [Fact]
    public async Task FindAsync_ShouldReturnMatchingComponents()
    {
        // Arrange
        var component1 = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Visible Component",
            Description = "This is visible",
            Visible = true,
            Height = 100,
            Width = 200,
            ZIndex = 1
        };

        var component2 = new Component
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Hidden Component",
            Description = "This is hidden",
            Visible = false,
            Height = 100,
            Width = 200,
            ZIndex = 1
        };

        await _repository.AddAsync(component1);
        await _repository.AddAsync(component2);
        await _repository.SaveChangesAsync();

        // Act
        var visibleComponents = await _repository.FindAsync(c => c.Visible == true);

        // Assert
        Assert.Single(visibleComponents);
        Assert.Equal("Visible Component", visibleComponents.First().Name);
    }

    [Fact]
    public async Task ComponentFactory_WithBasicButton_ShouldSaveComponentAndBasicComponents()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        factory
            .Begin(userId)
            .SetFlexBox()
            .SetFlexBoxDirection()
            .AddBasicButton();

        var component = factory.Build();

        component.Id = Guid.NewGuid();
        component.Name = "Component with Button";
        component.Description = "Component created via factory";

        // Act
        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var savedComponent = await _context.Components
            .Include(c => c.BasicComponents)
            .FirstOrDefaultAsync(c => c.Id == component.Id);

        Assert.NotNull(savedComponent);
        Assert.Equal(userId, savedComponent.UserId);
        Assert.True(savedComponent.FlexBox);
        Assert.Equal(FlexDirectionType.Row, savedComponent.FlexDirection);
        Assert.Single(savedComponent.BasicComponents);
        Assert.IsType<ButtonBasicComponent>(savedComponent.BasicComponents.First());

        var button = savedComponent.BasicComponents.First() as ButtonBasicComponent;
        Assert.NotNull(button);
        Assert.Equal("Ny knapp", button.Text);
    }

    [Fact]
    public async Task ComponentFactory_WithMultipleBasicComponents_ShouldSaveAll()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        factory
            .Begin(userId)
            .SetFlexBoxDirection(FlexDirectionType.Column)
            .AddBasicButton()
            .AddBasicTextBox()
            .AddBasicTextArea();

        var component = factory.Build();

        component.Id = Guid.NewGuid();
        component.Name = "Complex Component";
        component.Description = "Component with multiple basic components";
        component.Visible = true;
        component.Height = 400;
        component.Width = 600;

        // Act
        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var savedComponent = await _context.Components
            .Include(c => c.BasicComponents)
            .FirstOrDefaultAsync(c => c.Id == component.Id);

        Assert.NotNull(savedComponent);
        Assert.Equal(3, savedComponent.BasicComponents.Count);

        Assert.Contains(savedComponent.BasicComponents, bc => bc is ButtonBasicComponent);
        Assert.Contains(savedComponent.BasicComponents, bc => bc is TextBasicComponent);
        Assert.Contains(savedComponent.BasicComponents, bc => bc is TextAreaBasicComponent);
    }

    [Fact]
    public async Task ComponentFactory_WithFlexBoxSettings_ShouldSaveCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        factory
            .Begin(userId)
            .SetFlexBoxDirection(FlexDirectionType.Column)
            .AddBasicButton();

        var component = factory.Build();

        component.Id = Guid.NewGuid();
        component.Name = "FlexBox Test Component";

        // Act
        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var savedComponent = await _repository.GetByIdAsync(component.Id);
        Assert.NotNull(savedComponent);
        Assert.True(savedComponent.FlexBox);
        Assert.Equal(FlexDirectionType.Column, savedComponent.FlexDirection);
    }

    [Fact]
    public async Task ComponentFactory_MultipleComponents_ShouldEachHaveTheirOwnBasicComponents()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var factory1 = new ComponentFactory();
        factory1.Begin(userId).AddBasicButton();
        var component1 = factory1.Build();
        component1.Id = Guid.NewGuid();
        component1.Name = "Component 1";

        var factory2 = new ComponentFactory();
        factory2.Begin(userId)
            .AddBasicTextBox()
            .AddBasicTextArea();
        var component2 = factory2.Build();
        component2.Id = Guid.NewGuid();
        component2.Name = "Component 2";

        // Act
        await _repository.AddAsync(component1);
        await _repository.AddAsync(component2);
        await _repository.SaveChangesAsync();

        // Assert
        var savedComponent1 = await _context.Components
            .Include(c => c.BasicComponents)
            .FirstOrDefaultAsync(c => c.Id == component1.Id);

        var savedComponent2 = await _context.Components
            .Include(c => c.BasicComponents)
            .FirstOrDefaultAsync(c => c.Id == component2.Id);

        Assert.NotNull(savedComponent1);
        Assert.NotNull(savedComponent2);
        Assert.Single(savedComponent1.BasicComponents);
        Assert.Equal(2, savedComponent2.BasicComponents.Count);
    }

    [Fact]
    public void ComponentFactory_InvalidUserId_ShouldThrowException()
    {
        // Arrange
        var factory = new ComponentFactory();
        
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => factory.Begin(Guid.Empty));
        Assert.Equal("Must be a valid UserId", exception.Message);
    }

    [Fact]
    public async Task ComponentFactory_BuildWithoutBasicComponents_ShouldSaveEmptyCollection()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        factory.Begin(userId).SetFlexBox(false);
        var component = factory.Build();

        component.Id = Guid.NewGuid();
        component.Name = "Empty Component";

        // Act
        await _repository.AddAsync(component);
        await _repository.SaveChangesAsync();

        // Assert
        var savedComponent = await _context.Components
            .Include(c => c.BasicComponents)
            .FirstOrDefaultAsync(c => c.Id == component.Id);

        Assert.NotNull(savedComponent);
        Assert.Empty(savedComponent.BasicComponents);
        Assert.False(savedComponent.FlexBox);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}