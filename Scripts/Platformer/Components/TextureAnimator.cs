using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Platformer.Components
{
    public class TextureAnimator : ComponentBase
    {
        int[] frameIDs;
        int currentFrame = 0;
        int frameOffset = 0;
        TimeSpan timeOffset = TimeSpan.Zero;
        TimeSpan animationSpeed;
        TextureComponent texture;
        TimeSpan timeSinceLastFrame = TimeSpan.Zero;
        bool paused = false;

        public TextureAnimator() : base() { }
        public TextureAnimator(TextureComponent texture, int[] frameIDs, TimeSpan animationSpeed) : this() {
            this.frameIDs = frameIDs;
            this.animationSpeed = animationSpeed;
            this.texture = texture;
        }

        /*public void AssignVariables(TextureComponent texture, int[] frameIDs, TimeSpan animationSpeed)
        {
            this.frameIDs = frameIDs;
            this.animationSpeed = animationSpeed;
            this.texture = texture;
        }*/

        public void NextFrame()
        {
            currentFrame++;
            if (currentFrame >= frameIDs.Length)
            {
                currentFrame = 0;
            }
            texture.TextureID = frameIDs[currentFrame] - 1;
        }

        public void Update(TimeSpan deltaTime) {
            if (paused) return;
            timeSinceLastFrame += deltaTime;
            if (timeSinceLastFrame >= animationSpeed)
            {
                
                timeSinceLastFrame -= animationSpeed;
                NextFrame();
            }
        }

        public void AddTimeOffset(TimeSpan timeOffset)
        {
            this.timeOffset = timeOffset;
            timeSinceLastFrame += timeOffset;
        }

        public void AddFrameOffset(int frameOffset)
        {
            this.frameOffset = frameOffset;
            currentFrame += frameOffset;
        }
        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public void Reset()
        {
            currentFrame = 0;
            timeSinceLastFrame = TimeSpan.Zero;
        }

        public void SetFrameIDs(int[] frameIDs)
        {
            this.frameIDs = frameIDs;
            Reset();
            texture.TextureID = frameIDs[currentFrame] - 1;
        }
    }
}
