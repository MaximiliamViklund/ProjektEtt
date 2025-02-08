using System.ComponentModel;
using System.Runtime.CompilerServices;
//-----------------------------------------Hero----------------------------------------------
public class Hero{
    public string name;
    public string role;
    public int maxHp;
    public int hp;
    public Weapon weapon;
    List<Potion> bag = new();
    public static List<Hero> heroList=new();
    public bool visibility=true;

    void UseItem(){
        string resp;
        int respInt=0;
        bool check1=false;
        while(check1==false){
            Console.WriteLine("Which potion do you want to use?");
            for (int i = 0; i < bag.Count; i++){
                Console.WriteLine(i+") "+bag[i].name);
            }
            resp=Console.ReadLine();

            check1=int.TryParse(resp, out respInt);
            if(check1==false){
                Console.WriteLine("Please enter a valid answer");
                Console.ReadLine();
            }
            Console.Clear();
        }
        if(bag[respInt].type=="Healing"){
            int healed= bag[respInt].Use();
            hp+=healed;
            Console.WriteLine("You have been healed for "+healed+" health");
        }
        if(bag[respInt].type=="Mana"){
            /* int manaAmount=bag[respInt].Use();
            mana+=manaAmount;
            Console.WriteLine("Yor mana was raised with "+manaAmount+" mana"); */
        }
        Console.ReadLine();
        Console.Clear();
    }

    void Attack(){
        string resp;
        bool check=false;
        int respInt=0;
        while(check==false){
            Console.WriteLine("Which enemy would you like to attack?");
            for (int i = 0; i < Enemy.enemyList.Count; i++){
                Console.WriteLine(i+") "+Enemy.enemyList[i].name);
            }
            resp=Console.ReadLine();
            check=int.TryParse(resp, out respInt);
        }
        int damage=weapon.Attack();
        Enemy.enemyList[respInt].hp-=damage;
        Console.WriteLine("You dealt "+damage+" damage to "+Enemy.enemyList[respInt].name);
        Console.ReadLine();
        Console.Clear();
    }

    void ShowClass(){
        Console.WriteLine("Name: "+name);
        Console.WriteLine("Class: "+role);
        Console.WriteLine("Health: "+hp);
        Console.WriteLine("Weapon: "+weapon);
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
        //Sarahs fötter ser ut som drakklor
        Console.WriteLine();
        Console.WriteLine("Inventory");
        for (int i = 0; i < bag.Count; i++){
            Console.WriteLine("     "+bag[i].name);
        }
        Console.ReadLine();
    }
}

class Assassin:Hero{
    public Assassin(){
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
class Warrior:Hero{
    public bool rage;
    public static int rageDmg=5;

    public Warrior(){
        hp=50;
        maxHp=50;
        role="Warrior";
    }
}







//-----------------------------------------Enemy----------------------------------------------

public class Enemy{
    public int hp;
    int maxHp;
    public string name;
    int dmgDie;
    int dmgMod;
    public static List<Enemy> enemyList=new();

    void Attack(){
        Random gen=new();
        Hero.heroList[gen.Next(0,Hero.heroList.Count)].hp-=gen.Next(1,dmgDie+1)+dmgMod;
    }
}

class Goblin:Enemy{
    public bool visibility;
    bool Hide(){
        Random gen=new();
        if(gen.Next(1,21)>12)
        return true;
        return false;
    }
}