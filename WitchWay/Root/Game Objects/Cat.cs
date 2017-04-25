using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace WitchWay
{

    class Cat : AnimatedCollideableSprite // inherits from CollideableSprite class
    {
        private bool v;

        public Cat(bool v)
        {
            this.v = v;
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            base.Load(content, pos);
            m_animation = new Animation(content.Load<Texture2D>("catSpriteSheet"), (1 / 10f), 8, 1, 0, 8);

        }
    }
}
