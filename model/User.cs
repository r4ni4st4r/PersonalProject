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