using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_Project_1
{
    public abstract class Mob
    /*
     The "Mob" class contains some boilerplate code and data for all moving objects (mobiles) such as
     players, enemies, projectives, moving platforms, which can be shared between ALL mobs.

    NB: The name "mob" pays homage to the Minecraft term "mobs", a name that referred to any non-player
    active entity in the game. couldn't keep it as Mobiles!
     */
    {
        // Some basic shared info for all mobs -- Players, enemies, moving platforms, etc.
        protected Texture2D Tex;
        public Vector2 Position, Velocity;
        public float Width, Height;

        // Collision code will be useful for all mobs, might just make this an external tool later.
        // All collisions, at least for now, are basic AABB collision detections. Pixel Masks would be ideal.
        // Can accept any Mob class type. Again, might be work externalising this for statics for a platformer game...
        public bool CollidingWith(Mob Mobile)
        {
            // AABB Collision proves true if other mobile and THIS mobile intersect
            if (Mobile.Position.X < this.Position.X + this.Width &&
                Mobile.Position.X + Mobile.Width > this.Position.X &&
                Mobile.Position.Y < this.Position.Y + this.Height &&
                Mobile.Position.Y + Mobile.Height > this.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Update mob with delta time -- Abstract
        public abstract void Update(float Delta_time);

        // Draw mob using spritebatches -- Abstract
        public abstract void Draw(SpriteBatch SpriteBatch);
    }

    
     
    // PLAYER CLASS -- Just encapsulating the earlier code into a class
    public class Player : Mob
    {
        private float Speed = 400f;

        public Player(Vector2 pos, Texture2D tex)
        {
            this.Position = pos;
            this.Velocity = Vector2.Zero;
            this.Tex = tex;

            this.Width = this.Tex.Width;
            this.Height = this.Tex.Height;
        }

        public override void Update(float Delta_time)
        {

            this.Velocity = new Vector2(0, 0);
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.W))
            { this.Velocity += new Vector2(0, -1); }
            //if (kstate.IsKeyDown(Keys.A))
            //{ plrVel += new Vector2(-1, 0); }
            if (kstate.IsKeyDown(Keys.S))
            { this.Velocity += new Vector2(0, 1); }
            //if (kstate.IsKeyDown(Keys.D))
            //{ plrVel += new Vector2(1, 0); }     

            this.Position += this.Velocity * Speed * (Delta_time);
            
        }
        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(this.Tex, this.Position, Color.White);
        }
    }

    public class Ball : Mob
    {
        public int axis = 0;

        public Ball(Vector2 pos, Vector2 startvel, Texture2D tex)
        {
            this.Position = pos;
            this.Velocity = startvel;
            this.Tex = tex;

            this.Width = this.Tex.Width;
            this.Height = this.Tex.Height;

        }

        public override void Update(float Delta_time)
        {
            Vector2 dir;

            switch (this.axis)
            {
                case 0:
                    dir = new Vector2(1, 0);
                    break;
                case 1:
                    dir = new Vector2(0, 1);
                    break;
                default:
                    dir = new Vector2(0, 0);
                    break;                
            }

            this.Position += this.Velocity * dir * (Delta_time);

        }

        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(this.Tex, this.Position, Color.White);
        }
    }


}
