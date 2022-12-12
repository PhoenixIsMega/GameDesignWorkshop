using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Engine.Sound
{
    public class Playlist
    {
        // The list of AudioFileReader objects that will be stored in the playlist
        private List<AudioFileReader> items;

        // Constructor for the Playlist class
        public Playlist()
        {
            // Initialize the list of items
            items = new List<AudioFileReader>();
        }

        // Method to add an item to the playlist
        public void Add(AudioFileReader item)
        {
            // Add the item to the list of items
            items.Add(item);
        }

        // Method to remove an item from the playlist
        public void Remove(AudioFileReader item)
        {
            // Remove the item from the list of items
            items.Remove(item);
        }

        // Method to get the number of items in the playlist
        public int Count
        {
            get
            {
                // Return the number of items in the list
                return items.Count;
            }
        }
    }
}
