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

            _soundManager.AddMixerInput(new CachedSoundSampleProviderHelperClass(sound));
        }

    }
}
