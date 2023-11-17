public interface IGraph
{
    void Draw();
}

public abstract class Graph : IGraph
{
    public string Data { get; set; }

    public Graph(string data)
    {
        Data = data;
    }

    public abstract void Draw();
}

public class LineGraph : Graph
{
    public LineGraph(string data) : base(data) { }

    public override void Draw()
    {
        Console.WriteLine("Drawing line graph for data: " + Data);
    }
}

public class BarGraph : Graph
{
    public BarGraph(string data) : base(data) { }

    public override void Draw()
    {
        Console.WriteLine("Drawing bar graph for data: " + Data);
    }
}

public class PieChart : Graph
{
    public PieChart(string data) : base(data) { }

    public override void Draw()
    {
        Console.WriteLine("Drawing pie chart for data: " + Data);
    }
}

public interface IGraphFactory
{
    IGraph CreateGraph(string type, string data);
}

public class GraphFactory : IGraphFactory
{
    public IGraph CreateGraph(string type, string data)
    {
        switch (type)
        {
            case "Line":
                return new LineGraph(data);
            case "Bar":
                return new BarGraph(data);
            case "Pie":
                return new PieChart(data);
            default:
                throw new InvalidOperationException("Invalid graph type: " + type);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        IGraphFactory graphFactory = new GraphFactory();

        while (true)
        {
            Console.WriteLine("Enter graph type (Line, Bar, Pie) or type 'Exit' to quit:");
            string type = Console.ReadLine();

            if (type.ToLower() == "exit")
            {
                break;
            }

            Console.WriteLine("Enter data for the graph:");
            string data = Console.ReadLine();

            IGraph graph = graphFactory.CreateGraph(type, data);
            graph.Draw();
        }
    }
}