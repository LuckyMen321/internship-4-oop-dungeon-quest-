namespace Data.Classes
{
    public class Witch : Enemy
    {
        public Witch()
        {
            Random random = new Random();
            Name = "Witch";
            HealthPoints = random.Next(20, 31);
            Damage = random.Next(20, 31);
            Experience = random.Next(35, 51);
        }
    }
}