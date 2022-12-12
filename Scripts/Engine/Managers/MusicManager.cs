using GameDesignWorkshop.resources.sounds;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Managers
{
    public class MusicManager
    {
        private readonly SoundManager _soundManager;
        public AudioFileReader music;

        public Queue<AudioFileReader> musicQueue = new Queue<AudioFileReader>();

        public MusicManager(SoundManager soundManager)
        {
            this._soundManager = soundManager;
        }

        // A method to add a track to the music queue
        public void AddToQueue(string fileName)
        {
            // Add the track to the queue
            musicQueue.Enqueue(new AudioFileReader(fileName));
        }

        // Play music
        public void PlayMusic()
        {
            _soundManager.musicPlayer = new WaveOut();
            _soundManager.musicPlayer.Init(music);
            _soundManager.musicPlayer.Volume = _soundManager.musicVolume * _soundManager.masterVolume;
            _soundManager.musicPlayer.Play();
        }

        // A method to play the next track in the queue
        public bool PlayNextTrack() //make this into more of an update method
        {
            if (musicQueue.Count > 0)
            {
                // Crossfade
                /*
                _soundManager.fadeOutStart = DateTime.Now.Ticks;
                _soundManager.fadeInStart = _soundManager.fadeOutStart + (long)(_soundManager.fadeOutSpeed * TimeSpan.TicksPerSecond);
                while (DateTime.Now.Ticks < _soundManager.fadeOutStart + (long)(_soundManager.fadeOutSpeed * TimeSpan.TicksPerSecond))
                {
                    _soundManager.musicPlayer.Volume = _soundManager.masterVolume * _soundManager.musicVolume * (1 - (DateTime.Now.Ticks - _soundManager.fadeOutStart) / (float)(_soundManager.fadeOutSpeed * TimeSpan.TicksPerSecond));
                }*/
                music.Dispose();
                _soundManager.musicPlayer.Dispose();
                music = musicQueue.Dequeue();
                PlayMusic();
                return true;
                /*while (DateTime.Now.Ticks < _soundManager.fadeInStart + (long)(_soundManager.fadeInSpeed * TimeSpan.TicksPerSecond))
                { //change to doubles
                    _soundManager.musicPlayer.Volume = _soundManager.masterVolume * _soundManager.musicVolume * (DateTime.Now.Ticks - _soundManager.fadeInStart) / (float)(_soundManager.fadeInSpeed * TimeSpan.TicksPerSecond);
                }*/
            }
            else
            {
                // No music in queue - restart current track
                music.Position = 0;
                return true;
            }
        }

        // A method to stop the music
        public void StopMusic()
        {
            // Stop the music player
            _soundManager.musicPlayer.Stop();
        }
    }
}
