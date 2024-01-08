using System;

namespace GameDesignLearningAppPrototype.Scripts.Engine
{
    public class GameTime
    {
        public TimeSpan TotalGameTime { get; set; } // Represents the total elapsed game time since the game started.
        public TimeSpan ElapsedGameTime { get; set; } // Represents the elapsed game time since the last update.

        public GameTime()
        {
            TotalGameTime = TimeSpan.Zero; // Initialize the total game time to zero.
            ElapsedGameTime = TimeSpan.Zero; // Initialize the elapsed game time to zero.
        }

        public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
        {
            TotalGameTime = totalGameTime; // Set the total game time to the provided value.
            ElapsedGameTime = elapsedGameTime; // Set the elapsed game time to the provided value.
        }
    }
}
