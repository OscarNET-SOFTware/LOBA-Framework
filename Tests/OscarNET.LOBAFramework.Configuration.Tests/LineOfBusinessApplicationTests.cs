// -------------------------------------------------------------------------------------------------
// <copyright file="LineOfBusinessApplicationTests.cs" company="Osc@rNET SOFTware">
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

using FluentAssertions;

using OscarNET.LOBAFramework.Testing;

using Xunit;

/// <summary>
/// Represents the unit tests of the <see cref="LineOfBusinessApplication" /> class.
/// </summary>
[Collection(nameof(LineOfBusinessApplicationCollectionContainer))]
public sealed class LineOfBusinessApplicationTests
{
    #region Methods

    /// <summary>
    /// Checks if an application can be configured with a specific culture.
    /// </summary>
    /// <param name="cultureName">The culture name.</param>
    [Theory]
    [InlineData("pt-PT")]
    [InlineData("en-GB")]
    [InlineData("en-US")]
    public void Application_can_be_configured_with_specific_culture(string cultureName) =>
        LineOfBusinessApplicationBuilder.Initialize().WithCulture(cultureName).Build().Culture.Name.Should().Be(cultureName);

    /// <summary>
    /// Checks if an application can be configured with a specific time zone.
    /// </summary>
    /// <param name="timeZoneId">The time zone identifier.</param>
    [TheoryIgnoreOnNonWindowsOS]
    [InlineData("GMT Standard Time")]
    [InlineData("Central Europe Standard Time")]
    [InlineData("Central Standard Time")]
    public void Application_can_be_configured_with_specific_time_zone(string timeZoneId) =>
        LineOfBusinessApplicationBuilder.Initialize().WithTimeZone(timeZoneId).Build().TimeZone.Id.Should().Be(timeZoneId);

    /// <summary>
    /// Checks if an application can be configured with a specific time zone, on non-Windows OS.
    /// </summary>
    /// <param name="timeZoneId">The time zone identifier.</param>
    [TheoryIgnoreOnWindowsOS]
    [InlineData("America/New_York")]
    [InlineData("Europe/Paris")]
    [InlineData("Pacific/Honolulu")]
    public void Application_can_be_configured_with_specific_time_zone_non_Windows_OS(string timeZoneId) =>
        LineOfBusinessApplicationBuilder.Initialize().WithTimeZone(timeZoneId).Build().TimeZone.Id.Should().Be(timeZoneId);

    /// <summary>
    /// Checks if an application has its unique process identifier.
    /// </summary>
    [Fact]
    public void Application_has_its_PID() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().PID.Should().Be(Environment.ProcessId);

    /// <summary>
    /// Checks if an application has its start timestamp.
    /// </summary>
    [Fact]
    public void Application_has_its_start_timestamp()
    {
        TimeZoneInfo spanishTimeZone = TimeZoneInfo.FindSystemTimeZoneById(LineOfBusinessApplicationDefaults.TimeZoneId);
        DateTime spanishLocalTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, spanishTimeZone);

