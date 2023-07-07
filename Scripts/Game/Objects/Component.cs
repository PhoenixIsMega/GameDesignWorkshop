using System;
using System.Collections.Generic;
using System.Text;

namespace GameDesignWorkshop.Scripts.Game.Objects
{
    interface Component
    {
        void Update();
        void Start();
        void OnDestroy();
    }
}
