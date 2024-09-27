/// <summary>
/// Classe Character che incapsula tutte le caratteristiche del personaggio (eroe e avversario)
/// </summary>
public class Character{
    private string _class;
    private string _name;
    // .parameters[0] -> strength  .parameters[1] -> stealth  .parameters[2] -> magic 
    // .parameters[3] -> health   .parameters[4] -> skill .parameters[5] -> experience
    static public int FileName{get; set;} 
    public int[] Parameters{get; set;} 
    public int EscapePossibilities{get;set;} 
    public string Name{get {return _name;}}
    public string Class{get {return _class;}}
    // Tre costruttori (solo uno utilizzato) ma essendo ancora in versione pre-alpha
    // devo ancora verificare la loro utilità
    public Character(dynamic obj){
        _name = obj.Name;
        _class = obj.Class;
        Parameters = obj.Parameters;
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