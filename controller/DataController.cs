using System.Dynamic;
using Newtonsoft.Json;
public class DataController{
    public ExpandoObject ReadJson(){
        return new ExpandoObject();
    }
    public List<string> ReadCsv(){
        return new List<string>();
    }
    public bool WriteOnJson(Character hero){
        string path = Path.Combine(DefaultData.SAVEPATH, hero.Name + Character.FileName + ".json");
        File.Create(path).Close();
        using (StreamWriter sw = new StreamWriter(path)){                                          
            sw.Write(JsonConvert.SerializeObject(hero, Formatting.Indented));    
        }
        return true;
    }
    public void WriteOnTxt(){
        File.WriteAllText(Path.Combine(DefaultData.CONFIGPATH, "fileName.txt"), (++Character.FileName).ToString());
    }

    static public void ReadByTxt(){
        Character.FileName = Convert.ToInt32(File.ReadAllText(Path.Combine(DefaultData.CONFIGPATH, "fileName.txt")));
    }

    /*
        static void LoadHero(){
        List<string> filesList = new List<string>(Directory.GetFiles(SAVEPATH));
        if(filesList.Count!=0){
            Console.Clear();
            Console.WriteLine("Select the Hero to load by unique ref: \n");
            foreach(string s in filesList){
                string json = File.ReadAllText(s);
                dynamic obj = JsonConvert.DeserializeObject(json);
                Console.WriteLine($"\nRef -> {obj.parameters[6]} < - Hero's name = {obj.name} class = {obj.cClass} experience = {obj.parameters[5]} skill = {obj.parameters[4]}");
            }
            Console.WriteLine("\nchoice: \n");
            while(true){
                string path;
                if(int.TryParse(Console.ReadLine(), out int selection)){
                    path = Path.Combine(SAVEPATH, selection + ".json");
                    if(File.Exists(path)){
                        heroObj = JsonConvert.DeserializeObject(File.ReadAllText(path));
                        Console.WriteLine($"\n{heroObj.name} the {heroObj.cClass} loaded successfully!!!\nPlease press any key...");
                        Console.ReadLine();
                        heroSelected = true;
                        return;
                    }else{
                        Console.WriteLine("Please enter a valid choice: \n");
                    }
                }else{
                    Console.WriteLine("Please enter a valid choice: \n");
                }
            }
        }
    }*/
}