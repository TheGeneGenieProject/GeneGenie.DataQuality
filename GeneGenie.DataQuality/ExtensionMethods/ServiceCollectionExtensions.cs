// <copyright file="ServiceCollectionExtensions.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.ExtensionMethods
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods used for registering and resolving the services used by this library with the frameworks dependency injection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for consuming this package from another app.
        /// </summary>
        /// <param name="serviceCollection">The service collection to add the registrations to.</param>
        /// <returns>The service collection with all data quality classes registered.</returns>
        public static IServiceCollection AddDataQuality(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}
