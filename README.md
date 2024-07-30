# Gioco di ruolo basato su combattimenti tra personaggi

## Definizione delle funzionalità di base:

- [ ] Definizione delle caratteristiche di base e delle classi dei personaggi
- [ ] Creazione dell'eroe in base alle scelte del giocatore
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