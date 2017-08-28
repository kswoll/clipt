// <copyright file="KeyboardHook.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2017 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.Immutable;

namespace Wintomaton.Inputs
{
    public class Shortcut
    {
        public ImmutableList<KeyCode> Modifiers { get; }
        public KeyCode Activator { get; }

        public Shortcut(KeyCode activator, params KeyCode[] modifiers)
        {
            Activator = activator;
            Modifiers = modifiers.ToImmutableList();
        }

        public bool Process()
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