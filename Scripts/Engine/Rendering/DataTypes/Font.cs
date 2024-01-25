using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignLearningAppPrototype.Scripts.Engine.Rendering.DataTypes
{
    struct Character
    {
        uint textureID;
        Vector2 size;
        Vector2 bearing;
        uint advance;
    }
    internal class Font
    {
        Dictionary<char, Character> Characters;

    }
}
