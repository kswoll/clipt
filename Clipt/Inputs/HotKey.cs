// <copyright file="KeyboardHook.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2017 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.Immutable;

namespace Clipt.Inputs
{
    public class HotKey
    {
        public ImmutableList<KeyCode> Modifiers { get; }
        public KeyCode Activator { get; }

        public HotKey(KeyCode activator, params KeyCode[] modifiers)
        {
            Activator = activator;
            Modifiers = modifiers.ToImmutableList();
        }

        public bool Process(KeyCode key)
        {
            foreach (var modifier in Modifiers)
            {
                if (!InputHook.IsKeyPressed(modifier))
                    return false;
            }
            return true;
        }
    }
}