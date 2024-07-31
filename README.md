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

-  Creata la prima struttura dell'applicazione con le funzionalità minime
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

-  Gli attributi dei giocatori sono sono ```health, strength, stealth, magicSkill ed experience```. Ogni classe avrà 30 punti suddivisi tra le varie skills (Il valore di health sarà moltiplicato per 10). Experience partirà da 0 e verrà incrementata quando sarà implementata la persistenza dei dati e il salvataggio dei personaggi
-  Il personaggio ```Warrior``` avrà questi valori ```health=10 strength=15 stealth=0 magic=5``` e questi due attacchi firstAttak >> charged attack (attribute strength) seconAttak >> spell (attribute magic)
-  Il personaggio ```Thief``` avrà questi valori ```health=8 strength=7 stealth=15 magic=0``` e questi due attacchi firstAttak >> archery (attribute stealth) secondAttak >> charged attack (attribute strength)
-  Il personaggio ```Wizard``` avrà questi valori ```health=5 strength=0 stealth=5 magic=20``` e questi due attacchi firstAttak >> spell (attribute magic) secondAttak >> archery (attribute stealth)
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
                    NewHeroSetup(characterClasses[selection-1]);
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
    static void NewHeroSetup(string cClass){
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
-  Utilizzato un ```Dictionary<string, int[]>``` che correla il campo di battaglia con i bonus alle classi
-  Modificata la funzione ```NewHeroSetup``` per creare un eroe o un avversario

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

# Quinta versione

-  Definito a livello logico il funzionamento della battaglia
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