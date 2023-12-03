using Data.Classes;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

List<int> listOfEnemiesIndex = new();
List<Enemy> listOfEnemies= new();

Hero hero = null; 

Enemy enemy = null;

bool isCritChance = false;

int selfSetHPAmount = 0;
int healthPointsCap;
int whichEnemyType = 0;
int skeletonsCounter = 0;
int whichSuperAttackCount;
int stunCounter = 0;
int deathCountEnchanter;
int enemyType;
int heroType;
int roundNumber;
int heroAttack = 0;
int enemyAttack = 0;
void main()
{
    roundNumber = -1;
    whichSuperAttackCount = 0;
    deathCountEnchanter = 0;
    Console.Clear();
    Console.WriteLine("Odaberite kojeg heroja zelite izabrati:");
    Console.WriteLine("1 - Gladiator" +
        "\n2 - Enchanter" +
        "\n3 - Marksman");
    Console.WriteLine("Upisite jedan od ponudenih heroja");
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        switch (choice)
        {
            case 1:
                gladiatorChosen();
                break;
            case 2:
                enchanterChosen();
                break;
            case 3:
                marksmanChosen();
                break;
            default:
                Console.WriteLine("Krivo unesen heroj! Pritisnite 'enter' za vracanje na pocetni zaslon");
                Console.ReadLine();
                main();
                break;
        }
    }
    else
    {
        Console.WriteLine("Krivo unesen heroj! Pritisnite 'enter' za vracanje na pocetni zaslon");
        Console.ReadLine();
        main();
    }
    generateEnemies();
    playRound();
}
void selfSetHp()
{
    Console.Clear();
    Console.WriteLine("Zelite li sami postaviti svoj HP? 'yes' ako zelite.");
    string ifSelfSetHP = Console.ReadLine();
    if (ifSelfSetHP == "yes")
    {
        Console.WriteLine("Upisite iznos:");
        string selfSetHPAmountString = Console.ReadLine();
        if (int.TryParse(selfSetHPAmountString, out selfSetHPAmount))
        {
            if (selfSetHPAmount > 0)
            {
                hero.HealthPoints = selfSetHPAmount;
            }
            else
            {
                Console.WriteLine("Upisite cijeli broj koji nije negativan ili 0. Prvo pritisnite enter.");
                Console.ReadLine();
                selfSetHp();
            }
        }
        else
        {
            Console.WriteLine("Upisite cijeli broj koji nije negativan ili 0. Prvo pritisnite enter.");
            Console.ReadLine();
            selfSetHp();
        }
    }
}
void gladiatorChosen()
{
    hero = new Gladiator();
    heroType = 1;
    selfSetHp();
    healthPointsCap = hero.HealthPoints;
}
void enchanterChosen()
{
    hero = new Enchanter();
    heroType = 2;
    selfSetHp();
    healthPointsCap = hero.HealthPoints;
}
void marksmanChosen()
{
    hero = new Marksman();
    heroType = 3;
    selfSetHp();
    healthPointsCap = hero.HealthPoints;
}
void generateEnemies()
{
    listOfEnemiesIndex.Clear();
    listOfEnemies.Clear();
    for (int i = 0; i < 10; i++)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 100);
        listOfEnemiesIndex.Add(randomNumber);
    }
}
void playRound()
{
    increaseStatsByXP();
    hero.HealthPoints += (int)(healthPointsCap * 0.25);
    if (heroType == 2)
    {
        hero.Mana += 10;
    }
    roundNumber++;
    if (listOfEnemiesIndex[roundNumber] < 16)
    {
        listOfEnemies.Add(new Witch());
        enemy = listOfEnemies[roundNumber];
        if(whichSuperAttackCount > 0)
        {
            Random random = new Random();
            enemy.HealthPoints = random.Next(1, 101);
        }
        enemyType = 3;
        whichEnemyType = 3;
    }
    else if(listOfEnemiesIndex[roundNumber] < 46)
    {
        listOfEnemies.Add(new Brute());
        enemy = listOfEnemies[roundNumber];
        if (whichSuperAttackCount > 0)
        {
            Random random = new Random();
            enemy.HealthPoints = random.Next(1, 101);
        }
        enemyType = 2;
    }
    else
    {
        listOfEnemies.Add(new Goblin());
        enemy = listOfEnemies[roundNumber];
        if (whichSuperAttackCount > 0)
        {
            Random random = new Random();
            enemy.HealthPoints = random.Next(1, 101);
        }
        enemyType = 1;
    }
    if(hero.HealthPoints + 25 > healthPointsCap) 
    {
        hero.HealthPoints = healthPointsCap;
    }
    else
    {
        hero.HealthPoints += 25;
    }
    regenerateHealth();
    stateOfGame();
}
void increaseStatsByXP()
{
    if(hero.Experience > 99)
    {
        hero.Experience -= 100;
        hero.HealthPoints += 25;
        healthPointsCap += 25;
        hero.Damage += 10;
        hero.CriticalChance += 10;
        hero.StunChance += 10;
        hero.Mana += 25;
    }
}
void regenerateHealth()
{
    if(hero.Experience > 49)
    {
        Console.WriteLine("Zelite li utrositi 50XP za obnovu cijelog HP? 'yes' ako zelite.");
        string gainHealthForXP = Console.ReadLine();
        if (gainHealthForXP == "yes")
        {
            hero.Experience -= 50;
            hero.HealthPoints = healthPointsCap;
            stateOfGame();
        }
        else
        {
            stateOfGame();
        }
    }
}
void stateOfGame()
{
    Console.Clear();
    Console.WriteLine($"Ovo je Vas heroj:" +
        $"\nIme: {hero.Name}" +
        $"\nHP: {hero.HealthPoints}" +
        $"\nXP: {hero.Experience}" +
        $"\nDamage: {hero.Damage}" +
        $"\nMana: {hero.Mana}");
    Console.WriteLine();

    Console.WriteLine($"Ovo je Vas protivnik:" +
        $"\nTip: {enemy.Name}" +
        $"\nHP: {enemy.HealthPoints}" +
        $"\nDamage: {enemy.Damage}" +
        $"\nXP: {enemy.Experience}" );
    Console.WriteLine();

    Console.WriteLine("Odaberite kojom vrstom zelite napasti protivnika:" +
        "\n1 - Direktan napad" +
        "\n2 - Napad s boka" +
        "\n3 - Protunapad");
    Attack();
}
void attackGladiator()
{
    Console.Clear();
    Console.WriteLine("Vas napad je pobijedio!");
    Console.WriteLine("Zelite li napasti rageani? Upisite 'yes' ili 'no'");
    string chooseAttackGladiatorString = Console.ReadLine();
    if(chooseAttackGladiatorString == "yes")
    {
        hero.RageAttack();
        enemy.HealthPoints -= hero.Damage;
        hero.Damage = 25;
    }
    else if(chooseAttackGladiatorString == "no")
    {
        enemy.HealthPoints -= hero.Damage;
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Krivi upis. Upisite 'yes' ili 'no'. Prvo pritisnite enter.");
        Console.ReadLine();
        attackGladiator();
    }
    checkIfDead();
}
void attackMarksman()
{
    Console.Clear();

    Random random = new Random();
    int stunChance = random.Next(1, 101);
    int critChance = random.Next(1, 101);

    if (stunChance <= hero.StunChance)
    {
        stunCounter++;
    }
    if (critChance <= hero.CriticalChance)
    {
        isCritChance = true;
        enemy.HealthPoints -= hero.Damage * 2;
    }
    else
    {
        enemy.HealthPoints -= hero.Damage;
    }
    if(isCritChance)
    {
        Console.WriteLine($"Vas napad je pobijedio! Nanijeli ste {hero.Damage * 2} stete.");
        isCritChance = false;
    }
    else 
    { 
        Console.WriteLine($"Vas napad je pobijedio! Nanijeli ste {hero.Damage} stete.");
    }
    checkIfDead();
}
void attackEnchanter()
{
    Console.Clear();
    Console.WriteLine("Vas napad je pobijedio!");
    if(hero.Mana < 25)
    {
        Console.WriteLine("Nemate dovoljno mana za napasti niti regenerirate HP. Regenerirali ste 50 mana.");
        hero.Mana += 50;
        Console.WriteLine("Pritisnite enter za otici natrag.");
        Console.ReadLine();
        stateOfGame();
    }
    Console.WriteLine("Zelite li napasti za 75 damage ili regenerirati 30 HP za 50 mana?" +
        "\n1 - Napasti" +
        "\n2 - Utrošiti");
    string chooseAttackEnchanterString = Console.ReadLine();
    if (int.TryParse(chooseAttackEnchanterString, out int chooseAttackEnchanter))
    {
        if (chooseAttackEnchanter == 1)
        {
            enemy.HealthPoints -= hero.Damage;
            hero.Mana -= 50;
        }
        else if (chooseAttackEnchanter == 2)
        {
            hero.Mana -= 50;
            if (hero.HealthPoints + 30 > healthPointsCap)
            {
                hero.HealthPoints = healthPointsCap;
            }
            else
            {
                hero.HealthPoints += 30;
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Krivi upis. Upisite '1' ili '2'. Prvo pritisnite enter.");
            Console.ReadLine();
            attackEnchanter();
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Krivi upis. Upisite '1' ili '2'. Prvo pritisnite enter.");
        Console.ReadLine();
        attackEnchanter();
    }
    checkIfDead();
}
void attackGoblin()
{
    Console.Clear();
    Console.WriteLine("Vas napad je izgubio! Idite na sljedecu rundu pritiskom enter.");

    hero.HealthPoints -= enemy.Damage;
    
    Console.ReadLine();
    
    checkIfDead();
}
void attackBrute()
{
    Console.Clear();
    Console.WriteLine("Vas napad je izgubio!  Idite na sljedecu rundu pritiskom enter.");

    Random random = new Random();
    int bruteSuperAttackChance = random.Next(1, 101);

    if(bruteSuperAttackChance < 26)
    {
        hero.HealthPoints -= (int)Math.Round(hero.HealthPoints * 0.35);
    }
    else
    {
        hero.HealthPoints -= enemy.Damage;
    }

    Console.ReadLine();

    checkIfDead();
}
void attackWitch()
{
    Console.Clear();
    Console.WriteLine("Vas napad je izgubio!  Idite na sljedecu rundu pritiskom enter.");

    Random random = new Random();
    int witchSuperAttackChance = random.Next(1, 101);

    if (witchSuperAttackChance < 11)
    {
        Console.WriteLine("DUMBUS AKTIVIRAN! Svima je health randomiziran (1 - 100). Pritisnite enter.");
        whichSuperAttackCount++;
        hero.HealthPoints = random.Next(1, 101);
        enemy.HealthPoints = random.Next(1, 101);
    }
    else
    {
        hero.HealthPoints -= enemy.Damage;
        Console.WriteLine("Pritisnite enter.");
    }
    Console.ReadLine();

    checkIfDead();
}
void attackSkeletons()
{
    Console.Clear();
    Console.WriteLine("Vas napad je izgubio! Pritisnite enter.");

    hero.HealthPoints -= enemy.Damage;

    Console.ReadLine();

    checkIfDead();
}
void Attack()
{
    if (stunCounter > 0)
    {
        stunCounter = 0;
        Console.Clear();
        Console.WriteLine("Stunnali ste cudoviste! Napali ste ga bez mogucnosti da se obrani. Pritisnite enter.");
        Console.ReadLine();
        attackMarksman();
    }
    if (int.TryParse(Console.ReadLine(), out heroAttack) == false)
    {
        Console.WriteLine("Unesite tocan napad. Pritisnite enter.");
        Console.ReadLine();
        stateOfGame();
    }

    Random random = new Random();
    enemyAttack = random.Next(1, 4);

    switch (heroAttack)
    {
        case 1:
            switch (enemyAttack)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Oba napada su jednaka! Idite na sljedecu rundu pritiskom 'enter'");
                    Console.ReadLine();
                    stateOfGame();
                    break;
                case 2:
                    Console.WriteLine("Vas napad je pobijedio! Idite na sljedecu rundu pritiskom 'enter'");
                    if (heroType == 1)
                    {
                        attackGladiator();
                    }
                    if (heroType == 2)
                    {
                        attackEnchanter();
                    }
                    if (heroType == 3)
                    {
                        attackMarksman();
                    }
                    break;
                case 3:
                    Console.WriteLine("Vas napad je izgubio! Idite na sljedecu rundu pritiskom 'enter'");
                    if (enemyType == 1)
                    {
                        attackGoblin();
                    }
                    if (enemyType == 2)
                    {
                        attackBrute();
                    }
                    if (enemyType == 3)
                    {
                        attackWitch();
                    }
                    if (enemyType == 4)
                    {
                        attackSkeletons();
                    }
                    break;
            }
            break;

        case 2:
            switch (enemyAttack)
            {
                case 1:
                    Console.WriteLine("Vas napad je izgubio! Idite na sljedecu rundu pritiskom 'enter'");
                    if (enemyType == 1)
                    {
                        attackGoblin();
                    }
                    if (enemyType == 2)
                    {
                        attackBrute();
                    }
                    if (enemyType == 3)
                    {
                        attackWitch();
                    }
                    if (enemyType == 4)
                    {
                        attackSkeletons();
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Oba napada su jednaka! Idite na sljedecu rundu pritiskom 'enter'");
                    Console.ReadLine();
                    stateOfGame();
                    break;
                case 3:
                    Console.WriteLine("Vas napad je pobijedio! Idite na sljedecu rundu pritiskom 'enter'");
                    if (heroType == 1)
                    {
                        attackGladiator();
                    }
                    if (heroType == 2)
                    {
                        attackEnchanter();
                    }
                    if (heroType == 3)
                    {
                        attackMarksman();
                    }
                    break;
            }
            break;

        case 3:
            switch (enemyAttack)
            {
                case 1:
                    Console.WriteLine("Vas napad je pobijedio! Idite na sljedecu rundu pritiskom 'enter'");
                    if (heroType == 1)
                    {
                        attackGladiator();
                    }
                    if (heroType == 2)
                    {
                        attackEnchanter();
                    }
                    if (heroType == 3)
                    {
                        attackMarksman();
                    }
                    break;
                case 2:
                    Console.WriteLine("Vas napad je izgubio! Idite na sljedecu rundu pritiskom 'enter'");
                    if (enemyType == 1)
                    {
                        attackGoblin();
                    }
                    if (enemyType == 2)
                    {
                        attackBrute();
                    }
                    if (enemyType == 3)
                    {
                        attackWitch();
                    }
                    if (enemyType == 4)
                    {
                        attackSkeletons();
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Oba napada su jednaka! Idite na sljedecu rundu pritiskom 'enter'");
                    Console.ReadLine();
                    stateOfGame();
                    break;
            }
            break;
        default:
            Console.WriteLine("Unesite tocan napad. Pritisnite enter.");
            Console.ReadLine();
            stateOfGame();
            break;
    }
}
void playRoundSkeletons()
{
    skeletonsCounter++;
    enemy = new Skeletons();
    if (whichSuperAttackCount > 0)
    {
        Random random = new Random();
        enemy.HealthPoints = random.Next(1, 101);
    }
    enemyType = 4;
    stateOfGame();
}
void checkIfDead()
{
    if (hero.HealthPoints > 0 && enemy.HealthPoints > 0)
    {
        stateOfGame();
    }
    else if (hero.HealthPoints <= 0)
    {
        if (heroType == 2 && deathCountEnchanter < 1)
        {
            hero.HealthPoints = selfSetHPAmount;
            hero.Experience = 0;
            hero.Damage = 75;
            hero.Mana = 100;
            Console.Clear();
            Console.WriteLine("UMRLI STE!!! Ali nebrinite, Vaš dodatni život Vas je vratio u bitku! Pritisnite enter.");
            Console.ReadLine();
            deathCountEnchanter++;
            stateOfGame();
        }
        Console.WriteLine("IGRA GOTOVA! Unesite 'yes' ako zelite igrati ponovno");
        if(Console.ReadLine() == "yes")
        {
            main();
        }
        else
        {
            Environment.Exit(0);
        }
    }
    else if(enemy.HealthPoints <= 0 && roundNumber < 9)
    {
        hero.Experience += enemy.Experience;
        if (whichEnemyType == 3 && skeletonsCounter < 2) 
        {
            Console.WriteLine("Pobijedili ste protivnika, ali je on stvorio jos 2 svoja saveznika! Pritisnite enter za nastavak borbe!");
            Console.ReadLine();
            playRoundSkeletons();
        }
        whichEnemyType = 0;
        Console.WriteLine("Pobijedili ste protivnika! Pritisnite enter za nastavak borbe!");
        Console.ReadLine();
        playRound();
    }
    else
    {
        if(enemyType == 3 || whichEnemyType == 3)
        {
            if (whichEnemyType == 3 && skeletonsCounter < 2)
            {
                Console.WriteLine("Pobijedili ste protivnika, ali je on stvorio jos 2 svoja saveznika! Pritisnite enter za nastavak borbe!");
                Console.ReadLine();
                playRoundSkeletons();
            }
            whichEnemyType = 0;
        }
        Console.WriteLine("POBIJEDILI STE SVE PROTIVNIKE! Cestitamo hrabri heroju!");
        Environment.Exit(0);
    }
}
main();