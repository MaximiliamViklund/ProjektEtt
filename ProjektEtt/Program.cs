using System.ComponentModel.Design;
using System.Diagnostics;
List<Hero> availableHeroes=new();
List<Hero> heroList=new();      //Ändra namn till "yourTeam"
List<Enemy> enemyList=new();

Weapon GreatSword=new(){
    name="Great Sword",
    dmgDie=12,
    dmgMod=5
};

Weapon Knife=new(){
    name="Knife",
    dmgDie=6,
    dmgMod=6
};

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

HPotionLvl1 Healing1=new();
HPotionLvl1 Healing2=new();

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Goblin Gob1=new();
Goblin Gob2=new();

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Warrior Göran=new(){
    name="Göran",
    weapon=GreatSword
};
Assassin Kurt=new(){
    name="Kurt",
    weapon=Knife
};


Göran.bag.Add(Healing1);
Kurt.bag.Add(Healing2);
availableHeroes.Add(Göran);
availableHeroes.Add(Kurt);

/* foreach (Hero hero in heroList){
    if(hero.visibility) heroList.Add(hero);
    else heroList.Remove(hero);
} */
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


string resp;

int respInt=0;
int gold=0;

bool check1=true;
bool check3=false;
bool checkOptions=false;
bool checkStore=false;
bool checkPlay=false;

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
while(check1){
    Console.WriteLine("Welcome to Another Console Fighting Game");
    Console.WriteLine("Choose your team!");
    Console.WriteLine(heroList.Count+"/2");
    for (int i = 0; i < availableHeroes.Count; i++){
        Console.Write(i+") ");
        availableHeroes[i].ShowClass();
        Console.WriteLine();
    }
    resp=Console.ReadLine();
    bool check2=int.TryParse(resp, out respInt);
    if(check2&&respInt<=availableHeroes.Count&&respInt>=0){
        heroList.Add(availableHeroes[respInt]);
        Console.WriteLine(availableHeroes[respInt].name+" was added to your team!");
        availableHeroes.Remove(availableHeroes[respInt]);
    }
    else Console.WriteLine("Enter a valid answer");
    Console.ReadLine();
    Console.Clear();
    
    if(heroList.Count==2){
        check1=false;
        check3=true;
    }
}
while(check3){
    Console.WriteLine("Main Menu");
    Console.WriteLine();
    Console.WriteLine("a) Play");
    Console.WriteLine("b) Store");
    Console.WriteLine("c) Options");
    resp=Console.ReadLine().ToLower();

    switch(resp){
        case "a":
            checkPlay=true;
            Console.Clear();
        break;
        
        case "b":
            checkStore=true;
            Console.Clear();
        break;

        case "c":
            checkOptions=true;
            Console.Clear();
        break;

        default:
        Console.WriteLine("Enter a valid answer");
        break;
    }


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    while(checkPlay){
        Random gen=new();

        Console.WriteLine("Roll Initiative");
        Console.ReadLine();
        Kurt.initiative=gen.Next(1,20);
        Göran.initiative=gen.Next(1,20);
        Gob1.initiative=gen.Next(1,20);
        Gob2.initiative=gen.Next(1,20);

    }


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    while(checkStore){
    }


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    while(checkOptions){
        Console.WriteLine("YOU HAVE NO OPTION");
        Console.WriteLine("a) Huhh??");
        Console.WriteLine("b) Go back");
        resp=Console.ReadLine().ToLower();

        switch(resp){
            case "a":
            Console.WriteLine("MOAHAHAHAHAHA");
            Console.ReadLine();
            Console.Clear();
            break;

            case "b":
                Console.Clear();
                checkOptions=false;
            break;
        }
    }
}