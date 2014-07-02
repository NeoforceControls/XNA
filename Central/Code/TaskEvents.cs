////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: TaskEvents.cs                                //
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
//using System;
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
  public class TaskEvents: Window
  {
    
    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////           
    private Button btn;
    private ListBox lst;
    private ListBox txt;    
    ////////////////////////////////////////////////////////////////////////////
    

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////   
    public TaskEvents(Manager manager): base(manager)
    {                                         
      Height = 360;            
      MinimumHeight = 99;  
      MinimumWidth = 78;
      Text = "Events Test";
      Center();               
      
      btn = new Button(manager);
      btn.Init();
      btn.Parent = this;
      btn.Left = 20;
      btn.Top = 20;
      btn.MouseMove += new MouseEventHandler(btn_MouseMove);
      btn.MouseDown += new MouseEventHandler(btn_MouseDown);
      btn.MouseUp += new MouseEventHandler(btn_MouseUp);
      btn.MouseOver += new MouseEventHandler(btn_MouseOver);
      btn.MouseOut += new MouseEventHandler(btn_MouseOut);
      btn.MousePress += new MouseEventHandler(btn_MousePress);
      btn.Click += new EventHandler(btn_Click);    
      
      lst = new ListBox(manager);
      lst.Init();
      lst.Parent = this;
      lst.Left = 20;
      lst.Top = 60;
      lst.Width = 128;
      lst.Height = 128;
      lst.MouseMove += new MouseEventHandler(btn_MouseMove);
      lst.MouseDown += new MouseEventHandler(btn_MouseDown);
      lst.MouseUp += new MouseEventHandler(btn_MouseUp);
      lst.MouseOver += new MouseEventHandler(btn_MouseOver);
      lst.MouseOut += new MouseEventHandler(btn_MouseOut);
      lst.MousePress += new MouseEventHandler(btn_MousePress);
      lst.MouseScroll += new MouseEventHandler(lst_MouseScroll);
      lst.Click += new EventHandler(btn_Click);
      
      txt = new ListBox(manager);
      txt.Init();
      txt.Parent = this;
      txt.Left = 200;
      txt.Top = 8;
      txt.Width = 160;
      txt.Height = 300;                  
    }

    void lst_MouseScroll(object sender, MouseEventArgs e)
    {
        txt.Items.Add((sender == btn ? "Button" : "List") + ": Scroll");
        txt.ItemIndex = txt.Items.Count - 1;
    }

    void btn_Click(object sender, EventArgs e)
    {
      MouseEventArgs ex = (e is MouseEventArgs) ? (MouseEventArgs)e : new MouseEventArgs();          
      txt.Items.Add((sender == btn ? "Button" : "List") + ": Click " + ex.Button.ToString());
      txt.ItemIndex = txt.Items.Count - 1;     
    }

    void btn_MousePress(object sender, MouseEventArgs e)
    {
    //  txt.Items.Add((sender == btn ? "Button" : "List") + ": Press");
    //  txt.ItemIndex = txt.Items.Count - 1;
    }

    void btn_MouseOut(object sender, MouseEventArgs e)
    {
      txt.Items.Add((sender == btn ? "Button" : "List") + ": Out");
      txt.ItemIndex = txt.Items.Count - 1;
    }

    void btn_MouseOver(object sender, MouseEventArgs e)
    {
      txt.Items.Add((sender == btn ? "Button" : "List") + ": Over");
      txt.ItemIndex = txt.Items.Count - 1;
    }

    void btn_MouseUp(object sender, MouseEventArgs e)
    {
      txt.Items.Add((sender == btn ? "Button" : "List") + ": Up " + e.Button.ToString());
      txt.ItemIndex = txt.Items.Count - 1;
    }

    void btn_MouseDown(object sender, MouseEventArgs e)
    {
      txt.Items.Add((sender == btn ? "Button" : "List") + ": Down " + e.Button.ToString());
      txt.ItemIndex = txt.Items.Count - 1;
    }

    void btn_MouseMove(object sender, MouseEventArgs e)
    {
      txt.Items.Add((sender == btn ? "Button" : "List") + ": Move");
      txt.ItemIndex = txt.Items.Count - 1;
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
