/// <summary>
/// Semplice classe Game che rispecchia un elemento della tabella games del database
/// </summary>
public class Game{
    public int Id{get;set;}
    public User User{get;set;}
    public bool Won{get;set;}
    public DateTime Date{get;set;}

    public Game(int id, User user, bool won, DateTime date){
        Id = id;
        User = user;
        Won = won;
        Date = date;
    }
    public Game(){
 
    }
}