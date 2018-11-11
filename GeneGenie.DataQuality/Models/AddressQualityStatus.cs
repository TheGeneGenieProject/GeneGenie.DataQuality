// <copyright file="AddressQualityStatus.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    /// <summary>
    /// Addresses are parsed to one of these status' before doing an expensive
    /// lookup to see if they are junk data or not.
    /// </summary>
    public enum AddressQualityStatus
    {
        /// <summary>
        /// Address has not been parsed yet.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// It says 'address' on the tin, but it's a date!
        /// </summary>
        SeemsToBeADate = 1,

        /// <summary>
        /// Just a numch of numbers, not parseable as an address.
        /// </summary>
        AllNumeric = 2,

        /// <summary>
        /// Known junk data such as 'unknown'.
        /// </summary>
        KnownErroneous = 3,

        /// <summary>
        /// Whitespace or null.
        /// </summary>
        Empty = 4,

        /// <summary>
        /// The address appears to have enough information that an address lookup can be performed.
        /// </summary>
        OK = 100,
    }
}
