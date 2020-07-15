using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Mobs
{
    public class ActiveMob
    {
        public MobBase mobBase { get; private set; }
        public Vector2 pos { get; private set; }

        public ActiveMob(MobBase newBase, Vector2 newPos)
        {
            mobBase = newBase;
            pos = newPos;
        }

    }
}
