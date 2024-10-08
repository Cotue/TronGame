﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TronPlay
{
    public class Moto : LinkedList<TrailNode>
    {
        public Texture2D motoTexture;
        public Texture2D trailTexture;
        public const int MotoLength = 8; // La moto más la estela ocupan 8 posiciones
 // La moto más la estela ocupan 4 posiciones
        public Mapa mapa;
        public double trailDuration = 3000; // Duración de la estela en milisegundos (3 segundos)

        public Moto(GraphicsDevice graphicsDevice, Mapa mapa)
        {
            this.mapa = mapa;

            // Inicializa las texturas para la moto y su estela
            motoTexture = new Texture2D(graphicsDevice, 1, 1);
            motoTexture.SetData(new[] { Color.Red });

            trailTexture = new Texture2D(graphicsDevice, 1, 1);
            trailTexture.SetData(new[] { Color.Gray });

            // Crea un GameTime simulado
            GameTime simulatedGameTime = new GameTime(TimeSpan.Zero, TimeSpan.Zero);

            // Inicializa la posición de la moto
            InitializeMoto(new Point(10, 10), simulatedGameTime); // Comienza en el centro del mapa
        }

        public void InitializeMoto(Point startPosition, GameTime gameTime)
        {
            // Añade la posición inicial de la moto (cabeza)
            AddFirst(new TrailNode(startPosition, gameTime.TotalGameTime));

            // Inicializa las posiciones de la estela
            for (int i = 1; i < MotoLength; i++)
            {
                AddLast(new TrailNode(new Point(startPosition.X, startPosition.Y + i), gameTime.TotalGameTime));
            }

            // Actualiza el mapa para reflejar la posición de la moto y su estela
            UpdateMap();
        }




        public bool HasCollidedWithMoto(MotoEnemiga otherMoto)
        {
            // Obtener la posición de la cabeza de la moto actual
            var currentHeadPosition = head.Data.Position;

            // Comprobar colisión de la cabeza con la cabeza de la otra moto
            if (currentHeadPosition == otherMoto.head.Data.Position)
            {
                return true;
            }

            // Comprobar colisión de la cabeza con la estela de la otra moto
            var current = otherMoto.head;
            while (current != null)
            {
                if (currentHeadPosition == current.Data.Position)
                {
                    return true; // Colisión con la estela de la moto enemiga
                }
                current = current.Next;
            }

            return false; // No hubo colisión
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

        public void UpdateTrail(GameTime gameTime)
        {
            double currentTime = gameTime.TotalGameTime.TotalMilliseconds;

            // Remover los nodos cuya estela haya durado más de 3 segundos (3000 milisegundos)
            while (Count > 1 && (currentTime - tail.Data.CreationTime.TotalMilliseconds) > trailDuration)
            {
                RemoveLast(); // Elimina el último nodo si ha pasado más de 3 segundos
            }

            // Actualizar el mapa
            UpdateMap();
        }



        public void Move(Point newPosition, GameTime gameTime)
        {
            // Añadir la nueva posición de la cabeza al frente de la lista
            AddFirst(new TrailNode(newPosition, gameTime.TotalGameTime));

            // Asegúrate de que mantienes la longitud de la estela
            while (Count > MotoLength)
            {
                RemoveLast(); // Elimina el último nodo si hay más de MotoLength
            }

            // Actualiza el mapa para reflejar la nueva posición de la moto
            UpdateMap();
        }





        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var current = head;
            bool isFirst = true;

            while (current != null)
            {
                // Calculamos el tiempo transcurrido desde que la estela fue creada en segundos
                double elapsedTime = (gameTime.TotalGameTime - current.Data.CreationTime).TotalSeconds;

                // Dibuja la cabeza de la moto (roja)
                if (isFirst)
                {
                    spriteBatch.Draw(motoTexture,
                        new Rectangle(current.Data.Position.X * 10, current.Data.Position.Y * 10, 10, 10),
                        Color.Red);
                    isFirst = false;
                }
                // Dibuja la estela de la moto (gris), solo si han pasado menos de 3 segundos
                else if (elapsedTime < 3)
                {
                    spriteBatch.Draw(trailTexture,
                        new Rectangle(current.Data.Position.X * 10, current.Data.Position.Y * 10, 10, 10),
                        Color.Gray);
                }

                current = current.Next;
            }
        }



        public void MoveUp(GameTime gameTime)
        {
            Move(new Point(head.Data.Position.X, head.Data.Position.Y - 1), gameTime);
        }

        public void MoveDown(GameTime gameTime)
        {
            Move(new Point(head.Data.Position.X, head.Data.Position.Y + 1), gameTime);
        }

        public void MoveLeft(GameTime gameTime)
        {
            Move(new Point(head.Data.Position.X - 1, head.Data.Position.Y), gameTime);
        }

        public void MoveRight(GameTime gameTime)
        {
            Move(new Point(head.Data.Position.X + 1, head.Data.Position.Y), gameTime);
        }
    }
}

