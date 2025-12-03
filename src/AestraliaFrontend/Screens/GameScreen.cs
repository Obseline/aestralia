using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AestraliaFrontend.Screens {
    public abstract class GameScreen
    {
        protected Game1 Game;

        protected GameScreen(Game1 game)
        {
            Game = game;
        }

        public virtual void LoadContent() { }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
