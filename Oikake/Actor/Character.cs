using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Scene;
using Microsoft.Xna.Framework;
using Oikake.Device;
namespace Oikake.Actor
{
    /// <summary>
    /// キャラクター継承クラス
    /// </summary>
    abstract　class Character
    {
        protected Vector2 position;
        protected string name;
        protected bool isDeadFlag;
        protected IGameMediator mediator;
        public abstract void Hit(Character other);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">画像の名前</param>
        public Character(string name,IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
        }
        
        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();

        /// <summary>
        /// 死んでいるか？
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return isDeadFlag;
        }
        public bool IsCollision(Character other)
        {
            float length = (position - other.position).Length();

            float radiusSum = 32f + 32f;

            if(length<=radiusSum)
            {
                return true;
            }
            return false;
        }
        

        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
    }
}
