// -------------------------------------------------------------------------------------------------
// <copyright file="EnvironmentType.cs" company="Osc@rNET SOFTware">
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
/// Represents an environment type.
/// </summary>
public readonly record struct EnvironmentType
{
    #region Fields

    /// <summary>
    /// Development: The software is programmed based on the defined requirements.
    /// </summary>
    public static readonly EnvironmentType Development = new() { Name = "Development" };

    /// <summary>
    /// Proof of Concept: The technical feasibility of an idea is checked, through evidence of its functionality and potential.
    /// </summary>
    public static readonly EnvironmentType PoC = new() { Name = "PoC" };

    /// <summary>
    /// Production: Real exploitation environment accessible to all users, where the virtues and defects of our work will be seen.
    /// </summary>
    public static readonly EnvironmentType Production = new() { Name = "Production" };

    /// <summary>
    /// Quality Assurance: It's guaranteed that the software meets quality requirements (security, performance and availability, etc).
    /// </summary>
    public static readonly EnvironmentType QA = new() { Name = "QA" };

    /// <summary>
    /// Staging: Validation tests are carried out on the software as a whole, with the aim of locating any errors before reaching the production environment and thus avoiding the problems derived from them.
    /// </summary>
    public static readonly EnvironmentType Staging = new() { Name = "Staging" };

    /// <summary>
    /// Testing: The tests of a certain functionality developed are carried out.
    /// </summary>
    public static readonly EnvironmentType Testing = new() { Name = "Testing" };

    /// <summary>
    /// Unknown.
    /// </summary>
    public static readonly EnvironmentType Unknown = new() { Name = "Unknown" };

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; init; }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Performs an implicit conversion from <see cref="string" /> to <see cref="EnvironmentType" />.
    /// </summary>
    /// <param name="environmentName">The environment name to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator EnvironmentType(string environmentName) => environmentName switch
    {
        nameof(Development) => Development,
        nameof(PoC) => PoC,
        nameof(Production) => Production,
        nameof(QA) => QA,
        nameof(Staging) => Staging,
        nameof(Testing) => Testing,
        nameof(Unknown) => Unknown,
        _ => new() { Name = environmentName }
    };

    /// <summary>
    /// Performs an implicit conversion from <see cref="EnvironmentType" /> to <see cref="string" />.
    /// </summary>
    /// <param name="environmentType">The environment type to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator string(EnvironmentType environmentType) => environmentType.Name;

    #endregion Methods
}