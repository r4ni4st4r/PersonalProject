/// <summary>
/// Semplice classe Environment che mette in relazione il luogo del combattimento e 
/// i vari bonus da assegnare ai personaggi
/// </summary>
public class Environment{
    private string _location;
    private int[] _bonus;
    public string Location{get {return _location;}}
    public int[] Bonus{get {return _bonus;}}

    public Environment(string location, int[] bonus){
        _location = location;
        _bonus = bonus;
    }
}
