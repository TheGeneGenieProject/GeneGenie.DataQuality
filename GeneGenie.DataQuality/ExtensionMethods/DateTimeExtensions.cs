// <copyright file="DateTimeExtensions.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.ExtensionMethods
{
    /// <summary>
    /// Helper methods to find the end of the day or month. Useful for working with date ranges
    /// or when finding all events on a day.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the absolute last moment before midnight for the passed date.
        /// </summary>
        /// <param name="dateValue">The date you want the to return just before midnight for.</param>
        /// <returns>A <see cref="DateTime"/> set to just before midnight of the passed in date.</returns>
        public static DateTime EndOfDay(this DateTime dateValue)
        {
            return dateValue.Date.AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Given a date will return the a date set to the last day of the month.
        /// </summary>
        /// <param name="dateValue">The date you want the last day of the month for.</param>
        /// <returns>A <see cref="DateTime"/> with the day set to the last day of the month.</returns>
        public static DateTime EndOfMonth(this DateTime dateValue)
        {
            var daysInMonth = DateTime.DaysInMonth(dateValue.Year, dateValue.Month);
            return new DateTime(dateValue.Year, dateValue.Month, daysInMonth).EndOfDay();
        }
    }
}
