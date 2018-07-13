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
using Oikake.Util;
namespace Oikake.Actor
{

    class Player : Character
    {
        Vector2 velocity = Vector2.Zero;
        private Sound sound;
        private Motion motion;

        /// <summary>
        /// 向き
        /// </summary>
        private enum Direction
        {
            DOWN,UO,RIHT,LEFT
        };
        private Direction direction;//現在の向き
        //向きと範囲を管理
        private Dictionary<Direction, Range> directionRange;

        /// <summary>
        /// モーションの変更
        /// </summary>
        /// <param name="direction">変更したい向き</param>
        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
            motion.Initialize(directionRange[direction], new CountDownTimer(0.2f));
        }

        private void UpdateMotion()
        {
            //キー入力の状態を取得
            Vector2 velocity = Input.Velocity();

            //キー入力がなければ何もしない
            if(velocity.Length()<=0.0f)
            {
                return;
            }
            //キーが入力あったとき
            //下向きに変更
            if((velocity.Y>0.0f)&&(direction!=Direction.DOWN))
            {
                ChangeMotion(Direction.DOWN);
            }
            //上向きに変更
            else if ((velocity.Y < 0.0f) && (direction != Direction.UO))
            {
                ChangeMotion(Direction.UO);
            }
            //右向きに変更
            else if((velocity.X>0.0f)&&(direction!=Direction.RIHT))
            {
                ChangeMotion(Direction.RIHT);
            }
            //左向きに変更
            else if((velocity.X<0.0f)&&(direction!=Direction.LEFT))
            {
                ChangeMotion(Direction.LEFT);
            }
        }

        public Player(IGameMediator mediator)
            : base("oikake_player_4anime", mediator)
        {
            position = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        /// <summary>
        /// CharacterクラスのDrawメソッドに代わって描画
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(Renderer renderer)
        {
            //base.Draw(renderer);//親クラスで描画
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }

        public override void Initialize()
        {
            position = new Vector2(300, 400);

            motion = new Motion();
            //下向き
            for (int i = 0; i < 4; i++)
            {
                motion.Add(i, new Rectangle(64 *(i%4), 64 *(i/4), 64, 64));
            }            
            //上向き
            for(int i=4;i<8;i++)
            motion.Add(i, new Rectangle(64 *(i%4), 64 *(i/4), 64, 64));

            //右向き
            for(int i=8;i<12;i++)
            {
                motion.Add(i, new Rectangle(64 * (i%4), 64 * (i/4), 64, 64));
            }
            //左向き
            for (int i = 12; i < 16; i++)
            {
                motion.Add(i, new Rectangle(64 * (i%4), 64 * (i/4), 64, 64));
            }
            //下向きの画像の範囲0～3と切り替え時間を設定
            motion.Initialize(new Range(0, 15), new CountDownTimer(0.2f));
            //最初は下向きに
            direction = Direction.DOWN;
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.DOWN,new Range(0,3) },
                {Direction.UO,new Range(4,7) },
                {Direction.RIHT,new Range(8,11) },
                {Direction.LEFT,new Range(12,15) }
            };

        }
        public override void Update(GameTime gameTime)
        {
            //キー入力の移動量を取得
            Vector2 velocity = Input.Velocity();

            //移動処理
            float speed = 10.0f;
            position = position + Input.Velocity() * speed;

            if (position.X < 0.0f)
            {
                position.X = 0.0f;
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

            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

            UpdateMotion();
            motion.Update(gameTime);

            if(Input.GetKeyTrigger(Keys.Z))
            {
                //上下左右キーが押されていなければその向きに移動量を決定
                if(velocity.Length()<0)
                {
                    Dictionary<Direction, Vector2> velocityDict = new Dictionary<Direction, Vector2>()
                    {
                        {Direction.LEFT,new Vector2(-1,0) },
                        {Direction.RIHT,new Vector2(1,0) },
                        {Direction.UO,new Vector2(0,-1) },
                        {Direction.DOWN,new Vector2(0,1) },
                    };
                    velocity = velocityDict[direction];

                }
                //弾を発射
                mediator.AddActor(
                    new PlayerBullet(
                        position,
                        mediator,
                        velocity
                        ));
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
