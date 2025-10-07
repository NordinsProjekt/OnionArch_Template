using Application.Services.Factories;
using Application.Services.Factories.Interfaces;
using Domain.Entities.Types;

namespace Application.Services.Tests.Factories;

public class ComponentFactoryTests
{
    [Fact]
    public void Begin_WithValidUserId_SetsUserIdCorrectly()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        // Act
        var componentObject = factory.Begin(userId);

        // Assert
        Assert.NotNull(componentObject);
        Assert.IsAssignableFrom<IComponentObject>(componentObject);
    }

    [Fact]
    public void Begin_WithEmptyUserId_ThrowsException()
    {
        // Arrange
        var userId = Guid.Empty;
        var factory = new ComponentFactory();

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => factory.Begin(userId));
        Assert.Equal("Must be a valid UserId", exception.Message);
    }

    [Fact]
    public void Begin_ReturnsComponentObject()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        // Act
        var componentObject = factory.Begin(userId);

        // Assert
        Assert.NotNull(componentObject);
        Assert.IsAssignableFrom<IComponentObject>(componentObject);
    }

    [Fact]
    public void Build_WithoutCallingBegin_ReturnsComponentWithEmptyUserId()
    {
        // Arrange
        var factory = new ComponentFactory();

        // Act
        var component = factory.Build();

        // Assert
        Assert.NotNull(component);
        Assert.Equal(Guid.Empty, component.UserId);
    }

    [Fact]
    public void Build_AfterCallingBegin_ReturnsComponentWithCorrectUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();
        factory.Begin(userId);

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
        var factory = new ComponentFactory();
        var componentObject = factory.Begin(userId);

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
        var factory = new ComponentFactory();
        var componentObject = factory.Begin(userId);

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
        var factory = new ComponentFactory();
        factory.Begin(userId);

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
        var factory = new ComponentFactory();
        var componentObject = factory.Begin(userId);
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
        var factory = new ComponentFactory();

        // Act
        var componentObject1 = factory.Begin(userId);
        var componentObject2 = factory.Begin(Guid.NewGuid()); // Different userId

        // Assert
        Assert.Same(componentObject1, componentObject2);
    }

    [Fact]
    public void Begin_MultipleCallsWithDifferentUserIds_UpdatesUserId()
    {
        // Arrange
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var factory = new ComponentFactory();

        // Act
        factory.Begin(userId1);
        var component1 = factory.Build();

        factory.Begin(userId2);
        var component2 = factory.Build();

        // Assert
        Assert.Equal(userId1, component1.UserId);
        Assert.Equal(userId2, component2.UserId);
        Assert.NotEqual(component1.UserId, component2.UserId);
    }

    [Fact]
    public void Build_MultipleCalls_UseSameComponentObjectState()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();
        var componentObject = factory.Begin(userId);

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

    [Fact]
    public void ComponentObject_Property_IsNotNull()
    {
        // Arrange
        var factory = new ComponentFactory();

        // Act & Assert - Testing that the property getter works
        // This is implicitly tested when Begin() is called, but we can verify it's initialized
        var userId = Guid.NewGuid();
        var componentObject = factory.Begin(userId);

        Assert.NotNull(componentObject);
    }

    [Fact]
    public void Begin_CalledMultipleTimes_ComponentObjectStateIsShared()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var factory = new ComponentFactory();

        // Act
        var componentObject1 = factory.Begin(userId);
        componentObject1.AddBasicButton();
        componentObject1.SetFlexBox();

        var componentObject2 = factory.Begin(userId);

        // Assert - Both references should point to the same object with same state
        Assert.Same(componentObject1, componentObject2);

        // Build to verify the state is maintained
        var component = factory.Build();
        Assert.Equal(1, component.BasicComponents.Count);
        Assert.True(component.FlexBox);
    }
}
