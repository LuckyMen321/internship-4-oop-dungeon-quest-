namespace Data.Classes
{
    public class Gladiator : Hero
    {
        public Gladiator()
        {
            Name = "Gladiator";
            HealthPoints = 100;
            Experience = 0;
            Damage = 225;
        }
        public override void RegenerateMana()
        {
            throw new NotImplementedException();
        }
        public override void RageAttack()
        {
            HealthPoints -= (int)Math.Round(HealthPoints * 0.15);
            Damage = 50;
        }
    }
}
