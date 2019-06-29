namespace InMemoryDatabase.UnitTests.ExtensionMethods
{
    using System;
    using FluentAssertions;
    using InMemoryDatabase.Attributes;
    using InMemoryDatabase.Exceptions;
    using InMemoryDatabase.Extensions;
    using Xunit;

    public class IdentifierExtensionsTests
    {
        [Fact]
        public void GetIdentifier_ClassWithOneIdProperty_ShouldPass()
        {
            // Arrange
            var entity = new ClassWithOneIdentifier
            {
                Id = Guid.NewGuid(),
                Name = "Lucas"
            };

            // Act
            Action act = () => entity.GetIdentifier();

            // Assert
            act.Should()
                .NotThrow();
        }

        [Fact]
        public void GetIdentifier_ClassWithTwoIdProperty_ShouldPass()
        {
            // Arrange
            var entity = new ClassWithTwoIdentifiers
            {
                FirstName = "Lucas",
                LastName = "Poley"
            };

            // Act
            Action act = () => entity.GetIdentifier();

            // Assert
            act.Should()
                .NotThrow();
        }

        [Fact]
        public void GetIdentifier_ClassWithTwoIdPropertyWithSameOrder_ShouldThrow()
        {
            // Arrange
            var entity = new ClassWithTwoIdentifiersInSameOrder
            {
                FirstName = "Lucas",
                LastName = "Poley"
            };

            // Act
            Action act = () => entity.GetIdentifier();

            // Assert
            act.Should()
                .Throw<InvalidIdentityException>()
                .WithMessage("Found identifier properties with same order number");
        }

        [Fact]
        public void GetIdentifier_ClassWithNoIdProperty_ShouldThrow()
        {
            // Arrange
            var entity = new ClassWithNoIdentifiers
            {
                FirstName = "Lucas",
                LastName = "Poley"
            };

            // Act
            Action act = () => entity.GetIdentifier();

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