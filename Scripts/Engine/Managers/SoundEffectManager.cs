using GameDesignWorkshop.game;
using GameDesignWorkshop.resources.sounds;
using GameDesignWorkshop.Scripts.Engine.Sound;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Managers
{
    public class SoundEffectManager
    {
        private readonly SoundManager _soundManager;
        HashMap<string, CachedSound> sounds = new HashMap<string, CachedSound>();

        public SoundEffectManager(SoundManager soundManager)
        {
            this._soundManager = soundManager;
        }

        public void LoadSound(string key, string fileName)
        {
            // Create a new CachedSound object and add it to the dictionary
            CachedSound sound = new CachedSound(fileName);
            sounds.Put(key, sound);
        }

        // A method to play a sound effect from the cache
        public void PlaySoundEffect(string key)
        {
            // Get the CachedSound object from the dictionary
            CachedSound sound = sounds.Get(key);
            // Create a new WaveOut object to play the sound effect
            WaveOut sfxPlayer = new WaveOut(); //could be audioclip
            // Set the volume of the WaveOut object
            sfxPlayer.Volume = _soundManager.masterVolume * _soundManager.sfxVolume * sound.Volume;
            // Set the WaveOut object's output to the CachedSound's audio data
            sfxPlayer.Init(sound.AudioData);
            // Play the sound
            sfxPlayer.Play();

            //Dispose of the object after use
            //sfxPlayer.Dispose();
        }

    }
}
