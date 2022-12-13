using GameDesignWorkshop.game;
using GameDesignWorkshop.Scripts.Engine.Sound;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.resources.sounds
{
    public class SoundManager : IDisposable
    {
        //fix variable names + public etc
        private readonly WaveOutEvent outputDevice; //consider switching to waveout or Ipwavelayer
        //private WaveOut soundPlayer;

        public MixingSampleProvider mixer;

        public float masterVolume = 1.0f;
        public float musicVolume = 0.5f; //get from file next
        public float sfxVolume = 0.5f;

        // Crossfade in milliseconds?
        public float fadeOutStart;
        public float fadeInStart;

        public SoundManager(int sampleRate = 44100, int channelCount = 2)
        {
            // Create a new instance of the WaveOut class (which plays sound using the default audio device)
            //soundPlayer = new WaveOut();
            outputDevice = new WaveOutEvent();

            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;

            //soundPlayer.Init(mixer);
            //soundPlayer.Play();

            outputDevice.Init(mixer);
            outputDevice.Play();
        }

        public IWavePlayer GetMusicPlayer() { return outputDevice; }
        public void AddMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }
    }
}
