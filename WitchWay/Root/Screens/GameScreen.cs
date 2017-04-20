using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace WitchWay
{
    //GameScreen class inherits properties from the IDrawable, IUpdateable and ILoadable interface
    class GameScreen : BaseScreen
    {

        int currentLevel;
       List<IMoveable> moveableSprites;
       List<ICollideable> collideableSprites;

        Button continueButton;

        ImageCounter livesCounter;
        ImageCounter orbsCounter;

        public GameScreen(Game1 game) : base(game)
        {
        }

        public override void Load(ContentManager content, Vector2 pos)
        {
            moveableSprites = new List<IMoveable>();
            collideableSprites = new List<ICollideable>();

            LoadLevel(content, 1);

            continueButton = new Button();
            continueButton.Load(content, new Vector2(540, 310), "continueButton", "continueButtonHighlight");

            livesCounter = new ImageCounter(content.Load<Texture2D>("heart"), content.Load<Texture2D>("heartInactive"), new Vector2(1080, 15), new Vector2(50, 0), 3, 2);
            orbsCounter = new ImageCounter(content.Load<Texture2D>("orbCounter"), content.Load<Texture2D>("orbInactive"), new Vector2(50, 15), new Vector2(50, 0), 3, 2);

        }

        private void LoadLevel(ContentManager content, int level)
        {
            currentLevel = level;

            string levelName = "Level" + level.ToString() + "Positions.csv";

            collideableSprites.Clear();
            moveableSprites.Clear();

            string[] lines;

            if(File.Exists("../../../../Levels/" + levelName))
            {
                lines = File.ReadAllLines("../../../../Levels/" + levelName);
            }
            else if(File.Exists("Levels/" + levelName))
            {
                lines = File.ReadAllLines("Levels/" + levelName);
            }
            else { throw new FileNotFoundException("Level file not found"); }
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if (!string.IsNullOrWhiteSpace(values[0]))
                {

                    float x;
                    float y;

                    try
                    {
                        float.TryParse(values[1], out x);
                        float.TryParse(values[2], out y);
                    }
                    catch { continue; }

                    if (values[0] == "Platform")
                    {
                        addCollideable(content, new Vector2(x, y), new Platform(true));
                    }
                    else if (values[0] == "Cat")
                    {
                        addCollideable(content, new Vector2(x, y), new Cat(true));
                    }
                    else if (values[0] == "Witch")
                    {
                        addMoveable(content, new Vector2(x, y), new Witch(true, collideableSprites, moveableSprites, Game));
                    }
                    else if (values[0] == "Cauldron")
                    {
                        addCollideable(content, new Vector2(x, y), new Cauldron(true));
                    }
                    else if (values[0] == "DoubleCauldron")
                    {
                        addCollideable(content, new Vector2(x, y), new DoubleCauldron(true));
                    }
                    else if (values[0] == "Door")
                    {
                        addCollideable(content, new Vector2(x, y), new Door(true));
                    }
                    else if (values[0] == "Orb")
                    {
                        addCollideable(content, new Vector2(x, y), new Orb(true));

                    }
                    else if (values[0] == "Spike")
                    {
                        addCollideable(content, new Vector2(x, y), new Spike(true));
                    }
                    else if (values[0] == "Poop")
                    {
                        float velocityX;
                        float velocityY;

                        try
                        {
                            float.TryParse(values[3], out velocityX);
                            float.TryParse(values[4], out velocityY);
                        }
                        catch { continue; }
                        addMoveable(content, new Vector2(x, y), new Poop(true, collideableSprites, moveableSprites, new Vector2 (velocityX, velocityY)));
                    }
                }
            }
        }

        public void addCollideable(ContentManager content, Vector2 pos, CollideableSprite newSprite)
        {
            newSprite.Load(content, pos);
            collideableSprites.Add(newSprite);
        }
        public void addMoveable(ContentManager content, Vector2 pos, IMoveable newSprite)
        {
            newSprite.Load(content, pos);
            moveableSprites.Add(newSprite);
        }


        public override void Update(GameTime gameTime)
        {
            if (moveableSprites.OfType<Witch>().Count() > 0)
            {
                Witch witch = moveableSprites.OfType<Witch>().First();

                livesCounter.SetCount(witch.lives);
                orbsCounter.SetCount(witch.orbs);
            }



            if (Input.beenPressed(Keys.R))
            {
                LoadLevel(Game.Content, currentLevel);
            }
            foreach (var moveable in moveableSprites)
            {
                moveable.Update(gameTime);
            }

            foreach (var collidable in collideableSprites)
            {
                collidable.Update(gameTime);
            }

            bool pPressed =  Input.beenPressed(Keys.P);

            if (pPressed)
            {
                Game.ScreenMgr.Switch(new PauseScreen(Game));
            }

            if(collideableSprites.OfType<Cat>().Count() == 0 && continueButton.IsClicked())
            {
                if(currentLevel < 5)
                {
                    LoadLevel(Game.Content, currentLevel + 1);
                }
            }
            collideableSprites.RemoveAll(sprite => sprite.Destroyed);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var moveable in moveableSprites)
            {
                moveable.Draw(spriteBatch);
            }

            foreach (var collidable in collideableSprites)
            {
                collidable.Draw(spriteBatch);
            }
            if (collideableSprites.OfType<Cat>().Count() == 0)
            {
                continueButton.Draw(spriteBatch);
            }

            livesCounter.Draw(spriteBatch);
            orbsCounter.Draw(spriteBatch);
        }
    }
}
