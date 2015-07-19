using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AH64TD
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Interface
        Texture2D background;
        Texture2D UI_Top_Menu;
        int UI_Top_Menu_Height = 15;
        Texture2D UI_Bottom_Menu;
        int UI_Bottom_Menu_Height = 100;
        Rectangle CombatArea;

        // Fonts
        SpriteFont sfDefault;

        // Vehicle Textures
        Texture2D AH64;
        Texture2D AH64Rotor;

        // Weapon Icon Textures
        Texture2D txCannon;

        // Vehicles
        Vehicle Player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // UI Sprites
            background = Content.Load<Texture2D>("Art/Terrain/sand");
            UI_Top_Menu = Content.Load<Texture2D>("Art/Interface/ui_background");
            UI_Bottom_Menu = Content.Load<Texture2D>("Art/Interface/ui_background");

            // Fonts
            sfDefault = Content.Load<SpriteFont>("Fonts/QuadrantFont");

            // Vehicle Sprites
            AH64 = Content.Load<Texture2D>("Art/Vehicle/ah64");
            AH64Rotor = Content.Load<Texture2D>("Art/Vehicle/ah64-rotor");

            // Weapon Icons
            txCannon = Content.Load<Texture2D>("Art/Weapons/Cannon");

            // Vehicles
            Player = new Vehicle("Player", VEHICLE_TYPE.PLAYER,
                new Vector2(15, (float)(GraphicsDevice.Viewport.Height / 2) - (AH64.Height / 2)), AH64, AH64Rotor);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            // Allows the game to exit
            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

            // DIRECTION KEYS
            // @TODO: Implement a helper class for common input conditions (multiple movement press to pythag velocity, etc.).
            Vector2 newPosition = Player.Position;

            // UP
            if (state.IsKeyDown(Keys.S)
                || state.IsKeyDown(Keys.Down))
            {
                int newY = (int)Math.Round(Player.Position.Y + (Player.Velocity * gameTime.ElapsedGameTime.Milliseconds));
                if (newY >= CombatArea.Y
                    && newY < CombatArea.Y + CombatArea.Height - Player.txVehicleTexture.Height)
                {
                    newPosition.Y = newY;
                }
            }

            // RIGHT
            if (state.IsKeyDown(Keys.D)
                || state.IsKeyDown(Keys.Right))
            {
                int newX = (int)Math.Round(Player.Position.X + (Player.Velocity * gameTime.ElapsedGameTime.Milliseconds));
                if (newX >= CombatArea.X
                    && newX < CombatArea.Width - Player.txVehicleTexture.Width)
                {
                    newPosition.X = newX;
                }
            }

            // LEFT
            if (state.IsKeyDown(Keys.A)
                || state.IsKeyDown(Keys.Left))
            {
                int newX = (int)Math.Round(Player.Position.X - (Player.Velocity * gameTime.ElapsedGameTime.Milliseconds));
                if (newX >= CombatArea.X
                    && newX < CombatArea.Width)
                {
                    newPosition.X = newX;
                }
            }

            // DOWN
            if (state.IsKeyDown(Keys.W)
                || state.IsKeyDown(Keys.Up))
            {
                int newY = (int)Math.Round(Player.Position.Y - (Player.Velocity * gameTime.ElapsedGameTime.Milliseconds));
                if (newY >= CombatArea.Y
                    && newY < CombatArea.Y + CombatArea.Height - Player.txVehicleTexture.Height)
                {
                    newPosition.Y = newY;
                }
            }

            Player.Position = newPosition;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // SPRITES
            spriteBatch.Begin();

            // BACKGROUND
            spriteBatch.Draw(background, new Rectangle(0, 0,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);

            // TOP MENU BACKGROUND
            spriteBatch.Draw(UI_Top_Menu, new Rectangle(0, 0,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, UI_Top_Menu_Height), Color.White);

            // BOTTOM MENU
            int intBottomY = GraphicsDevice.Viewport.Height - UI_Bottom_Menu_Height;
            int intBottomFieldHeaderY = (int)Math.Round(GraphicsDevice.Viewport.Height - (UI_Bottom_Menu_Height * 0.9));
            spriteBatch.Draw(UI_Bottom_Menu,
                new Rectangle(0, intBottomY, GraphicsDevice.Viewport.Width, UI_Bottom_Menu_Height), Color.White);

            // WEAPON STATUS
            spriteBatch.DrawString(sfDefault, "WEAPON",
                new Vector2(10, intBottomFieldHeaderY),
                Color.Green);

            spriteBatch.Draw(txCannon, new Vector2(10, intBottomFieldHeaderY + 30), Color.White);

            // AMMO
            spriteBatch.DrawString(sfDefault, "AMMO",
                new Vector2((int)(Math.Round(GraphicsDevice.Viewport.Width * 0.2)), intBottomFieldHeaderY),
                Color.Green);

            // HEALTH
            spriteBatch.DrawString(sfDefault, "HEALTH",
                new Vector2((int)(Math.Round(GraphicsDevice.Viewport.Width * 0.8)), intBottomFieldHeaderY),
                Color.Green);

            spriteBatch.DrawString(sfDefault, ((Player.HealthValue / Player.MaxHealth) * 100).ToString() + "%",
                new Vector2(
                    (int)(Math.Round(GraphicsDevice.Viewport.Width * 0.815)),
                    (int)Math.Round(intBottomY + (UI_Bottom_Menu.Height * 0.9))
                    ),
                Color.Green);

            // COMBAT AREA
            CombatArea = new Rectangle(0, UI_Top_Menu_Height, GraphicsDevice.Viewport.Width,
                (GraphicsDevice.Viewport.Height - UI_Bottom_Menu_Height - UI_Top_Menu_Height));

            // PLAYER VEHICLE
            spriteBatch.Draw(Player.txVehicleTexture, new Rectangle((int)Math.Round(Player.Position.X),
                (int)Math.Round(Player.Position.Y),
                Player.txVehicleTexture.Width,
                Player.txVehicleTexture.Height), Color.White);

            // DEBUG COORDINATES
            spriteBatch.DrawString(sfDefault, "(" + Player.Position.X.ToString() + "," + Player.Position.Y.ToString() + ")",
                new Vector2((int)(Math.Round(GraphicsDevice.Viewport.Width * 0.42)), intBottomFieldHeaderY), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
