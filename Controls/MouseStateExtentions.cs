using SharpDX;
using SharpDX.Toolkit.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomShane.Neoforce.Controls
{
    public static class MouseStateExtentions
    {
        public static Point Absolute(this MouseState state)
        {
            return new Point((int)state.X, (int)state.Y);
        }
    }
}
