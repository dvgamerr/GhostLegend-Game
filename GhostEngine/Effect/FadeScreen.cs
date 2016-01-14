using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GhostEngine.Effect
{
    public class FadeScreen
    {
        private float FPS;
        private float TotalFrame;
        private int mAlphaValue;
        private int mFadeIncrement;
        private double mFadeDelay;
        private FadeStage mFadeState;

        private enum FadeStage
        {
            FadeIn, FadeOut, FadeStop
        }

        public FadeScreen()
        {
            mAlphaValue = 0;
            mFadeIncrement = 40;
            mFadeDelay = .035;
            mFadeState = FadeStage.FadeStop;
        }

        public Boolean FadePlaying
        {
            get { if (mFadeState != FadeStage.FadeStop) { return true; } else { return false; } }
        }

        public int ValueAlpha
        {
            get { return mAlphaValue; }
            set { mAlphaValue = value; }
        }

        public void ScreenFadein(GameTime gameTime, int fadeIncrement, int alpha)
        {
            mFadeIncrement = fadeIncrement;
            mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            if (mFadeDelay <= 0 && mAlphaValue <= alpha)
            {
                mFadeDelay = .035;
                mAlphaValue += mFadeIncrement;
                mFadeState = FadeStage.FadeIn;
            }
            if (mAlphaValue > alpha)
                mFadeState = FadeStage.FadeStop;
        }

        public void ScreenFadeOut(GameTime gameTime, int fadeIncrement, int alpha)
        {
            mFadeIncrement = fadeIncrement;
            mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            if (mFadeDelay <= 0 && mAlphaValue >= alpha)
            {
                mFadeDelay = .035;
                mAlphaValue -= mFadeIncrement;
                mFadeState = FadeStage.FadeOut;
            }
            if (mAlphaValue < alpha)
                mFadeState = FadeStage.FadeStop;
        }

        public float FramePerSecond(GameTime gameTime)
        {
            if ((float)gameTime.TotalGameTime.TotalMilliseconds % 1000 < 1)
            {
                FPS = (int)TotalFrame;
                TotalFrame = 0;
            }
            TotalFrame += 1;
            return FPS;
        }
    }
}
