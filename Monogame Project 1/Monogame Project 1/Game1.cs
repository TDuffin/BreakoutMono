using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_Project_1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D plrSprite;
        Texture2D ballSprite;

        float w, h;
        
        Ball ball;
        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            w = (float)GraphicsDevice.Viewport.Width;
            h = (float)GraphicsDevice.Viewport.Height;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            plrSprite = Content.Load<Texture2D>("Paddle");
            ballSprite = Content.Load<Texture2D>("Player");

            // Using ClassMode to ignore old code (but I want to make sure new code works!)
            ball = new Ball(new Vector2(w/2, h/2), new Vector2(-400, -300), ballSprite);
            player = new Player(new Vector2(30, h / 2), plrSprite);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Get the keyboad state
            KeyboardState kstate = Keyboard.GetState();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Update(dt);

            // Update based on gametime
            ball.axis = 0;
            ball.Update(dt);

            // Ball Collision X code
            if (ball.Position.X < 0)
            {
                ball.Position.X = w - ball.Width;
            }

            if (ball.Position.X + ball.Width > w)
            {
                ball.Velocity.X *= -1;
            }

            if (ball.CollidingWith(player))
            {
                ball.Velocity.X *= -1;
                ball.Position.X = player.Position.X + player.Width;
            }        

            // Move in y axis then check for cols again
            ball.axis = 1;
            ball.Update(dt);

            if (ball.CollidingWith(player))
            {
               
                if (ball.Velocity.Y < 0)
                { ball.Position.Y = player.Position.Y + player.Height; }
                else
                { ball.Position.Y = player.Position.Y - ball.Height; }

                ball.Velocity.Y *= -1;
            }

            if (ball.Position.Y <= 0 || ball.Position.Y + ball.Height >= h)
            { ball.Velocity.Y *= -1; }

            if (player.Position.Y + player.Height > h)
            {
                player.Position.Y = h - player.Height;
            }
            else if (player.Position.Y < 0)
            {
                player.Position.Y = 0;
            }



            //System.Diagnostics.Debug.WriteLine(dt);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Draw a new player
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteBatch.End();
           

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
