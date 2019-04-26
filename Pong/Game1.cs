using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteFont font;
        private SpriteFont fontbig;
        private int rightscore = 0;
        private int leftscore = 0;
        double relativeIntersectY;
        double intersectY;
        double normalizedRelativeIntersectionY;
        double bounceAngle;
        double maxbounceangle;

        int x_speed = 3;
        int y_speed = 3;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixel;
        Rectangle ball = new Rectangle(100, 100, 20, 20);
        Rectangle left_paddle = new Rectangle(10, 150, 20, 150);
        Rectangle right_paddle = new Rectangle(770, 150, 20, 150);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = Content.Load<Texture2D>("pixel");
            font = Content.Load<SpriteFont>("Scoreleft");
            fontbig = Content.Load<SpriteFont>("Scoreright");

            // TODO: use this.Content to load your game content here
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
            ball.X += x_speed;
            ball.Y += y_speed;
            if (ball.Y < 0 || ball.Y > Window.ClientBounds.Height - ball.Height)
                y_speed *= -1;
            if (ball.Intersects(left_paddle) || ball.Intersects(right_paddle))
            {
                intersectY = ball.Y-ball.Height/2;
                x_speed *= -1;
                if (x_speed > 0)
                {
                    x_speed = x_speed + 1;
                }
                else if (x_speed < 0)
                {
                    x_speed = x_speed - 1;
                }
            }
            KeyboardState kstate = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (kstate.IsKeyDown(Keys.Up))
                right_paddle.Y -= 7;
            if (kstate.IsKeyDown(Keys.Down))
                right_paddle.Y += 7;
            if (kstate.IsKeyDown(Keys.W))
                left_paddle.Y -= 7;
            if (kstate.IsKeyDown(Keys.S))
                left_paddle.Y += 7;
            if (left_paddle.Y < 0)
                left_paddle.Y = 0;
            if (left_paddle.Y > Window.ClientBounds.Height - left_paddle.Height)
                left_paddle.Y = Window.ClientBounds.Height - left_paddle.Height;
            if (right_paddle.Y < 0)
                right_paddle.Y = 0;
            if (right_paddle.Y > Window.ClientBounds.Height - right_paddle.Height)
                right_paddle.Y = Window.ClientBounds.Height - right_paddle.Height;
            if (kstate.IsKeyDown(Keys.R))
            {
                // Reset all variables
                ball.X = 150;
                ball.Y = 100;
                x_speed = 3;
                y_speed = 3;
                rightscore = 0;
                leftscore = 0;
            }
            if (ball.X > Window.ClientBounds.Width - ball.Width)
            {
                leftscore++;
            }
            if (ball.X < 0)
            {
                rightscore++;
            }
            if (ball.X < 0 || ball.X > Window.ClientBounds.Width - ball.Width)
            {
                // Reset all variables
                ball.X = 150;
                ball.Y = 100;
                x_speed = 3;
                y_speed = 3;
            }
            if (x_speed >= 22)
                x_speed = 22;
            if (ball.Intersects(left_paddle))
            {
                relativeIntersectY = (left_paddle.Height + (left_paddle.Height / 2)) - intersectY;
                normalizedRelativeIntersectionY = (relativeIntersectY / (left_paddle.Height / 2));
            }
            if (ball.Intersects(right_paddle))
            {
                relativeIntersectY = (right_paddle.Height + (right_paddle.Height / 2)) - intersectY;
                normalizedRelativeIntersectionY = (relativeIntersectY / (right_paddle.Height / 2));
            }
          //  bounceAngle = normalizedRelativeIntersectionY * MAXBOUNCEANGLE;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(pixel, ball, Color.Green);
            spriteBatch.Draw(pixel, left_paddle, Color.Green);
            spriteBatch.Draw(pixel, right_paddle, new Color(255,255,0));
            spriteBatch.DrawString(font, "Score: " + leftscore, new Vector2(200, 60), Color.Green);
            spriteBatch.DrawString(font, "Score: " + rightscore, new Vector2(520, 60), Color.Yellow);
            if (leftscore >= 5)
                spriteBatch.DrawString(fontbig, "Left player won", new Vector2(305, 230), Color.Green);
            else if (rightscore >= 5)
                spriteBatch.DrawString(fontbig, "Right player won", new Vector2(305, 230), Color.Yellow);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
