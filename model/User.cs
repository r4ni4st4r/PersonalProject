/// <summary>
/// Semplice classe User che incapsula id e username dell'utente attualmente loggato
/// id è valore univoco generato dal database.
/// </summary>
public class User{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Password{get;set;}

    public User(int id, string name){
        Id = id;
        Name = name;
    }
    public User(string name, string password){
        Name = name;
        Password = password;
    }
    public User(string name, string password, int id){
        Name = name;
        Password = password;
        Id = id;
    }
    public User(){
    }
}