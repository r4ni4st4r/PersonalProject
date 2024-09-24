using System.Data.SQLite;
public class Database{
    private SQLiteConnection _connection;
    private View _view;
    private int _userId;
    private string _sql;
    private string _username;
    private string _psw;
    private const string _defaultDbString = @"
                            CREATE TABLE IF NOT EXISTS games (id INTEGER PRIMARY KEY AUTOINCREMENT, won BOOLEAN, ended DATE default CURRENT_TIMESTAMP, user_id INTEGER, 
                                FOREIGN KEY (user_id) REFERENCES users(id)); 
                            CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT UNIQUE, password TEXT);";
    private const string _defaulUserString = @"INSERT INTO users (username, password) VALUES ('admin','admin');";    
    public SQLiteConnection Connection{get{return _connection;}}                        
    public Database(){
        // Creazione di una connessione al database
        _view = new View();
        _connection = new SQLiteConnection($"Data Source={DefaultData.DBPATH};version=3;");  
        _connection.Open();                                 // Apertura della connessione
        var command = new SQLiteCommand(_defaultDbString, _connection);
        command.ExecuteNonQuery();
        command = new SQLiteCommand(_defaulUserString, _connection);
        command.ExecuteNonQuery();
        _connection.Close();
    }

    public User Login(){
        bool userWhile = true;
        while(userWhile){
            _view.EnterUsername();
            _username = _view.GetStringInput();
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
                                        connection.Close();
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
        return new User(_userId, _psw);
    }
}