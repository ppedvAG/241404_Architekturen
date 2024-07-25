using ppedv.PizzaOrderManager.Data.EfCore;
using ppedv.PizzaOrderManager.Logic;
using ppedv.PizzaOrderManager.Model.Contracts;
using ppedv.PizzaOrderManager.Model.DomainModel;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("🍕🍕🍕 Pizza Order Manager v0.1 🍕🍕🍕");

string conString = "Server=(localdb)\\mssqllocaldb;Database=PizzaOrderManager_test;Trusted_Connection=true;";
IRepository repo = new  EfContextIRepositoryAdapter(conString);

var os = new OrderService(repo);

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