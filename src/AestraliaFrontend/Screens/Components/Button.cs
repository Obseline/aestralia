using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AestraliaFrontend.Components
{
    public class Button
    {
        public Rectangle Bounds;
        public string Text;
        private SpriteFont _font;
        private Texture2D _texture;
        private Color _color;
        private Color _hoverColor = Color.White;

        public bool IsHovering { get; private set; }

        public Button(SpriteFont font, Texture2D texture, string text, Rectangle bounds)
        {
            Bounds = bounds;
            Text = text;
            _font = font;
            _texture = texture;
            _color = Color.LightGray;
        }

        public void Update(MouseState mouse)
        {
            IsHovering = Bounds.Contains(mouse.Position);
        }

        public bool IsClicked(MouseState mouse, MouseState prevMouse)
        {
            return IsHovering && mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Bounds, IsHovering ? _hoverColor : _color);

            // Center the text within the button
            Vector2 textSize = _font.MeasureString(Text);
            Vector2 textPosition = new Vector2(
                Bounds.X + (Bounds.Width - textSize.X) / 2,
                Bounds.Y + (Bounds.Height - textSize.Y) / 2
            );

            spriteBatch.DrawString(_font, Text, textPosition, Color.Black);
        }
    }
}