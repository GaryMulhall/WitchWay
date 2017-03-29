//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using System.Collections;
//using Microsoft.Xna.Framework.Input;

//namespace WitchWay
//{
//    //GameScreen class inherits properties from the IDrawable, IUpdateable and ILoadable interface
//    class Level3Screen : BaseScreen
//    {
//        List<IMoveable> moveableSprites;
//        List<ICollideable> collideableSprites;

//        Button continueButton;

//        public Level3Screen (Game1 game) : base(game)
//        {
//        }

//        public override void Load(ContentManager content, Vector2 pos)
//        {
//            moveableSprites = new List<IMoveable>();
//            collideableSprites = new List<ICollideable>();

//            //bottom right platform
//            addCollideable(content, new Vector2(900, 620), new Platform(true));
//            addCollideable(content, new Vector2(1000, 620), new Platform(true));
//            addCollideable(content, new Vector2(1100, 620), new Platform(true));
//            addCollideable(content, new Vector2(1200, 620), new Platform(true));

//            //bottom  left
//            addCollideable(content, new Vector2(550, 550), new Platform(true));
//            addCollideable(content, new Vector2(450, 550), new Platform(true));
//            addCollideable(content, new Vector2(350, 550), new Platform(true));
//            addCollideable(content, new Vector2(250, 550), new Platform(true));
//            addCollideable(content, new Vector2(150, 550), new Platform(true));
//            addCollideable(content, new Vector2(50, 550), new Platform(true));
//            addCollideable(content, new Vector2(0, 550), new Platform(true));

//            //middle left
//            addCollideable(content, new Vector2(0, 400), new Platform(true));


//            //middle
//            addCollideable(content, new Vector2(1000, 400), new Platform(true));
//            addCollideable(content, new Vector2(1100, 400), new Platform(true));
//            addCollideable(content, new Vector2(1200, 400), new Platform(true));

//            //top
//            addCollideable(content, new Vector2(850, 200), new Platform(true));
//            addCollideable(content, new Vector2(650, 200), new Platform(true));
//            addCollideable(content, new Vector2(450, 200), new Platform(true));
//            addCollideable(content, new Vector2(0, 200), new Platform(true));

//            //cauldrons
//            addCollideable(content, new Vector2(750, 590), new Cauldron(true));
//            addCollideable(content, new Vector2(500, 420), new Cauldron(true));
//            addCollideable(content, new Vector2(850, 450), new Cauldron(true));
//            addCollideable(content, new Vector2(180, 450), new Cauldron(true));
//            addCollideable(content, new Vector2(1175, 290), new Cauldron(true));
//            addCollideable(content, new Vector2(230, 250), new Cauldron(true));

//            addMoveable(content, new Vector2(675, 620), new Poop(true));

//            //add the cat
//            addCollideable(content, new Vector2(0, 170), new Cat(true));

//            //add the moveable sprites

//            var witch = new Witch(true, collideableSprites);
//            witch.Load(content, new Vector2(50, 650));
//            moveableSprites.Add(witch);

//            continueButton = new Button();
//            continueButton.Load(content, new Vector2(540, 310), "continueButton", "continueButtonHighlight");

//        }

//        public void addCollideable(ContentManager content, Vector2 pos, CollideableSprite newSprite)
//        {
//            newSprite.Load(content, pos);
//            collideableSprites.Add(newSprite);
//        }
//        public void addMoveable(ContentManager content, Vector2 pos, MoveableSprite newSprite)
//        {
//            newSprite.Load(content, pos);
//            moveableSprites.Add(newSprite);
//        }

//        public void catCollected()
//        {

//        }

//        public override void Update(GameTime gameTime)
//        {
//            foreach (var moveable in moveableSprites)
//            {
//                moveable.Update(gameTime);
//            }

//            foreach (var collidable in collideableSprites)
//            {
//                collidable.Update(gameTime);
//            }

//            bool pPressed = Input.beenPressed(Keys.P);

//            if (pPressed)
//            {
//                Game.ScreenMgr.Switch(new PauseScreen(Game));
//            }

//            if (collideableSprites.OfType<Cat>().Count() == 0 && continueButton.IsClicked())
//            {
//                Game.ScreenMgr.Switch(new Level4Screen(Game));
//            }


//            collideableSprites.RemoveAll(sprite => sprite.Destroyed);
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            foreach (var moveable in moveableSprites)
//            {
//                moveable.Draw(spriteBatch);
//            }

//            foreach (var collidable in collideableSprites)
//            {
//                collidable.Draw(spriteBatch);
//            }
//            if (collideableSprites.OfType<Cat>().Count() == 0)
//            {
//                continueButton.Draw(spriteBatch);
//            }
//        }
//    }
//}
