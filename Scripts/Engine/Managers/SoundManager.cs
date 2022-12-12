using GameDesignWorkshop.game;
using GameDesignWorkshop.Scripts.Engine.Sound;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.resources.sounds
{
    public class SoundManager
    {
        //fix variable names + public etc
        public IWavePlayer musicPlayer;
        private WaveOut soundPlayer;

        public MixingSampleProvider mixer;

        public float masterVolume = 1.0f;
        public float musicVolume = 0.5f; //get from file next
        public float sfxVolume = 0.5f;

        // Crossfade
        public float fadeOutSpeed;
        public float fadeInSpeed;
        public float fadeOutStart;
        public float fadeInStart;

        public SoundManager(int sampleRate = 44100, int channelCount = 2)
        {
            // Create a new instance of the WaveOut class (which plays sound using the default audio device)
            soundPlayer = new WaveOut();

            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));

            soundPlayer.Init(mixer);
            soundPlayer.Play();
        }
    }
}
