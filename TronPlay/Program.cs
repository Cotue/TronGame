
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TronPlay
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
}

