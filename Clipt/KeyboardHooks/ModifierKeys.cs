// <copyright file="ModifierKeys.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2017 PlanGrid, Inc. All rights reserved.
// </copyright>

using System;

namespace Clipt.KeyboardHooks
{
    [Flags]
    public enum ModifierKeys : uint
    {
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Windows = 0x0008
    }
}