////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: TaskAutoScroll.cs                            //
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
using System.Collections.Generic;
using System.Text;
using TomShane.Neoforce.Controls;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit;
using System.Threading;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Central
{
  public class TaskAutoScroll: Window
  {
    
    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////           
    private Panel pnl1;
    private Panel pnl2;    
    ////////////////////////////////////////////////////////////////////////////
    

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////   
    public TaskAutoScroll(Manager manager): base(manager)
    {                                         
      Height = 360;            
      MinimumHeight = 99;  
      MinimumWidth = 78;
      Text = "Auto Scrolling";
      Center();               
      
      pnl1 = new Panel(manager);
      pnl1.Init();
      pnl1.Parent = this;
      pnl1.Width = 400;  
      pnl1.Height = 180;
      pnl1.Left = 20;
      pnl1.Top = 20;
      pnl1.BevelBorder = BevelBorder.All;
      pnl1.BevelStyle = BevelStyle.Flat;
      pnl1.BevelMargin = 1;
      pnl1.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
      pnl1.AutoScroll = true;      

      pnl2 = new Panel(manager);
      pnl2.Init();
      pnl2.Parent = this;
      pnl2.Width = 400;
      pnl2.Height = 320;
      pnl2.Left = 40;
      pnl2.Top = 80;
      pnl2.BevelBorder = BevelBorder.All;
      pnl2.BevelStyle = BevelStyle.Flat;
      pnl2.BevelMargin = 1;
      pnl2.Text = "2";
      pnl2.Anchor = Anchors.Left | Anchors.Top;
      pnl2.Color = SharpDX.Color.White;                   
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

    #endregion
    
  }
}
