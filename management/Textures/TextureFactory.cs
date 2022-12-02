using GameDesignWorkshop.rendering;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace GameDesignWorkshop.management.Textures
{
    public static class TextureFactory
    {
        public static Texture2D Load(string textureName)
        {
            int handle = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);
            using var image = new Bitmap(textureName);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0, PixelInternalFormat.Rgba,
                image.Width, image.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            //change to linear if not pixel art, otherwise use
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest); //min?
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            //allows for textures to be sampled from lowres texture if far away (more useful for 3d but allows expansion and still worth doing as not difficult)
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            return new Texture2D(handle);
        }
    }
}
