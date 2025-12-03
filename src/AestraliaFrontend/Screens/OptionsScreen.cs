using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AestraliaFrontend.Screens {
    public class OptionsScreen : GameScreen
    {
        private SpriteFont _font;
        private float _time;
        private MouseState _prevMouse;
        private string _message;
        private Vector2 _messageSize;

        public OptionsScreen(Game1 game) : base(game) { }

        public override void LoadContent() {
            _font = Game.DefaultFont;
            _message = "Press ESC to return";
            _messageSize = _font.MeasureString(_message);
        }

        public override void Update(GameTime gameTime) {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboard.IsKeyDown(Keys.Escape))
                Game.SetScreen(new MainMenuScreen(Game));
            
            _prevMouse = mouse;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();

            // Draw pulsating message at the bottom center of the screen
            float scale = 1.0f + 0.05f * (float)Math.Sin(_time * 3f);
            int screenWidth = Game.GraphicsDevice.Viewport.Width;
            int screenHeight = Game.GraphicsDevice.Viewport.Height;
            Vector2 position = new Vector2(
                (screenWidth - _messageSize.X * scale) / 2,
                screenHeight * 0.9f - (_messageSize.Y * scale) / 2
            );
            spriteBatch.DrawString(_font, _message, position, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.End();
        }
    }
}