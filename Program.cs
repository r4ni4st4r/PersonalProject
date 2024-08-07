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
    static readonly string[] attaks = {"Charged attack", "Archery shot", "Spell"}; // Array con i nomi degli attacchi speciali
    static string environment = "";
    static dynamic heroObj = new ExpandoObject();   // .parameters[0]->strength  .parameters[1]->stealth  .parameters[2]->magic .parameters[3]->health .parameters[3]->experience
    static dynamic villainObj = new ExpandoObject();// .parameters[0]->strength  .parameters[1]->stealth  .parameters[2]->magic .parameters[3]->health .parameters[3]->experience
    /*
    charObj.name            string
    charObj.cClass          string
    charObj.parameters[]    int[5]
    */
    static bool heroCreated = false;
    static bool environmentSelected = false;
    static Random random = new Random();
    static int hitPoints;
    
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
                    heroObj = NewCharacterSetup(characterClasses[selection - 1], name);
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
        charObj.parameters = new int[5];
        if(name != null){
            charObj.name = name;
            heroCreated = true; 
        }else
            charObj.name = "Villain";
        charObj.cClass = cClass;
        switch(cClass){
            case "Warrior":
                charObj.parameters[0] = warParams[0];       // parametro[0] = strength 
                charObj.parameters[1] = warParams[1];       // parametro[1] = stealth 
                charObj.parameters[2] = warParams[2];       // parametro[2] = magic 
                charObj.parameters[3] = warParams[3] * 10;  // parametro[3] = health
                charObj.parameters[4] = 0;                  // parametro[4] = experience
                break;
            case "Thief":
                charObj.parameters[0] = thiefParams[0];
                charObj.parameters[1] = thiefParams[1];
                charObj.parameters[2] = thiefParams[2];
                charObj.parameters[3] = thiefParams[3] * 10;
                charObj.parameters[4] = 0;
                break;
            case "Wizard":
                charObj.parameters[0] = wizParams[0];
                charObj.parameters[1] = wizParams[1];
                charObj.parameters[2] = wizParams[2];
                charObj.parameters[3] = wizParams[3] * 10;
                charObj.parameters[4] = 0;
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
        while(villainObj.parameters[3] > 0 && heroObj.parameters[3] > 0){
            if(yourTurn){
                Console.Clear();
                Console.WriteLine($"Your health is {heroObj.parameters[3]}!");
                Console.WriteLine($"Your strength is {heroObj.parameters[0]}!");
                Console.WriteLine($"Your stealth is {heroObj.parameters[1]}!");
                Console.WriteLine($"Your magic is {heroObj.parameters[2]}!");
                Console.WriteLine("IT'S YOUR TURN! CONSIDER YOUR PARAMETERS AND MAKE YOUR CHOICE: ");
                Console.WriteLine($"1 {attaks[0]}");
                Console.WriteLine($"2 {attaks[1]}!");
                Console.WriteLine($"3 {attaks[2]}!");
                Console.WriteLine($"4 Drink a potion to recharge your parameter!");
                Console.WriteLine("5 Try to run away!");
                Console.Write("choice: ");
                int.TryParse(Console.ReadLine(), out int selection);
                switch(selection){
                    case 1: case 2: case 3:
                        AttakResult(Attak(selection-1, yourTurn), yourTurn, selection-1);
                        yourTurn = false;
                        break;
                    case 4:
                        RechargeParameter();
                        break;
                    case 5:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Enter a valid choice!\nPlease press a key...");
                        Console.ReadKey();
                        return;  // *************** debug *************  perchè cilco infinito!
                }
            }else{
                Console.Clear();
                Console.WriteLine($"Your opposite health is {villainObj.parameters[3]}!");
                Console.WriteLine("IT'S YOUR OPPOSITE TURN!");
                Console.WriteLine("He/she's going to do something...\nPress a key...");
                Console.ReadKey();
                if(villainObj.parameters[0]==0 && villainObj.parameters[1]==0 && villainObj.parameters[2]==0){
                    Console.WriteLine("He/she hasn't enought points to launch an attack...\nPress a key...");
                    Console.ReadKey();
                    RechargeParameter();
                }else{
                    int attakSelection = CpuActionIa();
                    AttakResult(Attak(attakSelection, yourTurn), yourTurn, attakSelection);
                }
                yourTurn = true;
            }
        }
    }
    static int Attak(int attakType, bool turn){
        int attakExpense = (random.Next(2) == 1) ? 4 : 2; // Il costo dell'attacco è definito in maniera random 2 o 4 punti
        bool attakSuccess = random.Next(101) > 20;
        if(!attakSuccess) // L'attacco non ha successo!
            return 1; 
        if(turn && attakExpense > heroObj.parameters[attakType]){ // Il personaggio non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else if(!turn && attakExpense > villainObj.parameters[attakType]){ // Il "nemico" non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else{                                                              // Il colpo va a segno!
            hitPoints = attakExpense * random.Next(1, 13)/random.Next(1, 3);   // calcolo dei punti
            if(turn){
                heroObj.parameters[attakType] -= attakExpense;
                villainObj.parameters[3] -= hitPoints;
            }else{
                villainObj.parameters[attakType] -= attakExpense;
                heroObj.parameters[3] -= hitPoints;
            }
            return 0;
        }
    }
    static void AssignBonus(){  // Assegna il bonus enviroment!
        heroObj.parameters[0] += environmentsAndBonus[environment][0];
        heroObj.parameters[1] += environmentsAndBonus[environment][1];
        heroObj.parameters[2] += environmentsAndBonus[environment][2];
        villainObj.parameters[0] += environmentsAndBonus[environment][0];
        villainObj.parameters[1] += environmentsAndBonus[environment][1];
        villainObj.parameters[2] += environmentsAndBonus[environment][2];
    }
    static int CpuActionIa(){ // Per adesso la CPU attacca con il colpo corrispondente al parametro più alto
        int maxIndex = 0;
        for(int i = 0; i < 3; i++){
            maxIndex = villainObj.parameters[maxIndex] < villainObj.parameters[i] ? i : maxIndex; //
        }
        return maxIndex;
    }
    static void AttakResult(int attakResult, bool turn, int attakType){     // questa funzione stampa il risultato dell'attacco
        switch(attakResult){                                                // prendendo come parametri anche il turno (noi o la cpu) 
            case 0:                                                         // e il tipo di attacco
                Console.Clear();
                if(turn)
                    Console.WriteLine($"Your {attaks[attakType]} had success! You hit your opponent with {hitPoints} of damage!\nPress a key...");
                else
                    Console.WriteLine($"Your opponent {attaks[attakType]} had success! You you were hit with {hitPoints} of damage!\nPress a key...");
                Console.ReadKey();
                break;
            case 1:
                Console.Clear();
                if(turn)
                    Console.WriteLine($"Your {attaks[attakType]} miss your opponent!\nPress a key...");
                else
                    Console.WriteLine($"Your opponent {attaks[attakType]} miss you! You are lucky!!!\nPress a key...");
                Console.ReadKey(); 
                break;
            case 2:
                Console.Clear();
                if(turn)
                    Console.WriteLine($"You haven't enough points for a/an {attaks[attakType]}!\nPress a key...");
                else
                    Console.WriteLine($"Your opponent hasn't enough points for a/an {attaks[attakType]}!\nPress a key...");
                Console.ReadKey();
                break;    
            }
    }
    static void RechargeParameter(){}
}