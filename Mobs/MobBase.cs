using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobs
{
    public class MobBase
    {
        public string Name { get; protected set; }
        public float MovementSpeed { get; protected set; }

        public MobBase(string name, float movSpeed)
        {
            Name = name;
            MovementSpeed = movSpeed;
        }
    }
}
