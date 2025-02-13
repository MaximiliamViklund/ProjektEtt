using System.Reflection.Metadata.Ecma335;

public class Weapon{
    public string name;
    public int dmgDie;
    public int dmgMod;

    public int Attack(){
        Random gen=new();
        return gen.Next(1,dmgDie+1)+dmgMod;
    }
}

public class Potion{
    public string name;
    public string type;
    public int strength;
    public int strengthMod;

    public int Use(){
        Random gen=new();
        return gen.Next(1,strength+1)+strengthMod;
    }
}
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