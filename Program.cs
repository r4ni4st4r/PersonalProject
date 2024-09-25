/// <summary>
/// La classe Program è solo il punto di accesso dell'applicazione dove vengono create delle istanze delle principali
/// classi del programma per avviarlo
/// </summary>
class Program{
    public static void Main(string[] args){
        DataController.ReadFromTxt();
        Database _db = new Database();
        View _view = new View();
        DataController _dc = new DataController();
        Controller _controller = new Controller(_db,_view,_dc);
        _controller.LoginMenu();
    }
}
