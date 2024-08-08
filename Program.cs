using System.Dynamic;
using System.Runtime.InteropServices;

class Program{
    const string SAVEPATH = @".\data\save";    // Path per i files .json da salvare e caricare
    const string CONFIGPATH = @".\data\config"; // Path per i files di configurazione                                      
    static readonly string[]  characterClasses = {"Warrior", "Thief", "Wizard"}; // Classi dei personaggi
    static readonly Dictionary<string, int[]> environmentsAndBonus = new Dictionary<string, int[]> {{ "Arena", new int[] {4, 0, 2} },           // Possibili campi di battaglia e bonus per ogni classe
                                                                                                    { "Dark city alley", new int[] {2, 4, 0} }, // ["Arena"]->Warrior  ["Dark city alley"]->Thief  ["Ancient castle"]->Wizard
                                                                                                    { "Ancient castle", new int[] {0, 2, 4} }}; // [0]->strength   [1]->stealth   [2]->magic
    static readonly int[] warParams = {16, 0, 4, 16};  // ***************************************************
    static readonly int[] thiefParams = {8, 16, 0, 12}; // arrays con i valori degli attributi per ogni classe [0]->strength   [1]->stealth   [2]->magic [3]->health [4]->skill   
    static readonly int[] wizParams = {0, 8, 20, 8};   // ***************************************************
    static readonly string[] attaks = {"Charged attack", "Archery shot", "Spell", "Try to run away!"}; // Array con i nomi degli attacchi speciali
    static readonly string[] parameters = {"Strength", "Stealth", "Magic", "Skill"}; // Array con i nomi dei parametri associati agli attacchi + skill
    static readonly int[] skillValues = {2, 4, 8}; // parametro usato come moltiplicatore di attacco
    static readonly int[] rechargeArray = {4, 8, 12, 16, 20};
    static readonly string[] villainNames = {"Arcadius", "Zoltar", "Vinicius", "Geralth", "Howard", "Juni", "Scarsif", "Jolian", "Kilian", "Olifan"};
    static string environment = "";
    /*
    charObj.name            string
    charObj.cClass          string
    charObj.parameters[]    int[6]
    */
    static dynamic heroObj = new ExpandoObject();   //  .parameters[0]->strength  .parameters[1]->stealth  .parameters[2]->magic 
    static dynamic villainObj = new ExpandoObject();//  .parameters[3]->health   .parameters[4]->skill .parameters[5]->experience
    static bool heroCreated = false;
    static bool environmentSelected = false;
    static Random random = new Random();
    static int hitPoints;
    static int heroleak;
    
