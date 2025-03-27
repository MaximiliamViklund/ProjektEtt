using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Linq;

//////////////////////////////////////////////////////////CREATURE////////////////////////////////////////////////////////////
public class Creature{
    public string Name { get; set; }
    public int Initiative { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public virtual void ShowClass(){}
}
////////////////////////////////////////////////////////////HERO////////////////////////////////////////////////////////////
public class Hero: Creature{
    public string Role { get; set; }
    public Weapon Weapon { get; set; }
    public List<Potion> Bag = new();
    public bool Visibility { get; set; }

    ////////////////////////////////////////////////////////USEITEM////////////////////////////////////////////////////////
    public static void UseItem(Hero hero){
        string resp;
        int respInt=0;
        bool check1=false;
        while(check1==false){
            Console.WriteLine("Which potion do you want to use?");
            for (int i = 0; i < hero.Bag.Count; i++){
                Console.WriteLine(i+") "+hero.Bag[i].Name);
            }
            resp=Console.ReadLine();

            check1=int.TryParse(resp, out respInt);
            if(check1==false){
                Console.WriteLine("Please enter a valid answer");
                Console.ReadLine();
            }
            Console.Clear();
        }
        if(hero.Bag[respInt].Type=="Healing"){
            int healed= hero.Bag[respInt].Use();
            if(hero.Hp+healed>hero.MaxHp) healed=hero.MaxHp-hero.Hp;
            hero.Hp+=healed;
            Console.WriteLine("You have been healed for "+healed+" health");
        }
        if(hero.Bag[respInt].Type=="Mana"){
            /* int manaAmount=bag[respInt].Use();
            hero.mana+=manaAmount;
            Console.WriteLine("Your mana was raised with "+manaAmount+" mana"); */
        }
        Console.WriteLine(hero.Bag[respInt].Name+" was consumed");
        hero.Bag.Remove(hero.Bag[respInt]);
        Console.ReadLine();
        Console.Clear();
    }

    ////////////////////////////////////////////////////////ATTACK////////////////////////////////////////////////////////

    public void Attack(List<Creature> initiativeList){
        if(initiativeList.OfType<Enemy>().Count()>0){
            string resp;
            bool check=false;
            int respInt=0;
            List<Enemy> thisEnemyList=new();


            while(check==false){ //Frågar spelaren vilken fiende hen vill attackera
                Console.WriteLine("Which enemy would you like to attack?");
                foreach(Creature enemy in initiativeList){
                    if(enemy is Enemy&&enemy.Hp>0){
                        if(thisEnemyList.Count==0){
                            Console.WriteLine("Enemies:");
                        }
                        thisEnemyList.Add((Enemy)enemy);
                        Console.WriteLine(thisEnemyList.IndexOf((Enemy)enemy)+") "+enemy.Name);
                        enemy.ShowClass();
                        Console.WriteLine();
                    }
                }
                resp=Console.ReadLine();
                bool check1=int.TryParse(resp, out respInt);

                if(check1==false || respInt<0 || respInt>=thisEnemyList.Count){
                    Console.WriteLine("Please enter a valid answer");
                    Console.ReadLine();
                }
                else check=true;
            }
            
            int damage=Weapon.Attack();
            thisEnemyList[respInt].Hp-=damage;
            Console.WriteLine(Name+" dealt "+damage+" damage to "+thisEnemyList[respInt].Name);
            if(thisEnemyList[respInt].Hp<=0){
                Console.WriteLine(thisEnemyList[respInt].Name+" has been defeated");
            }
            else Console.WriteLine(thisEnemyList[respInt].Name+" has "+thisEnemyList[respInt].Hp+" health left");
            Console.ReadLine();
            Console.Clear();
        }
        else{
            Console.WriteLine("There are no enemies left to attack");
            Console.ReadLine();
            Console.Clear();
            return;
        }
    }

    public override void ShowClass(){ //Skriver ut data för vald Hero
        Console.WriteLine("Name: "+Name);
        Console.WriteLine("Class: "+Role);
        Console.WriteLine("Health: "+Hp);
        Console.WriteLine("Weapon: "+Weapon.Name);
        if(Role=="Assasin"){
            switch(Visibility){
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
        for (int i = 0; i < Bag.Count; i++){
            Console.WriteLine("     "+Bag[i].Name);
        }
    }
}

//////////////////////////////////////////////ASSASSIN////////////////////////////////////////////////////////////
class Assassin:Hero{
    public Assassin(){ //Bestämmer variabler för Assasin
        Hp=35;
        MaxHp=35;
        Role="Assasin";
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
    public bool Rage { get; set; }
    public static int RageDmg { get; set; }

    public Warrior(){
        Hp=50;
        MaxHp=50;
        Role="Warrior";
        RageDmg=5;
    }
}







////////////////////////////////////////////////////////////ENEMY////////////////////////////////////////////////////////////

public class Enemy:Creature{
    public int DmgDie { get; set; }
    public int DmgMod { get; set; }

    //////////////////////////////////////////////ATTACK////////////////////////////////////////////////////////////
    public void Attack(List<Hero> heroList){
        Random gen=new();

        int damage=gen.Next(1,DmgDie+1)+DmgMod;
        Hero hero=heroList[gen.Next(0,heroList.Count)];

        hero.Hp-=damage;
        Console.WriteLine(Name+" dealt "+damage+" damage to "+hero.Name);

        if(hero.Hp<=0){
            Console.WriteLine(hero.Name+" has been defeated");
        }
        else Console.WriteLine(hero.Name+" has "+hero.Hp+" health left");

        Console.ReadLine();
        Console.Clear();
    }

    public override void ShowClass(){
        Console.WriteLine("Health: "+Hp);
        Console.WriteLine("Damage Die: "+DmgDie);
        Console.WriteLine("Damage Modifier: "+DmgMod);
    }
}

//////////////////////////////////////////////GOBLIN////////////////////////////////////////////////////////////
public class Goblin:Enemy{
    public Goblin(){
        Name="Goblin";
        Hp=30;
        MaxHp=30;
        DmgDie=6;
        DmgMod=5;
    }
}
//////////////////////////////////////////////ORC////////////////////////////////////////////////////////////
public class Orc:Enemy{
    public Orc(){
        Name="Orc";
        Hp=50;
        MaxHp=50;
        DmgDie=12;
        DmgMod=9;
    }
}