using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GhostEngine;
using System.ComponentModel;

namespace GhostLegends
{
    public class EngineGame : Microsoft.Xna.Framework.Game
    {
        ChangedStage GhostGame;

        Boolean PreComplated = false;
        BackgroundWorker PreContent = new BackgroundWorker();
        GraphicsDeviceManager WinGame;
        SpriteBatch DrawBatch;
        SpriteFont FPSFont, MainMenuFont;
        Texture2D lv0Ground, lv1Ground;
        Texture2D MousePointer, MouseImage;

        Video VideoLogo, VideoCredit;
        VideoPlayer TitlePlayer, CreditPlayer;

        Dictionary<String, String[]> GroupMenu = new Dictionary<String, String[]>();
        List<Menu> MenuBase = new List<Menu>();
        String ReportProgress = "Title Preview";
        float AlphaObject;
        Boolean FadeObject = true;
        public EngineGame()
        {
            WinGame = new GraphicsDeviceManager(this);
            WinGame.PreferredBackBufferWidth = 1280;
            WinGame.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        } 

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        private void InitializeMenu(object sender, DoWorkEventArgs e)
        {
            PreContent.ReportProgress(1);
            GroupMenu.Add("MainMenu", new String[] { "Continue", "StartGame", "Versus", "Option", "Credit", "Exit" });
            GroupMenu.Add("StartGame", new String[] { "NormalMode", "ExpertMode", "StartGameBack" });
            GroupMenu.Add("Versus", new String[] { "VersusHM", "VersusAI", "VersusBack" });
            GroupMenu.Add("Option", new String[] { "Graphic", "Controller", "Audio", "OptionBack" });
            GroupMenu.Add("Exit", new String[] { "ExitText", "Yes", "No" });
            GroupMenu.Add("Pause", new String[] { "ResumeGame", "ExitToMain" });

            MenuBase.Add(new Menu("Continue", Content.Load<Texture2D>("Data\\MainMenu\\Continue")));
            MenuBase.Add(new Menu("StartGame", Content.Load<Texture2D>("Data\\MainMenu\\StartGame")));
            MenuBase.Add(new Menu("Versus", Content.Load<Texture2D>("Data\\MainMenu\\Versus")));
            MenuBase.Add(new Menu("Option", Content.Load<Texture2D>("Data\\MainMenu\\Option")));
            MenuBase.Add(new Menu("Credit", Content.Load<Texture2D>("Data\\MainMenu\\Credit")));
            MenuBase.Add(new Menu("Exit", Content.Load<Texture2D>("Data\\MainMenu\\Exit")));
            MenuBase.Add(new Menu("NormalMode", Content.Load<Texture2D>("Data\\MainMenu\\NormalMode")));
            MenuBase.Add(new Menu("ExpertMode", Content.Load<Texture2D>("Data\\MainMenu\\ExpertMode")));
            MenuBase.Add(new Menu("StartGameBack", Content.Load<Texture2D>("Data\\MainMenu\\Back")));
            MenuBase.Add(new Menu("VersusHM", Content.Load<Texture2D>("Data\\MainMenu\\VersusHM")));
            MenuBase.Add(new Menu("VersusAI", Content.Load<Texture2D>("Data\\MainMenu\\VersusAI")));
            MenuBase.Add(new Menu("VersusBack", Content.Load<Texture2D>("Data\\MainMenu\\Back")));
            MenuBase.Add(new Menu("Graphic", Content.Load<Texture2D>("Data\\MainMenu\\Graphic")));
            MenuBase.Add(new Menu("Controller", Content.Load<Texture2D>("Data\\MainMenu\\Controller")));
            MenuBase.Add(new Menu("Audio", Content.Load<Texture2D>("Data\\MainMenu\\Audio")));
            MenuBase.Add(new Menu("OptionBack", Content.Load<Texture2D>("Data\\MainMenu\\Back")));
            MenuBase.Add(new Menu("ExitText", Content.Load<Texture2D>("Data\\MainMenu\\ExitString")));
            MenuBase.Add(new Menu("Yes", Content.Load<Texture2D>("Data\\MainMenu\\Yes")));
            MenuBase.Add(new Menu("No", Content.Load<Texture2D>("Data\\MainMenu\\No")));
            MenuBase.Add(new Menu("ResumeGame", Content.Load<Texture2D>("Data\\MainMenu\\ResumeGame")));
            MenuBase.Add(new Menu("ExitToMain", Content.Load<Texture2D>("Data\\MainMenu\\Exit")));
            System.Threading.Thread.Sleep(5000);

            PreContent.ReportProgress(2);
            CreditPlayer = new VideoPlayer();
            CreditPlayer.IsLooped = true;
            CreditPlayer.IsMuted = true;
            VideoCredit = Content.Load<Video>("Data\\Media\\Credit-Loop");

        }

