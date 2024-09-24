using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

public class Controller{
    static public bool HeroSelected{get;set;}
    static public bool EnvironmentSelected{get;set;}
    private int _intInput;
    private Random _random = new Random();
    private Character _hero;
    private DataController _dataController;
    private Environment _environment;
    private Character _villain;
    private Database _db;
    private View _view;
    private User _currentUser;
    private List<Environment> _environments = new List<Environment>();
    private int _selectedEnvironment;
    public Character Hero{get{return _hero;}}
    public Character Villain{get{return _villain;}}
    public Environment Environment{get{return _environment;}}
    public bool MainMenuWhile{get;set;}
    public bool LoginMenuWhile{get;set;}

    public Controller(Database db, View view, DataController dataController){
        _db = db;
        _view = view;
        _dataController = dataController;
    }
    public void LoginMenu(){
        LoginMenuWhile = true;
        while(LoginMenuWhile){
            _view.LoginMenu();
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    _currentUser = _db.Login();
                    LoginMenuWhile = false;
                    break;
                case 2:
                    CreateUser();
                    break;
                case 3:
                    DeleteUser();
                    return;
                default:
                    _view.InvalidChoice();
                    break;
            }
        }
        MainMenu();
    }
    public void MainMenu(){
        MainMenuWhile = true;
        while(MainMenuWhile){
            _view.MainMenu();
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    if(!HeroSelected)
                        CreateNewHero();
                    else
                        _view.HeroAlreadyCreated();
                    break;
                case 2:
                    if(!HeroSelected)
                        LoadHero();
                    else
                        _view.HeroAlreadyCreated();
                    break;
                case 3:
                    if(!EnvironmentSelected)
                        SelectEnvirment();
                    else
                        _view.EnvironmentAlreadySelected();
                    break;
                case 4:
                    if(EnvironmentSelected && HeroSelected){
                        Fight();
                        MainMenuWhile = false;
                        break;
                    }else
                        _view.FightError();
                    break;
                case 5:
                    return;
                default:
                        _view.InvalidChoice();
                    break;
            }
        }
    }
    private void CreateUser(){
        _view.NotImplementedYet();
    }
    private void DeleteUser(){
        _view.NotImplementedYet();
    }
    private void LoadHero(){
        _view.NotImplementedYet();
    }
    private void AssignBonus(){}
    private void RechargeParameter(bool heroOrVillain){}
    private void SelectEnvirment(){ //**********************************************//
        bool fail = true;
        while(fail){
            _view.SelectEnvirmentMenu();
            switch(_view.GetIntInput()){
                case 1:
                    _environment = new Environment("Arena", DefaultData.WarBonus);
                    _villain = CharacterSetup("Warrior", true, true);
                    fail = false;
                    break;
                case 2:
                    _environment = new Environment("Dark city alley", DefaultData.ThiefBonus);
                    _villain = CharacterSetup("Thief", true, true);
                    fail = false;
                    break;
                case 3:
                    _environment = new Environment("Ancient castle", DefaultData.WizBonus);
                    _villain = CharacterSetup("Wizard", true, true);
                    fail = false;
                    break;
                default:
                    _view.InvalidChoice();
                    break;
            }
        }
        EnvironmentSelected = true;
    }
    private void CreateNewHero(){ // Funzione per selezionare il nome e la classe di un nuovo personaggio
        string name = "";
        bool failDo = true;
        bool failWhile = true;
        int selection;
        do{
            while(failWhile){
                _view.EnterHeroNeme();
                name = _view.GetStringInput();
                if(name != ""){
                    failWhile = false;
                }else{
                    _view.NotValidName();
                }
            }
            _view.ClassesSelectionMenu();
            selection = _view.GetIntInput();
            switch(selection){
                case 1:
                case 2:
                case 3:
                    CharacterSetup(DefaultData.CharacterClasses[selection - 1], true, false,name);
                    failDo = false;
                    break;
                default:
                    _view.InvalidChoice();
                    break;
            }
        }while(failDo);
    }
    private Character CharacterSetup(string cClass, bool newHero, bool villain, [Optional] string name){
        Character character;
        if(villain)
            character = new Character(DefaultData.VillainNames[_random.Next(DefaultData.VillainNames.Length)], cClass);
        else
            character = new Character(name, cClass);
        switch(cClass){
            case "Warrior":
                character.Parameters[0] = DefaultData.WarParams[0];       // parametro[0] = strength 
                character.Parameters[1] = DefaultData.WarParams[1];       // parametro[1] = stealth 
                character.Parameters[2] = DefaultData.WarParams[2];       // parametro[2] = magic 
                character.Parameters[3] = DefaultData.WarParams[3] * 10;  // parametro[3] = health
                break;
            case "Thief":
                character.Parameters[0] = DefaultData.ThiefParams[0];
                character.Parameters[1] = DefaultData.ThiefParams[1];
                character.Parameters[2] = DefaultData.ThiefParams[2];
                character.Parameters[3] = DefaultData.ThiefParams[3] * 10;
                break;
            case "Wizard":
                character.Parameters[0] = DefaultData.WizParams[0];
                character.Parameters[1] = DefaultData.WizParams[1];
                character.Parameters[2] = DefaultData.WizParams[2];
                character.Parameters[3] = DefaultData.WizParams[3] * 10;
                break;
        }
        if(newHero){
            character.Parameters[5] = 0;                                                                              // parameters[5] = experience verrà utilizzato con la persistenza dei dati
            character.Parameters[4] = DefaultData.SkillValues[_random.Next(DefaultData.SkillValues.Length-1)];                                 // parameters[4] = skill
        }else{
            //Da implementare quando è un personaggio loadato

            /*charObj.parameters[4] = heroObj.parameters[4];
            character.Parameters[5] = heroObj.parameters[5];*/
            HeroSelected = true;

        }
        return character;
    }
    private bool SaveMenu(){
        while(true){
            _view.SaveMenu(Hero.Name, Hero.Class);
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    return _dataController.WriteOnJson(Hero);
                case 2:
                    return false;
                default:
                    _view.InvalidChoice();
                    break;
            }
        }
    }
    private int CpuActionIa(){
        int maxIndex = 0;
        for(int i = 0; i < 3; i++){
            maxIndex = _villain.Parameters[maxIndex] < _villain.Parameters[i] ? i : maxIndex; //
        }
        return maxIndex;
    }
    private bool TryToRunAway(){
        bool success = false;
        switch(_environments[_selectedEnvironment].Location){
            case "Arena":
                if(_hero.Class == "Warrior")
                    success = _random.Next(101)>50;
                else if(_hero.Class == "Wizard")
                    success = _random.Next(101)>70;
                else
                    success = _random.Next(101)>85;
                break;
            case "Dark city alley":
                if(_hero.Class == "Warrior")
                    success = _random.Next(101)>70;
                else if(_hero.Class == "Wizard")
                    success = _random.Next(101)>85;
                else
                    success = _random.Next(101)>50;
                break;
            case "Ancient castle":
                if(_hero.Class == "Warrior")
                    success = _random.Next(101)>85;
                else if(_hero.Class == "Wizard")
                    success = _random.Next(101)>50;
                else
                    success = _random.Next(101)>70;
                break;
        }
        _hero.EscapePossibilities--;
        return success;
    }
    private void Fight(){
        bool ranAway = false;
        AssignBonus(); // Prima dell'inizio vengono incrementati i valori dei parametri dei personaggi col bonus legato al campo di battaglia
        bool yourTurn = _random.Next(2) == 1; // Chi inizia la battaglia è definito in modo random
        _view.FightMessage(_environments[_selectedEnvironment].Location, _villain.Name, _villain.Class);
        while(_villain.Parameters[3] > 0 && _hero.Parameters[3] > 0 && !ranAway){
            if(yourTurn){
                if(_hero.Parameters[0] == 0 && _hero.Parameters[1] == 0 && _hero.Parameters[2] == 0){
                    _view.HeroRechargeMessage();
                    RechargeParameter(yourTurn);
                }else{
                    _view.HeroFightMenu(_hero);
                    int selection = _view.GetIntInput();
                    switch(selection){
                        case 1: case 2: case 3:
                            _view.AttakResult(Attak(selection-1, yourTurn), yourTurn, selection-1);
                            yourTurn = false;
                            break;
                        case 4:
                            bool success = true;
                            if(_hero.EscapePossibilities > 0){
                                success= TryToRunAway();
                                if(success){
                                    ranAway = true;
                                    //*******************************************************
                                    //saveMenu = true;
                                    //*******************************************************
                                    break;
                                }else{
                                    yourTurn = false;
                                }
                            }
                            _view.TryToRunAway(success, _hero.EscapePossibilities);
                            break;
                        default:
                            _view.InvalidChoice();
                            break;
                    }
                }
            }else{
                _view.VillainAttak(_villain);
                if(_villain.Parameters[0] == 0 && _villain.Parameters[1] == 0 && _villain.Parameters[2] == 0){
                    _view.VillainRechargeMessage();
                    RechargeParameter(yourTurn);
                }else{
                    int attakSelection = CpuActionIa();
                    _view.AttakResult(Attak(attakSelection, yourTurn), yourTurn, attakSelection);
                }
                yourTurn = true;
            }
        }
        if(_hero.Parameters[3] <= 0){
            _view.DeathMessage();
        }else if(!ranAway){

            //*******************************************************
            //saveMenu = true;
            //*******************************************************
            
            _view.WinMessage();
        }
    }
    private int Attak(int attakType, bool turn){
        DefaultData.HitPoints = 0;
        int attakExpense = (_random.Next(2) == 1) ? 4 : 2;   // Il costo dell'attacco è definito in maniera random 2 o 4 punti
        bool attakSuccess = _random.Next(101) > 20;          // 80% di probabilità di centrare l'attacco
        if(!attakSuccess){                                  // L'attacco non ha successo!
            if(turn)
                _hero.Parameters[attakType] = attakExpense >= _hero.Parameters[attakType] ? 0 : _hero.Parameters[attakType] - attakExpense;
            else
                _villain.Parameters[attakType] = attakExpense >= _villain.Parameters[attakType] ? 0 : _villain.Parameters[attakType] - attakExpense;
            return 1;
        } 
        
        if(turn && _hero.Parameters[attakType] < attakExpense){           // Il personaggio non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else if(!turn && attakExpense > _villain.Parameters[attakType]){ // Il "nemico" non ha abbastanza punti parametro per sferrare l'attacco
            return 2;
        }else{                                                              // Il colpo va a segno!
            if(turn){
                if(_hero.Class == "Warrior" && DefaultData.Attaks[attakType] == "Charged attack")    // Se il colpo è il colpo speciale della classe 
                    DefaultData.HitPoints = DefaultData.WarParams[attakType];                                       // "hitPoints" parte dal valore di base del parametro
                else if(_hero.Class == "Thief" && DefaultData.Attaks[attakType] == "Archery shot")
                    DefaultData.HitPoints = DefaultData.ThiefParams[attakType];
                else if(_hero.Class == "Wizard" && DefaultData.Attaks[attakType] == "Spell")
                    DefaultData.HitPoints = DefaultData.WizParams[attakType];

                int tmp = _hero.Parameters[4]+1;             // problema con il load del personaggio ****************  (inutile, ma risolve)
                
                DefaultData.HitPoints += attakExpense * _random.Next(1, tmp); // calcolo dei punti 2 o 4 * un random tra 1 e il valore del parametro skill

                _hero.Parameters[attakType] -= attakExpense;
                _villain.Parameters[3] -= DefaultData.HitPoints;
            }else{
                if(_villain.Class == "Warrior" && DefaultData.Attaks[attakType] == "Charged attack")
                    DefaultData.HitPoints = DefaultData.WarParams[attakType];
                else if(_villain.Class == "Thief" && DefaultData.Attaks[attakType] == "Archery shot")
                    DefaultData.HitPoints = DefaultData.ThiefParams[attakType];
                else if(_villain.Class == "Wizard" && DefaultData.Attaks[attakType] == "Spell")
                    DefaultData.HitPoints = DefaultData.WizParams[attakType];
                DefaultData.HitPoints += attakExpense * _random.Next(1, _villain.Parameters[4]+1);
                _villain.Parameters[attakType] -= attakExpense;
                _hero.Parameters[3] -= DefaultData.HitPoints;
            }
            return 0;
        }
    }
 }