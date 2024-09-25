/// <summary>
/// Semplice classe User che incapsula id e username dell'utente attualmente loggato
/// id è valore univoco generato dal database.
/// </summary>
public class User{
    private int _id;
    private string _name;
    public int Id{get{return _id;}}
    public string Name{get{return _name;}}

    public User(int id, string name){
        _id = id;
        _name = name;
    }
}