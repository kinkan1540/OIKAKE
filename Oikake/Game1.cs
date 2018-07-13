﻿// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Oikake.Actor;
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene;
using Oikake.Util;
using System.Collections.Generic;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace Oikake
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDevice;//グラフィックスデバイスを管理するオブジェクト
        private Renderer renderer;
        private SceneManager sceneManager;
        private GameDevice gameDevice;
       
        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDevice = new GraphicsDeviceManager(this);
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";
            graphicsDevice.PreferredBackBufferWidth = Screen.Width;
            graphicsDevice.PreferredBackBufferHeight = Screen.Height;

            Window.Title = "おいかけ";
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            gameDevice = GameDevice.Instance(Content, GraphicsDevice);
            // この下にロジックを記述
            sceneManager = new SceneManager();
            sceneManager.Add(Scene.Scene.Title, new ScenFader(new Title()));
            IScene addScene = new GamePlay();
            sceneManager.Add(Scene.Scene.GamePlay, addScene);
            sceneManager.Add(Scene.Scene.Ending, new ScenFader(new Ending( addScene)));
            sceneManager.Change(Scene.Scene.Title);

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()

        {
            renderer = new Renderer(Content, GraphicsDevice);
            renderer = gameDevice.GetRenderer();

            // この下にロジックを記述
            renderer.LoadContent("black");
            renderer.LoadContent("ending");
            renderer.LoadContent("number");
            renderer.LoadContent("score");
            renderer.LoadContent("stage");
            renderer.LoadContent("timer");
            renderer.LoadContent("title");
            renderer.LoadContent("white");
            renderer.LoadContent("pipo-btleffect");
            renderer.LoadContent("oikake_enemy_4anime");
            renderer.LoadContent("oikake_player_4anime");
            renderer.LoadContent("puddle");

            //1ピクセル黒画像の生成
            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] colors = new Color[1 * 1];
            colors[0] = new Color(0, 0, 0);
            fade.SetData(colors);
            renderer.LoadContent("fade",fade);

            Sound sound = gameDevice.GetSound();
            string filepach = "./Sound/";
            sound.LoadBGM("titlebgm", filepach);
            sound.LoadBGM("gameplaybgm", filepach);
            sound.LoadBGM("endingbgm", filepach);
            sound.LoadSE("titlese", filepach);
            sound.LoadSE("gameplayse", filepach);
            sound.LoadSE("endingse", filepach);
            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                    (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            gameDevice.Update(gameTime);
            sceneManager.Update(gameTime);

            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！

        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // この下に描画ロジックを記述
            sceneManager.Draw(renderer);
            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
    }
}
