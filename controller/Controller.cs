using System.Runtime.InteropServices;

public class Controller{
    static public bool HeroSelected{get;set;}
    static public bool EnvironmentSelected{get;set;}
    public bool MainMenuWhile{get;set;}
    public bool LoginMenuWhile{get;set;}
    private int _intInput;
    private Database _db;
    private View _view;
    private List<Environment> _environments = new List<Environment>();

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
                        heroLeak = 2;
                        Fight();
                        mainMenu = false;
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
 }