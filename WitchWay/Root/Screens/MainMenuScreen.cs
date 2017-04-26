using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WitchWay
{
    class MainMenuScreen : BaseScreen
        //MainMenuScreen class inherits properties from the Basescreen class
    {
        private Sprite m_texture;
        Button playButton;
        Button quitButton;
        Button helpButton;

        PumpkinModel pumpkin;

        public MainMenuScreen(Game1 game) : base(game)
        {
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            m_texture = new Sprite();
            m_texture.Load(content, new Vector2(0, 0), "mainMenu");

            playButton = new Button();
            playButton.Load(content, new Vector2(800, 400), "playButton", "playButtonHighlight");

            quitButton = new Button();
            quitButton.Load(content, new Vector2(800, 600), "quitButton", "quitButtonHighlight");

            helpButton = new Button();
            helpButton.Load(content, new Vector2(800,500), "helpButton", "helpButtonHighlight");

            pumpkin = new PumpkinModel(content);

        }

        public override void Update(GameTime gameTime)
        {
            bool enterPressed = Input.isDown(Keys.Enter);
            if (enterPressed || playButton.IsClicked())
            {
                Game.ScreenMgr.Switch(new GameScreen(Game));
            }
            if(quitButton.IsClicked())
            {
                Game.Exit();
            }
            if (helpButton.IsClicked())
            {
                Game.ScreenMgr.Switch(new HelpScreen(Game));
            }

            pumpkin.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            //m_texture.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
            helpButton.Draw(spriteBatch);

            pumpkin.Draw(Game.GraphicsDevice);
        }

    }
}
