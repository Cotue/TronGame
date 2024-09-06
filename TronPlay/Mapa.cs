using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TronPlay
{
    public class Mapa : LinkedList<bool>
    {
        private const int Width = 100;
        private const int Height = 100;
        private bool[,] matriz;
        private Texture2D pixelTexture;

        public Mapa(GraphicsDevice graphicsDevice)
        {
            matriz = new bool[Width, Height];
            // Inicializa la matriz con valores predeterminados si es necesario
            InitializeMatrix();

            // Crea una textura de un píxel para dibujar la matriz
            pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.White });
        }

        public void InitializeMatrix()
        {
            // Puedes inicializar la matriz con valores por defecto aquí
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    matriz[x, y] = false; // O cualquier valor predeterminado
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color color = matriz[x, y] ? Color.White : Color.Black;
                    spriteBatch.Draw(pixelTexture, new Rectangle(x * 10, y * 10, 10, 10), color);
                   
                }
            }
        }

        public void UpdateMatrix(int x, int y, bool value)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                matriz[x, y] = value;
            }
        }
    }
}

