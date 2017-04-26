using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitchWay
{
    class HelpScreen : BaseScreen
    {
        private Sprite m_texture;

        Button mainMenuButton;

        public HelpScreen(Game1 game) : base(game)
        {
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            m_texture = new Sprite();
            m_texture.Load(content, new Vector2(0, 0), "helpScreen");

            mainMenuButton = new Button();
            mainMenuButton.Load(content, new Vector2(0, 600), "mainMenuButton", "mainMenuButtonHighlight");
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            m_texture.Draw(spriteBatch);
            mainMenuButton.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (mainMenuButton.IsClicked())
            {
                Game.ScreenMgr.Switch(new MainMenuScreen(Game));
            }
        }
    }
}
