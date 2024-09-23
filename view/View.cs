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
    public int GetIntInput(){
        if(int.TryParse(Console.ReadLine(), out int input))
            return input;
        return -1;
    }
    public string GetStringInput(){
        return Console.ReadLine();
    }
}