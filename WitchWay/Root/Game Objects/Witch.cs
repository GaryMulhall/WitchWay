using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WitchWay
{
    class Witch : AnimatedMoveableSprite // inherits from AnimatedMoveableSprite class
    {
        List<ICollideable> collideableSprites;
        List<IMoveable> moveableSprites;

        Dictionary<states, Animation> m_animations = new Dictionary<states, Animation>();

        protected override Animation m_animation { get { return m_animations[m_state]; } }

        private bool v;
        const float PlayerSpeed = 9.4f;
        float jumpSpeed;
        public int lives = 3;
        public int orbs = 0;
        protected Game1 Game;

        SpriteEffects Effects;

        int m_gameTime;

        Vector2 livesPos;
        Vector2 orbsPos;

        SpriteFont Font;

        // States the player character can achieve
        enum states { Idle, Falling, Jumping, Walking }

        private states m_state;

        public Witch(bool v, List<ICollideable> collideableSprites, List<IMoveable> moveableSprites, Game1 game)
        {
            // default state is grounded
            m_state = states.Idle;

            this.v = v;
            this.collideableSprites = collideableSprites;
            this.moveableSprites = moveableSprites;
            Game = game;

            Effects = SpriteEffects.None;
        }

        // boolean to check if the player character can move 
        public bool CanMove(Vector2 velocity)
        {
            Vector2 newPos = m_position + velocity;
            Rectangle newBounds = new Rectangle(newPos.ToPoint(), m_animation.FrameSize);

            if (newPos.X < 0 || newPos.Y < 0 || newPos.X + m_animation.FrameWidth > Game1.screenWidth || newPos.Y > Game1.screenHeight + m_animation.FrameHeight)
                return false;

            foreach (var platform in collideableSprites.OfType<Platform>())
            {
                if (newBounds.Intersects(platform.Hitbox))
                {
                    return false;
                }
            }
            foreach (var doubleCauldron in collideableSprites.OfType<DoubleCauldron>())
            {
                if (newBounds.Intersects(doubleCauldron.Hitbox))
                {
                    return false;
                }
            }
            foreach (var cauldron in collideableSprites.OfType<Cauldron>())
            {
                if (newBounds.Intersects(cauldron.Hitbox))
                {
                    return false;
                }
            }
            foreach (var door in collideableSprites.OfType<Door>())
            {
                if (newBounds.Intersects(door.Hitbox))
                {
                    return false;
                }
            }
                return true;
        }

        public override Vector2 PerformMove(GameTime gameTime)
        {

            foreach (var orb in collideableSprites.OfType<Orb>())
            {
                if (Hitbox.Intersects(orb.Hitbox))
                {
                    orbs++;
                    orb.Destroyed = true;
                }
            }
            foreach (var door in collideableSprites.OfType<Door>())
            {
                if (Hitbox.Intersects(door.Hitbox))
                {
                    m_position.X = door.Hitbox.X - m_animation.FrameWidth;
                }

                if (orbs == 3)
                {
                    door.Destroyed = true;
                }
            }
            if (lives == 0)
            {
                Game.ScreenMgr.Switch(new GameOverScreen(Game));
                lives = 3;
                orbs = 0;
            }
            foreach (var cat in collideableSprites.OfType<Cat>())
            {
                if (Hitbox.Intersects(cat.Hitbox))
                {
                    cat.Destroyed = true;
                    orbs = 0;
                }
            }
            foreach (var poop in moveableSprites.OfType<Poop>())
            {
                if (Hitbox.Intersects(poop.Hitbox) && m_gameTime > 0)
                {
                    m_gameTime = 0;
                    lives--;
                    m_position.X = 50;
                    m_position.Y = 650;
                }
            }
            foreach (var spike in collideableSprites.OfType<Spike>())
            {
                if (Hitbox.Intersects(spike.Hitbox) && m_gameTime > 0)
                {
                    m_gameTime = 0;
                    lives--;
                    m_position.X = 50;
                    m_position.Y = 650;
                }
            }

            Vector2 velocity = Vector2.Zero;
            // setting what happens if the character is currently in the falling state
            if (m_state == states.Falling)
            {
                jumpSpeed += 1;
                velocity.Y += jumpSpeed;
                foreach (var platform in collideableSprites.OfType<Platform>())
                {
                    if (Hitbox.Intersects(platform.Hitbox))
                    {
                        m_position.Y = platform.Hitbox.Y - m_animation.FrameHeight;
                        m_state = states.Idle;
                    }
                }
                foreach (var cauldron in collideableSprites.OfType<Cauldron>())
                {
                    if (Hitbox.Intersects(cauldron.Hitbox))
                    {
                        m_position.Y = cauldron.Hitbox.Y - m_animation.FrameHeight;
                        jumpSpeed = -17;
                        m_state = states.Jumping;
                    }
                }
                foreach (var doubleCauldron in collideableSprites.OfType<DoubleCauldron>())
                {
                    if (Hitbox.Intersects(doubleCauldron.Hitbox))
                    {
                        m_position.Y = doubleCauldron.Hitbox.Y - m_animation.FrameHeight;
                        jumpSpeed = -22;
                        m_state = states.Jumping;
                    }
                }

                if (m_position.Y >= Game1.screenHeight - m_animation.FrameHeight - 1)
                {
                    m_state = states.Idle;
                    m_position.Y = Game1.screenHeight - m_animation.FrameHeight;
                }
            }

            // setting what happens if the character is currently in the grounded state
            if (m_state == states.Idle || m_state == states.Walking)
            {
                if (Input.isDown(Keys.Space))
                {
                    jumpSpeed = -15;
                    m_state = states.Jumping;
                }
                else
                {
                    bool grounded = false;

                    foreach (var platform in collideableSprites.OfType<Platform>())
                    {
                        if (Hitbox.Intersects(platform.Hitbox))
                        {
                            grounded = true;
                        }
                    }
                    foreach (var cauldron in collideableSprites.OfType<Cauldron>())
                    {
                        if (Hitbox.Intersects(cauldron.Hitbox))
                        {
                            grounded = true;
                        }
                    }
                    if (m_position.Y >= Game1.screenHeight - m_animation.FrameHeight)
                    {
                        grounded = true;
                    }
                    if (grounded == false)
                    {
                        m_state = states.Falling;
                        jumpSpeed = 0;
                    }
                    velocity.Y = 0;
                }
            }
            // setting what happens if the character is currently in the jumping state
            if (m_state == states.Jumping)
            {
                if (CanMove(new Vector2(0, jumpSpeed)))
                {
                    velocity.Y += jumpSpeed;
                }
                else
                {
                    m_state = states.Falling;
                    velocity.Y = 0;
                    jumpSpeed = 0;
                }

                // decrease the jumping speed gradually
                jumpSpeed += 1;
                if (jumpSpeed > 0)
                {
                    m_state = states.Falling;
                    velocity.Y = 0;
                }
            }
            // control code
            if (Input.isDown(Keys.A))
            {
                if (m_state == states.Idle)
                {
                    m_state = states.Walking;
                }
                if (CanMove(new Vector2(-10, -1)))
                {
                    velocity.X -= 10;
                    Effects = SpriteEffects.FlipHorizontally;
                }
            }
            else if (Input.isDown(Keys.D))
            {
                if (m_state == states.Idle)
                {
                    m_state = states.Walking;
                }
                if (CanMove(new Vector2(10, -1)))
                {
                    velocity.X += 10;
                    Effects = SpriteEffects.None;
                }
            }
            else
            {
                if (m_state == states.Walking)
                {

                    m_state = states.Idle;
                }
            }
            return velocity;
        }

        public override void ResetPosYGround()
        {
            base.ResetPosYGround();
        }
        public override void ResetPosXRight()
        {
            base.ResetPosXRight();
        }

        public override void Update(GameTime gameTime)
        {
            m_gameTime += gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
            Move(gameTime);
        }
        public override void Load(ContentManager content, Vector2 pos)
        {
            livesPos = new Vector2(1180, 20);
            orbsPos = new Vector2(40, 20);
            Font = content.Load<SpriteFont>("Font");
            base.Load(content, pos);

            m_animations[states.Idle] = new Animation(content.Load<Texture2D>("witchSpriteSheet"), (1 / 5f), 10, 3, 0, 9);
            m_animations[states.Walking] = new Animation(content.Load<Texture2D>("witchSpriteSheet"), (1 / 7f), 10, 3, 10, 19);
            m_animations[states.Jumping] = new Animation(content.Load<Texture2D>("witchSpriteSheet"), (1 / 10f), 10, 3, 20, 29);
            m_animations[states.Falling] = m_animations[states.Jumping];

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(Font, string.Format("Lives: " + lives), livesPos, Color.White);
            //spriteBatch.DrawString(Font, string.Format("Orbs: " + orbs), orbsPos, Color.White);

            m_animation.Draw(spriteBatch, m_position, Effects);
        }
    }
}
