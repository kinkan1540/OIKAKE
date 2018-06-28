using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Scene;
using Microsoft.Xna.Framework;
using Oikake.Def;
namespace Oikake.Actor
{
    class RandomEnemy:Character
    {
        private static Random rnd = new Random();
        private int changeTimer;

        public RandomEnemy(IGameMediator mediator)
            :base("black",mediator)
        {
            changeTimer = 90;
        }

        public override void Hit(Character other)
        {
            isDeadFlag = true;
            
            mediator.AddActor(new RandomEnemy(mediator));
            mediator.AddActor(new RandomEnemy(mediator));
            mediator.AddScore(10);
            mediator.AddActor(new BurstEffect(position, mediator));
        }

        public override void Initialize()
        {
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
            changeTimer = 90 * rnd.Next(2, 5);
        }

        public override void Shutdown()
        {}

        public override void Update(GameTime gameTime)
        {
            changeTimer -= 1;
            if(changeTimer<0)
            {
                Initialize();
            }
        }
    }
}
