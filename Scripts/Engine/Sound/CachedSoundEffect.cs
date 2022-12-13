using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Sound
{
    class CachedSoundEffect
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }
        // A float variable to store the volume of the sound effect (0-1)
        public float Volume { get; set; }

        // An AudioFileReader object to store the audio data of the sound effect
        //public AudioFileReader AudioData { get; set; }

        // The constructor for the CachedSound class
        public CachedSoundEffect(string fileName)
        {
            // Set the default volume to 1 (full volume)
            Volume = 1.0f;

            // Create a new AudioFileReader to read the sound effect file
            //AudioData = new AudioFileReader(fileName);
            using (var audioFileReader = new AudioFileReader(fileName))
            {
                // TODO: could add resampling in here if required
                WaveFormat = audioFileReader.WaveFormat;
                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;
                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                {
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                }
                AudioData = wholeFile.ToArray();
            }
        }

        public CachedSoundEffect(string fileName, float volume)
        {
            // Set the default volume to 1 (full volume)
            Volume = volume;

            // Create a new AudioFileReader to read the sound effect file
            //AudioData = new AudioFileReader(fileName);

            using (var audioFileReader = new AudioFileReader(fileName))
            {
                // TODO: could add resampling in here if required
                WaveFormat = audioFileReader.WaveFormat;
                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;
                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                {
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                }
                AudioData = wholeFile.ToArray();
            }
        }
    }
}
