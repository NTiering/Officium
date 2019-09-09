using FluentAssert;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Officium.Plugins.Tests
{
    public class RegisterTests
    {
        [Fact]
        public void CanBeConstucted()
        {
            new Register(null).ShouldNotBeNull();
        }

        [Fact]
        public void CanAddServiceCollection()
        {
            // arrange 
            var serviceCollection = new Mock<IServiceCollection>();

            // act
            new Register(serviceCollection.Object).ShouldNotBeNull();
        }
    }
}
