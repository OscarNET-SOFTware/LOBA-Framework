// -------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="Osc@rNET SOFTware">
//
//          Copyright (c) Óscar Fernández González a.k.a. Osc@rNET and Contributors.
//
//          This open source software is licensed under the MIT License.
//          Please review the LICENSE file for more information.
//
//          All the relevant licenses for third party software and tools
//          that have been made possible this open source software are
//          mentioned in the 3DPARTYLICENSES file.
//
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace OscarNET.LOBAFramework.Configuration;

using System;

/// <summary>
/// Provides the extensions for working with dates and times.
/// </summary>
public static class DateTimeExtensions
{
    #region Methods

    /// <summary>
    /// Converts the given date and time to a date only.
    /// </summary>
    /// <param name="dateTime">The date time to convert.</param>
    /// <returns>
    /// A <see cref="DateOnly" /> that is set to the date part of the <paramref name="dateTime" />.
    /// </returns>
    public static DateOnly ToDate(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

    /// <summary>
    /// Converts the given date and time to a specified local time zone.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
    /// <returns>
    /// A <see cref="DateTime" /> that is set to the <paramref name="dateTime" />, expressed as the local time (using <paramref name="destinationTimeZone" />).
    /// </returns>
    public static DateTime ToLocalTime(this DateTime dateTime, TimeZoneInfo destinationTimeZone) =>
        TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, destinationTimeZone);

    /// <summary>
    /// Converts the given date and time to a time only.
    /// </summary>
    /// <param name="dateTime">The date time to convert.</param>
    /// <returns>
    /// A <see cref="TimeOnly" /> that is set to the time part of the <paramref name="dateTime" />.
    /// </returns>
    public static TimeOnly ToTime(this DateTime dateTime) => TimeOnly.FromDateTime(dateTime);

    #endregion Methods
}