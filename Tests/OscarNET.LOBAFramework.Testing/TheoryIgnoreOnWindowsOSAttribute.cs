﻿// -------------------------------------------------------------------------------------------------
// <copyright file="TheoryIgnoreOnWindowsOSAttribute.cs" company="Osc@rNET SOFTware">
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

namespace OscarNET.LOBAFramework.Testing;

using System.Runtime.InteropServices;

using Xunit;

/// <summary>
/// Attribute that is applied to a marked method as a data theory to indicate that it is a fact that should be run multiple times by the test runner, only on non-Windows OS.
/// </summary>
public sealed class TheoryIgnoreOnWindowsOSAttribute : TheoryAttribute
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TheoryIgnoreOnWindowsOSAttribute" /> class.
    /// </summary>
    public TheoryIgnoreOnWindowsOSAttribute()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) is true)
        {
            Skip = "Ignored on Windows OS";
        }
    }

    #endregion Constructors
}