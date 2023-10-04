using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Menu.Cursors;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignLearningAppPrototype.Scripts.Menu.Managers
{
    class CursorManager
    {
        public Cursor cursor = new Cursor();
        public CursorManager()
        {
            cursor.Move(500, 500);
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            cursor.Update(gameWindow, gameTime);
        }

        public float[] AssembleVertexData()
        {
            return cursor.AssembleVertexData();
        }

        public int CountTiles()
        {
            return 1;
        }
    }
}
