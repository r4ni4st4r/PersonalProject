using System.Dynamic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Data.SQLite;
class Program{
    public static void Main(string[] args){
        DataController.ReadByTxt();
        Database _db = new Database();
        View _view = new View();
        DataController _dc = new DataController();
        Controller _controller = new Controller(_db,_view,_dc);
        _controller.LoginMenu();
    }
}
