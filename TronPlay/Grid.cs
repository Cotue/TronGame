using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TronPlay
{
    public class Grid
    {
        private const int GridSize = 100;
        private Texture2D _pixel;
        private SpriteBatch _spriteBatch;

        public Grid(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            Initialize();
        }

        private void Initialize()
        {
            // Crear una textura de un solo píxel negro
            _pixel = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.Black });
        }

        public void Draw()
        {
            int cellSize = 10; // Tamaño de cada casilla (10x10 píxeles en este caso)

            for (int x = 0; x < GridSize; x++)
            {
                for (int y = 0; y < GridSize; y++)
                {
                    // Dibujar cada casilla en la posición (x, y)
                    _spriteBatch.Draw(_pixel, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize), Color.Black);
                }
            }
        }


    }
}
