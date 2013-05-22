////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Central                                          //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: TaskDialog.cs                                //
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
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Central
{
  public class TaskDialog: Dialog
  {
    
    #region //// Fields ////////////

    ////////////////////////////////////////////////////////////////////////////       
    private Button btnClose = null;
    private Button btnApply = null;
    private Button btnOk = null;
    private ImageBox imgTop = null;
    private TabControl tbcMain = null;    
    private Button btnFirst = null;
    private Button btnSecond = null;
    private Button btnThird = null;
    private GroupPanel grpFirst = null;
    ////////////////////////////////////////////////////////////////////////////
    

    #endregion

    #region //// Constructors //////

    ////////////////////////////////////////////////////////////////////////////   
    public TaskDialog(Manager manager): base(manager)
    {                                   
      //Alpha = 200;      
      Height = 520;
      MinimumWidth = 254;
      MinimumHeight = 160;  
      Center();           
          
      TopPanel.Height = 80;
      TopPanel.BevelStyle = BevelStyle.None;
      TopPanel.BevelBorder = BevelBorder.None;
      Caption.Visible = false;
      Description.Visible = false;
      Text = "Dialog Template";

      imgTop = new ImageBox(manager);
      imgTop.Init();
      imgTop.Parent = TopPanel;
      imgTop.Top = 0;
      imgTop.Left = 0;
      imgTop.Width = TopPanel.ClientWidth;
      imgTop.Height = TopPanel.ClientHeight;
      imgTop.Anchor = Anchors.Left | Anchors.Top | Anchors.Right | Anchors.Bottom;                       
      imgTop.SizeMode = SizeMode.Normal;
      imgTop.Image = Manager.Content.Load<Texture2D>("Content\\Images\\Caption");      
      
      tbcMain = new TabControl(manager);
      tbcMain.Init();
      tbcMain.Parent = this;
      tbcMain.Left = 4;
      tbcMain.Top = TopPanel.Height + 4;
      tbcMain.Width = ClientArea.Width - 8;
      tbcMain.Height = ClientArea.Height - 8 - TopPanel.Height - BottomPanel.Height;
      tbcMain.Anchor = Anchors.All;
      tbcMain.AddPage();
      tbcMain.TabPages[0].Text = "First";
      tbcMain.AddPage();
      tbcMain.TabPages[1].Text = "Second";            
      tbcMain.AddPage();      
      tbcMain.TabPages[2].Text = "Third";      

      btnFirst = new Button(manager);
      btnFirst.Init();
      btnFirst.Parent = tbcMain.TabPages[0];      
      btnFirst.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
      btnFirst.Top = 8;
      btnFirst.Left = 8;
      btnFirst.Width = btnFirst.Parent.ClientWidth - 16;
      btnFirst.Text = ">>> First Page Button <<<";
      
      grpFirst = new GroupPanel(manager);
      grpFirst.Init();
      grpFirst.Parent = tbcMain.TabPages[0];
      grpFirst.Anchor = Anchors.All; 
      //grpFirst.Type = GroupBoxType.Flat;
      grpFirst.Left = 8;
      grpFirst.Top = btnFirst.Top + btnFirst.Height + 4;
      grpFirst.Width = btnFirst.Parent.ClientWidth - 16;
      grpFirst.Height = btnFirst.Parent.ClientHeight - grpFirst.Top - 8;

      btnSecond = new Button(manager);
      btnSecond.Init();
      btnSecond.Parent = tbcMain.TabPages[1];      
      btnSecond.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
      btnSecond.Top = 8;
      btnSecond.Left = 8;
      btnSecond.Width = btnSecond.Parent.ClientWidth - 16;
      btnSecond.Text = ">>> Second Page Button <<<";

      btnThird = new Button(manager);
      btnThird.Init();
      btnThird.Parent = tbcMain.TabPages[2];
      btnThird.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
      btnThird.Top = 8;
      btnThird.Left = 8;
      btnThird.Width = btnThird.Parent.ClientWidth - 16;
      btnThird.Text = ">>> Third Page Button <<<";                 
         
      btnOk = new Button(manager);
      btnOk.Init();
      btnOk.Parent = BottomPanel;
      btnOk.Anchor = Anchors.Top | Anchors.Right;
      btnOk.Top = btnOk.Parent.ClientHeight - btnOk.Height - 8;
      btnOk.Left = btnOk.Parent.ClientWidth - 8 - btnOk.Width * 3  - 8;
      btnOk.Text = "OK";
      btnOk.ModalResult = ModalResult.Ok;      

      btnApply = new Button(manager);
      btnApply.Init();
      btnApply.Parent = BottomPanel;
      btnApply.Anchor = Anchors.Top | Anchors.Right;
      btnApply.Top = btnOk.Parent.ClientHeight - btnOk.Height - 8;
      btnApply.Left = btnOk.Parent.ClientWidth - 4 - btnOk.Width * 2 - 8;
      btnApply.Text = "Apply";
      
      btnClose = new Button(manager);
      btnClose.Init();
      btnClose.Parent = BottomPanel;
      btnClose.Anchor = Anchors.Top | Anchors.Right;
      btnClose.Top = btnOk.Parent.ClientHeight - btnClose.Height - 8;
      btnClose.Left = btnOk.Parent.ClientWidth - btnClose.Width - 8;
      btnClose.Text = "Close";                  
      btnClose.ModalResult = ModalResult.Cancel;   
      
      btnFirst.Focused = true;               
    }
    ////////////////////////////////////////////////////////////////////////////
    
    #endregion

    #region //// Methods ///////////

    ////////////////////////////////////////////////////////////////////////////    
    public override void Init()
    {
      base.Init();

      MaximumWidth = 654;      
    }
    ////////////////////////////////////////////////////////////////////////////   

    #endregion
    
  }
}
