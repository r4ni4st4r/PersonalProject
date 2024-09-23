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
        /*  
            if(int.TryParse(Console.ReadLine(), out int selection))
                selection = -1;
            switch(selection){
                case 1:
                    if(!HeroSelected)
                        CreateNewHero();
                    else{
                        
                    }
                    break;
                case 2:
                    if(!HeroSelected)
                        LoadHero();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already created an hero!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 3:
                    if(!EnvironmentSelected)
                        SelectEnvirment();
                    else{
                        Console.Clear();
                        Console.WriteLine("\nYou have already selected an environment!\nPlease go on! press any key...\n");
                        Console.ReadKey();
                    }
                    break;
                case 4:
                    if(EnvironmentSelected && HeroSelected){
                        heroLeak = 2;
                        Fight();
                        MainMenuWhile = false;
                        break;
                    }else {}
                case 5:
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("\nEnter a valid choice!\npress any key...\n");
                    Console.ReadKey();
                    break;
            }
    }*/

    public void SaveMenu(Character characterToSave){
        bool success = false;
        while(!success){
            Console.Clear();
            Console.WriteLine($"\nDo you want to save {characterToSave.Name} the {characterToSave.Class}\n");
            Console.WriteLine($"1 YES!");
            Console.WriteLine($"2 NO!");
            Console.Write("\nChoice: ");
            int.TryParse(Console.ReadLine(), out int selection);
            switch(selection){
                case 1:
                    if(SaveHero()){
                        Console.Clear();
                        Console.WriteLine($"\n{characterToSave.Name} the {characterToSave.Class} saved successfully!\nPlease press any key...");
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
    public string GetPassword(){
        string str ="";
        while (true){
            ConsoleKeyInfo i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter){
                break;
            } else if (i.Key == ConsoleKey.Backspace){
                if (str.Length > 0){
                    str.Remove(str.Length - 1);
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
    public void InvalidChoice(){
        Console.Clear();
        Console.WriteLine("\nEnter a valid choice!\nPress any key...\n");
        Console.ReadKey();
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
    public void AttakResult(int attakResult, bool turn, int attakType, int hitPoints){     // questa funzione stampa il risultato dell'attacco
        switch(attakResult){                                                // prendendo come parametri anche il turno (noi o la cpu) 
            case 0:                                                         // e il tipo di attacco
                Console.Clear();
                if(turn)
                    Console.WriteLine($"\nYour {DefaultData.Attaks[attakType]} had success! You hit your opponent with {hitPoints} of damage!\n\nPress any key...");
                else
                    Console.WriteLine($"\nYour opponent {DefaultData.Attaks[attakType]} had success! You were hit with {hitPoints} of damage!\n\nPress any key...");
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

    public int GetIntInput(){
        if(int.TryParse(Console.ReadLine(), out int input))
            return input;
        return -1;
    }
    public string GetStringInput(){
        return Console.ReadLine();
    }
}