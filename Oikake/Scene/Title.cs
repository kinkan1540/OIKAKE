﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
using Oikake.Util;
namespace Oikake.Scene
{
    class Title : IScene
    {
        private bool isEndFrag;
        private Sound sound;
        private Motion motion;

        public Title()
        {
            isEndFrag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("title", Vector2.Zero);
           renderer.DrawTexture("puddle", new Vector2(200,370),motion.DrawingRange());
            renderer.End();
        }

        public void Initialize()
        {
            isEndFrag = false;

            motion = new Motion();
            motion.Add(0, new Rectangle(64 * 0, 0, 64, 64));
            motion.Add(1, new Rectangle(64 * 1, 0, 64, 64));
            motion.Add(2, new Rectangle(64 * 2, 0, 64, 64));
            motion.Add(3, new Rectangle(64 * 3, 0, 64, 64));
            motion.Add(4, new Rectangle(64 * 4, 0, 64, 64));
            motion.Add(5, new Rectangle(64 * 5, 0, 64, 64));
            motion.Initialize(new Range(0, 5), new CountDownTimer(0.05f));

        }

        public bool IsEnd()
        {
            return isEndFrag;
        }

        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
            sound.PlayBGM("titlebgm");
            if(Input.GetKeyTrigger(Keys.Space))
            {
                sound.PlaySE("titlese");
                isEndFrag = true;
            }

        }
    }
}
