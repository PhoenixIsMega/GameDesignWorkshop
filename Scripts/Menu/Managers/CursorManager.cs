using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Menu.Cursors;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Windowing.Desktop;

namespace GameDesignLearningAppPrototype.Scripts.Menu.Managers
{
    class CursorManager
    {
        private readonly TileManager tileManager;
        public Cursor cursor;
        public CursorManager(TileManager tileManager)
        {
            this.tileManager = tileManager;
            this.cursor = new Cursor(tileManager);
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
