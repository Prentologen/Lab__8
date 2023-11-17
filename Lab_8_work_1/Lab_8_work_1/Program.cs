using System;
using System.IO;

class ConfigurationManager
{
    private static ConfigurationManager _instance;
    private static readonly object _lock = new object();

    public string LogMode { get; set; }
    public string DbConnectionString { get; set; }

    private ConfigurationManager()
    {
        // Initialize the default configuration settings
        LogMode = "Info";
        DbConnectionString = "DefaultDbConnectionString";
    }

    public static ConfigurationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }
                }
            }

            return _instance;
        }
    }

    public void ChangeLogMode(string newLogMode)
    {
        LogMode = newLogMode;
    }

    public void ChangeDbConnectionString(string newDbConnectionString)
    {
        DbConnectionString = newDbConnectionString;
    }

    public void SaveToFile(string filePath)
    {
        using (StreamWriter file = new StreamWriter(filePath))
        {
            file.WriteLine("LogMode: " + LogMode);
            file.WriteLine("DbConnectionString: " + DbConnectionString);
        }
    }

    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist. Please enter the correct file path.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.StartsWith("LogMode"))
            {
                LogMode = line.Substring(line.IndexOf(':') + 1).Trim();
            }
            else if (line.StartsWith("DbConnectionString"))
            {
                DbConnectionString = line.Substring(line.IndexOf(':') + 1).Trim();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConfigurationManager configManager = ConfigurationManager.Instance;

        Console.WriteLine("Initial LogMode: " + configManager.LogMode);
        Console.WriteLine("Initial DbConnectionString: " + configManager.DbConnectionString);

        configManager.ChangeLogMode("Debug");
        configManager.ChangeDbConnectionString("NewDbConnectionString");

        Console.WriteLine("Modified LogMode: " + configManager.LogMode);
        Console.WriteLine("Modified DbConnectionString: " + configManager.DbConnectionString);

        string filePath = "config.txt";
        configManager.SaveToFile(filePath);

        ConfigurationManager.Instance.LoadFromFile(filePath);

        Console.WriteLine("Loaded LogMode: " + configManager.LogMode);
        Console.WriteLine("Loaded DbConnectionString: " + configManager.DbConnectionString);
    }
}