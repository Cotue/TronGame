using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Cell
{
    public bool IsOccupied { get; set; }
    public Color Color { get; set; } // Color para representar la celda

    public Cell()
    {
        IsOccupied = false;
        Color = Color.Black; // Color por defecto
    }
}

