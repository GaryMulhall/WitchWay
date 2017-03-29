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
    class Door : CollideableSprite
    {

        private bool v;

        public Door(bool v)
        {
            this.v = v;
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            base.Load(content, pos);
            m_texture = content.Load<Texture2D>("door");
        }
    }

}
