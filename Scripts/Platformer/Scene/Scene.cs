using GameDesignWorkshop.game.objects.particles;
using GameDesignWorkshop.game.objects.tiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.game.objects.scene
{
    class Scene
    {
        public string Name { get; set; }
        public List<Tile> Tiles { get; set; }
        public List<ParticleSystem> ParticleSystems { get; set; }
    }
}
