using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WitchWay
{
    class GameOverScreen : BaseScreen
    {
        private Sprite m_texture;

        Button quitButton;
        Button mainMenuButton;

        public GameOverScreen(Game1 game) : base(game)
        {
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            m_texture = new Sprite();
            m_texture.Load(content, new Vector2(0, 0), "gameOverScreen");

            quitButton = new Button();
            quitButton.Load(content, new Vector2(300, 300), "quitButton", "quitButtonHighlight");

            mainMenuButton = new Button();
            mainMenuButton.Load(content, new Vector2(300, 500), "mainMenuButton", "mainMenuButtonHighlight");
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            m_texture.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
            mainMenuButton.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (quitButton.IsClicked())
            {
                Game.Exit();
            }
            if (mainMenuButton.IsClicked())
            {
                Game.ScreenMgr.Switch(new MainMenuScreen(Game));
            }
        }
    }
}
