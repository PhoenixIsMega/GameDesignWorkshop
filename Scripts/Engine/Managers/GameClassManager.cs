using GameDesignWorkshop.management.Textures;
using GameDesignWorkshop.resources.sounds;
using GameDesignWorkshop.Scripts.Engine.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.game.managers
{
    class GameClassManager
    {
        //NOTE: Consider making all these classes IDisposable so you dont have to rely on the garbage colllector to clean the memory usage.

        //Main class
        //private MusicIntegration _main;

        //Other classes that need initialising
        //private readonly SpritesheetManager _spritesheetManager;

        private readonly SoundManager _soundManager;
        private readonly SoundEffectManager _soundEffectManager;
        private readonly MusicManager _musicManager;

        public GameClassManager()
        {
            this._soundManager = new SoundManager();
            this._soundEffectManager = new SoundEffectManager(this._soundManager);
            this._musicManager = new MusicManager(this._soundManager);
        }

        public SoundManager GetSoundManager()
        {
            return _soundManager;
        }

        public SoundEffectManager GetSoundEffectManager()
        {
            return _soundEffectManager;
        }

        public MusicManager GetMusicManager()
        {
            return _musicManager;
        }
    }
}
