using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TronPlay
{
    public class Fuel : LinkedList<FuelNode>
    {
        private Texture2D fuelTexture;
        private Random random;
        private Mapa mapa;

        public Fuel(GraphicsDevice graphicsDevice, Mapa mapa)
        {
            this.mapa = mapa;

            // Inicializa la textura para el combustible
            fuelTexture = new Texture2D(graphicsDevice, 1, 1);
            fuelTexture.SetData(new[] { Color.Yellow }); // Color del combustible

            random = new Random();
            GenerateFuel();
        }

        private void GenerateFuel()
        {
            // Genera una posición aleatoria en el mapa
            int x = random.Next(0, Mapa.Width);
            int y = random.Next(0, Mapa.Height);
            AddFirst(new FuelNode(new Point(x, y))); // Asegúrate de que este constructor esté correcto
            mapa.UpdateMatrix(x, y, true); // Marca la posición en el mapa
        }

        public void Update(GameTime gameTime)
        {
            // Verifica si es el momento de generar nuevo combustible (cada 2 segundos)
            if (gameTime.TotalGameTime.TotalMilliseconds % 2000 < 16) // Aproximadamente 60 FPS
            {
                GenerateFuel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var current = head;

            while (current != null)
            {
                spriteBatch.Draw(fuelTexture,
                    new Rectangle(current.Data.Position.X * 10, current.Data.Position.Y * 10, 10, 10),
                    Color.Yellow); // Dibuja el combustible en amarillo

                current = current.Next;
            }
        }
    }

    public class FuelNode
    {
        public Point Position { get; private set; }

        public FuelNode(Point position) // Asegúrate de que este constructor esté correcto
        {
            Position = position;
        }
    }
}

