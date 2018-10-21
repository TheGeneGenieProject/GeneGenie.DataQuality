// <copyright file="DateRangeScope.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    public enum DateRangeScope
    {
        NotSet = 0,

        // TODO: For future usage, depending on data source.
        // ExactDateAndTime = 1,
        ExactDateWithTimeRange = 2,
        DateRangeWithTimeRange = 3,
    }
}
