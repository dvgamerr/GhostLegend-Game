using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GhostEngine.Component
{
    public class GameStage
    {
        protected Dictionary<String, Boolean> StageList;
        protected String CurrentStage;

        public GameStage(String[] isStage, String name)
        {
            StageList = new Dictionary<String, Boolean>();
            foreach (String isValue in isStage)
            {
                StageList.Add(isValue, false);
            }
            CurrentStage = name;
            StageList[name] = true;
        }

        public string IndexOf(int list)
        {
            int iIndex = 0;
            foreach (KeyValuePair<String, Boolean> isValue in StageList)
            {
                if (iIndex == list)
                {
                    CurrentStage = isValue.Key;
                }
                iIndex++;
            }
            return CurrentStage;
        }

        public Boolean StageOf(String name)
        {
            Boolean findStage = false;
            foreach (KeyValuePair<String, Boolean> isValue in StageList)
            {
                if (isValue.Key == name)
                    findStage = true;
            }
            return findStage;
        }

        public void StageNext(String name)
        {
            if (StageList.ContainsKey(name))
            {
                StageList[name] = true;
                StageList[CurrentStage] = false;
                CurrentStage = name;
            }
        }


        public string InStage
        {
            get
            {
                foreach (KeyValuePair<String, Boolean> isValue in StageList)
                {
                    if (StageList[isValue.Key])
                    {
                        CurrentStage = isValue.Key;
                    }
                }
                return CurrentStage;
            }
        }

    }
}