        private void PreloadAppChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportProgress = "Loading...";
            // PreloadAppChanged
        }

        private void PreloadAppCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!(e.Error == null))
            {

            }
            else
            {
                PreComplated = true;
                ReportProgress = "Start Game";
            }
        }
        protected override void LoadContent()
        {
            DrawBatch = new SpriteBatch(GraphicsDevice);
            GhostGame = new ChangedStage(Content, DrawBatch);


            FPSFont = Content.Load<SpriteFont>("Data\\Font\\GhostFont");
            // Mouse Graphic
            MousePointer = Content.Load<Texture2D>("Data\\Pointer");
            MouseImage = Content.Load<Texture2D>("Data\\Cursor");

            // LoadContent Graphic
            MainMenuFont = Content.Load<SpriteFont>("Data\\Font\\PreloadFont");
            lv0Ground = Content.Load<Texture2D>("Data\\Ground-Level0");
            lv1Ground = Content.Load<Texture2D>("Data\\Ground-Level1");

            // Preview Video Player
            VideoLogo = Content.Load<Video>("Data\\Media\\Logo-PkAtPay");
            TitlePlayer = new VideoPlayer();
            TitlePlayer.IsLooped = false;
            TitlePlayer.Play(VideoLogo);            

            // TODO: use this.Content to load your game content here
            PreContent.WorkerReportsProgress = true;
            PreContent.DoWork += new DoWorkEventHandler(InitializeMenu);
            PreContent.ProgressChanged += new ProgressChangedEventHandler(PreloadAppChanged);
            PreContent.RunWorkerCompleted += new RunWorkerCompletedEventHandler(PreloadAppCompleted);

            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Console.WriteLine("UnloadContent");
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState ControlGame = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (GhostGame.CurrentStage)
            {
                case ChangedStage.GameStage.Title:
                    if (TitlePlayer.State == MediaState.Stopped || ControlGame.IsKeyDown(Keys.Escape))
                    {
                        TitlePlayer.Stop();
                        GhostGame.StateChanged(ChangedStage.GameStage.Loading, 100, 1000, Color.White);
                    }
            
                    break;
                // End case GameStage.Title :
                case ChangedStage.GameStage.Loading:
                    if (PreContent.IsBusy != true && PreComplated == false)
                        PreContent.RunWorkerAsync();
                    Int32 _Speed = 400;

                    if (FadeObject == false)
                    {
                        AlphaObject -= (255 / (_Speed / (float)gameTime.ElapsedGameTime.Milliseconds));
                        if (AlphaObject < 0) FadeObject = true;
                    }

                    if (FadeObject == true)
                    {
                        AlphaObject += (255 / (_Speed / (float)gameTime.ElapsedGameTime.Milliseconds));
                        if (AlphaObject > 255) FadeObject = false;
                    }


                    if (PreComplated == true && ControlGame.IsKeyDown(Keys.Enter))
                        GhostGame.StateChanged(ChangedStage.GameStage.MainMenu, 0, 0, Color.Black);

                    break;
                // End case GameStage.Loading:
                case ChangedStage.GameStage.MainMenu:

                    break;
                // End case GameStage.MainMenu:
                case ChangedStage.GameStage.Playing:
                    if (GhostGame.PauseGame == ChangedStage.PauseStage.Playing)
                    {
                        switch (GhostGame.PlayMission)
                        {
                            case ChangedStage.MissionStage.Mission1:

                                break;
                            case ChangedStage.MissionStage.Mission2:

                                break;
                            case ChangedStage.MissionStage.Mission3:

                                break;
                            case ChangedStage.MissionStage.Boss1:

                                break;
                        }
                        break;
                    }
                    else if (GhostGame.PauseGame == ChangedStage.PauseStage.Pause)
                    {

                    }
                    break;

                // End case GameStage.Playing:
            }
            // TODO: Add your update logic here

            Int32 XScreen = (WinGame.PreferredBackBufferWidth / 2);
            Int32 YScreen = (WinGame.PreferredBackBufferHeight / 2);

            base.Update(gameTime);
        }

        Int32 fpsTime, fpsCount, fpsView;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // FPS Cal
            fpsTime += gameTime.ElapsedGameTime.Milliseconds;
            fpsCount++;
            if (fpsTime > 1000)
            {
                fpsView = fpsCount;
                fpsTime = 0;
                fpsCount = 0;
            }

            // State Draw Switch
            switch (GhostGame.CurrentStage)
            {
                case ChangedStage.GameStage.Title:
                    // TODO: Drawing Title Preview code.
                    DrawBatch.Begin();
                    DrawBatch.Draw(TitlePlayer.GetTexture(), new Rectangle(0, 0, VideoLogo.Width, VideoLogo.Height), Color.White);
                    DrawBatch.End();
                    break;
                // End case GameStage.Title :
                case ChangedStage.GameStage.Loading:
                case ChangedStage.GameStage.MainMenu:
                    DrawBatch.Begin();
                    DrawBatch.Draw(lv0Ground, new Vector2(-500, 0), Color.White);
                    DrawBatch.Draw(lv1Ground, new Vector2(-200, 600), Color.White);
                    if (GhostGame.CurrentStage == ChangedStage.GameStage.Loading)
                    {
                        if (ReportProgress != "Start Game")
                            DrawBatch.DrawString(MainMenuFont, ReportProgress, new Vector2(50, 480), Color.FromNonPremultiplied(0, 0, 0, (Byte)MathHelper.Clamp(AlphaObject, 0, 255)));                        
                        else DrawBatch.DrawString(MainMenuFont, ReportProgress, new Vector2(50, 480), Color.Black);                        
                    }
                    else
                    {

                    }
                    DrawBatch.End();
                    break;
                // End case GameStage.MainMenu:
                case ChangedStage.GameStage.Playing:
                    if (GhostGame.PauseGame == ChangedStage.PauseStage.Playing)
                    {
                        switch (GhostGame.PlayMission)
                        {
                            case ChangedStage.MissionStage.Mission1:
                                 
                                break;
                            case ChangedStage.MissionStage.Mission2:

                                break;
                            case ChangedStage.MissionStage.Mission3:

                                break;
                            case ChangedStage.MissionStage.Boss1:

                                break;
                        }
                        break;
                    }
                    else if (GhostGame.PauseGame == ChangedStage.PauseStage.Pause)
                    {

                    }
                    break;
                // End case GameStage.Playing:
            }
           
            MouseState CurrentMouse = Mouse.GetState();
            DrawBatch.Begin();
            DrawBatch.DrawString(FPSFont, "FPS: " + fpsView, new Vector2(10, 10), Color.DarkRed);
            DrawBatch.DrawString(FPSFont, "State: " + ReportProgress, new Vector2(10, 30), Color.DarkRed);

            // TODO: Drawing Mouse code
            DrawBatch.Draw(MousePointer, new Vector2(CurrentMouse.X, CurrentMouse.Y), Color.White);
            if (GhostGame.CurrentStage != ChangedStage.GameStage.Title && GhostGame.CurrentStage != ChangedStage.GameStage.Playing)
            {
                DrawBatch.Draw(MouseImage, new Vector2(CurrentMouse.X, CurrentMouse.Y), Color.White);
            }
            else if (GhostGame.CurrentStage == ChangedStage.GameStage.Playing)
            {

            }
            DrawBatch.End();

            // Fade State
            GhostGame.DrawFade(gameTime);
            base.Draw(gameTime);
        }

    }
}
