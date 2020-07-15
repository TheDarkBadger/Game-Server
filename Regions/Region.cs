using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobs;
using Models;
using Utility;

namespace Regions
{
    public class Region
    {
        public Models.Region Model { get; private set; }
        public List<ActiveMob> ActiveMobs { get; private set; }

        public Region(string name)
        {
            Model = Databases.FindRegionModel(name);
            ActiveMobs = new List<ActiveMob>();
        }

        public void SpawnMob(MobBase toSpawn, Vector2 pos)
        {
            ActiveMob newMob = new ActiveMob(toSpawn, pos);
            ActiveMobs.Add(newMob);
        }
    }
}
