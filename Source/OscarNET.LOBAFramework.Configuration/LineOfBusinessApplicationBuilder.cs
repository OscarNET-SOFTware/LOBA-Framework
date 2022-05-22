// -------------------------------------------------------------------------------------------------
// <copyright file="LineOfBusinessApplicationBuilder.cs" company="Osc@rNET SOFTware">
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
using System.Net.Sockets;
using System.Reflection;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Provides the mechanisms to create and configure a line-of-business application.
/// </summary>
public static class LineOfBusinessApplicationBuilder
{
    #region Methods

    /// <summary>
    /// Builds an application.
    /// </summary>
    /// <param name="appInfo">The application to build.</param>
    /// <returns>
    /// A new <see cref="LineOfBusinessApplication" /> instance.
    /// </returns>
    public static LineOfBusinessApplication Build(this LineOfBusinessApplicationInfo appInfo) => new(appInfo);

    /// <summary>
    /// Initializes a new application with default values and configures its runtime environment.
    /// </summary>
    /// <param name="args">The optional command line arguments.</param>
    /// <returns>
    /// A new <see cref="LineOfBusinessApplicationInfo" /> instance.
    /// </returns>
    /// <remarks>
    /// To determine the runtime environment, the following actions are performed in order:
    /// 1. The <![CDATA['--environment']]> command line argument value is read.
    /// 2. If it's missing then the <![CDATA['ASPNETCORE_ENVIRONMENT']]> environment variable value is read.
    /// 3. If it's missing then the <![CDATA['DOTNET_ENVIRONMENT']]> environment variable value is read.
    /// 4. If it's missing then the default value is <see cref="EnvironmentType.Production" />.
    /// </remarks>
    public static LineOfBusinessApplicationInfo Initialize(string[]? args = null)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args ?? Array.Empty<string>())
            .Build();

        LineOfBusinessApplicationInfo appInfo = new()
        {
            RuntimeEnvironment = configuration[nameof(Environment)]
            ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? EnvironmentType.Production
        };

        return appInfo;
    }

    /// <summary>
    /// Configures the culture for the given application.
    /// </summary>
    /// <param name="appInfo">The application to set the culture to.</param>
    /// <param name="cultureName">Name of the culture.</param>
    /// <returns>
    /// The <paramref name="appInfo" /> containing the specified <paramref name="cultureName" />.
    /// </returns>
    public static LineOfBusinessApplicationInfo WithCulture(this LineOfBusinessApplicationInfo appInfo, string cultureName)
    {
        appInfo.CultureName = cultureName;
        return appInfo;
    }

    /// <summary>
    /// Configures the time zone for the given application.
    /// </summary>
    /// <param name="appInfo">The application to set the time zone to.</param>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns>
    /// The <paramref name="appInfo" /> containing the specified <paramref name="timeZoneId" />.
    /// </returns>
    public static LineOfBusinessApplicationInfo WithTimeZone(this LineOfBusinessApplicationInfo appInfo, string timeZoneId)
    {
        appInfo.TimeZoneId = timeZoneId;
        return appInfo;
    }

    /// <summary>
    /// Builds a culture.
    /// </summary>
    /// <param name="cultureName">The culture name.</param>
    /// <returns>
    /// The built culture.
    /// </returns>
    internal static CultureInfo InternalCultureBuilder(string cultureName)
    {
        CultureInfo cultureInfo = new(cultureName, false);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        return cultureInfo;
    }

    /// <summary>
    /// Discovers the file version of the entry assembly.
    /// </summary>
    /// <returns>
    /// The discovered file version of the entry assembly.
    /// </returns>
    internal static FileVersionInfo InternalEntryAssemblyFileVersionDiscover() =>
        FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location);

    /// <summary>
    /// Discovers the file version of the executing assembly.
    /// </summary>
    /// <returns>
    /// The discovered file version of the executing assembly.
    /// </returns>
    internal static FileVersionInfo InternalExecutingAssemblyFileVersionDiscover() =>
        FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

    /// <summary>
    /// Discovers the IP address of the local computer.
    /// </summary>
    /// <returns>
    /// The discovered IP address of the local computer.
    /// </returns>
    /// <remarks>
    /// This makes a connection-less UDP connection to a dummy host
    /// which will contain the local computer outbound IP address.
    /// </remarks>
    internal static IPAddress InternalHostIPAddressDiscover()
    {
        using Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
        socket.Connect(new IPAddress(new byte[] { 1, 2, 3, 4 }), ushort.MaxValue);

        return ((IPEndPoint)socket.LocalEndPoint!).Address;
    }

    /// <summary>
    /// Builds a time zone.
    /// </summary>
    /// <param name="timeZoneId">The time zone identifier.</param>
    /// <returns>
    /// The built time zone.
    /// </returns>
    internal static TimeZoneInfo InternalTimeZoneBuilder(string timeZoneId) => TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

    #endregion Methods
}