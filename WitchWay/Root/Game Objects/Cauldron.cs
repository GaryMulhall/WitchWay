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

    class Cauldron : AnimatedCollideableSprite // inherits from AnimatedCollideableSprite class
    {

        private bool v;

        public Cauldron()
        {
        }

        public Cauldron(bool v)
        {
            this.v = v;
        }

        public override void Load(ContentManager content, Vector2 pos)
        {

            m_animation = new Animation(content.Load<Texture2D>("cauldronSpriteSheet"), (1 / 10f), 10, 1, 0, 9);
            base.Load(content, pos);
        }
    }
}
