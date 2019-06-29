namespace InMemoryDatabase.UnitTests.ConfigMethods
{
    using System;
    using FluentAssertions;
    using InMemoryDatabase.Attributes;
    using InMemoryDatabase.Setup;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SetupCollectionsTests
    {
        private IServiceCollection _services => new ServiceCollection();

        [Fact]
        public void SetupInMemoryCollection_AddCollectionToServices_ServicesShouldHaveCollection()
        {
            // Arrange
            var services = _services;

            // Act
            services.SetupInMemoryCollection<ClassWithOneIdentifier>();

            // Assert
            services.Count.Should().Be(1);
        }

        private class ClassWithOneIdentifier
        {
            [Identifier]
            public Guid Id { get; set; }

            public string Name { get; set; }
        }
    }
}