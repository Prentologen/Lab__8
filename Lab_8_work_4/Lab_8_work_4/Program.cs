public interface IDataAdapter
{
    string ReadData(string inputFilePath);
    void WriteData(string outputFilePath, string data);
}

public class CSVDataAdapter : IDataAdapter
{
    // Implement methods for reading and writing CSV data
}

public class XMLDataAdapter : IDataAdapter
{
    // Implement methods for reading and writing XML data
}

public class JSONDataAdapter : IDataAdapter
{
    // Implement methods for reading and writing JSON data
}

public class DataImportExportService
{
    private readonly IDataAdapter _inputDataAdapter;
    private readonly IDataAdapter _outputDataAdapter;

    public DataImportExportService(IDataAdapter inputDataAdapter, IDataAdapter outputDataAdapter)
    {
        _inputDataAdapter = inputDataAdapter;
        _outputDataAdapter = outputDataAdapter;
    }

    public void ImportExportData(string inputFilePath, string outputFilePath)
    {
        string data = _inputDataAdapter.ReadData(inputFilePath);
        _outputDataAdapter.WriteData(outputFilePath, data);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter input data format (CSV, XML, JSON):");
        string inputDataFormat = Console.ReadLine();

        Console.WriteLine("Enter output data format (CSV, XML, JSON):");
        string outputDataFormat = Console.ReadLine();

        IDataAdapter inputDataAdapter;
        IDataAdapter outputDataAdapter;

        switch (inputDataFormat.ToLower())
        {
            case "csv":
                inputDataAdapter = new CSVDataAdapter();
                break;
            case "xml":
                inputDataAdapter = new XMLDataAdapter();
                break;
            case "json":
                inputDataAdapter = new JSONDataAdapter();
                break;
            default:
                throw new InvalidOperationException("Invalid input data format: " + inputDataFormat);
        }

        switch (outputDataFormat.ToLower())
        {
            case "csv":
                outputDataAdapter = new CSVDataAdapter();
                break;
            case "xml":
                outputDataAdapter = new XMLDataAdapter();
                break;
            case "json":
                outputDataAdapter = new JSONDataAdapter();
                break;
            default:
                throw new InvalidOperationException("Invalid output data format: " + outputDataFormat);
        }

        DataImportExportService importExportService = new DataImportExportService(inputDataAdapter, outputDataAdapter);

        Console.WriteLine("Enter input file path:");
        string inputFilePath = Console.ReadLine();

        Console.WriteLine("Enter output file path:");
        string outputFilePath = Console.ReadLine();

        importExportService.ImportExportData(inputFilePath, outputFilePath);

        Console.WriteLine("Data has been imported and exported successfully.");
    }
}