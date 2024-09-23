using System.Runtime.InteropServices;

public class Controller{
    static public bool HeroSelected{get;set;}
    static public bool EnvironmentSelected{get;set;}
    public bool MainMenuWhile{get;set;}
    public bool LoginMenuWhile{get;set;}
    public Character Hero{get{return _hero;}}
    public Character Villain{get{return _villain;}}
    private int _intInput;
    private Random _random = new Random();
    private Character _hero;
    private Character _villain;
    private Database _db;
    private View _view;
    private List<Environment> _environments = new List<Environment>();
    private int _selectedEnvironment;

    public Controller(Database db, View view){
        _db = db;
        _view = view;
    }
    public void LoginMenu(){
        while(LoginMenuWhile){
            _view.LoginMenu();
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    Login();
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
    private void Login(){}
    private void CreateUser(){}
    private void DeleteUser(){}
    private void CreateNewHero(){}
    private void LoadHero(){}
    private void AssignBonus(){}
    private void SelectEnvirment(){}
    private void RechargeParameter(bool heroOrVillain){}
    /*
    Character CharacterSetup(string cClass, bool newHero,  [Optional] string name){  // Funzione che assegna i valori ai parametri del personaggio in base alla classe
        Character character = new Character();                                      // e ritorna il personaggio (eroe o avversario)
        character.Parameters = new int[7];
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
    }*/
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
                                    saveMenu = true;
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
            saveMenu = true;
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