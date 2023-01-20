using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    public class Menu : Game
    {
        KeyboardState ks1, ks2;

        Texture2D menu;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont menuFont;

        int width = 700;
        int height = 800;

        public Menu()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();
            this.Window.Title = "Connect 4";
            this.Window.AllowUserResizing = false;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            menu = Content.Load<Texture2D>("Menu");
            menuFont = Content.Load<SpriteFont>("MenuFont");
        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.Enter) && ks2.IsKeyUp(Keys.Enter))
            {
                using var game = new Connect4.Game1();
                Exit();
                game.Run();
            }

            ks2 = ks1;

            // Credit to Pixi91 for the fix on constant push of keys found here -> https://community.monogame.net/t/delay-after-keyboard-input/10999/2

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);



            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(menu, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(menuFont, "Press ENTER to play", new Vector2(225, 300), Color.Yellow);
            _spriteBatch.DrawString(menuFont, "Press ESCAPE to exit", new Vector2(225, 400), Color.Red);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
