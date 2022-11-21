// <copyright file="DependencyRegistration.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.IntegrationTests
{
    using ExtensionMethods;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Tests to ensure the library can be registered and consumed from an external project.
    /// </summary>
    public class DependencyRegistration
    {
        /// <summary>
        /// Ensures every year in the census enum has a date.
        /// </summary>
        [Fact]
        public void Dependency_injection_is_registered_and_classes_can_be_resolved()
        {
            var serviceProvider = new ServiceCollection()
                .AddDataQuality()
                .BuildServiceProvider();

            var addressQualityChecker = serviceProvider.GetRequiredService<AddressQualityChecker>();

            Assert.NotNull(addressQualityChecker);
        }
    }
}
