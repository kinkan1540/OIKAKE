using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oikake.Device;
namespace Oikake.Scene
{
    class Title : IScene
    {
        private bool isEndFrag;
        private Sound sound;
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
            renderer.End();
        }

        public void Initialize()
        {
            isEndFrag = false;
            //sound.PlaySE("titlese");
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
            

            sound.PlayBGM("titlebgm");
            if(Input.GetKeyTrigger(Keys.Space))
            {
                sound.PlaySE("titlese");
                isEndFrag = true;
            }

        }
    }
}
