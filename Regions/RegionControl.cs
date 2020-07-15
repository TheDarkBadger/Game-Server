using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Regions
{
    public static class RegionControl
    {
        public static Region[] LoadedRegions { get; private set; }

        static RegionControl()
        {
            LoadRegions();
        }

        public static void LoadRegions()
        {
            //TODO make regions loaded from files
            List<Region> loaded = new List<Region>();

            loaded.Add(new Region("TestRegion"));

            LoadedRegions = loaded.ToArray();
        }

        public static Region FindRegionByName(string name)
        {
            return (from r in LoadedRegions where r.Model.Name == name select r).FirstOrDefault();
        }
    }
}
