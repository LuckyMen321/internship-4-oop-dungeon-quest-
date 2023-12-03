using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Classes
{
    public class Enchanter : Hero
    {
        public Enchanter()
        {
            Name = "Enchanter";
            HealthPoints = 25;
            Experience = 0;
            Damage = 75;
            Mana = 100;
        }
        
        public override void RageAttack()
        {
            throw new NotImplementedException();
        }
        public override void RegenerateMana()
        {
            if(Experience > 100)
            {
                Experience -= 100;
                Mana += 50;
            }
            Mana += 25;
        }
    }
}
