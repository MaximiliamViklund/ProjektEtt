//Fixa så att alla publika variabler har stor bokstav i början av dess namn


using System.ComponentModel.Design;
using System.Diagnostics;
List<Hero> availableHeroes=new();
List<Hero> yourTeam=new();      //Ändra namn till "yourTeam"
List<Enemy> enemyList=new();


/////////////////////////////////////////////////////ITEM-CREATION///////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////FOR-HEROES/////////////////////////////////////////////////////////////////////


///WEAPONS///
Weapon GreatSword=new(){
    Name="Great Sword",
    DmgDie=12,
    DmgMod=5
};

Weapon Knife=new(){
    Name="Knife",
    DmgDie=6,
    DmgMod=6
};


///POTIONS///
HPotionLvl1 Healing1=new();
HPotionLvl1 Healing2=new();


/////////////////////////////////////////////////////ITEM-CREATION///////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////FOR-STORE/////////////////////////////////////////////////////////////////////

List<Item> storeItems=new();

///WEAPONS///   //Skapar nya vapen som läggs till i affären
Weapon GreatAxe=new(){
    Name="Great Axe",
    DmgDie=12,
    DmgMod=7,
    Price=15
};
Weapon ShortSword=new(){
    Name="Short Sword",
    DmgDie=6,
    DmgMod=8,
    Price=10
};

storeItems.Add(GreatAxe);
storeItems.Add(ShortSword);


////////////////////////////////////////////////////ENEMY-CREATION///////////////////////////////////////////////////////////////////

//Skapar instanser av Goblin fiender och lägger till dem i enemyList
enemyList.Add(new Goblin());
enemyList.Add(new Goblin());

/////////////////////////////////////////////////////HERO-CREATION//////////////////////////////////////////////////////////////////

//Skapar instanser av Hero karaktärer som styrs av spelaren och lägger till dem i availableHeroes
Warrior Göran=new(){
    Name="Göran",
    Weapon=GreatSword
};
Assassin Kurt=new(){
    Name="Kurt",
    Weapon=Knife
};


Göran.Bag.Add(Healing1);
Kurt.Bag.Add(Healing2);
availableHeroes.Add(Göran);
availableHeroes.Add(Kurt);

/* foreach (Hero hero in heroList){
    if(hero.visibility) heroList.Add(hero);
    else heroList.Remove(hero);
} */
/////////////////////////////////////////////////////VARIABLES//////////////////////////////////////////////////////////////////


string resp;

int respInt=0;
int gold=0;

bool check1=true;
bool check3=false;
bool checkOptions=false;
bool checkStore=false;
bool checkPlay=false;

/////////////////////////////////////////////////START//////////////////////////////////////////////////////////////////////
while(check1){
    Console.WriteLine("Welcome to Another Console Fighting Game");
    Console.WriteLine("Choose your team!");
    Console.WriteLine(yourTeam.Count+"/2");
    for (int i = 0; i < availableHeroes.Count; i++){
        Console.Write(i+") ");
        availableHeroes[i].ShowClass();
        Console.WriteLine();
    }
    resp=Console.ReadLine();
    bool check2=int.TryParse(resp, out respInt);
    if(check2&&respInt<=availableHeroes.Count&&respInt>=0){
        yourTeam.Add(availableHeroes[respInt]);
        Console.WriteLine(availableHeroes[respInt].Name+" was added to your team!");
        availableHeroes.Remove(availableHeroes[respInt]);
    }
    else Console.WriteLine("Enter a valid answer");
    Console.ReadLine();
    Console.Clear();
    
    if(yourTeam.Count==2){
        check1=false;
        check3=true;
    }
}

/////////////////////////////////////////////////MENU//////////////////////////////////////////////////////////////////////
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


/////////////////////////////////////////////////////PLAY//////////////////////////////////////////////////////////////////


    while(checkPlay){
        Random gen=new();

        Console.WriteLine("Roll Initiative");
        Console.ReadLine();

        List<Creature> initiativeList=new();
        foreach(Hero hero in yourTeam){
            hero.Initiative=gen.Next(1,21);
            initiativeList.Add(hero);
        }
        foreach(Enemy enemy in enemyList){
            enemy.Initiative=gen.Next(1,21);
            initiativeList.Add(enemy);
        }
        initiativeList.Sort((a,b)=>b.Initiative.CompareTo(a.Initiative));

        foreach(Creature creature in initiativeList){
            if(creature is Hero){
                Hero hero=(Hero)creature;
                Console.WriteLine(hero.Name+" got "+hero.Initiative);
            }

            //Kod under används för att se att sorteringen fungerar men spelaren ska inte ha tillgång till fiendernas initiativ
            /* if(creature is Enemy){
                Enemy enemy=(Enemy)creature;
                Console.WriteLine(enemy.name+" got "+enemy.initiative);
            } */
        }
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine("Combat");
        Console.ReadLine();
        Console.Clear();
        bool checkCombat=true;
        while(checkCombat&&yourTeam.Count>0&&enemyList.Count>0){
            int heroCount=0;
            int enemyCount=0;
            foreach(Creature creature in initiativeList){
                Console.WriteLine("Combat");
                Console.WriteLine();

                Console.WriteLine(creature.Name+"s turn");
                Console.WriteLine();
                creature.ShowClass();
                Console.WriteLine();

                if(creature is Hero){
                    heroCount++;
                    Hero Hero=(Hero)creature;
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("a) Attack");
                    Console.WriteLine("b) Use Potion");
                    resp=Console.ReadLine().ToLower();
                    Console.Clear();

                    switch(resp){
                        case "a":
                            Hero.Attack(enemyList);
                        break;
                        case "b":
                            Hero.UseItem(Hero);
                        break;
                        default:
                            Console.WriteLine("Enter a valid answer");
                            Console.ReadLine();
                            Console.Clear();
                        break;
                    }
                }
                else if(creature is Enemy){
                    enemyCount++;
                    Enemy enemy=(Enemy)creature;
                    Console.WriteLine(enemy.Name+"s turn");
                    Console.ReadLine();
                    enemy.Attack(yourTeam);
                }
            }
            if(heroCount==0){
                Console.WriteLine("You lost");
                checkCombat=false;
                checkPlay=false;
            }
            if(enemyCount==0){
                int loot=gen.Next(1,21)+5;
                gold+=loot;
                
                Console.WriteLine("You won");
                Console.WriteLine("You got "+loot+" gold");
                Console.ReadLine();
                Console.Clear();

                checkCombat=false;
                checkPlay=false;
            }
        }
    }


