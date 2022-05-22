// -------------------------------------------------------------------------------------------------
// <copyright file="LineOfBusinessApplication.cs" company="Osc@rNET SOFTware">
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

using System.Diagnostics;
using System.Globalization;
using System.Net;

/// <summary>
/// Represents a line-of-business application.
/// </summary>
public sealed class LineOfBusinessApplication
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LineOfBusinessApplication" /> class.
    /// </summary>
    /// <param name="appInfo">The application information.</param>
    internal LineOfBusinessApplication(LineOfBusinessApplicationInfo appInfo)
    {
        TimeZoneInfo timeZoneInfo = LineOfBusinessApplicationBuilder.InternalTimeZoneBuilder(appInfo.TimeZoneId);

        ComputerIPAddress = LineOfBusinessApplicationBuilder.InternalHostIPAddressDiscover();
        ComputerName = Environment.MachineName;
        Culture = LineOfBusinessApplicationBuilder.InternalCultureBuilder(appInfo.CultureName);
        Folder = Path.TrimEndingDirectorySeparator(AppDomain.CurrentDomain.BaseDirectory);
        FrameworkVersion = LineOfBusinessApplicationBuilder.InternalExecutingAssemblyFileVersionDiscover();
        PID = Environment.ProcessId;
        StartTimestamp = DateTime.Now.ToLocalTime(timeZoneInfo);
        TimeZone = timeZoneInfo;
        Version = LineOfBusinessApplicationBuilder.InternalEntryAssemblyFileVersionDiscover();
    }

    #endregion Constructors

    #region Properties

    /// <summary>
    /// Gets the IP address of the local computer on which the application is running.
    /// </summary>
    /// <value>
    /// The IP address of the local computer on which the application is running.
    /// </value>
    /// <remarks>
    /// </remarks>
    public IPAddress ComputerIPAddress { get; }

    /// <summary>
    /// Gets the NetBIOS name of the local computer on which the application is running.
    /// </summary>
    /// <value>
    /// The NetBIOS name of the local computer on which the application is running.
    /// </value>
    public string ComputerName { get; }

    /// <summary>
    /// Gets the culture of the application.
    /// </summary>
    /// <value>
    /// The culture of the application.
    /// </value>
    public CultureInfo Culture { get; }

    /// <summary>
    /// Gets the full path of the folder that contains the application executable.
    /// </summary>
    /// <value>
    /// The full path of the folder that contains the application executable.
    /// </value>
    public string Folder { get; }

    /// <summary>
    /// Gets the version of the LOBA-Framework.
    /// </summary>
    /// <value>
    /// The version of the LOBA-Framework.
    /// </value>
    public FileVersionInfo FrameworkVersion { get; }

    /// <summary>
    /// Gets the current date and time using the application's local time zone.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> that is set to the current date and time, expressed as the local time.
    /// </value>
    public DateTime LocalTime => DateTime.Now.ToLocalTime(TimeZone);

    /// <summary>
    /// Gets the unique identifier of the application process.
    /// </summary>
    /// <value>
    /// The unique identifier of the application process.
    /// </value>
    public int PID { get; }

    /// <summary>
    /// Gets the start timestamp of the application, that is, when it was built (using the application's local time zone).
    /// </summary>
    /// <value>
    /// A <see cref="DateTime" /> that is set to the start date and time, expressed as the local time.
    /// </value>
    public DateTime StartTimestamp { get; }

    /// <summary>
    /// Gets the time zone of the application.
    /// </summary>
    /// <value>
    /// The time zone of the application.
    /// </value>
    public TimeZoneInfo TimeZone { get; }

    /// <summary>
    /// Gets the version of the application.
    /// </summary>
    /// <value>
    /// The version of the application.
    /// </value>
    public FileVersionInfo Version { get; }

    #endregion Properties
}