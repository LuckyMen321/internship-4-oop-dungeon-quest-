using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Classes
{
    public class Marksman : Hero
    {
        public Marksman()
        {
            Name = "Marksman";
            HealthPoints = 50;
            Experience = 0;
            Damage = 50;
            CriticalChance = 25;
            StunChance = 10;
        }
        public override void RegenerateMana()
        {
            throw new NotImplementedException();
        }
        public override void RageAttack()
        {
            throw new NotImplementedException();
        }
        public void LevelUp()
        {
            Experience -= 100;
            CriticalChance += 5;
            StunChance += 5;
        }
    }
}
