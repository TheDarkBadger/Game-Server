using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources;
using Utility;

namespace Mobs
{
    public static class MobControl
    {
        public static MobBase rabbit;

        static MobControl()
        {
            SetupMobs();
        }

        private static void SetupMobs()
        {
            rabbit = new MobBase(ResMobs.Rabbit, 3);
        }
    }
}
