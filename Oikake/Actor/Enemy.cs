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
using Oikake.Def;
namespace Oikake.Actor
{
    class Enemy :Character

    {
        private AI ai;
        private Random rnd;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ai"></param>
        public Enemy(IGameMediator mediator, AI ai) : base("black", mediator)
        {
            this.ai = ai;
        }
        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public override void Initialize()
        {
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
        }
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public override void Update(GameTime gameTime)
        {
            //AIが決定した位置に
            position = ai.Think(this);
        }
        public override void Hit(Character other)
        {
            //得点の追加
            int score = 0;
            if(ai is BoudAI)
            {
                score = 100;
            }
            else if(ai is RandomAI)
            {
                score = 50;
            }
            else if(ai is AttackAI)
            {
                score= - 50;
                mediator.AddScore(score);
                mediator.AddActor(new Enemy(mediator, ai));
                isDeadFlag = true;
                return;
            }

            isDeadFlag = true;
            mediator.AddScore(score);

            //次のAIを決定
            AI nextAI = new BoudAI();//仮想の実装
            switch(rnd.Next(2))
            {
                case 0:
                    nextAI = new BoudAI();
                    break;
                case 1:
                    nextAI = new RandomAI();
                    break;
            } 
            mediator.AddActor(new Enemy(mediator, nextAI));

            //死亡処理
            isDeadFlag= true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }
     
        public override void Shutdown()
        { }

        
    }
}
