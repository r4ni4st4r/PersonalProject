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

