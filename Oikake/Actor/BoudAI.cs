
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Util;
using Oikake.Def;
namespace Oikake.Actor
{
    class BoudAI : AI
    {
        private Vector2 velocity;//移動量

        public BoudAI()
        {
            var gameDevice = GameDevice.Instance();
            var rnd = gameDevice.GetRandom();
            int speed = rnd.Next(5, 21);
            speed = (rnd.Next(2) == 0) ? (speed) : (-speed);
            velocity = new Vector2(speed, 0.0f);
        }
          
        public override Vector2 Think(Character character)
        {
            //キャラクターの座標位置の取得
            character.SetPosition(ref position);
            position = position + velocity;
            Range range = new Range(0, Screen.Width - 64);
            if (range.IsOutOFRange((int)position.X)) 
            {
                velocity = -velocity;
            }
            return position;
        }
    }
}
