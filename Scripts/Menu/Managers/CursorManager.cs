using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Engine.Rendering.Managers;
using GameDesignLearningAppPrototype.Scripts.Menu.Cursors;
using GameDesignLearningAppPrototype.Scripts.Platformer.Managers;
using OpenTK.Windowing.Desktop;

namespace GameDesignLearningAppPrototype.Scripts.Menu.Managers
{
    public class CursorManager
    {
        private readonly ClassManager classManager;
        private readonly TileManager tileManager;
        public Cursor cursor;
        public CursorManager(ClassManager classManager)
        {
            this.classManager = classManager;
            this.tileManager = classManager.TileManager;
            this.cursor = new Cursor(classManager);
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
