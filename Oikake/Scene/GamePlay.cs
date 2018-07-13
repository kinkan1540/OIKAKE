using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Util;
using Oikake.Def;
using Microsoft.Xna.Framework.Input;

namespace Oikake.Scene
{
    class GamePlay : IScene,IGameMediator
    {
        private CharacterManager characterManager;
        private List<Bullet> bullets;
        private Score score;
        private Timer timer;
        private TimerUI timerUI;
    
        private bool IsEndFlag;
        private Sound sound;
     
        public GamePlay()
        {
            IsEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Draw(Renderer renderer)
        {   
            renderer.Begin();
            renderer.DrawTexture("stage", Vector2.Zero);
            characterManager.Draw(renderer);
            foreach (var bullet in bullets)
            {
                bullet.Draw(renderer);
            }
            score.Draw(renderer);
            timerUI.Draw(renderer);
            //if (timer.IsTime() == true)
            //{ renderer.DrawTexture("ending", new Vector2(150, 150)); 
            renderer.End();
        }

        public void Initialize()
        {

            IsEndFlag = false;

            Player player = new Player(this);
            characterManager = new CharacterManager();
            characterManager.Initialize();
            characterManager.Add(new Player(this));
            characterManager.Add(new BoundEnemy(this));
            characterManager.Add(new Enemy(this, new BoudAI()));
            characterManager.Add(player);
            characterManager.Add(new Enemy(this, new AttackAI(player)));
            for (int i = 0; i < 10; i++)
            {
                characterManager.Add(new RandomEnemy(this));
            }
            bullets = new List<Bullet>();
            score = new Score();
            timer = new CountDownTimer(7);
            timerUI = new TimerUI(timer);
        }

        public bool IsEnd()
        {
            return IsEndFlag;
        }

        public Scene Next()
        {
            return Scene.Ending;
        }

        public void Shutdown()
        {
            
        }
       
        public void Update(GameTime gameTime)
        {
            characterManager.Update(gameTime);
            score.Update(gameTime);
            Input.Update();
            sound.PlayBGM("gameplaybgm");
          
            timer.Update(gameTime);
            if (timer.IsTime() == true)
            {
                score.Shutdown();
                IsEndFlag = true;
            }
        }

        public void AddActor(Character character)
        {
            characterManager.Add(character);
        }

        public void AddScore()
        {
            score.Add();
        }

        public void AddScore(int num)
        {
            score.Add(num);
        }
    }
}
