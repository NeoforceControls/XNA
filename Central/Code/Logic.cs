////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Logic.cs                                     //
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;
using Controls = TomShane.Neoforce.Controls;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Central
{
  public partial class MainWindow
  {

    #region //// Consts ////////////

    ////////////////////////////////////////////////////////////////////////////   
    private const int TasksCount = 5;
    private string[] Tasks = new string[TasksCount] {"Dialog Template", "Controls Test", "Auto Scrolling", "Layout Window","Events Test"};
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    #endregion

    #region //// Methods ///////////
 
    ////////////////////////////////////////////////////////////////////////////
    void btnClose_Click(object sender, Controls.EventArgs e)
    {
      ControlsList list = new ControlsList();      
      list.AddRange(Manager.Controls);                  

      for (int i = 0; i < list.Count; i++)
      {
        if (list[i] is Window)
        {
          if (((Window)list[i]).Text.Substring(0, 6) == "Window")
          {
            (list[i] as Window).Dispose();            
          }
        }
      }
      list.Clear();
    }
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    void btnRandom_Click(object sender, Controls.EventArgs e)
    {
      Window win = new Window(Manager);
      Button btn = new Button(Manager);
      TextBox txt = new TextBox(Manager);               
      
      win.Init();
      btn.Init();  
      txt.Init();    

      win.ClientWidth = 320;
      win.ClientHeight = 160;     
      
      win.MinimumWidth = 128;        
      win.MinimumHeight = 128;
      
      Random r = new Random((int)Central.Frames);
      win.ClientWidth += r.Next(-100, +100);
      win.ClientHeight += r.Next(-100, +100);
      
      win.Left = r.Next(200, Manager.ScreenWidth - win.ClientWidth / 2);
      win.Top = r.Next(0, Manager.ScreenHeight - win.ClientHeight / 2);
      win.Closed += new WindowClosedEventHandler(win_Closed);
      
    /*
      win.Width = 1024;
      win.Height = 768;
      win.Left = 220;
      win.Top = 0;
      win.StayOnBack = true;
      win.SendToBack();
*/
      btn.Anchor = Anchors.Bottom;
      btn.Left = (win.ClientWidth / 2) - (btn.Width / 2);
      btn.Top = win.ClientHeight - btn.Height - 8;
      btn.Text = "OK";

      win.Text = "Window (" + win.Width.ToString() + "x" + win.Height.ToString() + ")";
      
      txt.Parent = win;
      txt.Left = 8;
      txt.Top = 8;
      txt.Width = win.ClientArea.Width - 16;
      txt.Height = win.ClientArea.Height - 48;
      txt.Anchor = Anchors.All;
      txt.Mode = TextBoxMode.Multiline;
      txt.Text = "This is a Multiline TextBox.\n" +
                 "Allows to edit large texts,\n" +
                 "copy text to and from clipboard,\n" +
                 "select text with mouse or keyboard\n" +
                 "and much more...";

      txt.SelectAll();
      txt.Focused = true;      
      //txt.ReadOnly = true;
       
      txt.ScrollBars = ScrollBars.Both;
      
      win.Add(btn, true);      
      win.Show();
      Manager.Add(win);      
    }
    ////////////////////////////////////////////////////////////////////////////    
    
    ////////////////////////////////////////////////////////////////////////////    
    void win_Closed(object sender, WindowClosedEventArgs e)
    {
      e.Dispose = true;
    }
    ////////////////////////////////////////////////////////////////////////////    
  
    ////////////////////////////////////////////////////////////////////////////    
    void btnExit_Click(object sender, Controls.EventArgs e)
    {
      Close();
    }
    ////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////
    void btnApply_Click(object sender, Controls.EventArgs e)
    {
      Manager.Graphics.IsFullScreen = chkResFull.Checked;

      int w = 1024;
      int h = 768;

      if (rdbRes1024.Checked)
      {
        w = 1024;
        h = 768;
      }
      else if (rdbRes1280.Checked)
      {
        w = 1280;
        h = 1024;
      }
      else if (rdbRes1680.Checked)
      {
        w = 1680;
        h = 1050;
      }

      Manager.Graphics.PreferredBackBufferWidth = w;
      Manager.Graphics.PreferredBackBufferHeight = h;      
      
      Manager.Graphics.ApplyChanges();      
    }
    ////////////////////////////////////////////////////////////////////////////   
    
    ////////////////////////////////////////////////////////////////////////////   
    void rdbGreen_Click(object sender, Controls.EventArgs e)
    {
      (Manager.Game as Application).BackgroundImage = greenbg;  
      Manager.SetSkin("Green");
    }
    ////////////////////////////////////////////////////////////////////////////   
    
    ////////////////////////////////////////////////////////////////////////////   
    void rdbDefault_Click(object sender, Controls.EventArgs e)
    {
      (Manager.Game as Application).BackgroundImage = defaultbg;
      Manager.SetSkin("Default");
    }
    ////////////////////////////////////////////////////////////////////////////   
        
    ////////////////////////////////////////////////////////////////////////////   
    void btnTask_Click(object sender, Controls.EventArgs e)
    {  
      if (sender == btnTasks[0])
      {
        
        #if (!XBOX && !XBOX_FAKE)
          Manager.Cursor = Manager.Skin.Cursors["Busy"].Resource;
        #endif  
        
        btnTasks[0].Enabled = false;                
        TaskDialog tmp = new TaskDialog(Manager);
        tmp.Closing += new WindowClosingEventHandler(WindowClosing);
        tmp.Closed += new WindowClosedEventHandler(WindowClosed);
        tmp.Init();
        Manager.Add(tmp);
        
        #if (!XBOX && !XBOX_FAKE)
          Thread.Sleep(2000); // Sleep to demonstrate animated busy cursor
        #endif
        
        tmp.Show();
        
        #if (!XBOX && !XBOX_FAKE)
          Manager.Cursor = Manager.Skin.Cursors["Default"].Resource;
        #endif  
      }
      else if (sender == btnTasks[1])
      {
        btnTasks[1].Enabled = false;
        TaskControls tmp = new TaskControls(Manager);
        tmp.Closing += new WindowClosingEventHandler(WindowClosing);
        tmp.Closed += new WindowClosedEventHandler(WindowClosed);
        tmp.Init();
        Manager.Add(tmp);
        tmp.ShowModal();        
      }
      else if (sender == btnTasks[2])
      {
        btnTasks[2].Enabled = false;
        TaskAutoScroll tmp = new TaskAutoScroll(Manager);
        tmp.Closing += new WindowClosingEventHandler(WindowClosing);
        tmp.Closed += new WindowClosedEventHandler(WindowClosed);
        tmp.Init();
        Manager.Add(tmp);
        tmp.Show();
      }
      else if (sender == btnTasks[3])
      {
        btnTasks[3].Enabled = false;

        Window tmp = (Window)Layout.Load(Manager, "Window");
        tmp.Closing += new WindowClosingEventHandler(WindowClosing);
        tmp.Closed += new WindowClosedEventHandler(WindowClosed);        
        tmp.Init();
        tmp.GetControl("btnOk").Click += new Controls.EventHandler(Central_Click);          
        Manager.Add(tmp);
        tmp.Show();
      }
      else if (sender == btnTasks[4])
      {
        btnTasks[4].Enabled = false;

        TaskEvents tmp = new TaskEvents(Manager);
        tmp.Closing += new WindowClosingEventHandler(WindowClosing);
        tmp.Closed += new WindowClosedEventHandler(WindowClosed);
        tmp.Init();
        Manager.Add(tmp);
        tmp.Show();
      }
    }

    void Central_Click(object sender, TomShane.Neoforce.Controls.EventArgs e)
    {
      ((sender as Button).Root as Window).Close();
    }
    ////////////////////////////////////////////////////////////////////////////      
    
    ////////////////////////////////////////////////////////////////////////////      
    void WindowClosing(object sender, WindowClosingEventArgs e)
    {
      //e.Cancel = true; 
    }
    ////////////////////////////////////////////////////////////////////////////      
    
    ////////////////////////////////////////////////////////////////////////////      
    void WindowClosed(object sender, WindowClosedEventArgs e)
    {      
      if (sender is TaskDialog)
      {
        btnTasks[0].Enabled = true;      
        btnTasks[0].Focused = true;
      }  
      else if (sender is TaskControls)
      {
        btnTasks[1].Enabled = true;
        btnTasks[1].Focused = true;
      }
      else if (sender is TaskAutoScroll)
      {
        btnTasks[2].Enabled = true;
        btnTasks[2].Focused = true;
      }
      else if (sender is Window && (sender as Window).Name == "frmMain")
      {
        btnTasks[3].Enabled = true;
        btnTasks[3].Focused = true;
      }
      else if (sender is TaskEvents)
      {
        btnTasks[4].Enabled = true;
        btnTasks[4].Focused = true;
      } 
      e.Dispose = true;      
    }
    ////////////////////////////////////////////////////////////////////////////

    #endregion
    
  }
}
