using System;

namespace RPG_Gra___PP
{
  public abstract class Hero
  {
    public string Name;
    public int Strength;
    public int Dexterity;
    public int HP = 100;
    public int Stamina = 100;
    public string ClassName;
    public int Stun = 0;
    public string firstAbilityName;
    public string secondAbilityName;
    public string thirdAbilityName;
    public int specialty;
    public string specialtyName;
    public int burnDamage = 4;
    public int burnTurn = 0;
    public abstract int newRage(); //Tylko dla wojownika, najprostszy sposob na jaki wpadlem
    public abstract void FirstAbility(Hero target);
    public abstract void SecondAbility(Hero target);
    public abstract void ThirdAbility(Hero target);
    //Atak
    public void NormalAttack(Hero target){
      Random rand = new Random();
      int damage = Strength * rand.Next(5, 10) / 10;
      int staminaDrain = 5;
      if(Stamina - staminaDrain >= 0){
        if(rand.Next(0, 50) > target.Dexterity)
        {
          Console.WriteLine($"Zadano {damage} obrażeń mieczem!\n");
          target.HP -= damage;
        }
        else Console.WriteLine("Unik!\n");
        Stamina -= staminaDrain;
      }
      else {
        Console.Write("Wymagałeś odpoczynku! ");
        Rest(); 
      }
    }
    public void Rest() {
      //Odpoczynek
      Console.WriteLine("Odpocząłeś!\n");
      int staminaUp = 30;
      if(Stamina + staminaUp > 100) Stamina = 100;
      else Stamina += staminaUp;
    }

    public abstract string StatsTable();
  }
  //WOJOWNIK - KLASA BOHATERA ---------------------------------------------------------------------------------------------------------------------------------------
  public class Warrior : Hero {
    public Warrior(string _Name){
      this.Name = _Name;
      this.Strength = 8;
      this.Dexterity = 4;
      this.ClassName = "Wojownik";
      this.firstAbilityName = "Atak Mieczem (10 stamina)";
      this.secondAbilityName = "Atak Buzdyganem (15 stamina)";
      this.thirdAbilityName = "Pchnięcie";
      this.specialty = newRage();
      this.specialtyName = "Złość";
    }
    
    public override int newRage(){ //mapowanie
      int newRage, oldRage = HP, oldMin = 100, oldMax = 1, newMin = 1, newMax = 5, oldRange, newRange;
      oldRange = (oldMax - oldMin);
      if (oldRange == 0)
          newRage = newMin;
      else
      {
          newRange = (newMax - newMin);
          newRage = (((oldRage - oldMin) * newRange) / oldRange) + newMin;
      }
      return newRage;
    }
    
