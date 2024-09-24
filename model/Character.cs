public class Character{
    private string _class;
    private string _name;
    //  .parameters[0] -> strength  .parameters[1] -> stealth  .parameters[2] -> magic 
    //  .parameters[3] -> health   .parameters[4] -> skill .parameters[5] -> experience
    static public int FileName{get; set;} 
    public int[] Parameters{get; set;} 
    public int EscapePossibilities{get;set;} 
    public string Name{get {return _name;}}
    public string Class{get {return _class;}}

    public Character(string name, string cClass, int[] parameters){
        _name = name;
        _class = cClass;
        Parameters = parameters;
    }
    public Character(string name, string cClass){
        _name = name;
        _class = cClass;
        Parameters = new int[6];
    }
    public Character(string name){
        _name = name;
        _class = "";
        Parameters = new int[6];
    }
}