/// <summary>
/// La classe View viene utilizzata per stampare a schermo qualsiasi tipo di messaggio di cui l'applicazione abbia bisogno,
/// sono implementati anche due semplici metodi di acquisizione di input, una per gli interi e uno per le stringhe,
/// è presente anche un metodo per l'acquisizione delle password che nasconde in console i caratteri inseriti.
/// Gli unici parametri dei metodi sono stringhe e interi per la personalizzazione dei messaggi.
/// </summary>
public class View{
    public View(){
    }
    public void LoginMenu(){
        Console.Clear();
        Console.WriteLine("\n");
        Console.WriteLine("1 Login");
        Console.WriteLine("2 Register");
        Console.WriteLine("3 Delete User");
        Console.WriteLine("4 Exit\n");
        Console.Write("choice: ");
    }
    public void MainMenu(){
        Console.Clear();
        Console.WriteLine("\n");
        if(!Controller.HeroSelected){
            Console.WriteLine("1 New Hero");
            Console.WriteLine("2 Load Hero");
        }
        if(!Controller.EnvironmentSelected)
            Console.WriteLine("3 Environment");
        Console.WriteLine("4 Fight!");
        Console.WriteLine("5 Exit\n");
        Console.Write("choice: ");
    }
    public void SaveMenu(string name, string cClass){
            Console.Clear();
            Console.WriteLine($"\nDo you want to save {name} the {cClass}\n");
            Console.WriteLine($"1 YES!");
            Console.WriteLine($"2 NO!");
            Console.Write("\nChoice: ");
    }
    public void Saved(string name, string cClass){
        Console.Clear();
        Console.WriteLine($"\n{name} the {cClass} saved successfully!\nPlease press any key...");
        Console.ReadKey();
    }
    public void SelectEnvirmentMenu(){
        Console.Clear();
        Console.WriteLine("Select the enviroment for the battle: ");
        int i = 1;
        foreach(var ky in DefaultData.Environments){
            Console.WriteLine($"{i} {ky}");
            i++;
        }
        Console.Write("choice: ");
    }

    public void ParameterToRechargMenu(){
        Console.Clear();
        Console.WriteLine("\nSelect the parameter to recharge: ");
        for(int i = 0; i < DefaultData.ParametersString.Length;i++){
            Console.WriteLine($"{i+1} {DefaultData.ParametersString[i]}");
        }
        Console.Write("\nchoice: ");
    }

    public void RechargeMessage(string parameter, int value, string who){
        Console.WriteLine($"\nNow {who} {parameter} is {value}\n\nPlease press any key...");
        Console.ReadKey();
    }
    // Metodo per l'acquisizione delle password che nasconde in console i caratteri inseriti.
   /* public string GetPassword(){
        string input = "";
        ConsoleKeyInfo key;

        do
        {
            // Legge il tasto premuto
            key = Console.ReadKey(intercept: true);

            // Se il tasto è "Enter", termina la lettura
            if (key.Key != ConsoleKey.Enter)
            {
                // Aggiungi il carattere alla stringa di input
                input += key.KeyChar;

                // Stampa un asterisco
                Console.Write("*");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        // Ritorna la stringa immessa
        return input;
    }*/

