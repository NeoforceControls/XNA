////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Renderer.cs                                  //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls
{
    public enum BlendingMode
    {
        Default,
        None,
    }

    #region //// Classes ///////////

    ////////////////////////////////////////////////////////////////////////////
    public class DeviceStates
    {
        public readonly BlendState BlendState;
        public readonly RasterizerState RasterizerState;
        public readonly DepthStencilState DepthStencilState;
        public readonly SamplerState SamplerState;

        public DeviceStates(GraphicsDevice GraphicsDevice)
        {

            BlendState = BlendState.New(GraphicsDevice, GraphicsDevice.BlendStates.AlphaBlend.Description);

            RasterizerState = RasterizerState.New(GraphicsDevice, GraphicsDevice.RasterizerStates.CullNone.Description);

            SamplerState = SamplerState.New(GraphicsDevice, GraphicsDevice.SamplerStates.AnisotropicClamp.Description);

            DepthStencilState = GraphicsDevice.DepthStencilStates.None;
        }
    }
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    public class Renderer : Component
    {

        #region //// Fields ////////////

        ////////////////////////////////////////////////////////////////////////////
        private SpriteBatch sb = null;
        private DeviceStates states = null;
        private BlendingMode bmode = BlendingMode.Default;
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Properties ////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual SpriteBatch SpriteBatch
        {
            get
            {
                return sb;
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Constructors //////

        ////////////////////////////////////////////////////////////////////////////
        public Renderer(Manager manager)
            : base(manager)
        {
            sb = new SpriteBatch(Manager.GraphicsDevice);
            states = new DeviceStates(Manager.GraphicsDevice);
        }
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Destructors ///////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sb != null)
                {
                    sb.Dispose();
                    sb = null;
                }
            }
            base.Dispose(disposing);
        }
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        public override void Init()
        {
            base.Init();
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void Begin(BlendingMode mode)
        {
            bmode = mode;
            if (mode != BlendingMode.None)
            {
                sb.Begin(SpriteSortMode.Immediate, states.BlendState, states.SamplerState, states.DepthStencilState, states.RasterizerState);
            }
            else
            {
                //BlendState.Opaque
                sb.Begin(SpriteSortMode.Immediate, states.BlendState, states.SamplerState, states.DepthStencilState, states.RasterizerState);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void End()
        {
            sb.End();
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void Draw(Texture2DBase texture, Rectangle destination, Color color)
        {
            if (destination.Width > 0 && destination.Height > 0)
            {
                sb.Draw(texture, destination, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, Manager.GlobalDepth);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        public virtual void DrawTileTexture(Texture2DBase texture, Rectangle destination, Color color)
        {
            if (destination.Width > 0 && destination.Height > 0)
            {
                End();

                //sb.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
                sb.Begin();

                sb.Draw(texture, new Vector2(destination.X,destination.Y), destination, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

                End();
                Begin(bmode);
            }
        }

        ////////////////////////////////////////////////////////////////////////////
        public virtual void Draw(Texture2DBase texture, Rectangle destination, Rectangle source, Color color)
        {
            if (source.Width > 0 && source.Height > 0 && destination.Width > 0 && destination.Height > 0)
            {
                sb.Draw(texture, destination, source, color, 0.0f, Vector2.Zero, SpriteEffects.None, Manager.GlobalDepth);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void Draw(Texture2DBase texture, int left, int top, Color color)
        {
            sb.Draw(texture, new Vector2(left, top), null, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Manager.GlobalDepth);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void Draw(Texture2DBase texture, int left, int top, Rectangle source, Color color)
        {
            if (source.Width > 0 && source.Height > 0)
            {
                sb.Draw(texture, new Vector2(left, top), source, color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Manager.GlobalDepth);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(SpriteFont font, string text, int left, int top, Color color)
        {
            sb.DrawString(font, text, new Vector2(left, top), color, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, Manager.GlobalDepth);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(SpriteFont font, string text, Rectangle rect, Color color, Alignment alignment)
        {
            DrawString(font, text, rect, color, alignment, 0, 0, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(SpriteFont font, string text, Rectangle rect, Color color, Alignment alignment, bool ellipsis)
        {
            DrawString(font, text, rect, color, alignment, 0, 0, ellipsis);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect)
        {
            DrawString(control, layer, text, rect, true, 0, 0, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state)
        {
            DrawString(control, layer, text, rect, state, true, 0, 0, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, bool margins)
        {
            DrawString(control, layer, text, rect, margins, 0, 0, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state, bool margins)
        {
            DrawString(control, layer, text, rect, state, margins, 0, 0, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, bool margins, int ox, int oy)
        {
            DrawString(control, layer, text, rect, margins, ox, oy, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state, bool margins, int ox, int oy)
        {
            DrawString(control, layer, text, rect, state, margins, ox, oy, true);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, bool margins, int ox, int oy, bool ellipsis)
        {
            DrawString(control, layer, text, rect, control.ControlState, margins, ox, oy, ellipsis);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(Control control, SkinLayer layer, string text, Rectangle rect, ControlState state, bool margins, int ox, int oy, bool ellipsis)
        {
            Color col = Color.White;

            if (layer.Text != null)
            {
                if (margins)
                {
                    Margins m = layer.ContentMargins;
                    rect = new Rectangle(rect.Left + m.Left, rect.Top + m.Top, rect.Width - m.Horizontal, rect.Height - m.Vertical);
                }

                if (state == ControlState.Hovered && (layer.States.Hovered.Index != -1))
                {
                    col = layer.Text.Colors.Hovered;
                }
                else if (state == ControlState.Pressed)
                {
                    col = layer.Text.Colors.Pressed;
                }
                else if (state == ControlState.Focused || (control.Focused && state == ControlState.Hovered && layer.States.Hovered.Index == -1))
                {
                    col = layer.Text.Colors.Focused;
                }
                else if (state == ControlState.Disabled)
                {
                    col = layer.Text.Colors.Disabled;
                }
                else
                {
                    col = layer.Text.Colors.Enabled;
                }

                if (text != null && text != "")
                {
                    SkinText font = layer.Text;
                    if (control.TextColor != Control.UndefinedColor && control.ControlState != ControlState.Disabled) col = control.TextColor;
                    DrawString(font.Font.Resource, text, rect, col, font.Alignment, font.OffsetX + ox, font.OffsetY + oy, ellipsis);
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawString(SpriteFont font, string text, Rectangle rect, Color color, Alignment alignment, int offsetX, int offsetY, bool ellipsis)
        {

            if (ellipsis)
            {
                const string elli = "...";
                int size = (int)Math.Ceiling(font.MeasureString(text).X);
                if (size > rect.Width)
                {
                    int es = (int)Math.Ceiling(font.MeasureString(elli).X);
                    for (int i = text.Length - 1; i > 0; i--)
                    {
                        int c = 1;
                        if (char.IsWhiteSpace(text[i - 1]))
                        {
                            c = 2;
                            i--;
                        }
                        text = text.Remove(i, c);
                        size = (int)Math.Ceiling(font.MeasureString(text).X);
                        if (size + es <= rect.Width)
                        {
                            break;
                        }
                    }
                    text += elli;
                }
            }

            if (rect.Width > 0 && rect.Height > 0)
            {
                Vector2 pos = new Vector2(rect.Left, rect.Top);
                Vector2 size = font.MeasureString(text);

                int x = 0; int y = 0;

                switch (alignment)
                {
                    case Alignment.TopLeft:
                        break;
                    case Alignment.TopCenter:
                        x = GetTextCenter(rect.Width, size.X);
                        break;
                    case Alignment.TopRight:
                        x = rect.Width - (int)size.X;
                        break;
                    case Alignment.MiddleLeft:
                        y = GetTextCenter(rect.Height, size.Y);
                        break;
                    case Alignment.MiddleRight:
                        x = rect.Width - (int)size.X;
                        y = GetTextCenter(rect.Height, size.Y);
                        break;
                    case Alignment.BottomLeft:
                        y = rect.Height - (int)size.Y;
                        break;
                    case Alignment.BottomCenter:
                        x = GetTextCenter(rect.Width, size.X);
                        y = rect.Height - (int)size.Y;
                        break;
                    case Alignment.BottomRight:
                        x = rect.Width - (int)size.X;
                        y = rect.Height - (int)size.Y;
                        break;

                    default:
                        x = GetTextCenter(rect.Width, size.X);
                        y = GetTextCenter(rect.Height, size.Y);
                        break;
                }

                pos.X = (int)(pos.X + x);
                pos.Y = (int)(pos.Y + y);

                DrawString(font, text, (int)pos.X + offsetX, (int)pos.Y + offsetY, color);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        private static int GetTextCenter(float size1, float size2)
        {
            return (int)Math.Ceiling((size1 / 2) - (size2 / 2));
        }
        ////////////////////////////////////////////////////////////////////////////              

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawLayer(SkinLayer layer, Rectangle rect, Color color, int index)
        {
            Size imageSize = new Size(layer.Image.Resource.Width, layer.Image.Resource.Height);
            Size partSize = new Size(layer.Width, layer.Height);

            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.TopLeft), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.TopLeft, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.TopCenter), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.TopCenter, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.TopRight), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.TopRight, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.MiddleLeft), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.MiddleLeft, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.MiddleCenter), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.MiddleCenter, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.MiddleRight), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.MiddleRight, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.BottomLeft), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.BottomLeft, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.BottomCenter), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.BottomCenter, index), color);
            Draw(layer.Image.Resource, GetDestinationArea(rect, layer.SizingMargins, Alignment.BottomRight), GetSourceArea(imageSize, partSize, layer.SizingMargins, Alignment.BottomRight, index), color);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        private static Rectangle GetSourceArea(Size imageSize, Size partSize, Margins margins, Alignment alignment, int index)
        {
            Rectangle rect = new Rectangle();
            int xc = (int)((float)imageSize.Width / partSize.Width);
            int yc = (int)((float)imageSize.Height / partSize.Height);

            int xm = (index) % xc;
            int ym = (index) / xc;

            int adj = 1;
            margins.Left += margins.Left > 0 ? adj : 0;
            margins.Top += margins.Top > 0 ? adj : 0;
            margins.Right += margins.Right > 0 ? adj : 0;
            margins.Bottom += margins.Bottom > 0 ? adj : 0;

            margins = new Margins(margins.Left, margins.Top, margins.Right, margins.Bottom);
            switch (alignment)
            {
                case Alignment.TopLeft:
                    {
                        rect = new Rectangle((0 + (xm * partSize.Width)),
                                             (0 + (ym * partSize.Height)),
                                             margins.Left,
                                             margins.Top);
                        break;
                    }
                case Alignment.TopCenter:
                    {
                        rect = new Rectangle((0 + (xm * partSize.Width)) + margins.Left,
                                             (0 + (ym * partSize.Height)),
                                             partSize.Width - margins.Left - margins.Right,
                                             margins.Top);
                        break;
                    }
                case Alignment.TopRight:
                    {
                        rect = new Rectangle((partSize.Width + (xm * partSize.Width)) - margins.Right,
                                             (0 + (ym * partSize.Height)),
                                             margins.Right,
                                             margins.Top);
                        break;
                    }
                case Alignment.MiddleLeft:
                    {
                        rect = new Rectangle((0 + (xm * partSize.Width)),
                                             (0 + (ym * partSize.Height)) + margins.Top,
                                             margins.Left,
                                             partSize.Height - margins.Top - margins.Bottom);
                        break;
                    }
                case Alignment.MiddleCenter:
                    {
                        rect = new Rectangle((0 + (xm * partSize.Width)) + margins.Left,
                                             (0 + (ym * partSize.Height)) + margins.Top,
                                             partSize.Width - margins.Left - margins.Right,
                                             partSize.Height - margins.Top - margins.Bottom);
                        break;
                    }
                case Alignment.MiddleRight:
                    {
                        rect = new Rectangle((partSize.Width + (xm * partSize.Width)) - margins.Right,
                                             (0 + (ym * partSize.Height)) + margins.Top,
                                             margins.Right,
                                             partSize.Height - margins.Top - margins.Bottom);
                        break;
                    }
                case Alignment.BottomLeft:
                    {
                        rect = new Rectangle((0 + (xm * partSize.Width)),
                                             (partSize.Height + (ym * partSize.Height)) - margins.Bottom,
                                             margins.Left,
                                             margins.Bottom);
                        break;
                    }
                case Alignment.BottomCenter:
                    {
                        rect = new Rectangle((0 + (xm * partSize.Width)) + margins.Left,
                                             (partSize.Height + (ym * partSize.Height)) - margins.Bottom,
                                             partSize.Width - margins.Left - margins.Right,
                                             margins.Bottom);
                        break;
                    }
                case Alignment.BottomRight:
                    {
                        rect = new Rectangle((partSize.Width + (xm * partSize.Width)) - margins.Right,
                                             (partSize.Height + (ym * partSize.Height)) - margins.Bottom,
                                             margins.Right,
                                             margins.Bottom);
                        break;
                    }
            }

            return rect;
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public static Rectangle GetDestinationArea(Rectangle area, Margins margins, Alignment alignment)
        {
            Rectangle rect = new Rectangle();

            int adj = 1;
            margins.Left += margins.Left > 0 ? adj : 0;
            margins.Top += margins.Top > 0 ? adj : 0;
            margins.Right += margins.Right > 0 ? adj : 0;
            margins.Bottom += margins.Bottom > 0 ? adj : 0;

            margins = new Margins(margins.Left, margins.Top, margins.Right, margins.Bottom);

            switch (alignment)
            {
                case Alignment.TopLeft:
                    {
                        rect = new Rectangle(area.Left + 0,
                                             area.Top + 0,
                                             margins.Left,
                                             margins.Top);
                        break;

                    }
                case Alignment.TopCenter:
                    {
                        rect = new Rectangle(area.Left + margins.Left,
                                             area.Top + 0,
                                             area.Width - margins.Left - margins.Right,
                                             margins.Top);
                        break;

                    }
                case Alignment.TopRight:
                    {
                        rect = new Rectangle(area.Left + area.Width - margins.Right,
                                             area.Top + 0,
                                             margins.Right,
                                             margins.Top);
                        break;

                    }
                case Alignment.MiddleLeft:
                    {
                        rect = new Rectangle(area.Left + 0,
                                             area.Top + margins.Top,
                                             margins.Left,
                                             area.Height - margins.Top - margins.Bottom);
                        break;
                    }
                case Alignment.MiddleCenter:
                    {
                        rect = new Rectangle(area.Left + margins.Left,
                                             area.Top + margins.Top,
                                             area.Width - margins.Left - margins.Right,
                                             area.Height - margins.Top - margins.Bottom);
                        break;
                    }
                case Alignment.MiddleRight:
                    {
                        rect = new Rectangle(area.Left + area.Width - margins.Right,
                                             area.Top + margins.Top,
                                             margins.Right,
                                             area.Height - margins.Top - margins.Bottom);
                        break;
                    }
                case Alignment.BottomLeft:
                    {
                        rect = new Rectangle(area.Left + 0,
                                             area.Top + area.Height - margins.Bottom,
                                             margins.Left,
                                             margins.Bottom);
                        break;
                    }
                case Alignment.BottomCenter:
                    {
                        rect = new Rectangle(area.Left + margins.Left,
                                             area.Top + area.Height - margins.Bottom,
                                             area.Width - margins.Left - margins.Right,
                                             margins.Bottom);
                        break;
                    }
                case Alignment.BottomRight:
                    {
                        rect = new Rectangle(area.Left + area.Width - margins.Right,
                                             area.Top + area.Height - margins.Bottom,
                                             margins.Right,
                                             margins.Bottom);
                        break;
                    }
            }

            return rect;
        }
        ////////////////////////////////////////////////////////////////////////////    

        ////////////////////////////////////////////////////////////////////////////
        public void DrawGlyph(Glyph glyph, Rectangle rect)
        {
            Size imageSize = new Size(glyph.Image.Width, glyph.Image.Height);

            if (!glyph.SourceRect.IsEmpty)
            {
                imageSize = new Size(glyph.SourceRect.Width, glyph.SourceRect.Height);
            }

            if (glyph.SizeMode == SizeMode.Centered)
            {
                rect = new Rectangle((rect.X + (rect.Width - imageSize.Width) / 2) + glyph.Offset.X,
                                     (rect.Y + (rect.Height - imageSize.Height) / 2) + glyph.Offset.Y,
                                     imageSize.Width,
                                     imageSize.Height);
            }
            else if (glyph.SizeMode == SizeMode.Normal)
            {
                rect = new Rectangle(rect.X + glyph.Offset.X, rect.Y + glyph.Offset.Y, imageSize.Width, imageSize.Height);
            }
            else if (glyph.SizeMode == SizeMode.Auto)
            {
                rect = new Rectangle(rect.X + glyph.Offset.X, rect.Y + glyph.Offset.Y, imageSize.Width, imageSize.Height);
            }

            if (glyph.SourceRect.IsEmpty)
            {
                Draw(glyph.Image, rect, glyph.Color);
            }
            else
            {
                Draw(glyph.Image, rect, glyph.SourceRect, glyph.Color);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual void DrawLayer(Control control, SkinLayer layer, Rectangle rect)
        {
            DrawLayer(control, layer, rect, control.ControlState);
        }
        ////////////////////////////////////////////////////////////////////////////   

        ////////////////////////////////////////////////////////////////////////////    
        public virtual void DrawLayer(Control control, SkinLayer layer, Rectangle rect, ControlState state)
        {
            Color c = Color.White;
            Color oc = Color.White;
            int i = 0;
            int oi = -1;
            SkinLayer l = layer;

            if (state == ControlState.Hovered && (layer.States.Hovered.Index != -1))
            {
                c = l.States.Hovered.Color;
                i = l.States.Hovered.Index;

                if (l.States.Hovered.Overlay)
                {
                    oc = l.Overlays.Hovered.Color;
                    oi = l.Overlays.Hovered.Index;
                }
            }
            else if (state == ControlState.Focused || (control.Focused && state == ControlState.Hovered && layer.States.Hovered.Index == -1))
            {
                c = l.States.Focused.Color;
                i = l.States.Focused.Index;

                if (l.States.Focused.Overlay)
                {
                    oc = l.Overlays.Focused.Color;
                    oi = l.Overlays.Focused.Index;
                }
            }
            else if (state == ControlState.Pressed)
            {
                c = l.States.Pressed.Color;
                i = l.States.Pressed.Index;

                if (l.States.Pressed.Overlay)
                {
                    oc = l.Overlays.Pressed.Color;
                    oi = l.Overlays.Pressed.Index;
                }
            }
            else if (state == ControlState.Disabled)
            {
                c = l.States.Disabled.Color;
                i = l.States.Disabled.Index;

                if (l.States.Disabled.Overlay)
                {
                    oc = l.Overlays.Disabled.Color;
                    oi = l.Overlays.Disabled.Index;
                }
            }
            else
            {
                c = l.States.Enabled.Color;
                i = l.States.Enabled.Index;

                if (l.States.Enabled.Overlay)
                {
                    oc = l.Overlays.Enabled.Color;
                    oi = l.Overlays.Enabled.Index;
                }
            }

            if (control.Color != Control.UndefinedColor) c = control.Color * (control.Color.A / 255f);
            DrawLayer(l, rect, c, i);

            if (oi != -1)
            {
                DrawLayer(l, rect, oc, oi);
            }
        }
        ////////////////////////////////////////////////////////////////////////////   

        #endregion

    }
}