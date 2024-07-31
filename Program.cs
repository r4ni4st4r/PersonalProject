using System.Dynamic;
using System.Runtime.InteropServices;

class Program{
    static int selection = 0;
    static readonly string[]  characterClasses = {"Warrior", "Thief", "Wizard"}; // Classi dei personaggi
    static readonly Dictionary<string, int[]> environmentsAndBonus = new Dictionary<string, int[]> {{ "Arena", new int[] {4, 2, 0} },           //
                                                                                                    { "Dark city alley", new int[] {0, 4, 2} }, // Possibili campi di battaglia e bonus per ogni classe
                                                                                                    { "Ancient castle", new int[] {2, 0, 4} }}; // [0]->Warrior  [1]->Thief  [2]->Wizard
    static readonly int[] warParams = {10, 15, 0, 5};  // ***************************************************
    static readonly int[] thiefParams = {8, 7, 15, 0}; // arrays con i valori degli attributi per ogni classe [0]->health   [1]->strength   [2]->stealth   [3]->magic
    static readonly int[] wizParams = {5, 0, 5, 20};   // ***************************************************
    static string environment = "";
    static dynamic heroObj = new ExpandoObject();
    static dynamic villainObj = new ExpandoObject();
    static bool heroCreated = false;
    static bool environmentSelected = false;
    
    static void Main(string[] args){
        while(true){
            Console.Clear();
            if(!heroCreated)
                Console.WriteLine("1 New Hero Setup");
            if(!environmentSelected)
                Console.WriteLine("2 Environment Selection");
            Console.WriteLine("3 Start Game");
            Console.WriteLine("4 Exit");
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    if(!heroCreated)
                        CreateNewHero();
                    else{
                        Console.Clear();
                        Console.WriteLine("You have already created an hero!\nPlease go on and press a key...");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    if(!environmentSelected)
                        SelectEnvirment();
                    else{
                        Console.Clear();
                        Console.WriteLine("You have already selected an environment!\nPlease go on and press a key...");
                        Console.ReadKey();
                    }
                    break;
                case 3:
                    if(environmentSelected && heroCreated){
                        Battle();
                    }else if(environmentSelected && !heroCreated){
                        Console.Clear();
                        Console.WriteLine("You have to create an hero!\nPlease go on and press a key...");
                        Console.ReadKey();
                    }else if(!environmentSelected && heroCreated){
                        Console.Clear();
                        Console.WriteLine("You have to select an environment!\nPlease go on and press a key...");
                        Console.ReadKey();
                    }else{
                        Console.Clear();
                        Console.WriteLine("Before you start\nyou have to create an hero and select\nan environment!\nPlease go on and press a key...");
                        Console.ReadKey();
                    }
                    break;
                case 4:
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Enter a valid choice!\nPlease enter a key...");
                    Console.ReadKey();
                    break;
            }
        }
    }
    static private void CreateNewHero(){  // Funzione per selezionare il nome e la classe di un nuovo personaggio
        string name = "";
        bool failDo = true;
        bool failWhile = true;
        do{
            while(failWhile){
                Console.Clear();
                Console.WriteLine("Please select a name for your hero: ");
                name = Console.ReadLine();
                if(name != ""){
                    heroObj.name = name;
                    failWhile = false;
                }else{
                    Console.Clear();
                    Console.WriteLine("Enter a valid name!\nPlease enter a key...");
                    Console.ReadKey();
                }
            }
            Console.Clear();
            Console.WriteLine("Select your character class:");
            for(int i = 0; i < characterClasses.Length; i++){
                Console.WriteLine($"{i+1} {characterClasses[i]}");
            }
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                case 2:
                case 3:
                    heroObj = NewHeroSetup(characterClasses[selection-1], name);
                    failDo = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Enter a valid choice!\nPlease enter a key...");
                    Console.ReadKey();
                    break;
            }
        }while(failDo);
    }
    static ExpandoObject NewHeroSetup(string cClass, [Optional] string name){   // Funzione che assegna i valori ai parametri del personaggio in base alla classe
        dynamic charObj = new ExpandoObject();                                  // e ritorna il personaggio (eroe o avversario)
        if(name != null)
            charObj.name = name;
        else
            charObj.name = "Villain";
        charObj.cClass = cClass;
        switch(cClass){
            case "Warrior":
                charObj.health = warParams[0] * 10;
                charObj.strength = warParams[1];
                charObj.stealth = warParams[2];
                charObj.magic = warParams[3];
                break;
            case "Thief":
                charObj.health = thiefParams[0] * 10;
                charObj.strength = thiefParams[1];
                charObj.stealth = thiefParams[2];
                charObj.magic = thiefParams[3];
                break;
            case "Wizard":
                charObj.health = wizParams[0] * 10;
                charObj.strength = wizParams[1];
                charObj.stealth = wizParams[2];
                charObj.magic = wizParams[3];
                break;
        }
        heroCreated = true;
        return charObj;
    }
    static void SelectEnvirment(){  // Menu che permette di selezionare il campo di battaglia e richiama la creazione dell'avversario corrispondente
        bool fail = true;
        while(fail){
            int i = 1;
            Console.Clear();
            Console.WriteLine("Select the enviroment for the battle: ");
            foreach(var ky in environmentsAndBonus){
                Console.WriteLine($"{i} {ky.Key}");
                i++;
            }
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    environment = "Arena";
                    villainObj = NewHeroSetup("Warrior");
                    fail = false;
                    break;
                case 2:
                    environment = "Dark city alley";
                    villainObj = NewHeroSetup("Thief");
                    fail = false;
                    break;
                case 3:
                    environment = "Ancient castle";
                    villainObj = NewHeroSetup("Wizard");
                    fail = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Enter a valid choice!\nPlease enter a key...");
                    Console.ReadKey();
                    break;
            }
        }
        environmentSelected = true;
    }
    static void Fight(){
        Random random = new Random();
        bool yourTurn = random.Next(2) == 1;
        Console.Clear();
        Console.WriteLine($"You are in a/an {environment} and your opponent is a {villainObj.cClass}");
        while((villainObj.health && heroObj.health) > 0){
            if(yourTurn){
                Console.Clear();
                Console.WriteLine("IT'S YOUR TURN! SELECT YOUR ACTION: ");
                Console.WriteLine("1 Primary Attak!");
                Console.WriteLine("2 Secondary Attak!");
                Console.WriteLine("3 Try to run away!");
                Console.Write("choice: ");
            }else{
            }
        }
    }
}