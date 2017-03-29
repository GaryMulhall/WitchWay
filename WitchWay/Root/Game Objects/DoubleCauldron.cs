using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WitchWay
{
    class DoubleCauldron : MoveableSprite //DoubleCauldron class inherits properties from the Sprite class
    {
        private bool v;

        public DoubleCauldron(bool v)
        {
            this.v = v;
        }

        public DoubleCauldron()
        {
        }

        public override Vector2 PerformMove(GameTime gameTime)
        {
            return new Vector2();
        }
        public override void Load(ContentManager content, Vector2 pos)
        {
            base.Load(content, pos);
            m_texture = content.Load<Texture2D>("doublecauldron");
        }
    }
}
