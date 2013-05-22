////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Central.cs                                   //
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TomShane.Neoforce.Controls;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.GamerServices;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Central
{
    
  public class Central: Application
  {

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////               
    private int afps = 0;
    private int fps = 0;
    private double et = 0;
    public static long Frames = 0;             
    ////////////////////////////////////////////////////////////////////////////

    #endregion   

    #region //// Constructors //////
    
    ////////////////////////////////////////////////////////////////////////////    
    public Central(): base(true)
    {                                   
               
      SystemBorder = false;
      FullScreenBorder = false;
      ClearBackground = false;              
      ExitConfirmation = false;      
      Manager.TargetFrames = 60;
      //IsFixedTimeStep = true;

      //Components.Add(new GamerServicesComponent(this));         
      //Manager.UseGuide = true;
    }
    ////////////////////////////////////////////////////////////////////////////        
    
    #endregion  

		#region //// Methods ///////////
                
    ////////////////////////////////////////////////////////////////////////////
    protected override void Initialize()
    {      
      base.Initialize();                                
    }
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    protected override Window CreateMainWindow()
    {
      return new MainWindow(Manager);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void LoadContent()
    {
      base.LoadContent();
    }
    ////////////////////////////////////////////////////////////////////////////    
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Update(GameTime gameTime)
    {                       
      base.Update(gameTime);      
      UpdateStats(gameTime);
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Draw(GameTime gameTime)
    {
      Frames += 1;
      base.Draw(gameTime);            
    }
		////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////              
    private void UpdateStats(GameTime time)
    {          
      MainWindow wnd = MainWindow as MainWindow;
      if (et >= 500 || et == 0)
      {    
        if (wnd != null)
        {   
          wnd.lblObjects.Text = "Objects: " + Disposable.Count.ToString();
          wnd.lblAvgFps.Text = "Average FPS: " + afps.ToString();
          wnd.lblFps.Text = "Current FPS: " + fps.ToString();        
        }
                   
        if (time.TotalGameTime.TotalSeconds != 0)
        {
          afps = (int)(Frames / time.TotalGameTime.TotalSeconds);
        }

        if (time.ElapsedGameTime.TotalMilliseconds != 0)
        {
          fps = (int)(1000 / time.ElapsedGameTime.TotalMilliseconds);
        }       

        et = 1;
      }
      et += time.ElapsedGameTime.TotalMilliseconds;            
    }
    ////////////////////////////////////////////////////////////////////////////
/*
    ////////////////////////////////////////////////////////////////////////////    
    protected override RenderTarget2D CreateRenderTarget()
    {
      return new RenderTarget2D(GraphicsDevice, 1024, 768, false, SurfaceFormat.Color, DepthFormat.None, 0, Manager.RenderTargetUsage);
    }
    ////////////////////////////////////////////////////////////////////////////
    */
  	#endregion 
  	
	}
}