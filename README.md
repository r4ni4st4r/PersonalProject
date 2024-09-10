# Gioco di ruolo basato su combattimenti tra personaggi

## Definizione delle funzionalità di base:

- [X] Definizione delle caratteristiche di base e delle classi dei personaggi
- [X] Creazione dell'eroe in base alla scelta del giocatore
- [X] Definizione dei primi campi di battaglia
- [X] Selezione del campo di battaglia e assegnazione dell'avversario
- [X] Definizione delle funzionalità di base dello scontro 
- [X] Implementazione della modalità battaglia giocatore
- [X] Implementazione della delle mosse della CPU
- [X] Gestione della persistenza dei dati tramite cartelle, file .json, .csv e .txt
- [ ] Gestione degli utenti e della sessione attiva
- [ ] Implementazione degli utenti su SQLite
- [ ] Gestione degli errori durante l'interazione con il filesystem
- [ ] Implementazione della modalità rigioca e la possibilità di caricare un personaggio o un avversario

## TODO list:

- [ ] Implementare la funzionalità parata
- [ ] Implementare la funzionalità cura

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

## Settima versione

-  Aggiunto un elemento all'array dei parametri "experience" ```parameters[0] = strength```, ```parameters[1] = stealth```, ```parameters[2] = magic```, ```parameters[3] = health``` e ```parameters[4] = experience```
-  Aggiunta la funzione ```void AttakResult(int attakResult, bool turn, int attakType)``` che stampa l'esito dell'attacco sia per il giocateore che per la CPU
-  Implementata la scelta della mossa da parte della CPU con la funzione ```int CpuActionIa()``` che per adesso sceglie il colpo con il parametro più alto. Se tutti i parametri sono a 0 ricarica il principale del personaggio. (Funzione ```void RechargeParameter()``` non ancora implementata)
-  Completati i commenti

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
```

```bash
git status 
git add --all
git commit -m "Completato lo scontro e le scelte della CPU (semplificate)"
git push -u origin main
```

## Ottava versione A

-  Quando tutti i parametri del giocatore o della CPU sono a 0 è stata implementata la funzione di ricarica di un parametro
-  Il valore minimo ricaricato sarà di 4 e il massimo sarà il massimo valore del parametro per quella classe
-  Queste funzionalità sono state implementate con le funzioni ```void RechargeParameter(bool turn)``` e ```void RechargAssignement([Optional] int sel)```

```csharp
    static void RechargeParameter(bool turn){   // Il giocatore può scegliere che parametro ricaricare ma solo quando tutti e 3 saranno a 0
        bool fail = true;                       // La CPU ricarica il parametro dominante della classe e solo quando ha tutti i parametri a 0
        if(turn){                               // Il parametro si ricaricherà minimo di 4 punti sino e un massimo che sarà il massimo per la classe
            while(fail){
                int i = 1;
                Console.Clear();
                Console.WriteLine("\nSelect the parameter to recharge: ");
                foreach(var par in parameters){
                    Console.WriteLine($"{i} {par}");
                    i++;
                }
                Console.Write("choice: ");
                int.TryParse(Console.ReadLine(), out int selection);
                switch(selection){
                    case 1: case 2: case 3:
                        RechargAssignement(selection);
                        Console.WriteLine($"\nNow your {parameters[selection-1]} is {heroObj.parameters[selection-1]}\n\nPlease press a key...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nEnter a valid choice!\n\nPlease press a key...");
                        Console.ReadKey();
                        break;
                }
            }
        }else{
            RechargAssignement();
        }
    }
    static void RechargAssignement([Optional] int sel){
        if(sel!=0){
            if(heroObj.cClass == "Warrior"){
                if(4>=warParams[sel-1])
                    heroObj.parameters[sel-1] = 4;
                else
                    heroObj.parameters[sel-1] = random.Next(heroObj.parameters[sel-1]+4,warParams[sel-1]+1);
            }else if(heroObj.cClass == "Thief"){
                if(4>=warParams[sel-1])
                    heroObj.parameters[sel-1] = 4;
                else
                    heroObj.parameters[sel-1] = random.Next(heroObj.parameters[sel-1]+4,thiefParams[sel-1]+1);
            }else{
                if(4>=warParams[sel-1])
                    heroObj.parameters[sel-1] = 4;
                else
                    heroObj.parameters[sel-1] = random.Next(heroObj.parameters[sel-1]+4,wizParams[sel-1]+1);
            }
        }else{
            if(villainObj.cClass == "Warrior"){
                villainObj.parameters[0] = random.Next(4,warParams[0]+1);
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[0]} is {villainObj.parameters[0]}\n\nPlease press a key...");
                Console.ReadKey();
            }if(heroObj.cClass == "Thief"){
                villainObj.parameters[1] = random.Next(4,thiefParams[1]+1);
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[1]} is {villainObj.parameters[1]}\n\nPlease press a key...");
                Console.ReadKey();
            }else{
                villainObj.parameters[2] = random.Next(4,wizParams[2]+1);
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[2]} is {villainObj.parameters[2]}\n\nPlease press a key...");
                Console.ReadKey();
            }
        }
    }
