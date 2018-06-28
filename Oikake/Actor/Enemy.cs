using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Device;
using Oikake.Actor;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Oikake.Scene;
namespace Oikake.Actor
{
    class Enemy :Character

    {
        //private Vector2 position;
        public Enemy(IGameMediator mediator)
            :base("black",mediator)
        {
            position = Vector2.Zero;
        }
        public override void Initialize()
        {
            position = new Vector2(100, 100);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Hit(Character other)
        {
            isDeadFlag = true;
            
            //mediator.AddActor(new Enemy(mediator));
            //mediator.AddActor(new Enemy(mediator));
            mediator.AddScore(1);
            //mediator.AddActor(new BurstEffect(position, mediator));
        }
        //public void Draw(Renderer renderer)
        //{
        //    renderer.DrawTexture("black", position);
        //}
        public override void Shutdown()
        { }

        
    }
}
