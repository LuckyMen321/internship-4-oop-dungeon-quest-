namespace Data.Classes
{
    public class Goblin : Enemy
    {
        public Goblin()
        {
            Random random = new Random();
            Name = "Goblin";
            HealthPoints = random.Next(5, 11);
            Damage = random.Next(5, 11);
            Experience = random.Next(5, 16);
        }
    }
}
