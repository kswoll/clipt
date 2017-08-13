﻿using Clipt.Inputs;

namespace Clipt.Utils
{
    public class MouseUtils
    {
        public void AddHotMouse(HotMouse hotMouse, HotMouseHandler handler) => HotMouseProcessor.Instance.Register(hotMouse, handler);
    }
}