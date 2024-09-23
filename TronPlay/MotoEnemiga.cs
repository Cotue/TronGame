using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TronPlay
{
    public class MotoEnemiga : LinkedList<TrailNode>
    {
        private Texture2D motoTexture;
        private Texture2D trailTexture;
        private const int MotoLength = 8; // Longitud de la moto y su estela
        private Mapa mapa;
        private double trailDuration = 3000; // Duración de la estela en milisegundos
        private Random random;
        private int moveInterval = 200; // Intervalo de tiempo para cambiar de dirección
        private TimeSpan lastMoveTime;

        public MotoEnemiga(GraphicsDevice graphicsDevice, Mapa mapa)
        {
            this.mapa = mapa;

            // Inicializa las texturas para la moto y su estela
            motoTexture = new Texture2D(graphicsDevice, 1, 1);
            motoTexture.SetData(new[] { Color.Orange }); // Cambia el color aquí

            trailTexture = new Texture2D(graphicsDevice, 1, 1);
            trailTexture.SetData(new[] { Color.Gray });

            // Crea un GameTime simulado
            GameTime simulatedGameTime = new GameTime(TimeSpan.Zero, TimeSpan.Zero);

            // Inicializa la posición de la moto
            InitializeMoto(new Point(40, 20), simulatedGameTime); // Cambia la posición inicial aquí

            random = new Random();
            lastMoveTime = TimeSpan.Zero;
        }

        public void InitializeMoto(Point startPosition, GameTime gameTime)
        {
            // Añade la posición inicial de la moto (cabeza) y su estela
            AddFirst(new TrailNode(startPosition, gameTime.TotalGameTime));

            for (int i = 1; i < MotoLength; i++)
            {
                // Añadir estela detrás de la moto
                AddLast(new TrailNode(new Point(startPosition.X, startPosition.Y + i), gameTime.TotalGameTime));
            }

            // Actualiza el mapa para reflejar la posición de la moto y su estela
            UpdateMap();
        }

        public void Update(GameTime gameTime)
        {
            lastMoveTime += gameTime.ElapsedGameTime;

            // Mover cada cierto tiempo
            if (lastMoveTime.TotalMilliseconds > moveInterval)
            {
                MoveRandomly(gameTime);
                lastMoveTime = TimeSpan.Zero;
            }

            // Actualiza la estela (esto solo elimina nodos antiguos, no añade nuevos)
            UpdateTrail(gameTime);
        }

        private void MoveRandomly(GameTime gameTime)
        {
            // Elegir una dirección aleatoria
            int direction = random.Next(4); // 0: Arriba, 1: Abajo, 2: Izquierda, 3: Derecha
            Point newPosition = head.Data.Position;

            switch (direction)
            {
                case 0:
                    newPosition.Y -= 1; // Mover hacia arriba
                    break;
                case 1:
                    newPosition.Y += 1; // Mover hacia abajo
                    break;
                case 2:
                    newPosition.X -= 1; // Mover hacia la izquierda
                    break;
                case 3:
                    newPosition.X += 1; // Mover hacia la derecha
                    break;
            }

            // Realiza el movimiento con la nueva posición
            Move(newPosition, gameTime);
        }

        private void Move(Point newPosition, GameTime gameTime)
        {
            // Añadir la nueva posición de la cabeza al frente de la lista con el tiempo actual
            AddFirst(new TrailNode(newPosition, gameTime.TotalGameTime));

            // Mantén la longitud de la estela limitada a la longitud de la moto
            if (Count > MotoLength)
            {
                RemoveLast(); // Elimina el último nodo si hay más de MotoLength
            }

            // Actualiza el mapa para reflejar la nueva posición de la moto
            UpdateMap();
        }

        public void UpdateTrail(GameTime gameTime)
        {
            double currentTime = gameTime.TotalGameTime.TotalMilliseconds;

            // Remover los nodos cuya estela haya durado más de 3 segundos (3000 milisegundos)
            while (Count > 1 && (currentTime - tail.Data.CreationTime.TotalMilliseconds) > trailDuration)
            {
                RemoveLast(); // Elimina el último nodo si ha pasado más de 3 segundos
            }

            // Actualizar el mapa para reflejar la nueva posición de la moto y la estela
            UpdateMap();
        }

        public void UpdateMap()
        {
            // Limpia la matriz
            mapa.InitializeMatrix();

            // Marca las posiciones ocupadas por la moto y su estela en el mapa
            var current = head;
            while (current != null)
            {
                mapa.UpdateMatrix(current.Data.Position.X, current.Data.Position.Y, true);
                current = current.Next;
            }
        }
        public bool HasCollidedWithMoto(Moto otherMoto)
        {
            // Obtener la posición de la cabeza de la moto enemiga
            var currentHeadPosition = head.Data.Position;

            // Comprobar colisión de la cabeza de la moto enemiga con la cabeza de la moto del jugador
            if (currentHeadPosition == otherMoto.head.Data.Position)
            {
                return true;
            }

            // Comprobar colisión de la cabeza de la moto enemiga con la estela de la moto del jugador
            var current = otherMoto.head;
            while (current != null)
            {
                if (currentHeadPosition == current.Data.Position)
                {
                    return true; // Colisión con la estela de la moto del jugador
                }
                current = current.Next;
            }

            return false; // No hubo colisión
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var current = head;
            bool isFirst = true;

            while (current != null)
            {
                // Dibuja la cabeza de la moto (color naranja)
                if (isFirst)
                {
                    spriteBatch.Draw(motoTexture,
                        new Rectangle(current.Data.Position.X * 10, current.Data.Position.Y * 10, 10, 10),
                        Color.Orange); // Color de la cabeza de la moto enemiga
                    isFirst = false;
                }
                else
                {
                    // Dibuja la estela de la moto (gris)
                    spriteBatch.Draw(trailTexture,
                        new Rectangle(current.Data.Position.X * 10, current.Data.Position.Y * 10, 10, 10),
                        Color.Gray);
                }

                current = current.Next;
            }
        }
    }
}



