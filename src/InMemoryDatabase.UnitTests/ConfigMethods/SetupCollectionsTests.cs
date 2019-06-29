namespace InMemoryDatabase.UnitTests.ConfigMethods
{
    using System;
    using FluentAssertions;
    using InMemoryDatabase.Exceptions;
    using InMemoryDatabase.Extensions;
    using InMemoryDatabase.Identifier;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SetupCollectionsTests
    {
        private IServiceCollection _services => new ServiceCollection();

        public SetupCollectionsTests()
        {
        }

        [Fact]
        public void SetupInMemoryCollection_ClassWithOneIdProperty_ShouldPass()
        {
            // Arrange
            var services = _services;

            // Act
            Action act = () => services.SetupInMemoryCollection<ClassWithOneIdentifier>();

            // Assert
            act.Should()
                .NotThrow();
        }

        [Fact]
        public void SetupInMemoryCollection_ClassWithTwoIdProperty_ShouldPass()
        {
            // Arrange
            var services = _services;

            // Act
            Action act = () => services.SetupInMemoryCollection<ClassWithTwoIdentifiers>();

            // Assert
            act.Should()
                .NotThrow();
        }

        [Fact]
        public void SetupInMemoryCollection_ClassWithTwoIdPropertyWithSameOrder_ShouldThrow()
        {
            // Arrange
            var services = _services;

            // Act
            Action act = () => services.SetupInMemoryCollection<ClassWithTwoIdentifiersInSameOrder>();

            // Assert
            act.Should()
                .Throw<InvalidIdentityException>()
                .WithMessage("Found identifier properties with same order number");
        }

        [Fact]
        public void SetupInMemoryCollection_ClassWithNoIdProperty_ShouldThrow()
        {
            // Arrange
            var services = _services;

            // Act
            Action act = () => services.SetupInMemoryCollection<ClassWithNoIdentifiers>();

            // Assert
            act.Should()
                .Throw<InvalidIdentityException>()
                .WithMessage("Class must have an Identifier");
        }

        class ClassWithOneIdentifier
        {
            [Identifier]
            public Guid Id { get; set; }

            public string Name { get; set; }
        }

        class ClassWithTwoIdentifiers
        {
            [Identifier]
            public string FirstName { get; set; }

            [Identifier(2)]
            public string LastName { get; set; }
        }

        class ClassWithTwoIdentifiersInSameOrder
        {
            [Identifier]
            public string FirstName { get; set; }

            [Identifier]
            public string LastName { get; set; }
        }

        class ClassWithNoIdentifiers
        {
            public string FirstName { get; set; }
            
            public string LastName { get; set; }
        }
    }
}