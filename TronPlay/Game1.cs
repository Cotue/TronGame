
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TronPlay
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Mapa mapa;
        Moto moto;
        MotoEnemiga motoEnemiga; // Agregar la moto enemiga
        GameTime gameTime;
        Fuel fuel;
        private TimeSpan fuelSpawnTime;
        private TimeSpan lastFuelSpawnTime;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            fuelSpawnTime = TimeSpan.FromSeconds(2);
            lastFuelSpawnTime = TimeSpan.Zero;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapa = new Mapa(GraphicsDevice);

            // Inicializa la moto del jugador
            moto = new Moto(GraphicsDevice, mapa);

            // Inicializa la moto enemiga
            motoEnemiga = new MotoEnemiga(GraphicsDevice, mapa);

            // Inicializa el objeto Fuel
            fuel = new Fuel(GraphicsDevice, mapa);
        }


        protected override void Update(GameTime gameTime)
        {

            // Mover la moto del jugador
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                moto.MoveUp(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                moto.MoveDown(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                moto.MoveLeft(gameTime);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                moto.MoveRight(gameTime);
            }

            // Actualizar la estela de la moto del jugador
            moto.UpdateTrail(gameTime);

            // Mover y actualizar la moto enemiga
            motoEnemiga.Update(gameTime);
            fuel.Update(gameTime);

            // Verificar colisiones
            if (moto.HasCollidedWithMoto(motoEnemiga) || motoEnemiga.HasCollidedWithMoto(moto))
            {
                Console.WriteLine("¡Colisión detectada!");
                Exit(); // Salir del juego al detectar colisión
            }

            base.Update(gameTime);
        }





        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            mapa.Draw(spriteBatch);
            moto.Draw(spriteBatch, gameTime);
            motoEnemiga.Draw(spriteBatch, gameTime); // Dibuja la moto enemiga

            fuel.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
