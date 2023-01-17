using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Connect4
{
    public class Game1 : Game
    {
        KeyboardState ks1, ks2;

        Texture2D redTexture, yellowTexture, spaceTexture;

        List<Vector2> redVector2s = new List<Vector2>();
        List<Vector2> yellowVector2s = new List<Vector2>();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont turnFont;

        int width = 700;
        int height = 700;
        float playerx = 0;
        float playery = 0;
        string turn = "red";
        int spacesUsed;
        int redWin, yellowWin, Ties;

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
            turnFont = Content.Load<SpriteFont>("Turn");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ks1 = Keyboard.GetState();

            if (ks1.IsKeyDown(Keys.Down) && ks2.IsKeyUp(Keys.Down) && turn == "red")
            {
                playery = 600;
                while (redVector2s.Contains(new Vector2(playerx, playery)) || yellowVector2s.Contains(new Vector2(playerx, playery)))
                {
                    playery -= 100;
                }
                if (playery >= 100)
                {
                    redVector2s.Add(new Vector2(playerx, playery));
                    spacesUsed++;
                    turn = "yellow";
                }
                playery = 0;
            }
            else if (ks1.IsKeyDown(Keys.Down) && ks2.IsKeyUp(Keys.Down) && turn == "yellow")
            {
                playery = 600;
                while (redVector2s.Contains(new Vector2(playerx, playery)) || yellowVector2s.Contains(new Vector2(playerx, playery)))
                {
                    playery -= 100;
                }
                if (playery >= 100)
                {
                    yellowVector2s.Add(new Vector2(playerx, playery));
                    spacesUsed++;
                    turn = "red";
                }
                playery = 0;
            }

            if (ks1.IsKeyDown(Keys.Left) && ks2.IsKeyUp(Keys.Left) && playerx >= 100)
            {
                playerx -= 100;
            }
            if (ks1.IsKeyDown(Keys.Right) && ks2.IsKeyUp(Keys.Right) && playerx <= 500)
            {
                playerx += 100;
            }

            ks2 = ks1;

            // Credit to Pixi91 for the fix on constant push of keys found here -> https://community.monogame.net/t/delay-after-keyboard-input/10999/2

            base.Update(gameTime);
            // Tie/Stalemate
            if (spacesUsed >= 42)
            {
                Ties++;
                redVector2s.Clear();
                yellowVector2s.Clear();
                spacesUsed = 0;
            }
            // win check
            CheckForWin();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

           

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceTexture, new Vector2(0, 100), Color.White);

            if (turn == "red")
            {
                _spriteBatch.Draw(redTexture, new Vector2(playerx, playery), Color.White);
            }
            else if (turn == "yellow")
            {
                _spriteBatch.Draw(yellowTexture, new Vector2(playerx, playery), Color.White);
            }

            foreach (Vector2 r in redVector2s)
            {
                _spriteBatch.Draw(redTexture, r, Color.White);
            }
            foreach (Vector2 y in yellowVector2s)
            {
                _spriteBatch.Draw(yellowTexture, y, Color.White);
            }
            _spriteBatch.DrawString(turnFont, "Red wins: " + redWin, new Vector2(100, 0), Color.Black);
            _spriteBatch.DrawString(turnFont, "Ties: " + Ties, new Vector2(300, 0), Color.Black);
            _spriteBatch.DrawString(turnFont, "Yellow wins: " + yellowWin, new Vector2(500, 0), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool CheckForWin()
        {
            // Check horizontal win
            for (int y = 600; y >= 100; y -= 100)
            {
                for (int x = 0; x <= 400; x += 100)
                {
                    if (redVector2s.Contains(new Vector2(x, y)) && redVector2s.Contains(new Vector2(x + 100, y)) && redVector2s.Contains(new Vector2(x + 200, y)) && redVector2s.Contains(new Vector2(x + 300, y)))
                    { 
                        redWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                    if (yellowVector2s.Contains(new Vector2(x, y)) && yellowVector2s.Contains(new Vector2(x + 100, y)) && yellowVector2s.Contains(new Vector2(x + 200, y)) && yellowVector2s.Contains(new Vector2(x + 300, y)))
                    {
                        yellowWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                }
            }
            // Check vertical win
            for (int x = 0; x <= 600; x += 100)
            {
                for (int y = 600; y >= 100; y -= 100)
                {
                    if (redVector2s.Contains(new Vector2(x, y)) && redVector2s.Contains(new Vector2(x, y - 100)) && redVector2s.Contains(new Vector2(x, y - 200)) && redVector2s.Contains(new Vector2(x, y - 300)))
                    {
                        redWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                    if (yellowVector2s.Contains(new Vector2(x, y)) && yellowVector2s.Contains(new Vector2(x, y - 100)) && yellowVector2s.Contains(new Vector2(x, y - 200)) && yellowVector2s.Contains(new Vector2(x, y - 300)))
                    {
                        yellowWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                }
            }
            // Check diagonal win (top-left to bottom-right)
            for (int x = 0; x <= 400; x += 100)
            {
                for (int y = 600; y >= 300; y -= 100)
                {
                    if (redVector2s.Contains(new Vector2(x, y)) && redVector2s.Contains(new Vector2(x + 100, y - 100)) && redVector2s.Contains(new Vector2(x + 200, y - 200)) && redVector2s.Contains(new Vector2(x + 300, y - 300)))
                    {
                        redWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                    if (yellowVector2s.Contains(new Vector2(x, y)) && yellowVector2s.Contains(new Vector2(x + 100, y - 100)) && yellowVector2s.Contains(new Vector2(x + 200, y - 200)) && yellowVector2s.Contains(new Vector2(x + 300, y - 300)))
                    {
                        yellowWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                }
            }
            // Check diagonal win (top-right to bottom-left)
            for (int x = 600; x >= 200; x -= 100)
            {
                for (int y = 600; y >= 300; y -= 100)
                {
                    if (redVector2s.Contains(new Vector2(x, y)) && redVector2s.Contains(new Vector2(x - 100, y - 100)) && redVector2s.Contains(new Vector2(x - 200, y - 200)) && redVector2s.Contains(new Vector2(x - 300, y - 300)))
                    {
                        redWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                    if (yellowVector2s.Contains(new Vector2(x, y)) && yellowVector2s.Contains(new Vector2(x - 100, y - 100)) && yellowVector2s.Contains(new Vector2(x - 200, y - 200)) && yellowVector2s.Contains(new Vector2(x - 300, y - 300)))
                    {
                        yellowWin++;
                        redVector2s.Clear();
                        yellowVector2s.Clear();
                        spacesUsed = 0;
                    }
                }
            }
            return false;
        }

    }
}