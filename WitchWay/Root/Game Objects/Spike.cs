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
    class Spike : CollideableSprite //Spike class inherits properties from the CollideableSprite class
    {
        private bool v;

        public Spike(bool v)
        {
            this.v = v;
        }

        public Spike()
        {
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            base.Load(content, pos);
            m_texture = content.Load<Texture2D>("spike");
        }
    }
}
