using GameDesignWorkshop.game.objects.particles;
using GameDesignWorkshop.game.objects.tiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GameDesignWorkshop.game.objects.scene
{
    class SceneManager
    {
        List<Scene> scenes = new List<Scene>();
        public void loadScenes()
        {
            // Add some scenes to the list
            scenes.Add(new Scene()
            {
                Name = "Scene 1",
                Tiles = new List<Tile>()
                {
                    //new Tile() { X = 0, Y = 0, SpriteIndex = 0 },
                    //new Tile() { X = 1, Y = 0, SpriteIndex = 1 },
                    // Other tiles...
                },
                ParticleSystems = new List<ParticleSystem>()
                {
                    new ParticleSystem()
                    {
                        //Position = new Vector2(0.5f, 0.5f),
                        EmissionRate = 10,
                        //Lifespan = 100,
                        // Other particle system properties...
                    },
                    // Other particle systems...
                }
            });

            // Other scenes...

            // Create a binary formatter to serialize the scenes
            BinaryFormatter formatter = new BinaryFormatter();

            // Open a file to write the serialized data to
            using (FileStream stream = new FileStream("scenes.bin", FileMode.Create))
            {
                // Serialize the scenes to the file
                formatter.Serialize(stream, scenes);
            }

            // Open the file to read the serialized data from
            using (FileStream stream = new FileStream("scenes.bin", FileMode.Open))
            {
                // Deserialize the scenes from the file
                scenes = (List<Scene>)formatter.Deserialize(stream);
            }

            // Load the tiles and particle systems from the first scene in the list
            Scene scene = scenes[0];

            // Load the tiles
            foreach (Tile tile in scene.Tiles)
            {
                // Create a new tile at the specified position and with the specified sprite index
                // ...
            }

            // Load the particle systems
            foreach (ParticleSystem system in scene.ParticleSystems)
            {
                // Create a
            }


        }

        // Create a binary formatter to deserialize the scenes from the file
        BinaryFormatter formatter = new BinaryFormatter();

        // Create a variable to store the current scene
        Scene currentScene = null;

        // Create a variable to store the target scene
        Scene targetScene = null;

        // Create a variable to store the fade value
        float fade = 0.0f;

        // Create a variable to store the fade duration
        float fadeDuration = 1.0f;

        // Create a variable to store the elapsed time since the fade started
        float fadeElapsed = 0.0f;

        // Create a variable to store the interpolation value
        float t = 0.0f;

        // Create a method to change the scene
        void ChangeScene(string name)
        {
            // Open the file to read the serialized data from
            using (FileStream stream = new FileStream("scenes.bin", FileMode.Open))
            {
                // Deserialize the scenes from the file
                List<Scene> scenes = (List<Scene>)formatter.Deserialize(stream);

                // Find the current scene in the list
                //currentScene = scenes.FirstOrDefault(s => s.Name == currentScene.Name);

                // Find the target scene in the list
                //targetScene = scenes.FirstOrDefault(s => s.Name == name);

                // Check if the target scene was found
                if (targetScene != null)
                {
                    // Set the fade value to 1.0
                    fade = 1.0f;

                    // Set the elapsed time to 0.0
                    fadeElapsed = 0.0f;
                }
            }
        }

        //ChangeScene("Scene 2");

        // Update the game objects
        void Update(float deltaTime)
        {
            // Check if the target scene is set
            if (targetScene != null)
            {
                // Update the elapsed time
                fadeElapsed += deltaTime;

                // Calculate the interpolation value
                t = Math.Min(1.0f, fadeElapsed / fadeDuration);

                // Interpolate the game objects between the current and target scenes
                // based on the fade value and the t interpolation value
                // For example:
                foreach (Tile tile in targetScene.Tiles)
                {
                    //Tile currentTile = currentScene.Tiles.FirstOrDefault(t => t.X == tile.X && t.Y == tile.Y);
                    //if (currentTile != null)
                    {
                        // Interpolate the sprite index
                        //tile.SpriteIndex = (int)Math.Lerp(currentTile.SpriteIndex, tile.SpriteIndex, t);
                    }
                }

                // Update the fade value
                fade = Math.Max(0.0f, 1.0f - t);

                // Check if the fade is complete
                if (fade == 0.0f)
                {
                    // Set the current scene to the target scene
                    currentScene = targetScene;

                    // Clear the target scene
                    targetScene = null;
                }
            }
        }

        // Draw the game objects
        void Draw()
        {
            // Draw the game objects
            // For example:
            foreach (Tile tile in currentScene.Tiles)
            {
                // Draw the tile
                // ...
            }

            // Check if the target scene is set
            if (targetScene != null)
            {
                // Save the current model view matrix
                //GL.PushMatrix();

                // Apply the fade effect to the rendered image
                // For example, you can use a shader program that applies a fade effect
                // based on the fade value:
                //GL.UseProgram(fadeShaderProgram);
               // GL.Uniform1(fadeUniformLocation, fade);

                // Draw the game objects
                // For example:
                foreach (Tile tile in targetScene.Tiles)
                {
                    // Draw the tile
                    // ...
                }

                // Restore the model view matrix
                //GL.PopMatrix();
            }
        }
    }
}