        LineOfBusinessApplicationBuilder.Initialize().Build()
            .StartTimestamp.Should().BeCloseTo(spanishLocalTime, TimeSpan.FromSeconds(3));
    }

    /// <summary>
    /// Check if an application knows the folder containing its executable.
    /// </summary>
    [Fact]
    public void Application_knows_folder_containing_its_executable() => string.Equals(
        LineOfBusinessApplicationBuilder.Initialize().Build().Folder,
        Path.GetDirectoryName(Environment.ProcessPath),
        StringComparison.OrdinalIgnoreCase)
        .Should().BeTrue();

    /// <summary>
    /// Check if an application knows the LOBA-Framework version.
    /// </summary>
    [Fact]
    public void Application_knows_framework_version() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().FrameworkVersion.Should().BeEquivalentTo(
            LineOfBusinessApplicationBuilder.InternalExecutingAssemblyFileVersionDiscover());

    /// <summary>
    /// Checks if an application knows the IP address of the local computer on which it's running.
    /// </summary>
    [Fact]
    public void Application_knows_ip_address_of_local_computer_on_which_it_is_running() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().ComputerIPAddress.Should().Be(LineOfBusinessApplicationBuilder.InternalHostIPAddressDiscover());

    /// <summary>
    /// Check if an application knows its version.
    /// </summary>
    [Fact]
    public void Application_knows_its_version() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().Version.Should().BeEquivalentTo(
            LineOfBusinessApplicationBuilder.InternalEntryAssemblyFileVersionDiscover());

    /// <summary>
    /// Checks if an application knows the name of the local computer on which it's running.
    /// </summary>
    [Fact]
    public void Application_knows_name_of_local_computer_on_which_it_is_running() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().ComputerName.Should().Be(Environment.MachineName);

    /// <summary>
    /// Checks if an application provides the local time using the configured time zone.
    /// </summary>
    /// <param name="timeZoneId">The time zone identifier.</param>
    [TheoryIgnoreOnNonWindowsOS]
    [InlineData("GMT Standard Time")]
    [InlineData("Central Europe Standard Time")]
    [InlineData("Central Standard Time")]
    public void Application_provides_local_time_using_configured_time_zone(string timeZoneId)
    {
        TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        DateTime expectedLocalTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, destinationTimeZone);

        LineOfBusinessApplicationBuilder.Initialize().WithTimeZone(timeZoneId).Build()
            .LocalTime.Should().BeCloseTo(expectedLocalTime, TimeSpan.FromSeconds(3));
    }

    /// <summary>
    /// Checks if an initialized application has the expected runtime environment, the one indicated through the command line arguments.
    /// </summary>
    /// <param name="argumentValue">The argument value.</param>
    [Theory]
    [InlineData("TestingViaCmdArgs-CaseA")]
    [InlineData("TestingViaCmdArgs-CaseB")]
    public void Initialized_application_has_expected_runtime_environment_via_command_line_arguments(string argumentValue) =>
        LineOfBusinessApplicationBuilder.Initialize(new[] { "--environment", argumentValue }).RuntimeEnvironment.Name.Should().Be(argumentValue);

    /// <summary>
    /// Checks if an initialized application has the expected runtime environment, the one indicated through the environment variable.
    /// </summary>
    /// <param name="environmentVariableName">The environment variable name.</param>
    /// <param name="environmentVariableValue">The environment variable value.</param>
    [Theory]
    [InlineData("ASPNETCORE_ENVIRONMENT", "TestingViaEnvironmentVariable-ASPNETCORE")]
    [InlineData("DOTNET_ENVIRONMENT", "TestingViaEnvironmentVariable-DOTNET")]
    public void Initialized_application_has_expected_runtime_environment_via_environment_variable(string environmentVariableName, string environmentVariableValue)
    {
        string? environmentVariableCurrentValue = Environment.GetEnvironmentVariable(environmentVariableName);
        Environment.SetEnvironmentVariable(environmentVariableName, environmentVariableValue);

        LineOfBusinessApplicationBuilder.Initialize().RuntimeEnvironment.Name.Should().Be(environmentVariableValue);

        Environment.SetEnvironmentVariable(environmentVariableName, environmentVariableCurrentValue);
    }

    /// <summary>
    /// Checks if an unconfigured application has the Spanish culture by default.
    /// </summary>
    [Fact]
    public void Unconfigured_application_has_Spanish_culture_by_default() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().Culture.Name.Should().Be(LineOfBusinessApplicationDefaults.CultureName);

    /// <summary>
    /// Checks if an unconfigured application has the Spanish time zone by default (Romance Standard Time UTC+01:00).
    /// </summary>
    [FactIgnoreOnNonWindowsOS]
    public void Unconfigured_application_has_Spanish_time_zone_by_default() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().TimeZone.Id.Should().Be(LineOfBusinessApplicationDefaults.TimeZoneIdForWindowsOS);

    /// <summary>
    /// Checks if an unconfigured application has the Spanish time zone by default (Europe/Madrid UTC+01:00), on non-Windows OS.
    /// </summary>
    [FactIgnoreOnWindowsOS]
    public void Unconfigured_application_has_Spanish_time_zone_by_default_on_non_Windows_OS() =>
        LineOfBusinessApplicationBuilder.Initialize().Build().TimeZone.Id.Should().Be(LineOfBusinessApplicationDefaults.TimeZoneIdForNonWindowsOS);

    /// <summary>
    /// Checks if an unconfigured application provides the Spanish local time by default.
    /// </summary>
    [Fact]
    public void Unconfigured_application_provides_Spanish_local_time_by_default()
    {
        TimeZoneInfo spanishTimeZone = TimeZoneInfo.FindSystemTimeZoneById(LineOfBusinessApplicationDefaults.TimeZoneId);
        DateTime spanishLocalTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, spanishTimeZone);

        LineOfBusinessApplicationBuilder.Initialize().Build()
            .LocalTime.Should().BeCloseTo(spanishLocalTime, TimeSpan.FromSeconds(3));
    }

    #endregion Methods
}