/////////////////////////////////////////////////////STORE//////////////////////////////////////////////////////////////////


    while(checkStore){  //Håller spelaren i butiken tills hen väljer att lämna
        Console.WriteLine("STORE");
        Console.WriteLine("You have "+gold+" gold");
        Console.WriteLine("What would you like to buy?");
        Console.WriteLine();

        foreach(Item item in storeItems){
            if(item is Weapon){
                Console.WriteLine(storeItems.IndexOf(item)+") "+item.Name);
                item.ShowStats();
                Console.WriteLine();
            }
        }
        Console.WriteLine((storeItems.Count+1)+") Healing Potion Lvl 1");
        Healing1.ShowStats();
        Console.WriteLine();
        Console.WriteLine("a) Leave");
        resp=Console.ReadLine().ToLower();
        bool check4=int.TryParse(resp, out respInt);

        if(resp=="a"){
            Console.Clear();
            checkStore=false;
        }
        else if(check4&&respInt<=storeItems.Count&&respInt>=0){
            if(gold>=storeItems[respInt].Price){
                int itemIndex=respInt;
                Console.WriteLine("Who would you like to buy this for?");
                for (int i = 0; i < yourTeam.Count; i++){
                    Console.WriteLine(i+") "+yourTeam[i].Name);
                }
                resp=Console.ReadLine();
                Console.Clear();
                
                bool check5=int.TryParse(resp, out respInt);
                if(check5&&respInt<=yourTeam.Count&&respInt>=0){

                    foreach(Item item in storeItems){ //Kollar om det valda itemet är ett vapen, gör det till en instans av Weapon och byter ut det nya mot det gamla vapnet // Kan vara en bra idé att låta spelaren behålla sitt tidigare vapen
                        if(item is Weapon&&item==storeItems[itemIndex]){
                            yourTeam[respInt].Weapon=(Weapon)item;
                            gold-=item.Price;
                            Console.WriteLine(item.Name+" was added to "+yourTeam[respInt].Name+"s inventory");
                            Console.WriteLine(item.Price+" gold was removed from your inventory");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    storeItems.Remove(storeItems[respInt]);
                }
                else{
                    Console.WriteLine("Enter a valid answer");
                    Console.ReadLine();
                    Console.Clear();
                }


            }
            else{
                Console.WriteLine("You don't have enough gold");
                Console.ReadLine();
                Console.Clear();
            }
        }
        else if(respInt==storeItems.Count+1){
            if(gold>=5){
                Console.WriteLine("Who would you like to buy this for?");
                for (int i = 0; i < yourTeam.Count; i++){
                    Console.WriteLine(i+") "+yourTeam[i].Name);
                }
                resp=Console.ReadLine();
                Console.Clear();
                bool check5=int.TryParse(resp, out respInt); //Kollar om svaret är en siffra
                if(check5&&respInt<=yourTeam.Count&&respInt>=0){
                    yourTeam[respInt].Bag[yourTeam[respInt].Bag.Count+1]=new HPotionLvl1(); //Skapar en ny instans av HPotionLvl1 och lägger till den i den valda karaktärens bag
                    Console.WriteLine(Healing1.Name+" was added to "+yourTeam[respInt].Name+"s inventory");
                    gold-=5;
                }
                else{
                    Console.WriteLine("Enter a valid answer");
                }
            }
            else{
                Console.WriteLine("You don't have enough gold");
            }
            Console.ReadLine();
            Console.Clear();
        }


        else{
            Console.WriteLine("Enter a valid answer");
            Console.ReadLine();
            Console.Clear();
        }
    }


////////////////////////////////////////////////////OPTIONS///////////////////////////////////////////////////////////////////


    while(checkOptions){
        Console.WriteLine("YOU HAVE NO OPTIONS");
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