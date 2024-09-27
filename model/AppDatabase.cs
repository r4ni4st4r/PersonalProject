using Microsoft.EntityFrameworkCore;
/// <summary>
/// Semplice classe Database (incompleta) che si occupa di leggere e scivere su database 
/// </summary>
public class AppDatabase : DbContext{
    public DbSet<User> Users{get;set;}
    public DbSet<Game> Games{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite($"Data Source = {DefaultData.DBPATH}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
    //private SQLiteConnection _connection;
    private View _view;
    //private int _userId;
    //private string _sql;
    private string _username;
    private string _psw;
    private int _id;
    // La base di dati dell'applicazione è strutturata da due tabelle. Quella principale è games che stora le partite
    /*private const string _defaultDbString = @"
                            CREATE TABLE IF NOT EXISTS games (id INTEGER PRIMARY KEY AUTOINCREMENT, won BOOLEAN, ended DATE default CURRENT_TIMESTAMP, user_id INTEGER, 
                                FOREIGN KEY (user_id) REFERENCES users(id)); 
                            CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT UNIQUE, password TEXT);";
    private const string _defaulUserString = @"INSERT INTO users (username, password) VALUES ('admin','admin');"; */   
    //Probabilmente proprietà inutile...
    //public SQLiteConnection Connection{get{return _connection;}}                        
    public AppDatabase(){
        _view = new View();
    }
    // Metodo Login()
    // si occupa, con l'ausilio della view, dell'inserimento di username e password, 
    // di verificare se l'uente è presente nel db e di eseguirne l'accesso e ritornando un'istanza della classe User
    // che rappresenta l'utente loggato  
    public User Login(){
        bool userWhile = true;
        bool userExist;
        bool pwdCorrect = false;
        int intInput;
        bool passwordRetry = true;
        bool usernameRetry = true;
        //if(!File.Exists(DefaultData.DBPATH))
            //DefaultDbCreation();
        while(userWhile){
            usernameRetry = true;
            _view.EnterUsername();
            _username = _view.GetStringInput();
            // switch che fa il check della stringa inserita come input e se non è vuota e non contiene spazi
            // verifica che l'utente sia presente nel db e successivamente chiede e verifica la password 
            if(string.IsNullOrWhiteSpace(_username) || _username.Contains(" ")){
                _view.UsernameEmptyError();
            }else{    
                using(var context = new AppDatabase()){
                    userExist = context.Users.Any(u => u.Name == _username);
                }
                // se l'utente esiste
                // chiede la password, fa un check che non sia vuota e non contenga spazi e verifica se 
                // sia uguale a quella dell'utente selezionato verificando nel database  
                if(userExist){
                    userWhile = false;
                    bool passWhile = true;
                    while(passWhile){
                        _view.EnterPassword();
                        _psw = _view.GetPassword();
                        if (string.IsNullOrWhiteSpace(_psw) || _psw.Contains(" ")){
                            _view.InvalidPasswordError();
                        }else{
                            using(var context = new AppDatabase()){
                                pwdCorrect = context.Users.Any(u => (u.Name == _username) && (u.Password == _psw));
                            }
                        }
                        if(pwdCorrect){
                            _view.SessionStarted(_username);
                            passWhile = false;
                        }else{
                            _view.WrongPasswordError();
                            passwordRetry = true;
                            while(passwordRetry){
                                _view.RetryMenu();
                                intInput = _view.GetIntInput();
                                switch(intInput){
                                    case 1:
                                        passwordRetry = false;
                                        break;
                                    case 2:
                                        passwordRetry = false;
                                        passWhile = false;
                                        break;
                                    default:
                                        _view.InvalidChoice();
                                        break;
                                }
                            }
                        }
                    }
                }else{
                    _view.UserDoesntExistError(_username);
                    usernameRetry = true;
                    while(usernameRetry){
                        _view.RetryMenu();
                        intInput = _view.GetIntInput();
                        switch(intInput){
                            case 1:
                                usernameRetry = false;
                                break;
                            case 2:
                                usernameRetry = false;
                                userWhile = false;
                                return null;
                            default:
                                _view.InvalidChoice();
                                break;
                        }
                    }
                }        
            }
            
        }
        // restituisce l'utente loggato 
        return new User(_username, _psw, _id);
    }
    public bool DeleteUser(){
        bool userWhile = true;
        bool userExist;
        bool pwdCorrect = false;
        int intInput;
        bool passwordRetry = true;
        bool usernameRetry = true;
        //if(!File.Exists(DefaultData.DBPATH))
            //DefaultDbCreation();
        while(userWhile){
            usernameRetry = true;
            _view.EnterUsername();
            _username = _view.GetStringInput();
            // switch che fa il check della stringa inserita come input e se non è vuota e non contiene spazi
            // verifica che l'utente sia presente nel db e successivamente chiede e verifica la password 
            if(string.IsNullOrWhiteSpace(_username) || _username.Contains(" ")){
                _view.UsernameEmptyError();
            }else{    
                using(var context = new AppDatabase()){
                    userExist = context.Users.Any(u => u.Name == _username);
                }
                // se l'utente esiste
                // chiede la password, fa un check che non sia vuota e non contenga spazi e verifica se 
                // sia uguale a quella dell'utente selezionato verificando nel database  
                if(userExist){
                    userWhile = false;
                    bool passWhile = true;
                    while(passWhile){
                        _view.EnterPassword();
                        _psw = _view.GetPassword();
                        if (string.IsNullOrWhiteSpace(_psw) || _psw.Contains(" ")){
                            _view.InvalidPasswordError();
                        }else{
                            using(var context = new AppDatabase()){
                                pwdCorrect = context.Users.Any(u => (u.Name == _username) && (u.Password == _psw));
                            }
                        }
                        if(pwdCorrect){
                            _view.SessionStarted(_username);
                            passWhile = false;
                        }else{
                            _view.WrongPasswordError();
                            passwordRetry = true;
                            while(passwordRetry){
                                _view.RetryMenu();
                                intInput = _view.GetIntInput();
                                switch(intInput){
                                    case 1:
                                        passwordRetry = false;
                                        break;
                                    case 2:
                                        passwordRetry = false;
                                        passWhile = false;
                                        break;
                                    default:
                                        _view.InvalidChoice();
                                        break;
                                }
                            }
                        }
                    }
                }else{
                    _view.UserDoesntExistError(_username);
                    usernameRetry = true;
                    while(usernameRetry){
                        _view.RetryMenu();
                        intInput = _view.GetIntInput();
                        switch(intInput){
                            case 1:
                                usernameRetry = false;
                                break;
                            case 2:
                                usernameRetry = false;
                                userWhile = false;
                                return false;
                            default:
                                _view.InvalidChoice();
                                break;
                        }
                    }
                }        
            }
            
        }
        // restituisce l'utente loggato 
        return true;
    }
    public User CreateUser(){
        bool userWhile = true;
        bool userExist;
        bool pwdCorrect = false;
        int intInput;
        bool passwordRetry = true;
        //if(!File.Exists(DefaultData.DBPATH))
            //DefaultDbCreation();
        while(userWhile){
            bool usernameRetry = true;
            _view.EnterUsername();
            _username = _view.GetStringInput();
            // if che fa il check della stringa inserita come input e se non è vuota e non contiene spazi
            // verifica che l'utente non sia già presente nel db e successivamente di immettere la password 
            if(string.IsNullOrWhiteSpace(_username) || _username.Contains(" ")){
                _view.UsernameEmptyError();
            }else{    
                using(var context = new AppDatabase()){
                    userExist = context.Users.Any(u => u.Name == _username);
                }
                // se l'utente non esiste
                // chiede di inserire una password e fa un check che non sia vuota e non contenga spazi
                if(!userExist){
                    userWhile = false;
                    bool passWhile = true;
                    while(passWhile){
                        _view.EnterPassword();
                        _psw = _view.GetPassword();
                        if (string.IsNullOrWhiteSpace(_psw) || _psw.Contains(" ") || _psw.Length < 4){
                            _view.InvalidPasswordError();
                        }else{
                            // Iserisce l'utente nel DB
                            using(var context = new AppDatabase()){
                                context.Users.Add(new User(_username, _psw));
                                context.SaveChanges();
                            }
                            pwdCorrect = true;
                        }
                        if(pwdCorrect){
                            _view.SessionStarted(_username);
                            passWhile = false;
                        }
                    }
                }else{
                    _view.UserAlreadyExistError(_username);
                    while(usernameRetry){
                        _view.RetryMenu();
                        intInput = _view.GetIntInput();
                        switch(intInput){
                            case 1:
                                usernameRetry = false;
                                break;
                            case 2:
                                usernameRetry = false;
                                userWhile = false;
                                return null;
                            default:
                                _view.InvalidChoice();
                                break;
                        }
                    }
                }        
            }
            
        }
        // restituisce l'utente creato
        return new User(_username, _psw, _id);
    }
        
        
    //**************************** DA RIVEDERE ****************************
    // Metodo che crea il database e l'utente di default admin
    // il metodo viene chiamato solo se il file del database non esiste
    /*public void DefaultDbCreation(){
        using (var context = new AppDatabase()){
            // Assicurati che il database sia aggiornato
            //context.Database.Migrate();  
            // Aggiungi un nuovo utente
            try{
                context.Users.Add(new User("admin","admin"));
                context.SaveChanges();
            }
            catch (DbUpdateException ex){
                Console.WriteLine("Error: " + ex.InnerException?.Message);
            }
        }
    }*/
}