using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitchWay
{
    abstract class MoveableSprite : CollideableSprite, IMoveable
    {
     
        public void Move(GameTime gameTime)
        {

            Vector2 velocity = PerformMove(gameTime);

            m_position += velocity;

        }

        public abstract Vector2 PerformMove(GameTime gameTime);

    }
}
