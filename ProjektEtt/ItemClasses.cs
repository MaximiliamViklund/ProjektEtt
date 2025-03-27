using System.Reflection.Metadata.Ecma335;

/////////////////////////////////////////ITEM/////////////////////////////////////////
public class Item{
    public string Name { get; set; }
    public int Price { get; set; }
    public virtual void ShowStats(){}
}
/////////////////////////////////////////WEAPON/////////////////////////////////////////
public class Weapon:Item{
    public int DmgDie { get; set; }
    public int DmgMod { get; set; }

    public int Attack(){
        Random gen=new();
        return gen.Next(1,DmgDie+1)+DmgMod;
    }

    public override void ShowStats(){
        Console.WriteLine("     Damage Die: "+DmgDie);
        Console.WriteLine("     Damage Modifier: "+DmgMod);
        Console.WriteLine("     Price: "+Price);
    }
}

/////////////////////////////////////////POTION/////////////////////////////////////////
public class Potion:Item{
    public string Type { get; set; }
    public int Strength { get; set; }
    public int StrengthMod { get; set; }

    ////////////////////////////USE////////////////////////////////////
    public int Use(){
        Random gen=new();
        return gen.Next(1,Strength+1)+StrengthMod;
    }

    public override void ShowStats(){
        Console.WriteLine("Strength: "+Strength);
        Console.WriteLine("Strength Modifier: "+StrengthMod);
        Console.WriteLine("Price: "+Price);
    }
}

///LVL 1 POTIONS///
public class PotionLvl1:Potion{
    public PotionLvl1(){
        Strength=6;
        StrengthMod=3;
    }
}
public class HPotionLvl1:PotionLvl1{
    public HPotionLvl1(){
        Name="Healing Potion";
        Type="Healing";
    }
}