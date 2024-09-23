using System.Data.SQLite;
public class Database{
    private SQLiteConnection _connection;

    public SQLiteConnection Connection{get{return _connection;}}

    private const string _defaultDbString = @"
                            CREATE TABLE TABLE IF NOT EXISTS sessions (id INTEGER PRIMARY KEY AUTOINCREMENT, active BOOL default TRUE, win INTEGER, lose INTEGER, user_id INTEGER, 
                                FOREIGN KEY (user_id) REFERENCES users(id)); 
                            CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT UNIQUE, password TEXT, ranking INTEGER default 0,
                                last_access DATE default CURRENT_TIMESTAMP);";
    private const string _defaulUserString = @"INSERT INTO users (username,password) VALUES ('admin','admin');";                            

    public Database(){
        // Creazione di una connessione al database
        _connection = new SQLiteConnection($"Data Source={Program.DBPATH};version=3;");  
        _connection.Open();                                 // Apertura della connessione
        var command = new SQLiteCommand(_defaultDbString, _connection);
        if(command.ExecuteNonQuery()!=0){
            command = new SQLiteCommand(_defaulUserString, _connection);
            command.ExecuteNonQuery();
        }                   
    }
}