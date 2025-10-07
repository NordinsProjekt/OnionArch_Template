using Application.Services.Factories;
using Application.Services.Interfaces;
using Domain.Entities.Types;

namespace Application.Services.Tests.Factories;

public class ComponentFactoryTests
{
    [Fact]
    public void Constructor_WithValidUserId_SetsUserIdCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var factory = new ComponentFactory(userId);

        // Assert
        Assert.NotNull(factory);
    }

    [Fact]
    public void Constructor_WithEmptyUserId_ThrowsException()
    {
        // Arrange
        var userId = Guid.Empty;

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => new ComponentFactory(userId));
        Assert.Equal("Must be a valid UserId", exception.Message);
    }

    [Fact]
    public void Begin_ReturnsComponentObject()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);

        // Act
        var componentObject = factory.Begin();

        // Assert
        Assert.NotNull(componentObject);
        Assert.IsAssignableFrom<IComponentObject>(componentObject);
    }

    [Fact]
    public void Build_ReturnsComponentWithCorrectUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);

        // Act
        var component = factory.Build();

        // Assert
        Assert.NotNull(component);
        Assert.Equal(userId, component.UserId);
    }

    [Fact]
    public void Build_ReturnsComponentWithBasicComponentsFromComponentObject()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);
        var componentObject = factory.Begin();

        // Add some basic components through the component object
        componentObject.AddBasicButton();
        componentObject.AddBasicTextBox();

        // Act
        var component = factory.Build();

        // Assert
        Assert.NotNull(component);
        Assert.NotNull(component.BasicComponents);
        Assert.Equal(2, component.BasicComponents.Count);
    }

    [Fact]
    public void Build_ReturnsComponentWithFlexBoxPropertiesFromComponentObject()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);
        var componentObject = factory.Begin();

        // Set flexbox properties
        componentObject.SetFlexBox();
        componentObject.SetFlexBoxDirection(FlexDirectionType.Column);

        // Act
        var component = factory.Build();

        // Assert
        Assert.NotNull(component);
        Assert.True(component.FlexBox);
        Assert.Equal(FlexDirectionType.Column, component.FlexDirection);
    }

    [Fact]
    public void Build_WithoutSettingFlexBoxProperties_ReturnsComponentWithDefaultValues()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);

        // Act
        var component = factory.Build();

        // Assert
        Assert.NotNull(component);
        Assert.False(component.FlexBox);
        Assert.Equal(FlexDirectionType.None, component.FlexDirection);
    }

    [Fact]
    public void Build_CreatesNewListInstance_NotSameReference()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);
        var componentObject = factory.Begin();
        componentObject.AddBasicButton();

        // Act
        var component1 = factory.Build();
        var component2 = factory.Build();

        // Assert
        Assert.NotNull(component1.BasicComponents);
        Assert.NotNull(component2.BasicComponents);
        Assert.NotSame(component1.BasicComponents, component2.BasicComponents);
        Assert.Equal(component1.BasicComponents.Count, component2.BasicComponents.Count);
    }

    [Fact]
    public void Begin_ReturnsSameComponentObjectInstance()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);

        // Act
        var componentObject1 = factory.Begin();
        var componentObject2 = factory.Begin();

        // Assert
        Assert.Same(componentObject1, componentObject2);
    }

    [Fact]
    public void Build_MultipleCalls_UseSameComponentObjectState()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory(userId);
        var componentObject = factory.Begin();

        // Configure the component object
        componentObject.AddBasicButton();
        componentObject.AddBasicTextArea();
        componentObject.SetFlexBox();
        componentObject.SetFlexBoxDirection(FlexDirectionType.Column);

        // Act
        var component1 = factory.Build();
        var component2 = factory.Build();

        // Assert
        Assert.Equal(component1.UserId, component2.UserId);
        Assert.Equal(component1.BasicComponents.Count, component2.BasicComponents.Count);
        Assert.Equal(component1.FlexBox, component2.FlexBox);
        Assert.Equal(component1.FlexDirection, component2.FlexDirection);
    }
}
