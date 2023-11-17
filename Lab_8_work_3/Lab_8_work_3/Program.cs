public interface IProductComponent
{
    void ShowInfo();
}

public class Screen : IProductComponent
{
    public Screen(string resolution)
    {
        Resolution = resolution;
    }

    public string Resolution { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Screen: {Resolution}");
    }
}

public class Processor : IProductComponent
{
    public Processor(string type)
    {
        Type = type;
    }

    public string Type { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Processor: {Type}");
    }
}

public class Camera : IProductComponent
{
    public Camera(string resolution)
    {
        Resolution = resolution;
    }

    public string Resolution { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Camera: {Resolution}");
    }
}

public interface IProductFactory
{
    IProductComponent CreateScreen();
    IProductComponent CreateProcessor();
    IProductComponent CreateCamera();
}

public class PhoneFactory : IProductFactory
{
    public IProductComponent CreateScreen()
    {
        return new Screen("5.99 inch");
    }

    public IProductComponent CreateProcessor()
    {
        return new Processor("Snapdragon 750G");
    }

    public IProductComponent CreateCamera()
    {
        return new Camera("48MP");
    }
}

public class LaptopFactory : IProductFactory
{
    public IProductComponent CreateScreen()
    {
        return new Screen("15.6 inch");
    }

    public IProductComponent CreateProcessor()
    {
        return new Processor("Intel Core i7");
    }

    public IProductComponent CreateCamera()
    {
        return new Camera("720p");
    }
}

public class TabletFactory : IProductFactory
{
    public IProductComponent CreateScreen()
    {
        return new Screen("10.4 inch");
    }

    public IProductComponent CreateProcessor()
    {
        return new Processor("MediaTek Helio G90T");
    }

    public IProductComponent CreateCamera()
    {
        return new Camera("8MP");
    }
}

public interface IProductFactoryFactory
{
    IProductFactory CreateProductFactory(string type);
}

public class ProductFactoryFactory : IProductFactoryFactory
{
    public IProductFactory CreateProductFactory(string type)
    {
        switch (type)
        {
            case "Phone":
                return new PhoneFactory();
            case "Laptop":
                return new LaptopFactory();
            case "Tablet":
                return new TabletFactory();
            default:
                throw new InvalidOperationException("Invalid product type: " + type);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        IProductFactoryFactory productFactoryFactory = new ProductFactoryFactory();

        while (true)
        {
            Console.WriteLine("Enter product type (Phone, Laptop, Tablet) or type 'Exit' to quit:");
            string type = Console.ReadLine();

            if (type.ToLower() == "exit")
            {
                break;
            }

            IProductFactory productFactory = productFactoryFactory.CreateProductFactory(type);
            IProductComponent screen = productFactory.CreateScreen();
            IProductComponent processor = productFactory.CreateProcessor();
            IProductComponent camera = productFactory.CreateCamera();

            Console.WriteLine("Product Info:");
            screen.ShowInfo();
            processor.ShowInfo();
            camera.ShowInfo();
            Console.WriteLine();
        }
    }
}