    public override void FirstAbility(Hero target) {
      //Atak mieczem (1.5x damage, 2x stamina, damage * rage)
      Random rand = new Random();
      int damage = Strength * rand.Next(5, 10) / 10;
      damage *= (int)1.5f;
      damage *= specialty = newRage();
      int staminaDrain = 5;
      staminaDrain *= (int)2f;
      if(Stamina - staminaDrain >= 0){
        if(rand.Next(0, 80) > target.Dexterity)
        {
          Console.WriteLine($"Zadano {damage} obrażeń mieczem!\n");
          target.HP -= damage;
        }
        else Console.WriteLine("Unik!\n");
        Stamina -= staminaDrain;
      }
      else {
        Console.Write("Wymagałeś odpoczynku! ");
        Rest(); 
      }
    }
    public override void SecondAbility(Hero target) {
      //Atak buzdyganem (1x damage, 3x stamina, damage * (rage * 1.5f))
      Random rand = new Random();
      int damage = Strength * rand.Next(5, 10) / 10;
      damage *= (int)1f;
      damage *= specialty = newRage() * (int)1.5f;
      int staminaDrain = 5;
      staminaDrain *= (int)3f;
      if(Stamina - staminaDrain >= 0){
        if(rand.Next(0, 80) > target.Dexterity)
        {
          Console.WriteLine($"Zadano {damage} obrażeń buzdyganem!\n");
          target.HP -= damage;
        }
        else Console.WriteLine("Unik!\n");
        Stamina -= staminaDrain;
      }
      else {
        Console.WriteLine("Wymagałeś odpoczynku!"); 
        Rest(); 
      }
    }
    public override void ThirdAbility(Hero target) {
      //Pchnięcie
      Random rand = new Random();
      if(target.Stun == 0) {
        if(rand.Next(0, 20) > target.Dexterity){
          target.Stun += 2;
          Console.WriteLine("Oszołomiono przeciwnika!\n");
        }
        else Console.WriteLine("Nie udało się oszołomić przeciwnika!\n");
      } else Console.WriteLine("Nie możesz oszołomić oszołomionego!");
    }
    public override string StatsTable() {
      return String.Format($"{Name, -20} | {ClassName,-12} | {Strength,-10} | {Dexterity,-10} | {Stamina,-12} | {HP,-10} | {Stun,-12} | {burnTurn,-11} | {specialtyName}: {specialty,-12}");
    }
  }
  //SKRYTOBÓJCA - KLASA BOHATERA ---------------------------------------------------------------------------------------------------------------------------------------
  public class Assassin : Hero {
    public Assassin(string _Name){
      this.Name = _Name;
      this.Strength = 4;
      this.Dexterity = 8;
      this.ClassName = "Skrytobójca";
      this.firstAbilityName = "Rzut Shurikenem (5 stamina, 15 energy)";
      this.secondAbilityName = "Atak Krótkimi Mieczami (5 stamina, 30 energy)";
      this.thirdAbilityName = "Podwójny atak (15 stamina, 15 energy)";
      this.specialty = 100;
      this.specialtyName = "Energia";
    }
    public override int newRage(){return 0;}
    public bool usedThirdAbility = false;
    public override void FirstAbility(Hero target) {
      //Rzut shurikenem (1x damage, 1x stamina drain, 1x energy cost)
      Random rand = new Random();
      int damage = Strength * rand.Next(6, 10) / 10;
      damage *= (int)1f;
      if(usedThirdAbility == true) { damage *= 2; usedThirdAbility = false; }
      int staminaDrain = 5;
      staminaDrain *= (int)1f;
      int energyDrain = 15;
      energyDrain *= (int)1f;
      if(specialty - energyDrain >= 0){
        if(Stamina - staminaDrain >= 0){
          if(rand.Next(0, 50) > target.Dexterity)
          {
            Console.WriteLine($"Zadano {damage} obrażeń shurikenem!\n");
            target.HP -= damage;
          }
          else Console.WriteLine("Unik!\n");
          Stamina -= staminaDrain;
          specialty -= energyDrain;
        }
        else {
          Console.Write("Wymagałeś odpoczynku! ");
          Rest();
          specialty = 0;
        }
      }
      else {
        Console.WriteLine("Nie starczyło ci energii!");
        specialty = 0;
      }
    }
    public override void SecondAbility(Hero target) {
      //Atak krótkimi mieczami (1x damage, 1x stamina drain, 2x energy cost, 50% chance to double attack)
      Random rand = new Random();
      int damage = Strength * rand.Next(6, 10) / 10;
      damage *= (int)1f;
      if(usedThirdAbility == true) { damage *= 2; usedThirdAbility = false; }
      int staminaDrain = 5;
      staminaDrain *= (int)1f;
      int energyDrain = 15;
      energyDrain *= (int)2f;
      bool canDoubleAttack = true;
      if(specialty - energyDrain >= 0){
        if(Stamina - staminaDrain >= 0){
          if(rand.Next(0, 50) > target.Dexterity)
          {
            Console.WriteLine($"Zadano {damage} obrażeń krótkimi mieczami!\n");
            target.HP -= damage;
          }
          else Console.WriteLine("Unik!\n");
          Stamina -= staminaDrain;
          specialty -= energyDrain;
        }
        else {
          Console.Write("Wymagałeś odpoczynku! ");
          Rest();
          specialty = 0;
          canDoubleAttack = false;
        }
        if(rand.Next(1,2) == 1 && canDoubleAttack == true){ //Podwójny atak
          if(rand.Next(0, 50) > target.Dexterity)
          {
            Console.WriteLine($"Udało się drugi raz zadać {damage} obrażeń krótkimi mieczami!\n");
            target.HP -= damage;
          }
        }
      }
      else {
        Console.WriteLine("Nie starczyło ci energii!");
        specialty = 0;
      }
    }
    public override void ThirdAbility(Hero target) {
      //Skrycie - kolejny atak zada podwójne obrażenia (3x stamina cost, 1x energy cost)
      Random rand = new Random();
      int staminaDrain = 5;
      staminaDrain *= (int)3f;
      int energyDrain = 15;
      energyDrain *= (int)1f;
      if(specialty - energyDrain >= 0){
        if(Stamina - staminaDrain >= 0){
          usedThirdAbility = true;
          Console.WriteLine("Następny atak zada podwójne obrażenia\n");
          specialty -= energyDrain;
          Stamina -= staminaDrain;
        }
        else {
          Console.Write("Wymagałeś odpoczynku! ");
          Rest();
          specialty = 0;
        }
      }
      else {
        Console.WriteLine("Nie starczyło ci energii!");
        specialty = 0;
      }
    }
    public override string StatsTable() {
      return String.Format($"{Name, -20} | {ClassName,-12} | {Strength,-10} | {Dexterity,-10} | {Stamina,-12} | {HP,-10} | {Stun,-12} | {burnTurn,-11} | {specialtyName}: {specialty,-12}");
    }
  }
  //MAG - KLASA BOHATERA ---------------------------------------------------------------------------------------------------------------------------------------
  public class Mage : Hero {
    public Mage(string _Name){
      this.Name = _Name;
      this.Strength = 6;
      this.Dexterity = 6;
      this.ClassName = "Mag";
      this.firstAbilityName = "Magiczny pocisk (5 stamina, 10 mana)";
      this.secondAbilityName = "Kula ognia (15 stamina, 20 mana)";
      this.thirdAbilityName = "Wzmocnienie (20 stamina, cała obecna mana)";
      this.specialty = 100;
      this.specialtyName = "Mana";
    }
    public bool usedThirdAbility = false;
    public int empoweredDamage = 1;
    public override int newRage(){return 0;}
    public override void FirstAbility(Hero target) {
      //Magiczny pocisk (2x damage, 1x stamina, 1x mana drain)
      Random rand = new Random();
      int damage = Strength * rand.Next(7, 10) / 10;
      damage *= (int)2f;
      int manaDrain = 10;
      manaDrain *= (int)1f;
      if(usedThirdAbility == true) { manaDrain = 0; damage *= empoweredDamage; usedThirdAbility = false; }
      int staminaDrain = 5;
      staminaDrain *= (int)1f;
      if(specialty - manaDrain >= 0){
        if(Stamina - staminaDrain >= 0){
          if(rand.Next(0, 50) > target.Dexterity)
          {
            Console.WriteLine($"Zadano {damage} obrażeń magicznym pociskiem!\n");
            target.HP -= damage;
          }
          else Console.WriteLine("Unik!\n");
          Stamina -= staminaDrain;
          specialty -= manaDrain;
        }
        else {
          Console.Write("Wymagałeś odpoczynku! ");
          Rest();
        }
      }
      else {
        Console.WriteLine("Nie starczyło ci many!");
      }
    }
    public override void SecondAbility(Hero target) {
      //Kula ognia - zadaje obrazenia i podpala (2x damage, 4 burn damage, 3x stamina, 2x mana drain)
      Random rand = new Random();
      int damage = Strength * rand.Next(8, 10) / 10;
      damage *= (int)2f;
      int manaDrain = 10;
      manaDrain *= (int)2f;
      if(usedThirdAbility == true) { manaDrain = 0; damage *= empoweredDamage; usedThirdAbility = false; }
      int staminaDrain = 5;
      staminaDrain *= (int)3f;
      if(specialty - manaDrain >= 0){
        if(Stamina - staminaDrain >= 0){
          if(rand.Next(0, 50) > target.Dexterity)
          {
            Console.WriteLine($"Zadano {damage} obrażeń kulą ognia oraz podpalono zadając {burnDamage} obrażeń przez następne dwie rundy!\n");
            target.HP -= damage;
            target.burnTurn = 2;
          }
          else Console.WriteLine("Unik!\n");
          Stamina -= staminaDrain;
          specialty -= manaDrain;
        }
        else {
          Console.Write("Wymagałeś odpoczynku! ");
          Rest();
        }
      }
      else {
        Console.WriteLine("Nie starczyło ci many!");
      }
    }
    public override void ThirdAbility(Hero target) {
      //Wzmocnienie - zwieksza obrazenia nastepnej umiejetnosci(w zaleznosci od uzytej many) (4x stamina)
      Random rand = new Random();
      int staminaDrain = 5;
      staminaDrain *= (int)4f;
      if(specialty > 0){
        if(Stamina - staminaDrain >= 0){
          usedThirdAbility = true;
          empoweredDamage = (int)(((specialty / 100f) * 3.5f) + 0.5f);
          Console.WriteLine("Wzmocnienie: " + empoweredDamage);
          Console.WriteLine("Wybierz atak, którego obrażenia chcesz wzmocnić: 1 - Magiczny pocisk, 2 - Kula ognia");
          int input = userInput(2);
          Console.WriteLine();
          switch(input){ case 1: FirstAbility(target); break; case 2: SecondAbility(target); break; }
          specialty = 0;
          Stamina -= staminaDrain;
        }
        else {
          Console.Write("Wymagałeś odpoczynku! ");
          Rest();
        }
      }
      else {
        Console.WriteLine("Nie masz many!");
      }
    }
    public override string StatsTable() {
      return String.Format($"{Name, -20} | {ClassName,-12} | {Strength,-10} | {Dexterity,-10} | {Stamina,-12} | {HP,-10} | {Stun,-12} | {burnTurn,-11} | {specialtyName}: {specialty,-12}");
    }
    int userInput(int maxValue){
      int input;
      while (true) {
        try {
          input = int.Parse(Console.ReadLine());
          if(input >= 1 && input <= maxValue) break;
          else Console.WriteLine("Niepoprawnie wprowadzona wartość!");
        }
        catch (Exception) {
            Console.WriteLine("Niepoprawnie wprowadzona wartość!");
        }
      }
      return input;
    }
  }

