public class Character{
    static private int _fileName = 0; 

    private string _class;
    private string _name;
    //  .parameters[0] -> strength  .parameters[1] -> stealth  .parameters[2] -> magic 
    //  .parameters[3] -> health   .parameters[4] -> skill .parameters[5] -> experience 
    private int[] _parameters;
    public int EscapePossibilities{get;set;} 
    public string Name{get {return _name;}}
    public string Class{get {return _class;}}
    public int[] Parameters{get {return _parameters;}set{_parameters = value;}}
    
}