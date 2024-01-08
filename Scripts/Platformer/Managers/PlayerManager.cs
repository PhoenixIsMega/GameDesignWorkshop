using GameDesignLearningAppPrototype.Scripts.Engine;
using GameDesignLearningAppPrototype.Scripts.Platformer.Players;
using OpenTK.Windowing.Desktop;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Managers
{
    class PlayerManager
    {
        private Player player = new Player();
        public PlayerManager()
        {
            player.Move(0, 0);
        }

        public void Update(GameWindow gameWindow, GameTime gameTime)
        {
            player.Update(gameWindow, gameTime);
        }

        public float[] AssembleVertexData()
        {
            return player.AssembleVertexData();
        }

        public int CountTiles()
        {
            return 1;
        }

        public (float, float) GetPlayerPosition()
        {
            return player.GetPosition();
        }

        public (float, float) GetPlayerSize()
        {
            return player.GetSize();
        }
    }
}
