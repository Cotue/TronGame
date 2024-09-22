using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TronPlay
{
    public class Moto : LinkedList<Point>
    {
        private Texture2D motoTexture;
        private Texture2D trailTexture;
        private const int MotoLength = 4; // La moto más la estela ocupan 4 posiciones
        private Mapa mapa;

        public Moto(GraphicsDevice graphicsDevice, Mapa mapa)
        {
            this.mapa = mapa;

            // Inicializa las texturas para la moto y su estela
            motoTexture = new Texture2D(graphicsDevice, 1, 1);
            motoTexture.SetData(new[] { Color.Red });

            trailTexture = new Texture2D(graphicsDevice, 1, 1);
            trailTexture.SetData(new[] { Color.Gray });

            // Inicializa la posición de la moto
            InitializeMoto(new Point(10, 10)); // Comienza en el centro del mapa
        }
        public bool HasCollided(int width, int height)
        {
            // Obtener la posición de la cabeza de la moto
            var headPosition = head.Data;

            // Verificar si está fuera de los límites del mapa
            if (headPosition.X < 0 || headPosition.X >= width ||
                headPosition.Y < 0 || headPosition.Y >= height)
            {
                return true; // Colisión con los bordes del mapa
            }

            // Verificar si ha colisionado con su propia estela (celdas ocupadas)
            var current = head.Next; // Saltamos la cabeza de la moto
            while (current != null)
            {
                if (current.Data == headPosition)
                {
                    return true; // Colisión con la estela
                }
                current = current.Next;
            }

            return false; // No ha colisionado
        }

        public void InitializeMoto(Point startPosition)
        {
            // Añade la posición inicial de la moto y su estela
            AddFirst(startPosition);

            for (int i = 1; i < MotoLength; i++)
            {
                // Añadir estela detrás de la moto
                AddLast(new Point(startPosition.X, startPosition.Y + i));
            }

            // Actualiza la matriz para reflejar la posición de la moto y su estela
            UpdateMap();
        }

        public void UpdateMap()
        {
            // Limpia la matriz
            mapa.InitializeMatrix();

            // Marca las posiciones ocupadas por la moto y su estela en el mapa
            var current = head;
            bool isFirst = true;

            while (current != null)
            {
                // La primera parte de la moto (la cabeza) es roja
                if (isFirst)
                {
                    mapa.UpdateMatrix(current.Data.X, current.Data.Y, true);
                    isFirst = false;
                }
                // Las siguientes partes (la estela) son grises
                else
                {
                    mapa.UpdateMatrix(current.Data.X, current.Data.Y, true);
                }

                current = current.Next;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            var current = head;
            bool isFirst = true;

            while (current != null)
            {
                // Dibuja la cabeza de la moto (roja)
                if (isFirst)
                {
                    spriteBatch.Draw(motoTexture, new Rectangle(current.Data.X * 10, current.Data.Y * 10, 10, 10), Color.Red);
                    isFirst = false;
                }
                // Dibuja la estela de la moto (gris)
                else
                {
                    spriteBatch.Draw(trailTexture, new Rectangle(current.Data.X * 10, current.Data.Y * 10, 10, 10), Color.Gray);
                }

                current = current.Next;
            }
        }
        public void MoveUp()
        {
            Move(new Point(head.Data.X, head.Data.Y - 1));
        }

        public void MoveDown()
        {
            Move(new Point(head.Data.X, head.Data.Y + 1));
        }

        public void MoveLeft()
        {
            Move(new Point(head.Data.X - 1, head.Data.Y));
        }

        public void MoveRight()
        {
            Move(new Point(head.Data.X + 1, head.Data.Y));
        }
        private void Move(Point newPosition)
        {
            // Quitar la última parte de la estela (el último nodo de la lista)
            RemoveLast();

            // Añadir la nueva posición de la cabeza al frente de la lista
            AddFirst(newPosition);

            // Actualiza el mapa para reflejar la nueva posición de la moto
            UpdateMap();
        }


    }
}

