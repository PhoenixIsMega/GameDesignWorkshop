using GameDesignWorkshop.Scripts.Engine.Sound;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Utility
{
    public static class AudioUtilities
    {
        public static string GetAudioFilePath(Track audioFile)
        {
            switch (audioFile)
            {
                case Track.Overworld:
                    return "audio/overworld.mp3";
                case Track.Underworld:
                    return "audio/underworld.mp3";
                case Track.BossTheme:
                    return "audio/bosstheme.mp3";
                default:
                    throw new ArgumentException("Invalid audio file.");
            }
        }
    }
}
