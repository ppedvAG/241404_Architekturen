using ppedv.PizzaOrderManager.Model.DomainModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ppedv.PizzaOrderManager.UI.WPF.ViewModels
{
    public class PizzaViewModel
    {
        public ObservableCollection<Pizza> PizzaList { get; set; } = new ObservableCollection<Pizza>();
        public Pizza SelectedPizza { get; set; }

        public AddNewCommand AddNewCommand { get; set; }

        public PizzaViewModel()
        {
            PizzaList.Add(new Pizza() { Name = "Salami", Toppings = [new Topping() { Name = "Käse" }, new Topping() { Name = "Salami" }] });
            PizzaList.Add(new Pizza() { Name = "Hawai", Toppings = [new Topping() { Name = "Käse" }, new Topping() { Name = "Ananas" }, new Topping() { Name = "Schinken" }] });

            AddNewCommand = new AddNewCommand(PizzaList);
        }

    }

    public class AddNewCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public ObservableCollection<Pizza> PizzaList;
        public AddNewCommand(ObservableCollection<Pizza> pizzaList)
        {
            PizzaList = pizzaList;
        }

        public void Execute(object? parameter)
        {
            PizzaList.Add(new Pizza() { Name = "NEU" });
        }
    }
}
