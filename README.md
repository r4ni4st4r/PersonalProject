# Gioco di ruolo basato su combattimenti tra personaggi

## Definizione delle funzionalità di base:

- [X] Definizione delle caratteristiche di base e delle classi dei personaggi
- [X] Creazione dell'eroe in base alle scelte del giocatore
- [ ] Definizione dei primi campi di battaglia
- [ ] Selezione del campo di battaglia e assegnazione di un avversario casuale (in base al luogo)
- [ ] Definizione delle funzionalità di base dello scontro 
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

-  Gli attributi dei giocatori sono sono health, strength, stealth, magicSkill ed experience -> ogni classe avrà 30 punti suddivisi tra le varie skills experience partirà da 0 e verrà incrementata con la persistenza dei dati 
-  Il personaggio Warrior avrà questi valori health=10 strength=15 stealth=0 magicSkill=5 e questi attacchi speciali special1 >> charged attack (attribute strength) special2 >> spell (attribute magic skill)
-  Il personaggio Thief avrà questi valori health=8 strength=7 stealth=15 magicSkill=0 e questi attacchi speciali special1 >> archery (attribute stealth) special2 >> charged attack (attribute strength)
-  Il personaggio Wizard avrà questi valori health=5 strength=0 stealth=5 magicSkill=20 e questi attacchi speciali special1 >> spell (attribute magic skill) special2 >> archery (attribute stealth)
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