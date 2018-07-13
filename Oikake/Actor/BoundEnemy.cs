using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Def;
using Oikake.Scene;
namespace Oikake.Actor
{
    class BoundEnemy:Character
    {
        private Vector2 velocity;
        private static Random rnd = new Random();
        public BoundEnemy(IGameMediator mediator)
            :base("black",mediator)
        {
            velocity = Vector2.Zero;
        }
        public override void Initialize()
        {
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
           velocity = new Vector2(rnd.Next(40,80), rnd.Next(40,80));
        }
        public override void Shutdown()
        {
            
        }
        public override void Hit(Character other)
        {
            isDeadFlag = true;
            
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddScore(100);
            mediator.AddActor(new BurstEffect(position, mediator));
        }
        public override void Update(GameTime gameTime)
        {
            if(position.X<Screen.Null)
            {
                velocity.X *= -1;
            }
            else if(position.X>Screen.Width-64)
            {
                velocity.X *= -1;
            }
            if(position.Y<Screen.Null)
            {
                velocity.Y *= -1;
            }
            else if(position.Y>Screen.Height-64)
            {
                velocity.Y *= -1;
            }
            position += velocity;
        }
    }
  

}
