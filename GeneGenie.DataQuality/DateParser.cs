// <copyright file="DateParser.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality
{
    using ExtensionMethods;
    using Models;

    /// <summary>
    /// This is only for parsing dates from DNAGedcom, we should merge with the code from GeneGenie for fuller date parsing.
    /// </summary>
    /// <remarks>
    /// Limited to years from 50 - 2050, could do better but good enough for now.
    /// </remarks>
    public static class DateParser
    {
        private const int MinMonth = 1;
        private const int MaxMonth = 12;
        private const int MinDay = 1;
        private const int MaxDay = 31;
        private const int MinMonthNameLength = 3; // Don't want to compare to 'ju', it might match jun or jul.

        private static readonly char[] DateDelimiters = { ' ', '-', '/', '\\' };

        // Should not be less than or equal to 31 as could be confused with the day.
        private static readonly int MinYear = 50;

        // We only deal with historical dates, but just in case.
        private static readonly int MaxYear = DateTime.Now.Year + 10;

        private static readonly List<string> fullEnglishMonthNames = new List<string>
        {
            "january", "february", "march", "april", "may", "jun", "july", "august", "september", "october", "november", "december",
        };

        /// <summary>
        /// Parses a textual input date into a date range.
        /// The input can be a partial date such as a year and a month
        /// which is then split into a range representing the whole month.
        /// </summary>
        /// <param name="value">The text to parse that should contain a user entered date string.</param>
        /// <returns>A <see cref="DateRange"/> with the status of the parse, the source value
        /// and the parsed date range if valid.
        /// </returns>
        public static DateRange Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new DateRange(value) { Status = DateQualityStatus.Empty };
            }

            var dateComponents = value
                .Split(DateDelimiters)
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .ToList();
            if (dateComponents.Count == 0 || dateComponents.Count > 3)
            {
                return new DateRange(value) { Status = DateQualityStatus.TooManyDateParts };
            }

            if (dateComponents.Count == 1)
            {
                return ParseSingleComponent(value, ref dateComponents);
            }

            if (dateComponents.Count == 2)
            {
                return ParseTwoComponents(value, ref dateComponents);
            }

            // Must be 3 parts in the date, extract the year and record where we found it.
            return ParseThreeComponents(value, ref dateComponents);
        }

        private static DateRange ParseSingleComponent(string value, ref List<string> dateComponents)
        {
            var dateRange = new DateRange(value);

            // Just a year?
            if (TextIsNumberBetween(dateComponents[0], MinYear, MaxYear))
            {
                var year = int.Parse(dateComponents[0]);
                dateRange.DateFrom = new DateTime(year, MinMonth, MinDay);
                dateRange.DateTo = new DateTime(year, MaxMonth, MaxDay).EndOfDay();
                dateRange.SourceFormat = DateFormat.Yyyy;

                dateRange.Status = DateQualityStatus.OK;
                return dateRange;
            }

            // Just a month?
            if (ExtractMonth(ref dateComponents, out var _, out var monthIsNamed) >= 0)
            {
                dateRange.SourceFormat = monthIsNamed ? DateFormat.Mmm : DateFormat.Mm;
                dateRange.Status = DateQualityStatus.OnlyMonthIsPresent;
                return dateRange;
            }

            dateRange.SourceFormat = DateFormat.UnableToParse;
            dateRange.Status = DateQualityStatus.NotValid;

            return dateRange;
        }

        private static DateRange ParseTwoComponents(string value, ref List<string> dateComponents)
        {
            var dateRange = new DateRange(value);
            var yearPos = ExtractNumberBetween(ref dateComponents, MinYear, MaxYear, out var year);
            var monthPos = ExtractMonth(ref dateComponents, out var month, out var monthIsNamed);

            if (monthPos >= 0)
            {
                if (yearPos >= 0)
                {
                    dateRange.DateFrom = new DateTime(year, month, MinDay);
                    dateRange.DateTo = new DateTime(year, month, MinDay).EndOfMonth();
                    if (monthIsNamed)
                    {
                        dateRange.SourceFormat = yearPos < monthPos ? DateFormat.Yyyy_mmm : DateFormat.Mmm_yyyy;
                    }
                    else
                    {
                        dateRange.SourceFormat = yearPos < monthPos ? DateFormat.Yyyy_mm : DateFormat.Mm_yyyy;
                    }

                    dateRange.Status = DateQualityStatus.OK;
                    return dateRange;
                }

                if (monthIsNamed)
                {
                    dateRange.SourceFormat = monthPos == 0 ? DateFormat.Mmm_dd : DateFormat.Dd_mmm;
                }
                else
                {
                    dateRange.SourceFormat = monthPos == 0 ? DateFormat.Mm_dd : DateFormat.Dd_mm;
                }

                dateRange.Status = DateQualityStatus.NotValid;
                return dateRange;
            }

            dateRange.SourceFormat = DateFormat.UnableToParse;
            dateRange.Status = DateQualityStatus.NotValid;

            return dateRange;
        }

        private static DateRange ParseThreeComponents(string value, ref List<string> dateComponents)
        {
            var dateRange = new DateRange(value);
            var yearPos = ExtractNumberBetween(ref dateComponents, MinYear, MaxYear, out var year);

            if (yearPos == -1)
            {
                dateRange.SourceFormat = DateFormat.UnableToParse;
                dateRange.Status = DateQualityStatus.ThreePartsWithoutYear;
                return dateRange;
            }

            if (yearPos == 1)
            {
                // Year is in the middle, we're not going to try this unless we get lot of demand from users.
                dateRange.SourceFormat = DateFormat.UnableToParseAsYearInMiddle;
                dateRange.Status = DateQualityStatus.YearInMiddle;
                return dateRange;
            }

            // If year is at the start, it'll offset where we look for day and month by 1.
            int yearOffset = yearPos == 0 ? 1 : 0;

            // Is either of the two positions definitely an English language month (May, August etc.)?
            int month, day, monthPos = -1, dayPos = -1;
            bool monthIsNamed;

            // Check if either of the other two positions can't be a month.
            if (IsMonthName(dateComponents[yearOffset + 1])
                || TextIsNumberBetween(dateComponents[yearOffset], MaxMonth + 1, MaxDay))
            {
                // Position 1 is too large to be a month, so month should be in position 2.
                if (ParseNumberBetween(dateComponents[yearOffset], MinDay, MaxDay, out day))
                {
                    dayPos = 1;
                }

                if (ParseMonth(dateComponents[yearOffset + 1], out month, out monthIsNamed))
                {
                    monthPos = 2;
                }
            }
            else
            {
                if (ParseMonth(dateComponents[yearOffset], out month, out monthIsNamed))
                {
                    monthPos = 1;
                }

                if (ParseNumberBetween(dateComponents[yearOffset + 1], MinDay, MaxDay, out day))
                {
                    dayPos = 2;
                }
            }

            if (dayPos >= 0 && monthPos >= 0)
            {
                dateRange.DateFrom = new DateTime(year, month, day);
                dateRange.DateTo = new DateTime(year, month, day).EndOfDay();
                if (yearPos == 0)
                {
                    if (day <= MaxMonth && !monthIsNamed)
                    {
                        dateRange.SourceFormat = DateFormat.UnsureEndingWithDateOrMonth;
                        dateRange.Status = DateQualityStatus.MonthIsAmbiguous;
                    }
                    else
                    {
                        dateRange.SourceFormat = FormatFromMonthPositionWithYearPrefix(monthPos, dayPos, monthIsNamed);
                        dateRange.Status = DateQualityStatus.OK;
                    }
                }
                else
                {
                    if (day <= MaxMonth && !monthIsNamed)
                    {
                        dateRange.SourceFormat = DateFormat.UnsureStartingWithDateOrMonth;
                        dateRange.Status = DateQualityStatus.MonthIsAmbiguous;
                    }
                    else
                    {
                        dateRange.SourceFormat = FormatFromMonthPositionWithYearSuffix(monthPos, dayPos, monthIsNamed);
                        dateRange.Status = DateQualityStatus.OK;
                    }
                }
            }
            else
            {
                dateRange.SourceFormat = DateFormat.UnableToParse;
                dateRange.Status = DateQualityStatus.NotValid;
            }

            return dateRange;
        }

        private static DateFormat FormatFromMonthPositionWithYearSuffix(int monthPos, int dayPos, bool monthIsNamed)
        {
            if (monthIsNamed)
            {
                return dayPos > monthPos ? DateFormat.Mmm_dd_yyyy : DateFormat.Dd_mmm_yyyy;
            }

            return dayPos > monthPos ? DateFormat.Mm_dd_yyyy : DateFormat.Dd_mm_yyyy;
        }

        private static DateFormat FormatFromMonthPositionWithYearPrefix(int monthPos, int dayPos, bool monthIsNamed)
        {
            if (monthIsNamed)
            {
                return dayPos > monthPos ? DateFormat.Yyyy_mmm_dd : DateFormat.Yyyy_dd_mmm;
            }

            return dayPos > monthPos ? DateFormat.Yyyy_mm_dd : DateFormat.Yyyy_dd_mm;
        }

        private static bool IsNumeric(string value)
        {
            if (value.All(char.IsNumber))
            {
                return true;
            }

            return false;
        }

        private static int MonthNumberFromName(string monthName)
        {
            if (monthName.Length >= MinMonthNameLength)
            {
                for (int m = 0; m < fullEnglishMonthNames.Count; m++)
                {
                    var fullMonthName = fullEnglishMonthNames[m];

                    if (fullMonthName.StartsWith(monthName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return m + 1;
                    }
                }
            }

            return -1;
        }

        private static bool ParseMonth(string dateComponent, out int month, out bool monthIsNamed)
        {
            month = MonthNumberFromName(dateComponent);
            monthIsNamed = month > 0;
            if (month < 0)
            {
                return ParseNumberBetween(dateComponent, MinMonth, MaxMonth, out month);
            }

            return true;
        }

        private static int ExtractMonth(ref List<string> dateComponents, out int month, out bool monthIsNamed)
        {
            var monthPos = ExtractMonthByName(ref dateComponents, out month);
            monthIsNamed = monthPos >= 0;

            if (monthPos < 0)
            {
                // Month is preferred to be in position 2 to handle most formats (d-m-yy, y-m-d, y-m, d-m), so check that first.
                if (dateComponents.Count >= 2 && ParseNumberBetween(dateComponents[1], MinMonth, MaxMonth, out month))
                {
                    monthPos = 2;
                }
                else
                {
                    monthPos = ExtractNumberBetween(ref dateComponents, MinMonth, MaxMonth, out month);
                }
            }

            return monthPos;
        }

        private static int ExtractMonthByName(ref List<string> dateComponents, out int month)
        {
            month = -1;

            for (int i = 0; i < dateComponents.Count; i++)
            {
                month = MonthNumberFromName(dateComponents[i]);
                if (month >= 1)
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool IsMonthName(string monthName)
        {
            return MonthNumberFromName(monthName) > 0;
        }

        private static int ExtractNumberBetween(ref List<string> dateComponents, int min, int max, out int value)
        {
            value = -1;

            for (int i = 0; i < dateComponents.Count; i++)
            {
                var dateComponent = dateComponents[i];
                if (ParseNumberBetween(dateComponent, min, max, out value))
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool ParseNumberBetween(string dateComponent, int min, int max, out int value)
        {
            value = -1;

            if (TextIsNumberBetween(dateComponent, min, max))
            {
                value = int.Parse(dateComponent);
                return true;
            }

            return false;
        }

        private static bool TextIsNumberBetween(string dateText, int minValue, int maxValue)
        {
            if (!IsNumeric(dateText))
            {
                return false;
            }

            var dateValue = int.Parse(dateText);
            return dateValue >= minValue && dateValue <= maxValue;
        }
    }
}
