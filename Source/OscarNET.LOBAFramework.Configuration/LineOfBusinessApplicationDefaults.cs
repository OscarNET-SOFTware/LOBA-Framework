// -------------------------------------------------------------------------------------------------
// <copyright file="LineOfBusinessApplicationDefaults.cs" company="Osc@rNET SOFTware">
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
/// Represents the default values for a line-of-business application.
/// </summary>
public static class LineOfBusinessApplicationDefaults
{
    #region Fields

    /// <summary>
    /// Defines the default culture name.
    /// </summary>
    public const string CultureName = "es-ES";

    /// <summary>
    /// Defines the default time zone identifier, for non-Windows Operating System.
    /// </summary>
    public const string TimeZoneIdForNonWindowsOS = "Europe/Madrid";

    /// <summary>
    /// Defines the default time zone identifier, for Windows Operating System.
    /// </summary>
    public const string TimeZoneIdForWindowsOS = "Romance Standard Time";

    /// <summary>
    /// Defines the default time zone identifier.
    /// </summary>
    public static readonly string TimeZoneId = OperatingSystem.IsWindows() is not true
        ? TimeZoneIdForNonWindowsOS
        : TimeZoneIdForWindowsOS;

    #endregion Fields
}