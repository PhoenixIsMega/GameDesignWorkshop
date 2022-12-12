using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Sound
{
    class CachedSound
    {
        // A float variable to store the volume of the sound effect (0-1)
        public float Volume { get; set; }

        // An AudioFileReader object to store the audio data of the sound effect
        public AudioFileReader AudioData { get; set; }

        // The constructor for the CachedSound class
        public CachedSound(string fileName)
        {
            // Set the default volume to 1 (full volume)
            Volume = 1.0f;

            // Create a new AudioFileReader to read the sound effect file
            AudioData = new AudioFileReader(fileName);
        }
    }
}
