using Microsoft.Xna.Framework;
using System;

namespace TronPlay
{
    public class TrailNode
    {
        public Point Position { get; set; }  // Posición de la moto o la estela
        public TimeSpan CreationTime { get; set; }  // Tiempo de creación del nodo

        public TrailNode(Point position, TimeSpan creationTime)
        {
            Position = position;
            CreationTime = creationTime;
        }
    }

}