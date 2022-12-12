using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.resources.sounds
{
    class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly OLDCachedSound cachedSound;
        private long position;

        public CachedSoundSampleProvider(OLDCachedSound cachedSound)
        {
            this.cachedSound = cachedSound;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = cachedSound.AudioData.Length - position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy);
            position += samplesToCopy;
            return (int)samplesToCopy;
        }

        public WaveFormat WaveFormat { get { return cachedSound.WaveFormat; } }
    }
}
