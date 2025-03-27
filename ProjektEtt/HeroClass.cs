using System.ComponentModel;
using System.Runtime.CompilerServices;

//////////////////////////////////////////////////////////CREATURE////////////////////////////////////////////////////////////
public class Creature{
    public int initiative;
}
////////////////////////////////////////////////////////////HERO////////////////////////////////////////////////////////////
public class Hero: Creature{
    public string name;
    public string role;
    public int maxHp;
    public int hp;
    public Weapon weapon;
    public List<Potion> bag = new();
    public bool visibility=true;

    ////////////////////////////////////////////////////////USEITEM////////////////////////////////////////////////////////
    public static void UseItem(Hero hero){
        string resp;
        int respInt=0;
        bool check1=false;
        while(check1==false){
            Console.WriteLine("Which potion do you want to use?");
            for (int i = 0; i < hero.bag.Count; i++){
                Console.WriteLine(i+") "+hero.bag[i].name);
            }
            resp=Console.ReadLine();

            check1=int.TryParse(resp, out respInt);
            if(check1==false){
                Console.WriteLine("Please enter a valid answer");
                Console.ReadLine();
            }
            Console.Clear();
        }
        if(hero.bag[respInt].type=="Healing"){
            int healed= hero.bag[respInt].Use();
            if(hero.hp+healed>hero.maxHp) healed=hero.maxHp-hero.hp;
            hero.hp+=healed;
            Console.WriteLine("You have been healed for "+healed+" health");
        }
        if(hero.bag[respInt].type=="Mana"){
            /* int manaAmount=bag[respInt].Use();
            hero.mana+=manaAmount;
            Console.WriteLine("Your mana was raised with "+manaAmount+" mana"); */
        }
        Console.WriteLine(hero.bag[respInt].name+" was consumed");
        hero.bag.Remove(hero.bag[respInt]);
        Console.ReadLine();
        Console.Clear();
    }

    ////////////////////////////////////////////////////////ATTACK////////////////////////////////////////////////////////

    public void Attack(List<Enemy> enemyList){
        string resp;
        bool check=false;
        int respInt=0;


        while(check==false){ //Frågar spelaren vilken fiende hen vill attackera
            Console.WriteLine("Which enemy would you like to attack?");
            foreach(Enemy enemy in enemyList){
                Console.WriteLine(enemyList.IndexOf(enemy)+") "+enemy.name);
                enemy.ShowStats();
                Console.WriteLine();
            }
            /* for (int i = 0; i < enemyList.Count; i++){
                Console.WriteLine(i+") "+enemyList[i].name);
                enemyList[i].ShowStats();
                Console.WriteLine();
            } */
            resp=Console.ReadLine();
            check=int.TryParse(resp, out respInt);
        }
        
        int damage=weapon.Attack();
        enemyList[respInt].hp-=damage;
        Console.WriteLine("You dealt "+damage+" damage to "+enemyList[respInt].name);
        if(enemyList[respInt].hp<=0){
            Console.WriteLine(enemyList[respInt].name+" has been defeated");
            enemyList.Remove(enemyList[respInt]);
        }
        else Console.WriteLine(enemyList[respInt].name+" has "+enemyList[respInt].hp+" health left");
        Console.ReadLine();
        Console.Clear();
    }

    public void ShowClass(){ //Skriver ut data för vald Hero
        Console.WriteLine("Name: "+name);
        Console.WriteLine("Class: "+role);
        Console.WriteLine("Health: "+hp);
        Console.WriteLine("Weapon: "+weapon.name);
        if(role=="Assasin"){
            switch(visibility){
                case true:
                    Console.WriteLine("Is Visible");
                break;
                case false:
                    Console.WriteLine("Is Not Visible");
                break;
            }
        }
        Console.WriteLine();
        Console.WriteLine("Inventory");
        for (int i = 0; i < bag.Count; i++){
            Console.WriteLine("     "+bag[i].name);
        }
    }
}

//////////////////////////////////////////////ASSASSIN////////////////////////////////////////////////////////////
class Assassin:Hero{
    public Assassin(){ //Bestämmer variabler för Assasin
        hp=35;
        maxHp=35;
        role="Assasin";
    }
    bool Hide(){
        Random gen=new();
        if(gen.Next(21)>12)
        return true;
        return false;
    }
}

//////////////////////////////////////////////WARRIOR////////////////////////////////////////////////////////////
class Warrior:Hero{ //Bestämmer variabler för Warrior
    public bool rage;
    public static int rageDmg=5;

    public Warrior(){
        hp=50;
        maxHp=50;
        role="Warrior";
    }
}







////////////////////////////////////////////////////////////ENEMY////////////////////////////////////////////////////////////

public class Enemy:Creature{
    public int hp;
    public int maxHp;
    public string name;
    public int dmgDie;
    public int dmgMod;

    //////////////////////////////////////////////ATTACK////////////////////////////////////////////////////////////
    public void Attack(List<Hero> heroList){
        Random gen=new();

        int damage=gen.Next(1,dmgDie+1)+dmgMod;
        Hero hero=heroList[gen.Next(0,heroList.Count)];

        hero.hp-=damage;
        Console.WriteLine(name+" dealt "+damage+" damage to "+hero.name);

        if(hero.hp<=0){
            Console.WriteLine(hero.name+" has been defeated");
            heroList.Remove(hero);
        }
        else Console.WriteLine(hero.name+" has "+hero.hp+" health left");

        Console.ReadLine();
        Console.Clear();
    }

    public void ShowStats(){
        Console.WriteLine("Health: "+hp);
        Console.WriteLine("Damage Die: "+dmgDie);
        Console.WriteLine("Damage Modifier: "+dmgMod);
    }
}

//////////////////////////////////////////////GOBLIN////////////////////////////////////////////////////////////
public class Goblin:Enemy{
    public Goblin(){
        name="Goblin";
        hp=30;
        maxHp=30;
        dmgDie=6;
        dmgMod=5;
    }
}
//////////////////////////////////////////////ORC////////////////////////////////////////////////////////////
public class Orc:Enemy{
    public Orc(){
        name="Orc";
        hp=50;
        maxHp=50;
        dmgDie=12;
        dmgMod=9;
    }
}