namespace Data.Classes
{
    public abstract class Hero
    {
        public int CriticalChance { get; set; }
        public int StunChance { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int Experience { get; set; }
        public int Damage { get; set; }
        public int Mana { get; set; }
        public abstract void RageAttack();
        public abstract void RegenerateMana();
    }
}