  class Program
  {
    //Wprowadzenie nowego bohatera
    public static Hero NewHero(Hero hero, string heroNumber){
      Wstawka();
      Console.Write($"Graczu {heroNumber}, podaj imię (max. 20 znaków): ");
      string imie = nameInput(20);
      Console.WriteLine("\n1 - Wojownik, 2 - Skrytobójca, 3 - Mag");
      Console.Write($"Graczu {heroNumber}, wybierz klasę: ");
      switch(userInput(3)){
        case 1:
          hero = new Warrior(imie);
          break;
        case 2:
          hero = new Assassin(imie);
          break;
        case 3:
          hero = new Mage(imie);
          break;
      }
      Console.Clear();
      return hero;
    }
    //Wstawka
    private static void Wstawka(){
      Console.WriteLine("Klasy bohaterów:\n");
      Console.WriteLine(">Wojownik: Siła - 8, Zręczność - 4");
      Console.WriteLine("-Złość - im niższy poziom zdrowia, tym więcej zadaje obrażeń z umiejętności (max. 5 przy HP = 1)");
      Console.WriteLine("-Atak mieczem (1,5x damage, 2x stamina cost, 1x rage)");
      Console.WriteLine("-Atak buzdyganem (1x damage, 3x stamina cost, 1,5x rage)");
      Console.WriteLine("-Pchnięcie - oszołomienie przeciwnika(Sukces zależy od zręczności przeciwnika)\n");
      Console.WriteLine(">Skrytobójca: Siła - 4, Zręczność - 8");
      Console.WriteLine("-Energia - pozwala na podwójny atak jedną z pozostałych umiejętności");
      Console.WriteLine("-Rzut shurikenem (1x damage, 1x stamina drain, 1x energy cost)");
      Console.WriteLine("-Atak krótkimi mieczami (1x damage, 1x stamina drain, 2x energy cost, 50% chance to double attack)");
      Console.WriteLine("-Skrycie - kolejny atak zada podwójne obrażenia (3x stamina cost, 1x energy cost)");
      Console.WriteLine("-Może używać umiejętności, dopóki ma energię\n");
      Console.WriteLine(">Mag: Siła - 6, Zręczność - 6");
      Console.WriteLine("-Mana - pozwala na korzystanie z magii (wszystkie umiejętności)");
      Console.WriteLine("-Magiczny pocisk (2x damage, 1x mana cost)");
      Console.WriteLine("-Kula ognia - zadaje obrażenia i podpala (2x damage, 4 burn damage, 3x stamina, 2x mana drain)");
      Console.WriteLine("-Wzmocnienie - zwieksza obrazenia nastepnej umiejetnosci (w zaleznosci od uzytej many) (4x stamina)\n");
    }
    public static void Koty(){ //UŻYĆ NA WŁASNE RYZYKO!!!
      Console.WriteLine(
        "                               (\"`-\'\'-/\").___..--\'\'\"`-._ " +"\n"
      + "                                 `6_ 6  )   `-.  (     ).`-.__.`) " +"\n"
      + "                                 (_Y_.)'  ._   )  `._ `. ``-..-' " +"\n"
      + "                                   _..`--'_..-_/  /--'_.'" +"\n"
      + "                                  ((((.-''  ((((.'  (((.-' ");
      Console.WriteLine(@"
                                  
                                  
                      (`.-,')
                    .-'     ;
                _.-'   , `,-
          _ _.-'     .'  /._
        .' `  _.-.  /  ,'._;)
       (       .  )-| (
        )`,_ ,'_,'  \_;)
('_  _,'.'  (___,))
 `-:;.-'" + "\n");
    }
    //Tura gracza
    public static void PlayerTurn(Hero attacker, Hero enemy) {
      if(enemy is Assassin) enemy.specialty = 100;
      if(enemy is Mage) if(enemy.specialty + 20 > 100) enemy.specialty = 100; else enemy.specialty += 20;
      if(attacker.burnTurn > 0) { attacker.HP -= attacker.burnDamage; attacker.burnTurn--; Console.WriteLine($"{attacker.Name}, otrzymałeś {attacker.burnDamage} obrażeń z podpalenia!\n"); }
      if(enemy.burnTurn > 0) { enemy.HP -= enemy.burnDamage; enemy.burnTurn--; Console.WriteLine($"{enemy.Name}, otrzymałeś {enemy.burnDamage} obrażeń z podpalenia!\n"); }
      if(attacker.Stun == 0) {
        do {
          Console.WriteLine("Twoj ruch: " + attacker.Name + "\n");
          Console.WriteLine($"Wybierz: 1 - Zwykły Atak, 2 - {attacker.firstAbilityName}, 3 - {attacker.secondAbilityName}, 4 - {attacker.thirdAbilityName}, 5 - Odpoczynek");
          int input = userInput(5);
          Console.Clear();
          switch(input){
            case 1: //Zwykły atak
              attacker.NormalAttack(enemy); if(attacker is Assassin) attacker.specialty -= 25; break;
            case 2: //Pierwsza umiejętność
              attacker.FirstAbility(enemy); break;
            case 3: //Druga umiejętność
              attacker.SecondAbility(enemy); break;
            case 4: //Trzecia umiejętność
              attacker.ThirdAbility(enemy); break;
            case 5: //Odpoczynek
              attacker.Rest(); if(attacker is Assassin) attacker.specialty = 0; break;
          }
          if (attacker.HP <= 0 || enemy.HP <= 0) break;
          if (enemy is Warrior) enemy.specialty = enemy.newRage();
          Koty(); ////UŻYĆ NA WŁASNE RYZYKO!!! ----------------------------------------------------------------------------------------------------------------------------
          HeroesTable();
        } while(attacker is Assassin && attacker.specialty >= 15);
      } else attacker.Stun--;
    }

    //Wyswietlanie tabeli statystyk graczy
    private static void HeroesTable(){
      Console.WriteLine(String.Format("{0,-20} | {1,-12} | {2,-10} | {3,-10} | {4,-12} | {5,-10} | {6,-12} | {7,-11} | {8,-12}", 
        "Imię", "Klasa", "Siła", "Zręczność", "Wytrzymałość", "Zdrowie", "Oszołomienie","Podpalenie", "Specjalność"));
      Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
      Console.WriteLine(hero1.StatsTable());
      Console.WriteLine(hero2.StatsTable());
      Console.WriteLine();
    }

    //Poprawny wybor przez gracza
    public static int userInput(int maxValue){
      int input;
      while (true) {
        try {
          input = int.Parse(Console.ReadLine());
          if(input >= 1 && input <= maxValue) break;
          else Console.WriteLine("Niepoprawnie wprowadzona wartość!");
        }
        catch (Exception) {
            Console.WriteLine("Niepoprawnie wprowadzona wartość!");
        }
      }
      return input;
    }
    public static string nameInput(int maxLength){
      string input;
      while (true) {
        try {
          input = Console.ReadLine();
          if(input.Length < maxLength) break;
          else Console.WriteLine("Imię powinno mieć do 20 znaków!");
        }
        catch (Exception) {
          Console.WriteLine("Niepoprawnie wprowadzona wartość!");
        }
      }
      return input;
    }
    public static Hero hero1 = new Warrior("");
    public static Hero hero2 = new Warrior("");
    public static void Main(string[] args)
    {
      Console.Clear();
      //Wprowadzenie pierwszego gracza
      hero1 = NewHero(hero1, "1");
      //Wprowadzenie drugiego gracza
      hero2 = NewHero(hero2, "2");

      Koty(); //UŻYĆ NA WŁASNE RYZYKO!!! -----------------------------------------------------------------------------------------------------------------------------------
      HeroesTable();
      while (hero1.HP > 0 && hero2.HP > 0) {
        //Kolej pierwszego gracza
        PlayerTurn(hero1, hero2);
        if (hero1.HP <= 0 || hero2.HP <= 0) break;
        //Kolej drugiego gracza
        PlayerTurn(hero2, hero1);
      } 
      HeroesTable();
      //Zwycięstwo - koniec gry
      if(hero1.HP <= 0) Console.WriteLine($"Wygrał {hero2.Name} !");
      else Console.WriteLine($"Wygrał {hero1.Name} !");
      Console.WriteLine("\nWciśnij Enter aby zakończyć...");
      Console.ReadLine();
    }
  }
}

