////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: StackPanel.cs                                //
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
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls
{
  
  public class StackPanel: Container
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
      private Orientation orientation;
      public Orientation Orientation
      {
          get { return this.orientation; }
          set
          {
              this.orientation = value;
              this.CalcLayout();
          }
      }    
    ////////////////////////////////////////////////////////////////////////////

    #endregion
   
    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////
    public StackPanel(Manager manager, Orientation orientation): base(manager)
    {
      this.orientation = orientation;
      this.Color = Color.Transparent;
    }
    ////////////////////////////////////////////////////////////////////////////

    #endregion                    
        
    #region //// Methods ///////////

    ////////////////////////////////////////////////////////////////////////////
    private void CalcLayout()
    {
      int top = Top;
      int left = Left;            

      foreach (Control c in ClientArea.Controls)
      {
        Margins m = c.Margins;
  
        if (orientation == Orientation.Vertical)
        {
          top += m.Top;
          c.Top = top;
          top += c.Height;
          top += m.Bottom;
          c.Left = left;
        }

        if (orientation == Orientation.Horizontal)
        {
          left += m.Left;
          c.Left = left;
          left += c.Width;
          left += m.Right;
          c.Top = top;
        }
      }
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
    {      
      base.DrawControl(renderer, rect, gameTime);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void OnResize(ResizeEventArgs e)
    {
      CalcLayout();
      base.OnResize(e);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    #endregion
  
  }       
  
}
