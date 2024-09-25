/// <summary>
/// Semplice classe Game che rispecchia un elemento della tabella games del database
/// </summary>
public class Game{
    private int _id;
    private int _userId;
    private bool _won;
    private DateTime _date;
    public int Id{get{return _id;}}
    public int UserId{get{return _userId;}}
    public bool Won{get{return _won;}}
    public DateTime Date{get{return _date;}}

    public Game(int id, int userId, bool won, DateTime date){
        _id = id;
        _userId = userId;
        _won = won;
        _date = date;
    }
}