    static void Main(string[] args){
        while(true){
            Console.Clear();
            Console.WriteLine("\n");
            if(!heroCreated)
                Console.WriteLine("1 New Hero");
            if(!environmentSelected)
                Console.WriteLine("2 Environment");
            Console.WriteLine("3 Fight!");
            Console.WriteLine("4 Exit\n");
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    if(!heroCreated)
                        CreateNewHero();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already created an hero!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    if(!environmentSelected)
                        SelectEnvirment();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already selected an environment!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 3:
                    if(environmentSelected && heroCreated){
                        heroleak = 2;
                        Fight();
                        return;
                    }else if(environmentSelected && !heroCreated){
                        Console.Clear();
                        Console.WriteLine("\nYou have to create an hero!\nPlease create it! press any key...\n");
                        Console.ReadKey();
                    }else if(!environmentSelected && heroCreated){
                        Console.Clear();
                        Console.WriteLine("\nYou have to select an environment!\nPlease select it! press any key...\n");
                        Console.ReadKey();
                    }else{
                        Console.Clear();
                        Console.WriteLine("\nBefore you start:\nyou have to create an hero\nand select an environment!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 4:
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("\nEnter a valid choice!\npress any key...\n");
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
                    Console.WriteLine("\nEnter a valid name!\nPlease enter a key...");
                    Console.ReadKey();
                }
            }
            Console.Clear();
            Console.WriteLine("\nSelect character's class:\n");
            for(int i = 0; i < characterClasses.Length; i++){
                Console.WriteLine($"{i+1} {characterClasses[i]}");
            }
            Console.Write("\nchoice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                case 2:
                case 3:
                    heroObj = CharacterSetup(characterClasses[selection - 1], true, name);
                    failDo = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nEnter a valid choice!\nPlease enter a key...");
                    Console.ReadKey();
                    break;
            }
        }while(failDo);
    }
    static ExpandoObject CharacterSetup(string cClass, bool newHero,  [Optional] string name){  // Funzione che assegna i valori ai parametri del personaggio in base alla classe
        dynamic charObj = new ExpandoObject();                                      // e ritorna il personaggio (eroe o avversario)
        charObj.parameters = new int[6];
        if(name != null){
            charObj.name = name;
            heroCreated = true; 
        }else
            charObj.name = villainNames[random.Next(villainNames.Length)]+" the "+cClass;
        charObj.cClass = cClass;
        switch(cClass){
            case "Warrior":
                charObj.parameters[0] = warParams[0];       // parametro[0] = strength 
                charObj.parameters[1] = warParams[1];       // parametro[1] = stealth 
                charObj.parameters[2] = warParams[2];       // parametro[2] = magic 
                charObj.parameters[3] = warParams[3] * 10;  // parametro[3] = health
                break;
            case "Thief":
                charObj.parameters[0] = thiefParams[0];
                charObj.parameters[1] = thiefParams[1];
                charObj.parameters[2] = thiefParams[2];
                charObj.parameters[3] = thiefParams[3] * 10;
                break;
            case "Wizard":
                charObj.parameters[0] = wizParams[0];
                charObj.parameters[1] = wizParams[1];
                charObj.parameters[2] = wizParams[2];
                charObj.parameters[3] = wizParams[3] * 10;
                break;
        }
        if(newHero){
            charObj.parameters[5] = 0;                                              // parametro[5] = experience verrà utilizzato con la persistenza dei dati
            charObj.parameters[4] = skillValues[random.Next(skillValues.Length-1)]; // parametro[4] = skill
        }else{
            charObj.parameters[5] = heroObj.parameters[5];
            charObj.parameters[4] = heroObj.parameters[4];
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
                    villainObj = CharacterSetup("Warrior", true);
                    fail = false;
                    break;
                case 2:
                    environment = "Dark city alley";
                    villainObj = CharacterSetup("Thief", true);
                    fail = false;
                    break;
                case 3:
                    environment = "Ancient castle";
                    villainObj = CharacterSetup("Wizard", true);
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
        Console.WriteLine($"\nYou are in a/an {environment} against {villainObj.name}");
        Console.WriteLine($"\nPlease press any key...");
        Console.ReadKey();
        while(villainObj.parameters[3] > 0 && heroObj.parameters[3] > 0){
            if(yourTurn){
                if(heroObj.parameters[0] == 0 && heroObj.parameters[1] == 0 && heroObj.parameters[2] == 0){
                    Console.Clear();
                    Console.WriteLine("You need to recharge a parameter of yours!!\nPress any key...\n");
                    Console.ReadKey();
                    RechargeParameter(yourTurn);
                }else{
                    Console.Clear();
                    Console.WriteLine($"\nFIGHT {heroObj.name} the {heroObj.cClass}!");
                    Console.WriteLine($"\nYour health is {heroObj.parameters[3]}!\n");
                    Console.WriteLine($"Your strength is {heroObj.parameters[0]}!");
                    Console.WriteLine($"Your stealth is {heroObj.parameters[1]}!");
                    Console.WriteLine($"Your magic is {heroObj.parameters[2]}!\n");
                    Console.WriteLine($"Your skill is {heroObj.parameters[4]}!");
                    Console.WriteLine("\nIT'S YOUR TURN! CONSIDER YOUR PARAMETERS AND MAKE YOUR CHOICE:\n");
                    Console.WriteLine($"1 {attaks[0]}! ({parameters[0]})");
                    Console.WriteLine($"2 {attaks[1]}! ({parameters[1]})");
                    Console.WriteLine($"3 {attaks[2]}! ({parameters[2]})\n");
                    Console.WriteLine($"4 {attaks[3]}\n");
                    Console.Write("\nchoice: ");
                    int.TryParse(Console.ReadLine(), out int selection);
                    switch(selection){
                        case 1: case 2: case 3:
                            AttakResult(Attak(selection-1, yourTurn), yourTurn, selection-1);
                            yourTurn = false;
                            break;
                        case 4:
                            if(heroleak>0){
                                if(TryToRunAway()){
                                    Console.Clear();
                                    Console.WriteLine("\nYou ran away!!!\nPlease press any key...");
                                    Console.ReadKey();
                                    return;  //////**************** PROVVISORIO ****************/////////////
                                }else{
                                    Console.Clear();
                                    Console.WriteLine("\nYou fail to run away!!!\nYou miss your turn\nPlease press any key...");
                                    Console.ReadKey();
                                    yourTurn = false;
                                }
                            }else{
                                Console.Clear();
                                Console.WriteLine("\nYou haven't any possibilities to run away!!!\nPlease press any key...");
                                Console.ReadKey();
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("\nEnter a valid choice!\nPlease press any key...");
                            Console.ReadKey();
                            return;  // *************** DEBUG PROVVISORIO *************  perchè cilco infinito!
                    }
                }
            }else{
                Console.Clear();
                Console.WriteLine($"Your opposite health is {villainObj.parameters[3]}!");
                Console.WriteLine("\nIT'S YOUR OPPOSITE TURN!\n");
                Console.WriteLine("He/she's going to do something...\nPress any key...");
                Console.ReadKey();
                if(villainObj.parameters[0] == 0 && villainObj.parameters[1] == 0 && villainObj.parameters[2] == 0){
                    Console.WriteLine("\nHe/she hasn't enought points to launch an attack...\nHe/she's going to recharge\nPress any key...");
                    Console.ReadKey();
                    RechargeParameter(yourTurn);
                }else{
                    int attakSelection = CpuActionIa();
                    AttakResult(Attak(attakSelection, yourTurn), yourTurn, attakSelection);
                }
                yourTurn = true;
            }
        }
        if(heroObj.parameters[3] <= 0){
            Console.Clear();
            Console.WriteLine($"\nYou LOSE!!! You are DEAD!!!\n");
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }else{
            Console.Clear();
            Console.WriteLine($"\nYou WIN!!! Your opponent is DEAD!!!\n");
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
    static int Attak(int attakType, bool turn){
        int attakExpense = (random.Next(2) == 1) ? 4 : 2;   // Il costo dell'attacco è definito in maniera random 2 o 4 punti
        bool attakSuccess = random.Next(101) > 20;          // 80% di probabilità di centrare l'attacco
        if(!attakSuccess){                                  // L'attacco non ha successo!
            if(turn)
                heroObj.parameters[attakType] = attakExpense > heroObj.parameters[attakType] ? 0 : heroObj.parameters[attakType]-attakExpense;
            else
                villainObj.parameters[attakType] = attakExpense > villainObj.parameters[attakType] ? 0 : villainObj.parameters[attakType]-attakExpense;
            return 1;
        } 
        if(turn && attakExpense > heroObj.parameters[attakType]){ // Il personaggio non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else if(!turn && attakExpense > villainObj.parameters[attakType]){ // Il "nemico" non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else{                                                              // Il colpo va a segno!
            if(turn){
                if(heroObj.cClass == "Warrior" && attaks[attakType] == "Charged attack")    // Se il colpo è il colpo speciale della classe 
                    hitPoints = warParams[attakType];                                       // viene aggiunto a "hitPoints" il valore di base del parametro
                else if(heroObj.cClass == "Thief" && attaks[attakType] == "Archery shot")
                    hitPoints = thiefParams[attakType];
                else if(heroObj.cClass == "Thief" && attaks[attakType] == "Spell")
                    hitPoints = wizParams[attakType];
                hitPoints += attakExpense * random.Next(1, heroObj.parameters[4]+1); // calcolo dei punti 2 o 4 * un random tra 1 e il valore del parametro skill
                heroObj.parameters[attakType] -= attakExpense;
                villainObj.parameters[3] -= hitPoints;
            }else{
                if(villainObj.cClass == "Warrior" && attaks[attakType] == "Charged attack")
                    hitPoints = warParams[attakType];
                else if(villainObj.cClass == "Thief" && attaks[attakType] == "Archery shot")
                    hitPoints = thiefParams[attakType];
                else if(villainObj.cClass == "Thief" && attaks[attakType] == "Spell")
                    hitPoints = wizParams[attakType];
                hitPoints += attakExpense * random.Next(1, villainObj.parameters[4]+1);
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
                    Console.WriteLine($"\nYour {attaks[attakType]} had success! You hit your opponent with {hitPoints} of damage!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent {attaks[attakType]} had success! You you were hit with {hitPoints} of damage!\n\nPress any key...");
                Console.ReadKey();
                break;
            case 1:
                Console.Clear();
                if(turn)
                    Console.WriteLine($"\nYour {attaks[attakType]} miss your opponent!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent {attaks[attakType]} miss you! You are lucky!!!\n\nPress any key...");
                Console.ReadKey(); 
                break;
            case 2:
                Console.Clear();
                if(turn)
                    Console.WriteLine($"\nYou haven't enough points for a/an {attaks[attakType]}!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent hasn't enough points for a/an {attaks[attakType]}!\n\nPress any key...");
                Console.ReadKey();
                break;    
            }
    }
    static void RechargeParameter(bool turn){   // Il giocatore può scegliere che parametro ricaricare ma solo quando tutti e 3 saranno a 0
        bool fail = true;                       // La CPU ricarica il parametro dominante della classe e solo quando ha tutti i parametri a 0
        if(turn){                               // Il parametro si ricaricherà minimo di 4 punti sino e un massimo che sarà il massimo per la classe
            while(fail){
                Console.Clear();
                Console.WriteLine("\nSelect the parameter to recharge: ");
                for(int i = 0; i<parameters.Length;i++){
                    Console.WriteLine($"{i+1} {parameters[i]}");
                }
                Console.Write("\nchoice: ");
                int.TryParse(Console.ReadLine(), out int selection);
                switch(selection){
                    case 1: case 2: case 3:
                        RechargAssignement(selection);
                        Console.WriteLine($"\nNow your {parameters[selection-1]} is {heroObj.parameters[selection-1]}\n\nPlease press any key...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nEnter a valid choice!\n\nPlease press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }else{
            RechargAssignement();
        }
    }
    static void RechargAssignement([Optional] int sel){
        int rechValue = rechargeArray[random.Next(rechargeArray.Length)];;
        if(sel!=0){
            if(heroObj.cClass == "Warrior"){
                heroObj.parameters[sel-1] = rechValue <= warParams[sel-1] ? rechValue : warParams[sel-1];
            }else if(heroObj.cClass == "Thief"){
                heroObj.parameters[sel-1] = rechValue <= thiefParams[sel-1] ? rechValue : thiefParams[sel-1];
            }else{
                heroObj.parameters[sel-1] =  rechValue <= wizParams[sel-1] ? rechValue : wizParams[sel-1];
            }
        }else{
            if(villainObj.cClass == "Warrior"){
                villainObj.parameters[0] = rechValue <= warParams[0] ? rechValue : warParams[0];
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[0]} is {villainObj.parameters[0]}\n\nPlease press any key...");
                Console.ReadKey();
            }if(heroObj.cClass == "Thief"){
                villainObj.parameters[1] = rechValue <= thiefParams[1] ? rechValue : thiefParams[1];
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[1]} is {villainObj.parameters[1]}\n\nPlease press any key...");
                Console.ReadKey();
            }else{
                villainObj.parameters[2] = rechValue <= wizParams[2] ? rechValue : wizParams[2];
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[2]} is {villainObj.parameters[2]}\n\nPlease press any key...");
                Console.ReadKey();
            }
        }
    }
    static bool TryToRunAway(){ // Le possibilità di scappare sono legate al campo di battaglia con percentuali diverse a seconda della classe
        bool success = false;
        switch(environment){
            case "Arena":
                if(heroObj.cClass == "Warrior")
                    success = random.Next(101)>50;
                else if(heroObj.cClass == "Wizard")
                    success = random.Next(101)>70;
                else
                    success = random.Next(101)>85;
                break;
            case "Dark city alley":
                if(heroObj.cClass == "Warrior")
                    success = random.Next(101)>70;
                else if(heroObj.cClass == "Wizard")
                    success = random.Next(101)>85;
                else
                    success = random.Next(101)>50;
                break;
            case "Ancient castle":
                if(heroObj.cClass == "Warrior")
                    success = random.Next(101)>85;
                else if(heroObj.cClass == "Wizard")
                    success = random.Next(101)>50;
                else
                    success = random.Next(101)>70;
                break;
        }
        heroleak--;
        return success;
    }
    static void SaveHero(){}

    static void SaveMenu(){
        bool success = false;
        while(!success){
            Console.Clear();
            Console.WriteLine($"\nDo you want to save {heroObj.name} the {heroObj.cClass}\n");
            Console.WriteLine($"1 YES!");
            Console.WriteLine($"2 NO!");
            Console.Write("\nchoice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    SaveHero();
                    break;
                case 2:
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("\nEnter a valid choice!\nPlease press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}