namespace Data.Classes
{
    public class Brute : Enemy
    {
        public Brute()
        {
            Random random = new Random();
            Name = "Brute";
            HealthPoints = random.Next(25, 36);
            Damage = random.Next(15, 26);
            Experience = random.Next(15, 26);
        }
    }
}
