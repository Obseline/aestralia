using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AestraliaFrontend.Components;

namespace AestraliaFrontend.Screens {
    public class MainMenuScreen : GameScreen
    {
        private SpriteFont _font;
        private Texture2D _buttonTexture;
        private MouseState _prevMouse;
        
        private Button _pingButton;
        private Button _optionsButton;
        private Button _exitButton;

        public MainMenuScreen(Game1 game) : base(game) { }

        public override void LoadContent()
        {
            _font = Game.DefaultFont;

            _buttonTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            _buttonTexture.SetData(new[] { Color.White });

            int buttonWidth = 300;
            int buttonHeight = 80;
            Point center = Game.Window.ClientBounds.Center;

            _pingButton = new Button(
                _font,
                _buttonTexture,
                "Ping server",
                new Rectangle(center.X - buttonWidth / 2, (int)(Game.Window.ClientBounds.Height * 0.5f) - buttonHeight / 2, buttonWidth, buttonHeight)
            );

            _optionsButton = new Button(
                _font,
                _buttonTexture,
                "Options",
                new Rectangle(center.X - buttonWidth / 2, (int)(Game.Window.ClientBounds.Height * 0.6f) - buttonHeight / 2, buttonWidth, buttonHeight)
            );

            _exitButton = new Button(
                _font,
                _buttonTexture,
                "Exit",
                new Rectangle(center.X - buttonWidth / 2, (int)(Game.Window.ClientBounds.Height * 0.7f) - buttonHeight / 2, buttonWidth, buttonHeight)
            );
        }

        public async override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            _pingButton.Update(mouse);
            _optionsButton.Update(mouse);
            _exitButton.Update(mouse);

            if (_pingButton.IsClicked(mouse, _prevMouse))
                await Game.Network.SendMessageAsync("ping");

            if (_optionsButton.IsClicked(mouse, _prevMouse))
                Game.SetScreen(new OptionsScreen(Game));

            if (_exitButton.IsClicked(mouse, _prevMouse))
                Game.Exit();

            _prevMouse = mouse;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _pingButton.Draw(spriteBatch);
            _optionsButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}