using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Databases
    {
        public static GameDatabaseEntities Game { get; private set; }
        public static AccountsDatabaseEntities Accounts { get; private set; }

        static Databases()
        {
            Game = new GameDatabaseEntities();
            Accounts = new AccountsDatabaseEntities();
        }

        public static Region FindRegionModel(string name)
        {
            return (from region in Game.Regions where region.Name == name select region).FirstOrDefault();
        }

        public static IQueryable<Character> GetCharactersForAccount(string userName)
        {
            return from character in Game.Characters where character.UserName == userName select character;
        }
    }
}
