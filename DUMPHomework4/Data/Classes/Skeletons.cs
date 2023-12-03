using System;

namespace Data.Classes
{
    public class Skeletons : Enemy
    {
        public Skeletons()
        {
            Random random = new Random();
            Name = "Skeleton";
            HealthPoints = random.Next(1, 6);
            Damage = random.Next(1, 6);
            Experience = random.Next(1, 6);
        }
    }
}
