List<Hero> heroList=new();
List<Enemy> enemyList=new();

Weapon GreatSword=new(){
    name="Great Sword",
    dmgDie=12,
    dmgMod=5
};
PotionLvl1 Healing1=new(){
    name="Healing Potion",
    type="Healing",
};
Warrior Göran=new(){
    name="Göran",
    weapon=GreatSword

};


foreach (Hero hero in heroList){
    if(hero.visibility) heroList.Add(hero);
    else heroList.Remove(hero);
}