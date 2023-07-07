using GameDesignWorkshop.game.managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new RefactorNumeroUno("Game Design Workshop by Phoenix Thomson", 800, 800);
            game.Start();
        }
    }
}
