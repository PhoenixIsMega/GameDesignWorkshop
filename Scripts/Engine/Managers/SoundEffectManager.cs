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
        HashMap<string, CachedSoundEffect> sounds = new HashMap<string, CachedSoundEffect>();

        public SoundEffectManager(SoundManager soundManager)
        {
            this._soundManager = soundManager;
        }

        public void LoadSound(string key, string fileName)
        {
            // Create a new CachedSound object and add it to the dictionary
            CachedSoundEffect sound = new CachedSoundEffect(fileName);
            sounds.Put(key, sound);
        }

        // A method to play a sound effect from the cache
        public void PlaySoundEffect(string key)
        {
            // Get the CachedSound object from the dictionary
            CachedSoundEffect sound = sounds.Get(key);

            if (sound == null)
            {
                Console.WriteLine("Error: sound not found");
                return;
            }

            // Create a new WaveOut object to play the sound effect
            //WaveOut sfxPlayer = new WaveOut(); //could be audioclip
            // Set the volume of the WaveOut object
            //sfxPlayer.Volume = _soundManager.masterVolume * _soundManager.sfxVolume * sound.Volume;
            // Set the WaveOut object's output to the CachedSound's audio data
            //sfxPlayer.Init(sound);
            // Play the sound
            //sfxPlayer.Play();

            _soundManager.AddMixerInput(new CachedSoundSampleProviderHelperClass(sound));

            //or just add to mixe

            //Dispose of the object after use
            //sfxPlayer.Dispose();
        }

    }
}
