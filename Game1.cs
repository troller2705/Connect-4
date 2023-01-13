using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Connect4
{
    public class Game1 : Game
    {
        Texture2D redTexture, yellowTexture, spaceTexture;

        int[] spacesx = {0, 100, 200, 300, 400, 500, 600, 700};
        int[] spacesy = {0, 100, 200, 300, 400, 500, 600};

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int width = 700;
        int height = 700;
        float playerx = 0;
        float playery = 0;

        public Game1()
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
            redTexture = Content.Load<Texture2D>("Red");
            yellowTexture = Content.Load<Texture2D>("Yellow");
            spaceTexture = Content.Load<Texture2D>("Board");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && playerx >= 5)
            {
                playerx -= 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && playerx <= 600)
            {
                playerx += 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceTexture, new Vector2(0, 100), Color.White);
            _spriteBatch.Draw(redTexture, new Vector2(playerx, playery), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}