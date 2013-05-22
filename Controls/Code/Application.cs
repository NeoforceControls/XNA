////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Controls                                         //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: Application.cs                               //
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
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;
using System;

#if (!XBOX && !XBOX_FAKE)
using System.Windows.Forms;
#endif
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Controls
{
    public class Application : Game
    {

        #region //// Fields ////////////

        ////////////////////////////////////////////////////////////////////////////
        private GraphicsDeviceManager graphics;
        private Manager manager;
        private SpriteBatch sprite;
        private bool clearBackground = false;
        private Color backgroundColor = Color.Black;
        private Texture2D backgroundImage;
        private Window mainWindow;
        private bool appWindow = false;
        private Point mousePos = Point.Zero;
        private bool systemBorder = true;
        private bool fullScreenBorder = true;
        private bool exitConfirmation = true;
        private bool exit = false;
        private ExitDialog exitDialog = null;
#if (!XBOX && !XBOX_FAKE)
        private bool mouseDown = false;
#endif
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Properties ////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual Manager Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual bool ClearBackground
        {
            get { return clearBackground; }
            set { clearBackground = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual Texture2D BackgroundImage
        {
            get { return backgroundImage; }
            set { backgroundImage = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual Window MainWindow
        {
            get { return mainWindow; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual bool SystemBorder
        {
            get { return systemBorder; }
            set { systemBorder = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual bool FullScreenBorder
        {
            get { return fullScreenBorder; }
            set { fullScreenBorder = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        public virtual bool ExitConfirmation
        {
            get { return exitConfirmation; }
            set { exitConfirmation = value; }
        }
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Constructors //////

        ////////////////////////////////////////////////////////////////////////////
        public Application()
            : this("Default", false)
        {
        }
        ////////////////////////////////////////////////////////////////////////////    

        ////////////////////////////////////////////////////////////////////////////
        public Application(string skin)
            : this(skin, false)
        {
        }
        ////////////////////////////////////////////////////////////////////////////            

        ////////////////////////////////////////////////////////////////////////////
        public Application(bool appWindow)
            : this("Default", appWindow)
        {
        }
        ////////////////////////////////////////////////////////////////////////////        

        ////////////////////////////////////////////////////////////////////////////
        public Application(string skin, bool appWindow)
        {
            this.appWindow = appWindow;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            graphics.IsFullScreen = false;
            graphics.PreferMultiSampling = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.DeviceReset += new EventHandler<System.EventArgs>(Graphics_DeviceReset);

            IsFixedTimeStep = false;
            IsMouseVisible = true;

            manager = new Manager(this, graphics, skin);
            manager.AutoCreateRenderTarget = false;
            manager.TargetFrames = 60;
            manager.WindowClosing += new WindowClosingEventHandler(Manager_WindowClosing);
        }
        ////////////////////////////////////////////////////////////////////////////                      

        #endregion

        #region //// Destructors ///////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (manager != null) manager.Dispose();
                if (sprite != null) sprite.Dispose();
            }
            base.Dispose(disposing);
        }
        ////////////////////////////////////////////////////////////////////////////

        #endregion

        #region //// Methods ///////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Initialize()
        {
            base.Initialize();

            manager.Initialize();

            Manager.RenderTarget = CreateRenderTarget();
            Manager.Input.InputOffset = new InputOffset(0, 0, Manager.ScreenWidth / (float)Manager.TargetWidth, Manager.ScreenHeight / (float)Manager.TargetHeight);

            sprite = new SpriteBatch(GraphicsDevice);

#if (!XBOX && !XBOX_FAKE)
            Manager.Window.BackColor = System.Drawing.Color.Black;
            Manager.Window.FormBorderStyle = systemBorder ? System.Windows.Forms.FormBorderStyle.FixedDialog : System.Windows.Forms.FormBorderStyle.None;

            Manager.Input.MouseMove += new MouseEventHandler(Input_MouseMove);
            Manager.Input.MouseDown += new MouseEventHandler(Input_MouseDown);
            Manager.Input.MouseUp += new MouseEventHandler(Input_MouseUp);
#endif

            if (appWindow)
            {
                mainWindow = CreateMainWindow();
            }

            InitMainWindow();
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            manager.Update(gameTime);
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected override void Draw(GameTime gameTime)
        {
            manager.BeginDraw(gameTime);

            if (clearBackground) GraphicsDevice.Clear(backgroundColor);
            if (backgroundImage != null && mainWindow == null)
            {
                int sx = manager.TargetWidth;
                int sy = manager.TargetHeight;
                sprite.Begin();
                sprite.Draw(backgroundImage, new Rectangle(0, 0, sx, sy), Color.White);
                sprite.End();
            }

            base.Draw(gameTime);
            DrawScene(gameTime);

            manager.EndDraw(new Rectangle(0, 0, Manager.ScreenWidth, Manager.ScreenHeight));
        }
        ////////////////////////////////////////////////////////////////////////////	    

        ////////////////////////////////////////////////////////////////////////////	    
        protected virtual Window CreateMainWindow()
        {
            return new Window(manager);
        }
        ////////////////////////////////////////////////////////////////////////////	    

        ////////////////////////////////////////////////////////////////////////////	        
        protected virtual void InitMainWindow()
        {
            if (mainWindow != null)
            {
                if (!mainWindow.Initialized) mainWindow.Init();

                mainWindow.Alpha = 255;
                mainWindow.Width = manager.TargetWidth;
                mainWindow.Height = manager.TargetHeight;
                mainWindow.Shadow = false;
                mainWindow.Left = 0;
                mainWindow.Top = 0;
                mainWindow.CloseButtonVisible = true;
                mainWindow.Resizable = false;
                mainWindow.Movable = false;
                mainWindow.Text = this.Window.Title;
                mainWindow.Closing += new WindowClosingEventHandler(MainWindow_Closing);
                mainWindow.ClientArea.Draw += new DrawEventHandler(MainWindow_Draw);
                mainWindow.BorderVisible = mainWindow.CaptionVisible = (!systemBorder && !Graphics.IsFullScreen) || (Graphics.IsFullScreen && fullScreenBorder);
                mainWindow.StayOnBack = true;

                manager.Add(mainWindow);

                mainWindow.SendToBack();
            }
        }
        ////////////////////////////////////////////////////////////////////////////	      

        ////////////////////////////////////////////////////////////////////////////	    
        private void MainWindow_Draw(object sender, DrawEventArgs e)
        {
            if (backgroundImage != null && mainWindow != null)
            {
                e.Renderer.Draw(backgroundImage, e.Rectangle, Color.White);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////	      
        public new virtual void Exit()
        {
            exit = true;
            base.Exit();
        }
        ////////////////////////////////////////////////////////////////////////////	  

        ////////////////////////////////////////////////////////////////////////////
        private void Manager_WindowClosing(object sender, WindowClosingEventArgs e)
        {
            e.Cancel = !exit && exitConfirmation;

            if (!exit && exitConfirmation && exitDialog == null)
            {
                exitDialog = new ExitDialog(Manager);
                exitDialog.Init();
                exitDialog.Closed += new WindowClosedEventHandler(closeDialog_Closed);
                exitDialog.ShowModal();
                Manager.Add(exitDialog);
            }
            else if (!exitConfirmation)
            {
                Exit();
            }
        }
        ////////////////////////////////////////////////////////////////////////////      

        ////////////////////////////////////////////////////////////////////////////      
        private void closeDialog_Closed(object sender, WindowClosedEventArgs e)
        {
            if ((sender as Dialog).ModalResult == ModalResult.Yes)
            {
                Exit();
            }
            else
            {
                exit = false;
                exitDialog.Closed -= closeDialog_Closed;
                exitDialog.Dispose();
                exitDialog = null;
                if (mainWindow != null) mainWindow.Focused = true;
            }
        }
        ////////////////////////////////////////////////////////////////////////////      

        ////////////////////////////////////////////////////////////////////////////	    
        private void MainWindow_Closing(object sender, WindowClosingEventArgs e)
        {
            e.Cancel = true;
            Manager_WindowClosing(sender, e);
        }
        ////////////////////////////////////////////////////////////////////////////	    

        ////////////////////////////////////////////////////////////////////////////	    
        private void Graphics_DeviceReset(object sender, System.EventArgs e)
        {
            if (Manager.RenderTarget != null)
            {
                //These steps are already done by the manager on Graphics Device Reset.
                //Manager.RenderTarget.Dispose();
                //Manager.RenderTarget = CreateRenderTarget(); 
                Manager.Input.InputOffset = new InputOffset(0, 0, Manager.ScreenWidth / (float)Manager.TargetWidth, Manager.ScreenHeight / (float)Manager.TargetHeight);
            }

            if (mainWindow != null)
            {
                mainWindow.Height = Manager.TargetHeight;
                mainWindow.Width = Manager.TargetWidth;
                mainWindow.BorderVisible = mainWindow.CaptionVisible = (!systemBorder && !Graphics.IsFullScreen) || (Graphics.IsFullScreen && fullScreenBorder);
            }
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected virtual RenderTarget2D CreateRenderTarget()
        {
            return manager.CreateRenderTarget();
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////
        protected virtual void DrawScene(GameTime gameTime)
        {
        }
        ////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////	  
        public new void Run()
        {
            // try
            {
                base.Run();
            }
            /* catch (Exception x)
             {
              #if (!XBOX && !XBOX_FAKE)         
                MessageBox.Show("An unhandled exception has occurred.\n" + x.Message, Window.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Manager.LogException(x);
              #else
                throw x;
              #endif 
             }*/
        }
        ////////////////////////////////////////////////////////////////////////////	  

        #endregion

        #region //// Windows ///////////

#if (!XBOX && !XBOX_FAKE)

        ////////////////////////////////////////////////////////////////////////////	
        private void Input_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Manager.Window.Left = e.Position.X + Manager.Window.Left - mousePos.X;
                Manager.Window.Top = e.Position.Y + Manager.Window.Top - mousePos.Y;
            }
        }
        ////////////////////////////////////////////////////////////////////////////	

        ////////////////////////////////////////////////////////////////////////////	
        private void Input_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left && !Graphics.IsFullScreen && !systemBorder)
            {
                if (CheckPos(e.Position))
                {
                    mouseDown = true;
                    mousePos = e.Position;
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////	

        ////////////////////////////////////////////////////////////////////////////	
        private void Input_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        ////////////////////////////////////////////////////////////////////////////	

        ////////////////////////////////////////////////////////////////////////////	
        private bool CheckPos(Point pos)
        {
            if (pos.X >= 24 && pos.X <= Manager.TargetWidth - 48 &&
                pos.Y >= 0 && pos.Y <= Manager.Skin.Controls["Window"].Layers["Caption"].Height)
            {
                foreach (Control c in Manager.Controls)
                {
                    if (c.Visible && c != MainWindow &&
                        pos.X >= c.AbsoluteRect.Left && pos.X <= c.AbsoluteRect.Right &&
                        pos.Y >= c.AbsoluteRect.Top && pos.Y <= c.AbsoluteRect.Bottom)
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }
        ////////////////////////////////////////////////////////////////////////////	  

#endif

        #endregion

    }
}
