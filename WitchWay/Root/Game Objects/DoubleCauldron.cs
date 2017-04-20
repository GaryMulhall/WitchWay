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
    class DoubleCauldron : AnimatedCollideableSprite //DoubleCauldron class inherits from AnimatedCollideableSprite
    {
        private bool v;

        public DoubleCauldron(bool v)
        {
            this.v = v;
        }

        public DoubleCauldron()
        {
        }

       
        public override void Load(ContentManager content, Vector2 pos)
        {
            base.Load(content, pos);
            m_animation = new Animation(content.Load<Texture2D>("doubleCauldronSpriteSheet"), (1 / 10f), 10, 1, 0, 9);
        }
    }
}
