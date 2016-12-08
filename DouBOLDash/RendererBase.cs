/*
    Copyright 2016-2017 shibboleet, StapleButter
    This file is part of DouBOL Dash.
    DouBOL Dash is free software: you can redistribute it and/or modify it under
    the terms of the GNU General Public License as published by the Free
    Software Foundation, either version 3 of the License, or (at your option)
    any later version.
    DouBOL Dash is distributed in the hope that it will be useful, but WITHOUT ANY
    WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
    FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
    You should have received a copy of the GNU General Public License along
    with DouBOL Dash. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouBOLDash
{
    public enum RenderMode
    {
        Picking = 0,
        Opaque,
        Translucent
    }

    public class RenderInfo
    {
        // rendering mode -- those that saw SM64DSe's renderer will find out
        // that Whitehole's renderer works the same way
        public RenderMode Mode;

        public static RenderMode[] Modes = { RenderMode.Picking, RenderMode.Opaque, RenderMode.Translucent };
    }

    public class RendererBase
    {
        public virtual void Close()
        {
        }

        public virtual bool GottaRender(RenderInfo info)
        {
            return false;
        }

        public virtual void Render(RenderInfo info)
        {
        }
    }
}