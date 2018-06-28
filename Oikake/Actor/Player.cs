using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Device;
using Oikake.Actor;
using Oikake.Def;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Oikake.Scene;
namespace Oikake.Actor
{

    class Player : Character
    {
        Vector2 velocity = Vector2.Zero;
        private Sound sound;
        //private Vector2 position;
        public Player(IGameMediator mediator)
            : base("white",mediator)
        {
            position = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public override void Initialize()
        {
            position = new Vector2(300, 400);
        }
        public override void Update(GameTime gameTime)
        {
            velocity = Vector2.Zero;
            if (Input.GetKeyState(Keys.Right))
            {
                velocity.X = 1f;
            }
            if (Input.GetKeyState(Keys.Left))
            {
                velocity.X = -1f;
            }
            if (Input.GetKeyState(Keys.Up))
            {
                velocity.Y = -1f;
            }
            if (Input.GetKeyState(Keys.Down))
            {
                velocity.Y = 1f;
            }
            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }


            float speed = 5.0f;
            position = position + velocity * speed;

            if (position.X < Screen.Null)
            {
                position.X = Screen.Null;
            }
            if (position.X >= Screen.Width - 64)
            {
                position.X = Screen.Width - 64;
            }
            if (position.Y < Screen.Null)
            {
                position.Y = Screen.Null;
            }
            if (position.Y >= Screen.Height - 64)
            {
                position.Y = Screen.Height - 64;
            }            
        }
         public override void Hit(Character other)
        {
            sound.PlaySE("gameplayse");
        }
       
             public Vector2 GetPosition()
        {
            return position;
        }
        public Vector2 GetVelocity()
        {
            return velocity;           
        }
        public override void Shutdown()
        {

        }

        
    }
}
