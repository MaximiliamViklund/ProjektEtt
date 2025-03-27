using System.Reflection.Metadata.Ecma335;

/////////////////////////////////////////ITEM/////////////////////////////////////////
public class Item{
    public string name;
    public int price;
}
/////////////////////////////////////////WEAPON/////////////////////////////////////////
public class Weapon:Item{
    public int dmgDie;
    public int dmgMod;

    public int Attack(){
        Random gen=new();
        return gen.Next(1,dmgDie+1)+dmgMod;
    }

    public void ShowStats(){
        Console.WriteLine("Damage Die: "+dmgDie);
        Console.WriteLine("Damage Modifier: "+dmgMod);
        Console.WriteLine("Price: "+price);
    }
}

/////////////////////////////////////////POTION/////////////////////////////////////////
public class Potion:Item{
    public string type;
    public int strength;
    public int strengthMod;

    ////////////////////////////USE////////////////////////////////////
    public int Use(){
        Random gen=new();
        return gen.Next(1,strength+1)+strengthMod;
    }
}

///LVL 1 POTIONS///
public class PotionLvl1:Potion{
    public PotionLvl1(){
        strength=6;
        strengthMod=3;
    }
}
public class HPotionLvl1:PotionLvl1{
    public HPotionLvl1(){
        name="Healing Potion";
        type="Healing";
    }
}