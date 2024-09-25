/// <summary>
/// Classe che rappresenta la sessione attiva
/// è ancora incompleta e non è certa la sua utilità
/// </summary>
public class Session{
    private User _user;
    private List<Game> _gameList;
    public Session(User user){
        _user = user;
        _gameList = new List<Game>();
    }
}