    public string GetPassword(){
        string str = "";
        while (true){
            ConsoleKeyInfo i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter){
                break;
            } else if (i.Key == ConsoleKey.Backspace){
                if (str.Length > 0){
                    // ******** DA VERIFICARE LA CORRETTEZZA **********
                    str = str.Remove(str.Length - 1);
                    Console.Write("\b \b");
                }
            }else if (i.KeyChar != '\u0000' ){ // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
                str+=i.KeyChar;
                Console.Write("*");
            }
        }
        return str;
    }
    public void FightError(){
        if(Controller.EnvironmentSelected && !Controller.HeroSelected){
            Console.Clear();
            Console.WriteLine("\nYou have to create an hero!\nPlease create it! Press any key...\n");
            Console.ReadKey();
        }else if(!Controller.EnvironmentSelected && Controller.HeroSelected){
            Console.Clear();
            Console.WriteLine("\nYou have to select an environment!\nPlease select it! Press any key...\n");
            Console.ReadKey();
        }else{
            Console.Clear();
            Console.WriteLine("\nBefore you start:\nyou have to create an hero\nand select an environment!\nPlease go on! Press any key...\n");
            Console.ReadKey();
        }
    }
    public void UsernameEmptyError(){
        Console.Clear();
        Console.Write("\nUsername can't be empty...\nPlease press any key...");
        Console.ReadKey();
    }
    public void EnterUsername(){
        Console.Clear();
        Console.Write("\nPlease enter a username: ");
    }

    public void EnterPassword(){
        Console.Clear();
        Console.Write("\nPlease enter a password: ");
    }
    public void InvalidPasswordError(){
        Console.Clear();
        Console.Write("\nInvalid password...\nPlease press any key...\n");
        Console.ReadKey();
    }
    public void WrongPasswordError(){
        Console.Clear();
        Console.Write("\nWrong Password...\nPlease press any key...\n");
        Console.ReadKey();
    }
    public void RetryMenu(){
        Console.Clear();
        Console.WriteLine($"Do you want to retry?");
        Console.WriteLine($"1 YES!");
        Console.WriteLine($"2 NO!");
        Console.Write("\nChoice: ");
    }
    public void EnterHeroNeme(){
        Console.Clear();
        Console.Write("Please enter your hero's name: ");
    }
    public void ExitMessage(){
        Console.Clear();
        Console.WriteLine($"BYE BYE!!!");
        Console.ReadKey();
    }
    public void SessionStarted(string username){
        Console.WriteLine($"\nWelcome {username}\nSession begin!\nPlease press any key...");
        Console.ReadKey();
    }
    public void NotValidName(){
        Console.Clear();
        Console.WriteLine("\nEnter a valid name!\nPlease enter a key...");
        Console.ReadKey();
    }
    public void UserDoesntExistError(string username){
        Console.Clear();
        Console.WriteLine($"\n{username} not found!\nPlease enter a key...");
        Console.ReadKey();
    }

    public void UserAlreadyExistError(string username){
        Console.Clear();
        Console.WriteLine($"\n{username} already exist!\nPlease enter a key...");
        Console.ReadKey();
    }
    public void InvalidChoice(){
        Console.Clear();
        Console.WriteLine("\nEnter a valid choice!\nPress any key...\n");
        Console.ReadKey();
    }
    public void NotImplementedYet(){
        Console.WriteLine("Not implemented yet");
    }
    public void HeroAlreadyCreated(){
        Console.Clear();
        Console.WriteLine("\nYou have already created an hero!\nPlease go on! Press any key...\n");
        Console.ReadKey();
    }
    public void EnvironmentAlreadySelected(){
        Console.Clear();
        Console.WriteLine("\nYou have already selected an environment!\nPlease go on! Press any key...\n");
        Console.ReadKey();
    }
    public void FightMessage(string environment, string name, string cClass){
        Console.Clear();
        Console.WriteLine($"\nYou are in a/an {environment} against {name} the {cClass}");
        Console.WriteLine($"\nPlease press any key...");
        Console.ReadKey();
    }
    public void HeroRechargeMessage(){
        Console.Clear();
        Console.WriteLine("You need to recharge a parameter of yours!!\nPress any key...\n");
        Console.ReadKey();
    }
    public void VillainRechargeMessage(){
        Console.WriteLine("\nHe/she hasn't enought points to launch an attack...\nHe/she's going to recharge\nPress any key...");
        Console.ReadKey();
    }
    public void ClassesSelectionMenu(){
        Console.Clear();
        Console.WriteLine("\nSelect character's class:\n");
        for(int i = 0; i < DefaultData.CharacterClasses.Length; i++){
            Console.WriteLine($"{i+1} {DefaultData.CharacterClasses[i]}");
        }
        Console.Write("\nchoice: ");
    }
    public void DeathMessage(){
        Console.Clear();
            Console.WriteLine($"\nYou LOSE!!! You are DEAD!!!\n");
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
    }
    public void WinMessage(){
        Console.Clear();
        Console.WriteLine($"\nYou WIN!!! Your opponent is DEAD!!!\n");
        Console.WriteLine("\nPress any key...");
        Console.ReadKey();
    }
    public void HeroFightMenu(Character hero){
        Console.Clear();
        Console.WriteLine($"\nFIGHT {hero.Name} the {hero.Class}!");
        Console.WriteLine($"\nYour health is {hero.Parameters[3]}!\n");
        Console.WriteLine($"Your strength is {hero.Parameters[0]}!");
        Console.WriteLine($"Your stealth is {hero.Parameters[1]}!");
        Console.WriteLine($"Your magic is {hero.Parameters[2]}!\n");
        Console.WriteLine($"Your skill is {hero.Parameters[4]}!");
        Console.WriteLine("\nIT'S YOUR TURN! CONSIDER YOUR PARAMETERS AND MAKE YOUR CHOICE:\n");
        Console.WriteLine($"1 {DefaultData.Attaks[0]}! ({DefaultData.ParametersString[0]})");
        Console.WriteLine($"2 {DefaultData.Attaks[1]}! ({DefaultData.ParametersString[1]})");
        Console.WriteLine($"3 {DefaultData.Attaks[2]}! ({DefaultData.ParametersString[2]})\n");
        Console.WriteLine($"4 {DefaultData.Attaks[3]}\n");
        Console.Write("\nchoice: ");
    }
    public void AttakResult(int attakResult, bool turn, int attakType){
        switch(attakResult){                                                 
            case 0:                                                         
                Console.Clear();
                if(turn)
                    Console.WriteLine($"\nYour {DefaultData.Attaks[attakType]} had success! You hit your opponent with {DefaultData.HitPoints} of damage!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent {DefaultData.Attaks[attakType]} had success! You were hit with {DefaultData.HitPoints} of damage!\n\nPress any key...");
                Console.ReadKey();
                break;
            case 1:
                Console.Clear();
                if(turn)
                    Console.WriteLine($"\nYour {DefaultData.Attaks[attakType]} miss your opponent!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent {DefaultData.Attaks[attakType]} miss you! You are lucky!!!\n\nPress any key...");
                Console.ReadKey(); 
                break;
            case 2:
                Console.Clear();
                if(turn)
                    Console.WriteLine($"\nYou haven't enough points for a/an {DefaultData.Attaks[attakType]}!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent hasn't enough points for a/an {DefaultData.Attaks[attakType]}!\n\nPress any key...");
                Console.ReadKey();
                break;    
        }
    }
    public void TryToRunAway(bool success, int heroPossibilities){
        if(heroPossibilities > 0){
            if(success){
                Console.Clear();
                Console.WriteLine("\nYou ran away!!!\nPlease press any key...");
                Console.ReadKey();
            }else{
                Console.Clear();
                Console.WriteLine("\nYou fail to run away!!!\nYou miss your turn\nPlease press any key...");
                Console.ReadKey();
            }
        }else{
            Console.Clear();
            Console.WriteLine("\nYou haven't any possibilities to run away!!!\nPlease press any key...");
            Console.ReadKey();
        }
    }
    public void VillainAttak(Character villain){
        Console.Clear();
        Console.WriteLine($"Your opposite health is {villain.Parameters[3]}!");
        Console.WriteLine("\nIT'S YOUR OPPOSITE TURN!\n");
        Console.WriteLine("He/she's going to do something...\nPress any key...");
        Console.ReadKey();
    }
    // Metodo che prende un input in letture e restituisce il valore intero convertito
    // o -1 se l'imput non è un intero
    public int GetIntInput(){
        if(int.TryParse(Console.ReadLine(), out int input))
            return input;
        return -1;
    }
    // metodo che acquisisce una stringa e la ritorna
    // andrà implementato con le verifiche sulla stringa *** TODO LIST ***
    public string GetStringInput(){
        return Console.ReadLine();
    }
}