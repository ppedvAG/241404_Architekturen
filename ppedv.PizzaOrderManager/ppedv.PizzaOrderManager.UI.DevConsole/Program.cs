using Autofac;
using ppedv.PizzaOrderManager.Data.EfCore;
using ppedv.PizzaOrderManager.Logic;
using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;
using System.Reflection;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("🍕🍕🍕 Pizza Order Manager v0.1 🍕🍕🍕");

string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaOrderManager_test;Trusted_Connection=true;";


//Di per Hand
//IRepository repo = new EfContextIRepositoryAdapter(conString);
//var os = new OrderService(repo);

//Dependency Injection per Reflection
//var path = @"C:\Users\Fred\source\repos\241404_Architekturen\ppedv.PizzaOrderManager\ppedv.PizzaOrderManager.Data.EfCore\bin\Debug\net8.0\ppedv.PizzaOrderManager.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(path);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//IRepository repo = (IRepository)Activator.CreateInstance(typeMitRepo,conString);
//var os = new OrderService(repo);

//Di per AutoFac
var builder = new ContainerBuilder();
builder.RegisterType<OrderService>();
builder.Register(x => new EfContextIRepositoryAdapter(conString)).AsImplementedInterfaces().SingleInstance();
var container = builder.Build();

IRepository repo = container.Resolve<IRepository>();
var os = container.Resolve<OrderService>();

foreach (var adr in repo.GetAll<Address>())
{
    Console.WriteLine($"{adr.Name1} {adr.Name2}");
    foreach (var order in os.GetMyOrders(adr.Id))
    {
        Console.WriteLine($"\t {order.OrderDate}");
    }
}

Console.WriteLine("Ende");
Console.ReadLine();