// -------------------------------------------------------------------------------------------------
// <copyright file="LineOfBusinessApplicationInfo.cs" company="Osc@rNET SOFTware">
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
/// <summary>
/// Provides information about a specific line-of-business application.
/// </summary>
public record struct LineOfBusinessApplicationInfo()
{
    #region Properties

    /// <summary>
    /// Gets the culture name of the application.
    /// </summary>
    /// <value>
    /// The culture name of the application.
    /// </value>
    public string CultureName { get; internal set; } = LineOfBusinessApplicationDefaults.CultureName;

    /// <summary>
    /// Gets the runtime environment of the application.
    /// </summary>
    /// <value>
    /// The runtime environment of the application.
    /// </value>
    public EnvironmentType RuntimeEnvironment { get; internal set; } = EnvironmentType.Unknown;

    /// <summary>
    /// Gets the time zone identifier of the application.
    /// </summary>
    /// <value>
    /// The time zone identifier of the application.
    /// </value>
    public string TimeZoneId { get; internal set; } = LineOfBusinessApplicationDefaults.TimeZoneId;

    #endregion Properties
}