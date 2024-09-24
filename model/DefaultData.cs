static class DefaultData{
    public const string SAVEPATH = @".\data\save";    
    public const string CONFIGPATH = @".\data\config";
    public const string DBPATH = @".\data\database.db";
    private static readonly string[] _environments = {"Arena", "Dark city alley", "Ancient castle"};
    private static readonly string[]  _characterClasses = {"Warrior", "Thief", "Wizard"};
    private static readonly string[] _attaks = {"Charged attack", "Archery shot", "Spell", "Try to run away"};
    private static readonly string[] _parametersString = {"Strength", "Stealth", "Magic", "Skill"};
    private static readonly int[] _warBonus = {4, 0, 2}; 
    private static readonly int[] _thiefBonus = {2, 4, 0};
    private static readonly int[] _wizBonus = {0, 2, 4};
    private static readonly int[] _warParams = {16, 0, 4, 16};  // ***************************************************
    private static readonly int[] _thiefParams = {8, 16, 0, 12}; // arrays con i valori degli attributi per ogni classe [0]->strength   [1]->stealth   [2]->magic [3]->health [4]->skill   
    private static readonly int[] _wizParams = {0, 8, 20, 8};   // ***************************************************
    private static readonly int[] _skillValues = {2, 4, 8}; // parametro usato come moltiplicatore di attacco
    private static readonly int[] _rechargeArray = {4, 8, 12, 16, 20};
    private static readonly string[] _villainNames = {"Arcadius", "Zoltar", "Vinicius", "Geralth", "Howard", "Juni", "Scarsif", "Jolian", "Kilian", "Olifan"};
    public static string[] Attaks{get {return _attaks;}}
    public static string[] ParametersString{get {return _parametersString;}}
    public static int[] WarParams{get {return _warParams;}}
    public static int[] WizParams{get {return _wizParams;}}
    public static int[] ThiefParams{get {return _thiefParams;}}
    public static int[] SkillValues{get {return _skillValues;}}
    public static int[] RechargeArray{get {return _rechargeArray;}}
    public static string[] VillainNames{get {return _villainNames;}}
    public static string[] CharacterClasses{get {return _characterClasses;}}
    public static string[] Environments{get {return _environments;}}
    public static int[] WizBonus{get {return _wizBonus;}}
    public static int[] WarBonus{get {return _warBonus;}}
    public static int[] ThiefBonus{get {return _thiefBonus;}}
    public static int HitPoints{get;set;}
}