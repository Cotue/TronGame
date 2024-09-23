
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
        GameTime gameTime;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mapa = new Mapa(GraphicsDevice);
            moto = new Moto(GraphicsDevice, mapa);
        }

        protected override void Update(GameTime gameTime)
        {
            // Verifica si el jugador presiona las teclas de flecha para mover la moto
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

            // Actualizar la estela de la moto y borrar los nodos después de 3 segundos
            moto.UpdateTrail(gameTime);

            // Verificar colisiones después de mover la moto
            if (moto.HasCollided(100, 100))
            {
                Console.WriteLine("¡Colisión detectada!");
                Exit(); // De momento, cerramos el juego al detectar colisión
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            mapa.Draw(spriteBatch);
            moto.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}