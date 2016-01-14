using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GhostLegends
{
    public class ChangedStage
    {
        public enum GameStage { Title, Loading, MainMenu, Playing }
        public enum MissionStage { Mission1, Mission2, Mission3, Boss1 }
        public enum PauseStage { Pause, Playing }
        enum Fade { FadeIn, FadeOut }

        public GameStage CurrentStage = GameStage.Title;
        public MissionStage PlayMission = MissionStage.Mission1;
        public PauseStage PauseGame = PauseStage.Playing;
        Fade FadeScreen;
        Boolean FadeBegin;
        GameStage NextState;
        SpriteBatch DrawFadeState;
        Texture2D SoildWhite, SoildBlack, FadeTexture;
        Int32 FadeInSpeed, FadeOutSpeed;
        float FadeAlpha = 0;

        public ChangedStage(ContentManager content, SpriteBatch sprite)
        {
            FadeScreen = Fade.FadeIn;
            DrawFadeState = sprite;
            SoildWhite = content.Load<Texture2D>("Data\\SoildColorWhite");
            SoildBlack = content.Load<Texture2D>("Data\\SoildColorBlack");
        }

        // 1000,1000
        public void StateChanged(GameStage state, Int32 inSpeed, Int32 outSpeed, Color color)
        {
            FadeBegin = true;
            NextState = state;
            FadeInSpeed = inSpeed;
            FadeOutSpeed = outSpeed;
            if (color == Color.White) FadeTexture = SoildWhite;
            if (color == Color.Black) FadeTexture = SoildBlack;
        }
        public void StateChanged(MissionStage state, Int32 inSpeed, Int32 outSpeed)
        {

        }
        public void StateChanged(PauseStage state, Int32 inSpeed, Int32 outSpeed)
        {

        }

        public void DrawFade(GameTime time)
        {
            if (FadeBegin)
            {
                if (FadeAlpha <= 0) FadeScreen = Fade.FadeIn;
                if (FadeAlpha >= 255)
                {
                    FadeScreen = Fade.FadeOut;
                    CurrentStage = NextState;
                }

                if (FadeInSpeed < time.ElapsedGameTime.Milliseconds && FadeOutSpeed < time.ElapsedGameTime.Milliseconds)
                {
                    FadeAlpha = -1;
                    CurrentStage = NextState;
                }
                else
                {
                    if (FadeScreen == Fade.FadeIn)
                    {
                        if (FadeInSpeed < time.ElapsedGameTime.Milliseconds) FadeAlpha = 256;
                        else FadeAlpha += (255 / (FadeInSpeed / (float)time.ElapsedGameTime.Milliseconds));
                    }
                    if (FadeScreen == Fade.FadeOut)
                    {
                        if (FadeOutSpeed < time.ElapsedGameTime.Milliseconds) FadeAlpha = -1;
                        else FadeAlpha -= (255 / (FadeOutSpeed / (float)time.ElapsedGameTime.Milliseconds));
                    }
                }

                DrawFadeState.Begin();
                DrawFadeState.Draw(FadeTexture, new Rectangle(0, 0, 1280, 720), Color.FromNonPremultiplied(255, 255, 255, (Byte)MathHelper.Clamp(FadeAlpha, 0, 255)));
                DrawFadeState.End();
                if (FadeAlpha < 0)
                {
                    FadeBegin = false;
                    FadeAlpha = 0;
                }
            }
        }
    }
}
