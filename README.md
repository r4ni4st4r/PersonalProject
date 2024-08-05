# Gioco di ruolo basato su combattimenti tra personaggi

## Definizione delle funzionalità di base:

- [X] Definizione delle caratteristiche di base e delle classi dei personaggi
- [X] Creazione dell'eroe in base alla scelta del giocatore
- [X] Definizione dei primi campi di battaglia
- [X] Selezione del campo di battaglia e assegnazione dell'avversario
- [X] Definizione delle funzionalità di base dello scontro 
- [ ] Implementazione della modalità battaglia
- [ ] Gestire la persistenza dei dati tramite cartelle, file .json, .csv e .txt
- [ ] Gestione degli errori durante l'interazione con il filesystem

## Prima versione

-  Creata la prima struttura dell'applicazione e impostazione delle funzionalità minime
-  visualizzazione delle possibilità del giocatore tramite un menu testuale
-  Nessuna funzionalità è ancora implementata

```csharp
class Program{
    static int selection = 0;
    static void Main(string[] args){
        while(true){
            Console.Clear();
            Console.WriteLine("1 New Hero Setup");
            Console.WriteLine("2 Arena Selection");
            Console.WriteLine("3 Start Game");
            Console.WriteLine("4 Exit");
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    Console.Clear();
                    Console.WriteLine("Not implemented yet\nPress a key...");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Not implemented yet\nPress a key...");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Not implemented yet\nPress a key...");
                    Console.ReadKey();
                    break;
                case 4:
                    return;
                default:
                    break;
            }
        }
    }
}
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Definizione delle funzionalità di base dell'applicazione, prima implementazione del menù. Nessuna funzionalità è ancora attiva"
git push -u origin main
```

# Seconda versione

-  Gli attributi dei giocatori sono sono ```health, strength, stealth, magic ed experience```. Ogni classe avrà 36 punti suddivisi tra le varie skills (Il valore di health sarà moltiplicato per 10). Experience partirà da 0 e verrà incrementata quando sarà implementata la persistenza dei dati e il salvataggio dei personaggi
-  Il personaggio ```Warrior``` avrà questi valori ```health=16 strength=16 stealth=0 magic=4``` e questi due attacchi firstAttak >> charged attack (attribute strength) seconAttak >> spell (attribute magic)
-  Il personaggio ```Thief``` avrà questi valori ```health=12 strength=8 stealth=16 magic=0``` e questi due attacchi firstAttak >> archery (attribute stealth) secondAttak >> charged attack (attribute strength)
-  Il personaggio ```Wizard``` avrà questi valori ```health=8 strength=0 stealth=8 magic=20``` e questi due attacchi firstAttak >> spell (attribute magic) secondAttak >> archery (attribute stealth)
-  Abbozzata la creazione del personaggio

```csharp
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Dynamic;

class Program{
    static int selection = 0;
    static readonly string[] characterClasses = {"Warrior", "Thief", "Wizard"}; // Classi dei personaggi
    
    static dynamic testObj = new ExpandoObject();
    
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
                    CreateNewHero();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Not implemented yet\nPress a key...");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Not implemented yet\nPress a key...");
                    Console.ReadKey();
                    break;
                case 4:
                    return;
                default:
                    break;
            }
        }
    }

    static private void CreateNewHero(){
        string name;
        bool failDo = true;
        bool failWhile = true;
        do{
            while(failWhile){
                Console.Clear();
                Console.WriteLine("Please select a name for your hero: ");
                name = Console.ReadLine();
                if(name != ""){
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
                Console.WriteLine(i+1 + characterClasses[i]);
            }
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                default:
                    break;
            }
        }while(failDo);
    }
}
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Definizione delle caratteristiche di base e delle classi dei personaggi e Impostazione della funzione di creazione del personaggio"
git push -u origin main
```

# Terza versione

-  Completata la creazione del personaggio con le funzioni ```void CreateNewHero()``` e ```void NewHeroSetup(string cClass)```

```csharp
    static private void CreateNewHero(){
        string name;
        bool failDo = true;
        bool failWhile = true;
        do{
            while(failWhile){
                Console.Clear();
                Console.WriteLine("Please select a name for your hero: ");
                name = Console.ReadLine();
                if(name != ""){
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
                    NewCharacterSetup(characterClasses[selection-1]);
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
    static void NewCharacterSetup(string cClass){
        heroObj.cClass = cClass;
        switch(cClass){
            case "Warrior":
                heroObj.health = 10;
                heroObj.strength = 15;
                heroObj.stealth = 0;
                heroObj.magicSkill = 5;
                break;
            case "Thief":
                heroObj.health = 8;
                heroObj.strength = 7;
                heroObj.stealth = 15;
                heroObj.magicSkill = 0;
                break;
            case "Wizard":
                heroObj.health = 5;
                heroObj.strength = 0;
                heroObj.stealth = 5;
                heroObj.magicSkill = 20;
                break;
        }
        heroCreated = true;
    }
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Completata la creazione del personaggio"
git push -u origin main
```

# Quarta versione A