```

```bash
git status 
git add --all
git commit -m "Implementata la ricarica di un parametro per giocatore e CPU"
git push -u origin main
```

## Ottava versione B

-  Inserito il parametro ```Skill``` assegnato in fase di creazione del personaggio (che in futuro interagirà con experience). Questo parametro è determinante per calcolare con quanti punti colpisce il giocatore nell'attacco
-  Modifica della funzione ```void RechargAssignement([Optional] int sel)``` che ora ricarica il parametro con un multiplo di 4 random sino al massimo del parametro per quella classe 
-  Inserito il nome random tra 10 disponibili all'avversario

```csharp
    static readonly int[] skillValues = {4, 8, 12};
    static readonly int[] rechargeArray = {4, 8, 12, 16, 20};
    static readonly string[] villainNames = {"Arcadius", "Zoltar", "Vinicius", "Geralth", "Howard", "Juni", "Scarsif", "Jolian", "Kilian", "Olifan"};

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
```

```bash
git status 
git add --all
git commit -m "Inserito il parametro skill, inseriti i nomi degli avversari, modificato il calcolo dei punti di attacco in base a skill, modificata la funzione RechargAssignement"
git push -u origin main
```

## Nona versione

-  Implementata la funzionalità della fuga tramite ```bool TryToRunAway()``` e le possibilità di successo sono legata al campo di battaglia e alla classe del giocatore
-  Inserite la variabile ```heroLeak = 2```, il giocatore ha due tentativi per tentare di scappare

```csharp
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
```

```bash
git status 
git add --all
git commit -m "Implementata la possibilità di fuggire"
git push -u origin main
```

## Nona versione

-  Creata la struttura per le cartelle di salvataggio e configurazione
-  Impostate le funzioni ```static void SaveMenu()``` e ```static void SaveHero()```
-  Modificata la funzione ```static ExpandoObject CharacterSetup(string cClass, bool newHero,  [Optional] string name)``` con il parametro ```newHero``` che resetta tutti i parametri a default ranne ```experience``` e ```skill```

```csharp
    const string SAVEPATH = @".\data\save";    // Path per i files .json da salvare e caricare
    const string CONFIGPATH = @".\data\config"; // Path per i files di configurazione   
    
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
```

```bash
git status 
git add --all
git commit -m "Iniziato a implementare il salvataggio"
git push -u origin main
```

## Decima versione A

-  Implementazione completata di ```static void SaveMenu()``` e ```static void SaveHero()```
-  Aggiunta la libreria ```Newtonsoft.Json``` per gestire la serializzazione/deserializzazione dei file .json
-  Impostata la logica del salvataggio che avviene solo se il personaggio vince lo scontro o se riesce a scappare (se sopravvive)
-  Aggiunta la funzionalità che gestisce l'unicità dei nomi dei file di salvataggio tramite un file .txt, il nome del file viene associato al personaggio ```.parameters[6]``` così se muore ed era stato caricato il file viene eliminato

```csharp
using System.Dynamic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

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
    static readonly string[] attaks = {"Charged attack", "Archery shot", "Spell", "Try to run away"}; // Array con i nomi degli attacchi + Try to run away
    static readonly string[] parameters = {"Strength", "Stealth", "Magic", "Skill"}; // Array con i nomi dei parametri associati agli attacchi + skill
    static readonly int[] skillValues = {2, 4, 8}; // parametro usato come moltiplicatore di attacco
    static readonly int[] rechargeArray = {4, 8, 12, 16, 20};
    static readonly string[] villainNames = {"Arcadius", "Zoltar", "Vinicius", "Geralth", "Howard", "Juni", "Scarsif", "Jolian", "Kilian", "Olifan"};
    static string environment = "";
    /*
    charObj.name            string
    charObj.cClass          string
    charObj.parameters[]    int[7]
    */
    static dynamic heroObj = new ExpandoObject();   //  .parameters[0] -> strength  .parameters[1] -> stealth  .parameters[2] -> magic 
    static dynamic villainObj = new ExpandoObject();//  .parameters[3] -> health   .parameters[4] -> skill .parameters[5] -> experience .parameters[6] -> file name
    static bool heroSelected = false;
    static bool environmentSelected = false;
    static Random random = new Random();
    static int hitPoints;
    static int heroLeak;
    static bool saveMenu = false;
    
    static void Main(string[] args){
        bool mainMenu = true;
        while(mainMenu){
            Console.Clear();
            Console.WriteLine("\n");
            if(!heroSelected){
                Console.WriteLine("1 New Hero");
                Console.WriteLine("2 Load Hero");
            }
            if(!environmentSelected)
                Console.WriteLine("3 Environment");
            Console.WriteLine("4 Fight!");
            Console.WriteLine("5 Exit\n");
            Console.Write("choice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    if(!heroSelected)
                        CreateNewHero();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already created an hero!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    if(!heroSelected)
                        LoadHero();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already created an hero!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 3:
                    if(!environmentSelected)
                        SelectEnvirment();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already selected an environment!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 4:
                    if(environmentSelected && heroSelected){
                        heroLeak = 2;
                        Fight();
                        mainMenu = false;
                        break;
                    }else if(environmentSelected && !heroSelected){
                        Console.Clear();
                        Console.WriteLine("\nYou have to create an hero!\nPlease create it! press any key...\n");
                        Console.ReadKey();
                    }else if(!environmentSelected && heroSelected){
                        Console.Clear();
                        Console.WriteLine("\nYou have to select an environment!\nPlease select it! press any key...\n");
                        Console.ReadKey();
                    }else{
                        Console.Clear();
                        Console.WriteLine("\nBefore you start:\nyou have to create an hero\nand select an environment!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 5:
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("\nEnter a valid choice!\npress any key...\n");
                    Console.ReadKey();
                    break;
            }
        }
        if(saveMenu){
            SaveMenu();
        }
    }
    static private void CreateNewHero(){  // Funzione per selezionare il nome e la classe di un nuovo personaggio
        string name = "";
        bool failDo = true;
        bool failWhile = true;
        do{
            while(failWhile){
                Console.Clear();
                Console.WriteLine("Please enter your hero's name: ");
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
        charObj.parameters = new int[7];
        if(name != null){
            charObj.name = name;
            heroSelected = true; 
        }else
            charObj.name = villainNames[random.Next(villainNames.Length)] + " the " + cClass;
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
            charObj.parameters[5] = 0;                                                                              // parameters[5] = experience verrà utilizzato con la persistenza dei dati
            charObj.parameters[4] = skillValues[random.Next(skillValues.Length-1)];                                 // parameters[4] = skill
            charObj.parameters[6] = Convert.ToInt32(File.ReadAllText(Path.Combine(CONFIGPATH, "fileName.txt")));    // parameters[6] = file name
        }else{
            charObj.parameters[5] = heroObj.parameters[5];
            charObj.parameters[4] = heroObj.parameters[4];
            charObj.parameters[6] = heroObj.parameters[6];
            heroSelected = true;
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
        bool ranAway = false;
        AssignBonus(); // Prima dell'inizio vengono incrementati i valori dei parametri dei personaggi col bonus legato al campo di battaglia
        bool yourTurn = random.Next(2) == 1; // Chi inizia la battaglia è definito in modo random
        Console.Clear();
        Console.WriteLine($"\nYou are in a/an {environment} against {villainObj.name}");
        Console.WriteLine($"\nPlease press any key...");
        Console.ReadKey();
        while(villainObj.parameters[3] > 0 && heroObj.parameters[3] > 0 && !ranAway){
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
                            if(heroLeak > 0){
                                if(TryToRunAway()){
                                    Console.Clear();
                                    Console.WriteLine("\nYou ran away!!!\nPlease press any key...");
                                    Console.ReadKey();
                                    ranAway = true;
                                    saveMenu = true;
                                    break;
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
                            break;
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
            string filePath = Path.Combine(Path.Combine(SAVEPATH, heroObj.parameters[6] + ".json")); 
            if(File.Exists(filePath)){
                File.Delete(filePath);
                Console.Clear();
                Console.WriteLine($"\nYour Hero is been deleted!!!\n");
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }
        }else if(!ranAway){
            saveMenu = true;
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
                heroObj.parameters[attakType] = attakExpense >= heroObj.parameters[attakType] ? 0 : heroObj.parameters[attakType] - attakExpense;
            else
                villainObj.parameters[attakType] = attakExpense >= villainObj.parameters[attakType] ? 0 : villainObj.parameters[attakType] - attakExpense;
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
                else if(heroObj.cClass == "Wizard" && attaks[attakType] == "Spell")
                    hitPoints = wizParams[attakType];
                hitPoints += attakExpense * random.Next(1, heroObj.parameters[4]+1); // calcolo dei punti 2 o 4 * un random tra 1 e il valore del parametro skill
                heroObj.parameters[attakType] -= attakExpense;
                villainObj.parameters[3] -= hitPoints;
            }else{
                if(villainObj.cClass == "Warrior" && attaks[attakType] == "Charged attack")
                    hitPoints = warParams[attakType];
                else if(villainObj.cClass == "Thief" && attaks[attakType] == "Archery shot")
                    hitPoints = thiefParams[attakType];
                else if(villainObj.cClass == "Wizard" && attaks[attakType] == "Spell")
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
                for(int i = 0; i < parameters.Length;i++){
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
        int rechargeValue = rechargeArray[random.Next(rechargeArray.Length)];;
        if(sel!=0){
            if(heroObj.cClass == "Warrior"){
                heroObj.parameters[sel-1] = rechargeValue <= warParams[sel-1] ? rechargeValue : warParams[sel-1];
            }else if(heroObj.cClass == "Thief"){
                heroObj.parameters[sel-1] = rechargeValue <= thiefParams[sel-1] ? rechargeValue : thiefParams[sel-1];
            }else{
                heroObj.parameters[sel-1] =  rechargeValue <= wizParams[sel-1] ? rechargeValue : wizParams[sel-1];
            }
        }else{
            if(villainObj.cClass == "Warrior"){
                villainObj.parameters[0] = rechargeValue <= warParams[0] ? rechargeValue : warParams[0];
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[0]} is {villainObj.parameters[0]}\n\nPlease press any key...");
                Console.ReadKey();
            }if(heroObj.cClass == "Thief"){
                villainObj.parameters[1] = rechargeValue <= thiefParams[1] ? rechargeValue : thiefParams[1];
                Console.Clear();
                Console.WriteLine($"\nNow your opponent's {parameters[1]} is {villainObj.parameters[1]}\n\nPlease press any key...");
                Console.ReadKey();
            }else{
                villainObj.parameters[2] = rechargeValue <= wizParams[2] ? rechargeValue : wizParams[2];
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
        heroLeak--;
        return success;
    }
    static void LoadHero(){
        
    }
    static void SaveMenu(){
        bool success = false;
        while(!success){
            Console.Clear();
            Console.WriteLine($"\nDo you want to save {heroObj.name} the {heroObj.cClass}\n");
            Console.WriteLine($"1 YES!");
            Console.WriteLine($"2 NO!");
            Console.Write("\nChoice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    if(SaveHero()){
                        Console.Clear();
                        Console.WriteLine($"\n{heroObj.name} the {heroObj.cClass} saved successfully!\nPlease press any key...");
                        Console.ReadKey();
                    }
                    return;
                case 2:
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("\nEnter a valid choice!\nPlease press any key...");
                    Console.ReadKey();
                    break;
            }
        }
    }
    static bool SaveHero(){
        heroObj = CharacterSetup(heroObj.cClass, false, heroObj.name);
        heroObj.parameters[5]++;
        string path = Path.Combine(SAVEPATH, heroObj.parameters[6] + ".json");
        File.Create(path).Close();
        using (StreamWriter sw = new StreamWriter(path)){                                          
            sw.Write(JsonConvert.SerializeObject(heroObj, Formatting.Indented));    
        }
        File.WriteAllText(Path.Combine(CONFIGPATH, "fileName.txt"), (heroObj.parameters[6]+1).ToString());
        return true;
    }
}
```

```bash
git status 
git add --all
git commit -m "Menu di salvataggio, salvataggi e nomi univoci dei file implementati"
git push -u origin main
```

## Decima versione B

-  Implementata la funzionalità "carica personaggio" tramite la funzione ```static void LoadHero()``` che visualizza il valore univoco, il nome, la classe, l'esperienza e la skill dei personaggi salvati e permette di selezionare quello da caricare.

```csharp
static void LoadHero(){
        List<string> filesList = new List<string>(Directory.GetFiles(SAVEPATH));
        if(filesList.Count!=0){
            Console.Clear();
            Console.WriteLine("Select the Hero to load by unique ref: \n");
            foreach(string s in filesList){
                string json = File.ReadAllText(s);
                dynamic obj = JsonConvert.DeserializeObject(json);
                Console.WriteLine($"\n> {obj.parameters[6]} < - Hero's name = {obj.name} class = {obj.cClass} experience = {obj.parameters[5]} skill = {obj.parameters[4]}");
            }
            Console.WriteLine("\nchoice: \n");
            while(true){
                string path;
                if(int.TryParse(Console.ReadLine(), out int selection)){
                    path = Path.Combine(SAVEPATH, selection + ".json");
                    if(File.Exists(path)){
                        heroObj = JsonConvert.DeserializeObject(File.ReadAllText(path));
                        Console.WriteLine($"\n{heroObj.name} the {heroObj.cClass} loaded successfully!!!\nPlease press any key...");
                        Console.ReadLine();
                        heroSelected = true;
                        return;
                    }else{
                        Console.WriteLine("Please enter a valid choice: \n");
                    }
                }else{
                    Console.WriteLine("Please enter a valid choice: \n");
                }
            }
        }
    }
```

```bash
git status 
git add --all
git commit -m "Implementato il caricamento dei personaggi"
git push -u origin main
```