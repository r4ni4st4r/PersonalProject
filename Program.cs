using System.Dynamic;
using System.Runtime.InteropServices;

class Program{
    static readonly string[]  characterClasses = {"Warrior", "Thief", "Wizard"}; // Classi dei personaggi
    static readonly Dictionary<string, int[]> environmentsAndBonus = new Dictionary<string, int[]> {{ "Arena", new int[] {4, 0, 2} },           // Possibili campi di battaglia e bonus per ogni classe
                                                                                                    { "Dark city alley", new int[] {2, 4, 0} }, // ["Arena"]->Warrior  ["Dark city alley"]->Thief  ["Ancient castle"]->Wizard
                                                                                                    { "Ancient castle", new int[] {0, 2, 4} }}; // [0]->strength   [1]->stealth   [2]->magic
    static readonly int[] warParams = {16, 0, 4, 16};  // ***************************************************
    static readonly int[] thiefParams = {8, 16, 0, 12}; // arrays con i valori degli attributi per ogni classe [0]->strength   [1]->stealth   [2]->magic [3]->health   
    static readonly int[] wizParams = {0, 8, 20, 8};   // ***************************************************
    static readonly string[] warAttaks = {"Charged attack", "Spell"};       //****************************************
    static readonly string[] thiefAttaks = {"Archery", "Charged attack"};   // arrays con i nomi degli attacchi primari e secondari per classe
    static readonly string[] wizAttaks = {"Spell", "Archery"};              //****************************************
    static string environment = "";
    static dynamic heroObj = new ExpandoObject();
    static dynamic villainObj = new ExpandoObject();
    static bool heroCreated = false;
    static bool environmentSelected = false;
    static Random random = new Random();
    
    static void Main(string[] args){
        while(true){
            Console.Clear();
            if(!heroCreated)
                Console.WriteLine("1 New Hero");
            if(!environmentSelected)
                Console.WriteLine("2 Environment");
            Console.WriteLine("3 Fight!");
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
                        Fight();
                    }else if(environmentSelected && !heroCreated){
                        Console.Clear();
                        Console.WriteLine("You have to create an hero!\nPlease create it and press a key...");
                        Console.ReadKey();
                    }else if(!environmentSelected && heroCreated){
                        Console.Clear();
                        Console.WriteLine("You have to select an environment!\nPlease select it and press a key...");
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
                    heroObj = NewCharacterSetup(characterClasses[selection-1], name);
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
    static ExpandoObject NewCharacterSetup(string cClass, [Optional] string name){  // Funzione che assegna i valori ai parametri del personaggio in base alla classe
        dynamic charObj = new ExpandoObject();                                      // e ritorna il personaggio (eroe o avversario)
        if(name != null){
            charObj.name = name;
            heroCreated = true; 
        }else
            charObj.name = "Villain";
        charObj.cClass = cClass;
        switch(cClass){
            case "Warrior":
                charObj.strength = warParams[0];
                charObj.stealth = warParams[1];
                charObj.magic = warParams[2];
                charObj.health = warParams[3] * 10;
                charObj.first = warAttaks[0];
                charObj.second = warAttaks[1];
                break;
            case "Thief":
                charObj.strength = thiefParams[0];
                charObj.stealth = thiefParams[1];
                charObj.magic = thiefParams[2];
                charObj.health = thiefParams[3] * 10;
                charObj.first = thiefAttaks[0];
                charObj.second = thiefAttaks[1];
                break;
            case "Wizard":
                charObj.strength = wizParams[0];
                charObj.stealth = wizParams[1];
                charObj.magic = wizParams[2];
                charObj.health = wizParams[3] * 10;
                charObj.first = wizAttaks[0];
                charObj.second = wizAttaks[1];
                break;
        }
        return charObj;
    }
    static void SelectEnvirment(){  // Menu che permette di selezionare il campo di battaglia 
        bool fail = true;           // e richiama la creazione dell'avversario corrispondente
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
                    villainObj = NewCharacterSetup("Warrior");
                    fail = false;
                    break;
                case 2:
                    environment = "Dark city alley";
                    villainObj = NewCharacterSetup("Thief");
                    fail = false;
                    break;
                case 3:
                    environment = "Ancient castle";
                    villainObj = NewCharacterSetup("Wizard");
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
        AssignBonus(); // Prima dell'inizio vengono incrementati i valori dei parametri dei personaggi col bonus legato al campo di battaglia
        bool yourTurn = random.Next(2) == 1; // Chi inizia la battaglia è definito in modo random
        Console.Clear();
        Console.WriteLine($"\nYou are in a/an {environment} against a {villainObj.cClass}");
        Console.WriteLine($"\nPlease press a key...");
        Console.ReadKey();
        while(villainObj.health > 0 && heroObj.health > 0){
            if(yourTurn){
                Console.Clear();
                Console.WriteLine("IT'S YOUR TURN! SELECT YOUR ACTION: ");
                Console.WriteLine($"1 Attak!");
                Console.WriteLine($"2 {heroObj.first}!");
                Console.WriteLine($"3 {heroObj.second}!");
                Console.WriteLine("4 Try to run away!");
                Console.Write("choice: ");
                yourTurn = false;
                int.TryParse(Console.ReadLine(), out int selection);
                switch(selection){
                    case 1: case 2: case 3:
                        Attak(selection-1);
                        break;
                    case 4:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Enter a valid choice!\nPlease press a key...");
                        Console.ReadKey();
                        return;  // *************** debug *************  perchè cilco infinito!
                }
            }else{
                Console.Clear();
                Console.WriteLine("IT'S YOUR OPPOSITE TURN!");
                Console.WriteLine("He's going to do something...");
                Console.ReadKey();
                CpuAction();
                yourTurn = true;
            }
        }
    }
    static void Attak(int attakType){
        int attakExpense = (random.Next(2) == 1) ? 4 : 2; // Il costo dell'attacco speciale è definito in maniera random 2 o 4 punti
    }
    static void AssignBonus(){ 
        heroObj.strength += environmentsAndBonus[environment][0];
        heroObj.stealth += environmentsAndBonus[environment][1];
        heroObj.magic += environmentsAndBonus[environment][2];
        villainObj.strength += environmentsAndBonus[environment][0];
        villainObj.stealth += environmentsAndBonus[environment][1];
        villainObj.magic += environmentsAndBonus[environment][2];
    }
    static void CpuAction(){}
}