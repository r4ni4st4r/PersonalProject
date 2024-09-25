using System.Dynamic;
using Newtonsoft.Json;
/// <summary>
/// Classe che si occupa della lettura e della scrittura sui files (configurazione e salvataggio)
/// .json - .txt - forse .csv
/// è ancora estremamente incompleta
/// </summary>
public class DataController{
    // Non ancora implementata ma è necessaria
    // metodo che caricherà i personaggi da .json
    public ExpandoObject ReadJson(){
        return new ExpandoObject();
    }
    // Non ancora implementata ma non è certo sia necessaria
    public List<string> ReadCsv(){
        return new List<string>();
    }
    // Metodo che salva il personaggio serializzandolo su di un file .json
    // c'è una cartella dentro data con il nome di ogni utente e all'interno vengono 
    // scritti i files e il nome del file è univoco e progressivo
    public bool WriteOnJson(string name, Character hero){
        string path = Path.Combine(DefaultData.SAVEPATH, name, Character.FileName + ".json");
        Character.FileName++;
        File.Create(path).Close();
        using (StreamWriter sw = new StreamWriter(path)){                                          
            sw.Write(JsonConvert.SerializeObject(hero, Formatting.Indented));    
        }
        return true;
    }
    // medodo che all'uscita del programma scrive su di un file di config .txt l'intero
    // all'interno della proprietà statica di character - Character.FileName -
    public void WriteOnTxt(){
        File.WriteAllText(Path.Combine(DefaultData.CONFIGPATH, "fileName.txt"), Character.FileName.ToString());
    }
    // medodo che all'avvio del programma legge l'intero all'internodi un file di config .txt e lo scrive
    // nella proprietà statica di character - Character.FileName -
    static public void ReadFromTxt(){
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