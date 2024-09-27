using System.Runtime.InteropServices;
/// <summary>
/// Classe Controller che rappresenta il cuore dell'applicazione tramite l'integrazione delle istanze di
/// Database, View e DataController
/// </summary>
public class Controller{
    // bool quando il personaggio è selezionato
    static public bool HeroSelected{get;set;}
    // bool quando la location è selezionata
    static public bool EnvironmentSelected{get;set;}
    private int _intInput;
    private Random _random = new Random();
    // Personaggio dell'utente
    private Character _hero;
    private DataController _dataController;
    // Location della battaglia
    private Environment _environment;
    // Personaggio avversario
    private Character _villain;
    private AppDatabase _db;
    private View _view;
    private User _currentUser;
    //private List<Environment> _environments = new List<Environment>(); *** probabilmente inutile ***
    //private int _selectedEnvironment; *********** DEBUG ***************
    public Character Hero{get{return _hero;}}
    public Character Villain{get{return _villain;}}
    public User CurrentUser{get{return _currentUser;}}
    public Environment Environment{get{return _environment;}}
    public bool MainMenuWhile{get;set;}
    public bool LoginMenuWhile{get;set;}

    public Controller(AppDatabase db, View view, DataController dataController){
        _db = db;
        _view = view;
        _dataController = dataController;
    }
    // Metodo LoginMenu()
    // Collettore di metodi della classe database e view
    // per effettuare la login, creare un utente o cancellarlo
    public void LoginMenu(){
        LoginMenuWhile = true;
        while(LoginMenuWhile){
            // Visualizzazione del menù attraverso la view
            _view.LoginMenu();
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    // Demanda al metodo della classe database di fare la login 
                    // e ritorna l'utente loggato
                    _currentUser = _db.Login();
                    if(_currentUser != null)
                        LoginMenuWhile = false;
                    break;
                case 2:
                    // Registrazione non ancora implementata
                    // sarà un metodo della classe database 
                    _currentUser = _db.CreateUser();
                    if(_currentUser != null)
                        LoginMenuWhile = false;
                    break;
                case 3:
                    // Cancellazione non ancora implementata
                    // sarà un metodo della classe database 
                    //_view.DeleteUser();
                    break;
                case 4:
                    _view.ExitMessage();
                    return;
                default:
                    _view.InvalidChoice();
                    break;
            }
        }
        // Finito il login o la registrazione viene chiamato il metodo 
        // MainMenu()
        MainMenu();
    }
    // Metodo MainMenu()
    // tramite le visualizzazioni dei menu della classe view
    // permette all'utente di creare o caricare un personaggio
    // scegliere la location iniziare la battaglia
    public void MainMenu(){
        MainMenuWhile = true;
        while(MainMenuWhile){
            // Visualizzazione del menù attraverso la view
            _view.MainMenu();
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    // se l'eroe non è stato selezionato o creato 
                    // verranno chiamati a seconda della selezione
                    // CreateNewHero() o LoadHero()
                    if(!HeroSelected)
                        CreateNewHero();
                    else
                        _view.HeroAlreadyCreated();
                    break;
                case 2:
                    if(!HeroSelected)
                        _dataController.LoadHero(_currentUser.Name);
                    else
                        _view.HeroAlreadyCreated();
                    break;
                case 3:
                    // se la location non è stata selezionata 
                    // verrà chiamato SelectEnvirment()
                    if(!EnvironmentSelected)
                        SelectEnvirment();
                    else
                        _view.EnvironmentAlreadySelected();
                    break;
                case 4:
                    // Quando il personaggio e la location saranno stati selezionata 
                    // si potrà iniziare la battaglia e si potrà chiamare il metodo
                    // Fight()
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
    private void LoadHero(){
        _view.NotImplementedYet();
    }
    // Metodo AssignBonus()
    // a seconda della location assegna i bonus ai parametri
    // .Parameters[0] -> strength  .Parameters[1] -> stealth  .Parameters[2] -> magic
    // l'Array Bonus è contenuto nell'istanza della classe Environment 
    // Viene chiamato prima dell'inizio della battaglia
    private void AssignBonus(){
        _hero.Parameters[0] += _environment.Bonus[0];
        _hero.Parameters[1] += _environment.Bonus[1];
        _hero.Parameters[2] += _environment.Bonus[2];
        _villain.Parameters[0] += _environment.Bonus[0];
        _villain.Parameters[1] += _environment.Bonus[1];
        _villain.Parameters[2] += _environment.Bonus[2];
    }
    // RechargeParameter(bool turn)
    // discrimina se la ricaricaè chiamato dall'utente o dalla cpu (eroe o avversario)
    // tramite la variabile turn Il giocatore può scegliere che parametro ricaricare 
    // ma solo quando tutti e 3 saranno a 0 La CPU ricarica il parametro dominante della classe
    // e solo quando ha tutti e 3 i parametri a 0 Il parametro si ricaricherà minimo di 4 punti
    // sino e un massimo che sarà il default per la classe
    private void RechargeParameter(bool turn){
        bool fail = true;
        int selection;                      
        if(turn){
            // chiama il metodo RechargeAssignement(selection) --- selectio int da 1 a 3
            // il base al parametro che l'utente vuole ricaricare
            while(fail){
                _view.ParameterToRechargMenu();
                selection = _view.GetIntInput();
                switch(selection){
                    case 1: case 2: case 3:
                        RechargeAssignement(selection);
                        _view.RechargeMessage(DefaultData.ParametersString[selection-1], _hero.Parameters[selection-1],"your");
                        break;
                    default:
                        _view.InvalidChoice();
                        break;
                }
            }
        }else{
            // chiama il metodo RechargeAssignement() senza parametro perchè la cpu
            // ricarica il parametro principale della classe
            RechargeAssignement();
        }
    }
    // Metodo RechargeAssignement([Optional] int sel)
    // metodo con il parametro opzionale rappresentante la selezione dell'utente
    // se è chiamato senza parametro si comporterà per ricaricare un avversario
    private void RechargeAssignement([Optional] int sel){
        int rechargeValue = DefaultData.RechargeArray[_random.Next(DefaultData.RechargeArray.Length)];
        if(sel!=0){
            // il parametro viene ricaricato di un numero random all'unterno dell'array di multipli di 4 DefaultData.RechargeArray
            // perchè i valori devono essere multipli di 4
            // il sistema va ancora affinato - come tutte le meccanica sono estremamente preliminari.
            // Dopodichè viene valutato se il numero è minore del valore di default della classe
            // e viene usato il più basso. Così non si potrà mai avere un valore ricaricato maggiore di
            // quello di default
            if(_hero.Class == "Warrior"){
                _hero.Parameters[sel-1] = rechargeValue <= DefaultData.WarParams[sel-1] ? rechargeValue : DefaultData.WarParams[sel-1];
            }else if(_hero.Class == "Thief"){
                _hero.Parameters[sel-1] = rechargeValue <= DefaultData.ThiefParams[sel-1] ? rechargeValue : DefaultData.ThiefParams[sel-1];
            }else{
                _hero.Parameters[sel-1] =  rechargeValue <= DefaultData.WizParams[sel-1] ? rechargeValue : DefaultData.WizParams[sel-1];
            }
        }else{
            if(_villain.Class == "Warrior"){
                _villain.Parameters[0] = rechargeValue <= DefaultData.WarParams[0] ? rechargeValue : DefaultData.WarParams[0];
                _view.RechargeMessage(DefaultData.ParametersString[0], _villain.Parameters[0],"your opponent's");
            }if(_villain.Class == "Thief"){
                _villain.Parameters[1] = rechargeValue <= DefaultData.ThiefParams[1] ? rechargeValue : DefaultData.ThiefParams[1];
               _view.RechargeMessage(DefaultData.ParametersString[1], _villain.Parameters[1],"your opponent's");
            }else{
                _villain.Parameters[2] = rechargeValue <= DefaultData.WizParams[2] ? rechargeValue : DefaultData.WizParams[2];
                _view.RechargeMessage(DefaultData.ParametersString[2], _villain.Parameters[2],"your opponent's");
            }
        }
    }
    // Metodo SelectEnvirment()
    // metodo che permette di selezionare la location del combattimento
    private void SelectEnvirment(){ 
        bool fail = true;
        while(fail){
            _view.SelectEnvirmentMenu();
            switch(_view.GetIntInput()){
                // il menù permette di selezionare il tipo di location 
                // e così creare un'istanza di Environment
                // e creare l'avversario residente in quella location
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
    // Metodo CreateNewHero()
    // metodo per selezionare il nome e la classe di un nuovo personaggio
    private void CreateNewHero(){ 
        string name = "";
        bool failDo = true;
        bool failWhile = true;
        int selection;
        do{
            // prima viene chiesto di inserire il nome del personaggio
            // viene verificato che non sia una stringa vuota
            while(failWhile){
                _view.EnterHeroNeme();
                name = _view.GetStringInput();
                if(name != ""){
                    failWhile = false;
                }else{
                    _view.NotValidName();
                }
            }
            // una volta selezionata la classe verrà chiamato il metodo
            // CharacterSetup() che si occupa materialmente di creare l'istanza dell'oggetto Character
            // riferita al nostro eroe
            _view.ClassesSelectionMenu();
            selection = _view.GetIntInput();
            switch(selection){
                case 1:
                case 2:
                case 3:
                    _hero = CharacterSetup(DefaultData.CharacterClasses[selection - 1], true, false,name);
                    failDo = false;
                    break;
                default:
                    _view.InvalidChoice();
                    break;
            }
        }while(failDo);
    }
    // Metodo CharacterSetup(string cClass, bool newHero, bool villain, [Optional] string name)
    // metodo per creare materialmente l'istanza della classe Character. Il fatto che sia creato 
    // in base alle nostre scelte, che sia caricato da file o da zero e che sia un eroe o un avversario
    // Viene gestito attraverso i parametri
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
            character.Parameters[4] = DefaultData.SkillValues[_random.Next(DefaultData.SkillValues.Length-1)];                                // parameters[4] = skill
        }else{
            //Da implementare quando è un personaggio loadato

            /*charObj.parameters[4] = heroObj.parameters[4];
            character.Parameters[5] = heroObj.parameters[5];*/

        }
        character.EscapePossibilities = 2;
        HeroSelected = true;
        return character;
    }
    private bool SaveMenu(){
        while(true){
            _view.SaveMenu(Hero.Name, Hero.Class);
            _intInput = _view.GetIntInput();
            switch(_intInput){
                case 1:
                    return _dataController.WriteOnJson(_currentUser.Name, Hero);
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
        switch(_environment.Location){
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
        _view.FightMessage(_environment.Location, _villain.Name, _villain.Class);
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
                                    _dataController.WriteOnJson(_currentUser.Name, _hero);
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