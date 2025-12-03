﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AestraliaFrontend.Screens;
using AestraliaFrontend.Networking;

namespace AestraliaFrontend {
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameScreen _currentScreen;

        public SpriteFont DefaultFont { get; private set; }
        public NetworkClient Network { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                HardwareModeSwitch = false
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "Aestralia";

            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            _graphics.PreferredBackBufferWidth = displayMode.Width;
            _graphics.PreferredBackBufferHeight = displayMode.Height;
            _graphics.ApplyChanges();

            Window.Position = Point.Zero;
            Window.IsBorderless = true;

            Network = new NetworkClient("ws://localhost:34000");

            Network.OnMessageReceived += msg =>
            {
                Console.WriteLine("Message reçu du serveur: " + msg);
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");

            SetScreen(new MainMenuScreen(this));
        }

        protected override void Update(GameTime gameTime)
        {
            _currentScreen?.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentScreen?.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        public void SetScreen(GameScreen screen)
        {
            _currentScreen = screen;
            _currentScreen.LoadContent();
        }
    }
}