using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes.Factories
{
    public static class TextureFactory
    {
        //OUTDATED
        private static int textureCursor = 0; // Cursor to keep track of the current texture slot

        public static Texture Load(string textureName)
        {
            Console.WriteLine("Loading texture: " + textureName);
            int handle = GL.GenTexture(); // Generate a texture handle
            Enum.TryParse(typeof(TextureUnit), $"Texture{textureCursor}", out var result); // Try to parse the texture unit based on the cursor

            if (result == null) // If parsing fails, indicating exceeding the maximum supported texture units
            {
                throw new Exception($"Exceeded the maximum number of texture slots natively supported by OpenGL. Count: {textureCursor}");
            }

            TextureUnit textureUnit = ((TextureUnit)result); // Convert the parsed result to a TextureUnit
            GL.ActiveTexture(textureUnit); // Activate the specified texture unit
            GL.BindTexture(TextureTarget.Texture2D, handle); // Bind the texture to the active texture unit

            using var image = new Bitmap(textureName); // Load the texture image using Bitmap
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); // Flip the image vertically
            var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                      ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb); // Lock the image data for processing

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0,
                          PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0); // Set the texture image data

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest); // Set texture minification filter
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest); // Set texture magnification filter
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat); // Set texture wrap mode for the S coordinate
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat); // Set texture wrap mode for the T coordinate

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D); // Generate mipmaps for the texture

            SetTextureCursor(GetTextureCursor() + 1); // Increment the texture cursor for the next texture

            return new Texture(handle, image.Width, image.Height, textureUnit); // Return the loaded texture as a Texture2D object
        }

        private static void SetTextureCursor(int cursor)
        {
            textureCursor = cursor;
        }

        private static int GetTextureCursor()
        {
            return textureCursor;
        }

        public static void UnloadTextures()
        {
            SetTextureCursor(0);
        }
    }

}
