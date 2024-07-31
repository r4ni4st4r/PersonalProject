using System.Dynamic;
using System.Runtime.InteropServices;

class Program{
    static int selection = 0;
    static readonly string[]  characterClasses = {"Warrior", "Thief", "Wizard"}; // Classi dei personaggi
    static readonly Dictionary<string, int[]> envirormentsAndBonus = new Dictionary<string, int[]> {{ "Arena", new int[] {4, 2, 0} },           //
                                                                                                    { "Dark city alley", new int[] {0, 4, 2} }, // Possibili campi di battaglia e bonus per ogni classe
                                                                                                    { "Ancient castle", new int[] {2, 0, 4} }}; //
    static readonly int[] warParams = {10,15,0,5};  // ***************************************************
    static readonly int[] thiefParams = {8,7,15,0}; // arrays con i valori degli attributi per ogni classe
    static readonly int[] wizParams = {5,0,5,20};   // ***************************************************
    static string selectedEnviroment = "";
    static dynamic heroObj = new ExpandoObject();
    static dynamic villainObj = new ExpandoObject();
    static bool heroCreated = false;
    static bool enviromentSelected = false;
    
    static void Main(string[] args){
        while(true){
            Console.Clear();
            Console.WriteLine("1 New Hero Setup");
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
                    if(!enviromentSelected)
                        SelectEnvirment();
                    else{
                        Console.Clear();
                        Console.WriteLine("You have already selected an enviroment!\nPlease go on and press a key...");
                        Console.ReadKey();
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Not implemented yet\nPress a key...");
                    Console.ReadKey();
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
    static private void CreateNewHero(){
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
            for(int i = 0; i<characterClasses.Length; i++){
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
    static ExpandoObject NewHeroSetup(string cClass, [Optional] string name){
        dynamic charObj = new ExpandoObject();
        if(name!=null)
            charObj.name = name;
        else
            charObj.name = "Villain";
        charObj.cClass = cClass;
        switch(cClass){
            case "Warrior":
                charObj.health = warParams[0];
                charObj.strength = warParams[1];
                charObj.stealth = warParams[2];
                charObj.magicSkill = warParams[3];
                break;
            case "Thief":
                charObj.health = thiefParams[0];
                charObj.strength = thiefParams[1];
                charObj.stealth = thiefParams[2];
                charObj.magicSkill = thiefParams[3];
                break;
            case "Wizard":
                charObj.health = wizParams[0];
                charObj.strength = wizParams[1];
                charObj.stealth = wizParams[2];
                charObj.magicSkill = wizParams[3];
                break;
        }
        heroCreated = true;
        return charObj;
    }
    static void SelectEnvirment(){
        bool fail = true;
        while(fail){
            int i = 1;
            Console.Clear();
            Console.WriteLine("Select the enviroment for the battle: ");
            foreach(var ky in envirormentsAndBonus){
                Console.WriteLine($"{i} {ky.Key}");
                i++;
            }
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    selectedEnviroment = "Arena";
                    villainObj = NewHeroSetup("Warrior");
                    fail = false;
                    break;
                case 2:
                    selectedEnviroment = "Dark city alley";
                    villainObj = NewHeroSetup("Thief");
                    fail = false;
                    break;
                case 3:
                    selectedEnviroment = "Ancient castle";
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
        enviromentSelected = true;
    }
}