-  Implementata la funzionalità  ```Environment Selection``` con la funzione ```void SelectEnvirment()```. Permette al giocatore di scegliere tra tre campi di battaglia e crea l'avversario residente corrispondente
-  Utilizzato un ```Dictionary<string, int[]>``` - ```envirormentsAndBonus``` che correla il campo di battaglia con i bonus alle classi (I bonus vengono assegnati a entrambi i personaggi, ma queelo con il beneficio maggiore è il resident dell'envirorment)
-  Modificata la funzione ```NewCharacterSetup``` per creare un eroe o un avversario

```csharp
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
    static ExpandoObject NewCharacterSetup(string cClass, [Optional] string name){
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
                    villainObj = NewCharacterSetup("Warrior");
                    fail = false;
                    break;
                case 2:
                    selectedEnviroment = "Dark city alley";
                    villainObj = NewCharacterSetup("Thief");
                    fail = false;
                    break;
                case 3:
                    selectedEnviroment = "Ancient castle";
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
        enviromentSelected = true;
    }
}
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Aggiunta funzionalità selezione del campo di battaglia e creazione dell'avversario"
git push -u origin main
```

# Quarta versione B

-  Semplicemente corretto il menu in modo che non visualizzi un opzione se già completata

```csharp
if(!heroCreated)
    Console.WriteLine("1 New Hero Setup");
if(!enviromentSelected)
    Console.WriteLine("2 Environment Selection");
Console.WriteLine("3 Start Game");
Console.WriteLine("4 Exit");
Console.Write("choice: ");
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Aggiunta piccola modifica alla visualizzazione del menu principale"
git push -u origin main
```

# Quinta versione A

-  Iniziato a definire a livello logico il funzionamento della battaglia
-  Aggiunta versione estremamente embrionale della funzione ```void Fight()```.

```csharp
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
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Iniziata l'implementazione dello scontro"
git push -u origin main
```

# Quinta versione B

-  Rivisto l'ordine dei parametri dei giocatori negli array di configurazione affinchè gli indici siano coerenti 
-  Secondo step della definizione a livello logico del funzionamento della battaglia. Tramite il prosieguo dell'implementazione della funzione ```Fight()``` e l'armonizzazione delle variabili con la logica stessa
-  Ai personaggi vengono aumentati i parametri con il bonus campo di battaglia attraverso la funzione ```AssignBonus()```

```csharp
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
```

## Comandi versionamento

```bash
git status 
git add --all
git commit -m "Armonizzazione del codice alla luce dell'evoluzione della logica della battaglia"
git push -u origin main
```

## Sesta versione

-  I parametri dei personaggi ora sono un array di 4 interi, hanno lo stesso indice degli attacchi corrispondenti
-  Tutto più generico
-  Continua l'implementazione delle funzioni ```void Fight()``` e ```int Attak(int attakType, bool turn)```, ora in grado di mettere in atto gli attacchi del giocatore
-  Manca ancora la funzione di ricarica dei parametri esauriti (al costo di un turno)
-  Manca ancora completamente l'intelligenza artificiale

```csharp
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
    static dynamic heroObj = new ExpandoObject();   // .parameters[0]->strength  .parameters[1]->stealth  .parameters[2]->magic .parameters[3]->health
    static dynamic villainObj = new ExpandoObject();// .parameters[0]->strength  .parameters[1]->stealth  .parameters[2]->magic .parameters[3]->health
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
        charObj.parameters = new int[4];
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
                Console.WriteLine("4 Try to run away!");
                Console.Write("choice: ");
                int.TryParse(Console.ReadLine(), out int selection);
                switch(selection){
                    case 1: case 2: case 3:
                        switch(Attak(selection-1, yourTurn)){
                            case 0:
                                Console.Clear();
                                Console.WriteLine($"Your {attaks[selection-1]} had success! You hit your opponent with {hitPoints} points!\nPress a key...");
                                Console.ReadKey();
                                break;
                            case 1:
                                Console.Clear();
                                Console.WriteLine($"Your {attaks[selection-1]} miss your opponent!\nPress a key...");
                                Console.ReadKey(); 
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine($"You haven't enough points for a/an {attaks[selection-1]}!\nPress a key...");
                                Console.ReadKey();
                                break;    
                        }
                        yourTurn = false;
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
                Console.WriteLine($"Your opposite health is {villainObj.parameters[3]}!");
                Console.WriteLine("IT'S YOUR OPPOSITE TURN!");
                Console.WriteLine("He/she's going to do something...\nPress a key...");
                Console.ReadKey();
                CpuAction();
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
        }else if(!turn && attakExpense > villainObj.parameters[attakType]){ // Il personaggio non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else{                                                    // Il colpo va a segno!
            hitPoints = attakExpense * random.Next(13)/random.Next(1, 3); // calcolo dei punti
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
    static void CpuAction(){}
    static void RechargeParameter(){}
}
```

```bash
git status 
git add --all
git commit -m "Prima versione con un embrionale combattimento funzionante (solo per il personaggio)"
git push -u origin main
```