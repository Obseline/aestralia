using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using Gum.Forms.Controls;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;

namespace AestraliaFrontend.Scenes;

public class TitleScene : Scene
{
    private Sprite _logo;
    private Panel _titleScreenButtonsPanel;
    private Button _pingButton;
    
    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        CreateTitlePanel();
    }

    public override void Initialize()
    {
        base.Initialize();
        Core.ExitOnEscape = true;
        InitializeUI();
    }

    public override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        
        _logo = atlas.CreateSprite("logo");
        _logo.Scale = new Vector2(0.15f);
    }

    public override void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.CornflowerBlue);

        if (_titleScreenButtonsPanel.IsVisible)
        {
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _logo.Draw(Core.SpriteBatch, new Vector2(50, 0));
            Core.SpriteBatch.End();
        }

        GumService.Default.Draw();
    }

    private void CreateTitlePanel()
    {
        // Create a container to hold all of our buttons
        _titleScreenButtonsPanel = new Panel();
        _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
        _titleScreenButtonsPanel.AddToRoot();

        _pingButton = new Button();
        _pingButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _pingButton.Visual.X = 15;
        _pingButton.Visual.Y = 40;
        _pingButton.Visual.Width = 50;
        _pingButton.Text = "Ping";
        _pingButton.Click += HandlePingClicked;
        _titleScreenButtonsPanel.AddChild(_pingButton);

        _pingButton.IsFocused = true;
    }

    private async void HandlePingClicked(object sender, EventArgs e)
    {
        await Core.Network.SendMessageAsync("ping");
    }

}