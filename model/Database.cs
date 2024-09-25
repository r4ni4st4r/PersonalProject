using System.Data.SQLite;
/// <summary>
/// Semplice classe Database (incompleta) che si occupa di leggere e scivere su database 
/// </summary>
public class Database{
    private SQLiteConnection _connection;
    private View _view;
    private int _userId;
    private string _sql;
    private string _username;
    private string _psw;
    // La base di dati dell'applicazione è strutturata da due tabelle. Quella principale è games che stora le partite
    private const string _defaultDbString = @"
                            CREATE TABLE IF NOT EXISTS games (id INTEGER PRIMARY KEY AUTOINCREMENT, won BOOLEAN, ended DATE default CURRENT_TIMESTAMP, user_id INTEGER, 
                                FOREIGN KEY (user_id) REFERENCES users(id)); 
                            CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT UNIQUE, password TEXT);";
    private const string _defaulUserString = @"INSERT INTO users (username, password) VALUES ('admin','admin');";    
    //Probabilmente proprietà inutile...
    public SQLiteConnection Connection{get{return _connection;}}                        
    public Database(){
        _view = new View();
        if(!File.Exists(DefaultData.DBPATH))
            DefaultDbCreation();
    }
    // Metodo Login()
    // si occupa, con l'ausilio della view, dell'inserimento di username e password, 
    // di verificare se l'uente è presente nel db e di eseguirne l'accesso e ritornando un'istanza della classe User
    // che rappresenta l'utente loggato  
    public User Login(){
        bool userWhile = true;
        while(userWhile){
            _view.EnterUsername();
            _username = _view.GetStringInput();
            // switch che fa il check della stringa inserita come input e se non è vuota
            // verifica che l'utente sia presente nel db e successivamente chiede e verifica la password 
            switch(_username){
                case "":
                    _view.UsernameEmptyError();
                    break;
                default:
                    using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DefaultData.DBPATH};version=3;")){
                        connection.Open();
                        bool hasRow;
                        bool stringComparison;
                        _sql = $"SELECT * FROM users WHERE username = @username;";

                        using (SQLiteCommand cmd = new SQLiteCommand(_sql, connection)){
                            cmd.Parameters.AddWithValue("@username", _username);
                            using (SQLiteDataReader reader = cmd.ExecuteReader()){
                                hasRow = reader.HasRows;
                            }
                        }
                        // se l'utente esiste (il reader ritorna un risultato essendo il campo username univoco) 
                        // chiede la password, fa un check che non sia vuota e verifica se 
                        // sia uguale a quella dell'utente selezionato verificando nel database  
                        if(hasRow){
                            bool passWhile = true;
                            while(passWhile){
                                _view.EnterPassword();
                                _psw = _view.GetPassword();
                                if (_psw.Length == 0 || _psw == null){
                                    _view.PasswordEmptyError();
                                }else{
                                    _sql = $"SELECT id, password FROM users WHERE username = @username;";

                                    using (SQLiteCommand cmd = new SQLiteCommand(_sql, connection)){
                                        cmd.Parameters.AddWithValue("@username", _username);
                                        using (SQLiteDataReader reader = cmd.ExecuteReader()){
                                            reader.Read();
                                            stringComparison = _psw == reader["password"].ToString();
                                            _userId = Convert.ToInt32(reader["id"]);
                                        }
                                    }
                                    if(stringComparison){
                                        _view.SessionStarted(_username);
                                        passWhile = false;
                                    } 
                                }
                            }
                        }       
                    }
                    userWhile = false;
                    break;
            } 
        }
        // restituisce l'utente loggato
        return new User(_userId, _username);
    }
    // Metodo che crea il database e l'utente di default admin
    // il metodo viene chiamato solo se il file del database non esiste
    public void DefaultDbCreation(){
        _connection = new SQLiteConnection($"Data Source={DefaultData.DBPATH};version=3;");  
        _connection.Open();                               
        var command = new SQLiteCommand(_defaultDbString, _connection);
        command.ExecuteNonQuery();
       /* command = new SQLiteCommand($"SELECT name FROM users WHERE name = @name", _connection);
        command.Parameters.AddWithValue("@name","admin");
        var reader = command.ExecuteReader();
        if(!reader.Read()){*/
            command = new SQLiteCommand(_defaulUserString, _connection);
            command.ExecuteNonQuery();
        /*}
        reader.Close();*/
        _connection.Close();
    }
}